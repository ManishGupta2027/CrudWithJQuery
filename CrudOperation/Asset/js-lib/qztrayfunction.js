/// Authentication setup ///
qz.security.setCertificatePromise(function (resolve, reject) {
    //Preferred method - from server
    fetch("/assets/private/digital-certificate.txt", {cache: 'no-store', headers: {'Content-Type': 'text/plain'}})
              .then(function(data) { data.ok ? resolve(data.text()) : reject(data.text()); });
});

qz.security.setSignatureAlgorithm("SHA512"); // Since 2.1
qz.security.setSignaturePromise(function (toSign) {
    return function (resolve, reject) {
        //Preferred method - from server
        //fetch("/account/QZSignMessage?request=" + toSign, {cache: 'no-store', headers: {'Content-Type': 'text/plain'}})
        //    .then(function(data) { data.ok ? resolve(data.text()) : reject(data.text()); });

        resolve();
    };
});


/// Connection ///
function launchQZ() {
    if (!qz.websocket.isActive()) {
        window.location.assign("qz:launch");
        //Retry 5 times, pausing 1 second between each attempt
        startConnection({ retries: 5, delay: 1 });
    }
}

function startConnection(config) {
    host = $('#connectionHost').val().trim();
    usingSecure = $("#connectionUsingSecure").prop('checked');
    // Connect to a print-server instance, if specified
    if (host != "" && host != 'localhost') {
        if (config) {
            config.host = host;
            config.usingSecure = usingSecure;
        } else {
            config = { host: host, usingSecure: usingSecure };
        }
    }
    if (!qz.websocket.isActive()) {
        updateState('Waiting', 'default');

        qz.websocket.connect(config).then(function () {
            //$("#connect-qz").hide();
            //$("#disconnect-qz").show();
            updateState('Active', 'success');
            findVersion();
            findPrinters();
        }).catch(handleConnectionError);
    } else {
        displayMessage('An active connection with QZ already exists.', 'alert-warning');
    }
}

function endConnection() {
    if (qz.websocket.isActive()) {
        qz.websocket.disconnect().then(function () {
            //$("#connect-qz").show();
            //$("#disconnect-qz").hide();
            updateState('Inactive', 'default');
        }).catch(handleConnectionError);
    } else {
        displayMessage('No active connection with QZ exists.', 'alert-warning');
    }
}


function listNetworkDevices() {
    var listItems = function (obj) {
        var html = '';
        var labels = { mac: 'MAC', ip: 'IP', up: 'Up', ip4: 'IPv4', ip6: 'IPv6', primary: 'Primary' };

        Object.keys(labels).forEach(function (key) {
            if (!obj.hasOwnProperty(key)) { return; }
            if (key !== 'ip' && obj[key] == obj['ip']) { return; }

            var value = obj[key];
            if (key === 'mac') { value = obj[key].match(/.{1,2}/g).join(':'); }
            if (typeof obj[key] === 'object') { value = value.join(', '); }

            html += '<li><strong>' + labels[key] + ':</strong> <code>' + value + '</code></li>';
        });

        return html;
    };

    qz.networking.devices().then(function (data) {
        var list = '';
        var hostname = '';
        var username = '';
        for (var i = 0; i < data.length; i++) {
            var info = data[i];

            if (i == 0) {
                list += "<li>" +
                    "   <strong>Hostname:</strong> <code>" + info.hostname + "</code>" +
                    "</li>" +
                    "<li>" +
                    "   <strong>Username:</strong> <code>" + info.username + "</code>"
                "</li>";
            }
            list += "<li>" +
                "   <strong>Interface:</strong> <code>" + (info.name || "UNKNOWN") + (info.id ? "</code> (<code>" + info.id + "</code>)" : "</code>") +
                "   <ul>" + listItems(info) + "</ul>" +
                "</li>";
        }

        pinMessage("<strong>Network details:</strong><ul>" + list + "</ul>");
    }).catch(displayError);
}

/// Detection ///
function findPrinter(query, set) {
    $("#printerSearch").val(query);
    qz.printers.find(query).then(function (data) {
        displayMessage("<strong>Found:</strong> " + data);
        if (set) { setPrinter(data); }
    }).catch(displayError);
}

function findDefaultPrinter(set) {
    qz.printers.getDefault().then(function (data) {
        displayMessage("<strong>Found:</strong> " + data);
        if (set) { setPrinter(data); }
    }).catch(displayError);
}

function findPrinters() {
    qz.printers.find().then(function (data) {
        PubSub.publish("availableprinters", data);
        //var list = '';
        //for (var i = 0; i < data.length; i++) {
            //list += "&nbsp; " + data[i] + "<br/>";
        //}

        //displayMessage("<strong>Available printers:</strong><br/>" + list, null, 15000);
    }).catch(displayError);
}

function detailPrinters() {
    qz.printers.details().then(function (data) {
        var list = '';
        for (var i = 0; i < data.length; i++) {
            list += "<li>" + (data[i].default ? "* " : "") + data[i].name + "<ul>" +
                "<li><strong>Driver:</strong> " + data[i].driver + "</li>" +
                "<li><strong>Density:</strong> " + data[i].density + "dpi</li>" +
                "<li><strong>Connection:</strong> " + data[i].connection + "</li>" +
                (data[i].trays ? "<li><strong>Trays:</strong> " + data[i].trays + "</li>" : "") +
                "</ul></li>";
        }

        pinMessage("<strong>Printer details:</strong><br/><ul>" + list + "</ul>");
    }).catch(displayError);
}


/// Raw Printers ///
function printCommand() {
    var config = getUpdatedConfig();
    var lang = getUpdatedOptions().language; //print options not used with this flavor, just check language requested

    var printData;
    switch (lang) {
        case 'EPL':
            printData = [
                '\nN\n',
                'q812\n', //  q=width in dots - check printer dpi
                'Q1218,26\n', // Q=height in dots - check printer dpi
                'B5,26,0,1A,3,7,152,B,"1234"\n',
                'A310,26,0,3,1,1,N,"SKU 00000 MFG 0000"\n',
                'A310,56,0,3,1,1,N,"TEST PRINT SUCCESSFUL"\n',
                'A310,86,0,3,1,1,N,"FROM SAMPLE.HTML"\n',
                'A310,116,0,3,1,1,N,"PRINTED WITH QZ ' + qzVersion + '"\n',
                '\nP1,1\n'
            ];
            break;
        case 'ZPL':
            printData = [
                '^XA\n',
                '^FO50,50^ADN,36,20^FDPRINTED WITH QZ ' + qzVersion + '\n',
                '^FS\n',
                '^XZ\n'
            ];
            break;
        case 'ESCPOS':
            printData = [
                //defaults to 'type: raw', 'format: command', and 'flavor: plain'
                { data: '\nPRINTED WITH QZ ' + qzVersion + '.\n\n\n\n\n\n' }
            ];
            break;
        case 'EPCL':
            printData = buildEPCL();
            break;
        case 'EVOLIS':
            printData = [
                '\x1BPps;0\x0D',   // Enable raw/disable driver printer parameter supervision
                '\x1BPwr;0\x0D',   // Landscape (zero degree) orientation
                '\x1BWcb;k;0\x0D', // Clear card memory

                '\x1BSs\x0D',      // Start of sequence
                { type: 'raw', format: 'image', data: 'assets/img/fade-test.png', options: { language: "EVOLIS", precision: 128 } },
                '\x1BWt;50;60;0;30;Printed using QZ Tray ' + qzVersion + '\x0D', // 50,60 = coordinates; 0 = arial font
                '\x1BSe\x0D'       // End of sequence
            ];
            break;
        default:
            displayError("Sample page cannot print commands using this printer language");
            return;
    }

    qz.print(config, printData).catch(displayError);
}


function buildEPCL() {
    var printData = [];
    $.merge(printData, convertEPCL('+RIB 4'));     // Monochrome ribbon
    $.merge(printData, convertEPCL('F'));          // Clear monochrome print buffer
    $.merge(printData, convertEPCL('+C 8'));       // Adjust monochrome intensity
    $.merge(printData, convertEPCL('&R'));         // Reset magnetic encoder
    $.merge(printData, convertEPCL('&CDEW 0 0'));  // Set R/W encoder to ISO default
    $.merge(printData, convertEPCL('&CDER 0 0'));  // Set R/W encoder to ISO default
    $.merge(printData, convertEPCL('&SVM 0'));     // Disable magnetic encoding verifications
    $.merge(printData, convertEPCL('T 80 600 0 1 0 45 1 QZ INDUSTRIES'));   // Write text buffer
    $.merge(printData, convertEPCL('&B 1 123456^INDUSTRIES/QZ^789012'));    // Write mag strip buffer
    $.merge(printData, convertEPCL('&E*'));        // Encode magnetic data
    $.merge(printData, convertEPCL('I 10'));       // Print card (10 returns to print ready pos.)
    $.merge(printData, convertEPCL('MO'));         // Move card to output hopper

    return printData;
}

/**
 * EPCL helper function that appends a single line of EPCL data, taking into account
 * special EPCL NUL characters, data length, escape character and carriage return
 */
function convertEPCL(data) {
    if (data == null || data.length == 0) {
        console.warn('Empty EPCL data, skipping');
    }

    // Data length for this command, in 2 character Hex (base 16) format
    var len = (data.length + 2).toString(16);
    if (len.length < 2) { len = '0' + len; }

    //defaults to 'type: raw' and 'format: command'
    return [
        { flavor: 'hex', data: 'x00x00x00' },  // Append 3 NULs
        { flavor: 'hex', data: 'x' + len },    // Append our command length, in base16
        { flavor: 'plain', data: data },       // Append our command
        { flavor: 'plain', data: '\r' }        // Append carriage return
    ];
}

/* Sample EPL Only */
function printBase64() {
    var config = getUpdatedConfig();
    //print options not used with this flavor

    // Send base64 encoded characters/raw commands to qz using data type 'base64'.
    // This will automatically convert provided base64 encoded text into text/ascii/bytes, etc.
    // This example is for EPL and contains an embedded image.
    // Please adapt to your printer language.

    //noinspection SpellCheckingInspection
    var printData = [
        {
            type: 'raw', format: 'command', flavor: 'base64',
            data: 'Ck4KcTYwOQpRMjAzLDI2CkI1LDI2LDAsMUEsMyw3LDE1MixCLCIxMjM0IgpBMzEwLDI2LDAsMywx' +
                'LDEsTiwiU0tVIDAwMDAwIE1GRyAwMDAwIgpBMzEwLDU2LDAsMywxLDEsTiwiUVogUFJJTlQgQVBQ' +
                'TEVUIgpBMzEwLDg2LDAsMywxLDEsTiwiVEVTVCBQUklOVCBTVUNDRVNTRlVMIgpBMzEwLDExNiww' +
                'LDMsMSwxLE4sIkZST00gU0FNUExFLkhUTUwiCkEzMTAsMTQ2LDAsMywxLDEsTiwiUVpJTkRVU1RS' +
                'SUVTLkNPTSIKR1cxNTAsMzAwLDMyLDEyOCz/////////6SSSX///////////////////////////' +
                '//////////6UlUqX////////////////////////////////////8kqkpKP/////////////////' +
                '//////////////////6JUpJSVf//////////////////////////////////9KpKVVU+////////' +
                '//////////////////////////8KSSlJJf5/////////////////////////////////9KUqpVU/' +
                '/7////////////////////////////////9KqUkokf//P///////////////////////////////' +
                '+VKUqpZP//+P///////////////////////////////ElKUlSf///9f/////////////////////' +
                '////////+ipSkqin////y/////////////////////////////+lVUpUlX/////r////////////' +
                '/////////////////qlJKUql/////+n////////////////////////////BFKVKUl//////8v//' +
                '/////////////////////////zVSlKUp///////0f//////////////////////////wiSlSUpf/' +
                '//////q///////////////////////////KqlJUpV///////+R//////////////////////////' +
                '4UlKSpSX///////9T/////////6L///////////////BKlKpSqP///////1X////////0qg/23/V' +
                'VVVVVVf//8CSlJKklf///////kv///////+pS0/JP8AAAAAAB///wFSlSSpV///////+pf//////' +
                '/pUoq+qfwAAAAAAH//+AClSqpUT///////9S///////8pJUlkr+AAAAAAA///4AFJSSSUv//////' +
                '/yl///////KVUpTUv8AAAAAAH///gBKSqlVU////////lX//////6UkqoiU/wAAAAAA///+ABKpJ' +
                'Uko////////JH//////UpIiqlJ/AAAAAAD///wACkSUpJX///////6q//////6pVVSqiv4AAAAAA' +
                'f///AAJVVIqpP///////pI//////pSVtSSq/wAAAAAD///8AAJSlVJVf///////Sp/////8Sq//U' +
                'qL/ttttoAP///wAAUpVSpJ///////+pT/////qkn//UlH/////AB////AABKUSpSX///////5Sn/' +
                '///+lJ//+pS/////4AP///8AABKUkpVP///////ylP////1Kv//+qr/////AA////4AAKVVJUl//' +
                '/////+lKf////KS///8kv////8AH////gAAKSSpJR///////9Kq////9Kv///5Kf////gAf///+A' +
                'AAUlUqov///////1JT////lS////qn////8AD////4AABKpKSqf///////Skj///+kr////JH///' +
                '/wAf////wAACkqUlK///////8pKv///ypf///9V////+AD/////AAAFKUVSj///////wqlP///JT' +
                '////yR////wAP////8AAAFKqkpv///////JSlf//9Sv////U/////AB/////4AAAVIpKRf//////' +
                '+ElV///pS////8of///4AP/////gAAASZVKr///////4qkj///Sn////0v////AA//////AAABUS' +
                'VJH///////glJn//8pP////KH///8AH/////+AAACtUlVf//////+ClRP//qV////9K////gA///' +
                '///4AAACEpJK///////8BSqf/+lX////yr///8AD//////wAAAVUqVH///////gUlU//5Rf////R' +
                'P///gAf//////gAAApKqTP//////8AVSV//pU////6qf//+AD//////+AAAAqkki//////8AEpVL' +
                '/+qP////1L///wAP//////4AAACSVVB/////+AFUpKX/9KP////Sv//+AB///////AAAAEqSgH//' +
                '//+ACkpSUv/lV////6k///4AP//////+AAAAUlSgf////gAJKRUpf/ST////1J///AA///////4A' +
                'AAAVJVB////gAtVFUpV/8lX///+Vf//4AH///////gAAABKSSD///wASSVVJSR/1Vf///8kf//gA' +
                '///////+AAAABVUof//4AElUpKqqv/SL////1L//8AD///////4AAAABJJQ//8AFVJKVKSSP+qj/' +
                '///Kv//gAf///////gAAAAKSpT/+ACkqSlKUkqf5Rf///6S//+AD///////+AAAAAKqpP/ABJKVS' +
                'klKqU/xUf///qp//wAP///////4AAAAAkko+gASVKUlVKlKX/VK///9Sf/+AB////////gAAAACp' +
                'UrgAKqVKVJKSlKf+Sl///0kf/4AP///////+AAAAABSVIAFJUlKqSUpKV/0pX//8qr//AA//////' +
                '//8AAAAACklACSopKSVUqVKX/qpH//okv/4AH////////gAAAAAVVKBUpUqUkkpKSk//SSv/xVK/' +
                '/AAAAAAD////AAAAAAJKWSUpVKVVUqVSp/+qqH9SlR/8AAAAAAH///4AAAAABSUklJSSlJJKUkpf' +
                '/8klQFSo//gAAAAAA////wAAAAABVKqlUkqlSqkqqU//6pUqkkof8AAAAAAB/r//AAAAAAElEpSK' +
                'qSlSSpJKL//pUqpVKr/wAAAAAAP8v/8AAAAAAJLKUqkkpSqkqSVf//yUkpKSv+AAAAAAAfqf/wAA' +
                'AAAAVClKVVUoklUqqp///UpKVVS/wAAAAAAD+S//AAAAAAAlpSkkkpVKkpKSX///JVKTpR+AAAAA' +
                'AAH9X/8AAAAAABRUpVJUqqSpSUlf///SSk/Sv4AAAAAAA/y//wAAAAAAFSVUlSUkUkpUqr////VS' +
                'v9S/AAAAAAAB/3//AAAAAAAFUkpSlJMqqUpJP////13/pT////////////8AAAAAAAEpJSlSqUkk' +
                'pVS////////Un////////////wAAAAAABJVSlSpUqpUpJX///////8q/////////////gAAAAAAC' +
                'pSqkkpKSUpSSP///////5L////////////+AAAAAAACSkVVKSklKpVV///////+SX///////////' +
                '/4AAAAAAAFSqJKlSqqiVSX///////9U/////////////gAAAAAAASpVSlSkklVJU////////yr//' +
                '//////////+AAAAAAAAkpJSklKpKSUp////////kn////////////4AAAAAAABJSqlKqkqUqVf//' +
                '/////5K/////////////gAAAAAAACpUlKpJKUqlI////////1L////////////+AAAAAAAAFSVKS' +
                'SqkpFKX////////SX////////////4AAAAAAAAiklKlSSpTKKv///////9U/////////////wAAA' +
                'AAAABSpSlSqlSiVJ////////pV/////////////AAAAAAAAVUpSkklSlUqX////////Uv///////' +
                '/////8AAAAAAAAkqUpVJJSqpVf///////8pf////////////4AAAAAAAFJKUpKqUpJUT////////' +
                '4r/////////////wAAAAAAAKqVKVKUqSSVX///////+Uv/////////////gAAAAAAASUlKSkpKql' +
                'S////////+qf/////////////AAAAAAAEkpKUlUpJJCn////////iH///////////wAAAAAAAAAA' +
                'AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA' +
                'AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA' +
                'AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA' +
                'AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAH/4B+A8AH/AAAAA' +
                'AAAAAAAAAAAAAA//AAfwD4H4HwAAf/4H4DwB//gAAAAAAAAAAAAAAAAAD/+AB/APgfgfAAB//wfw' +
                'PAf/+AAAAAAAAAAAAAgAAAAP/8AH8AfB+D4AAH//B/g8D//4AAAAAAAAAAAADwAAAA//4A/4B8H4' +
                'PgAAfB+H+DwP4HgAAAAAAAAAAAAPwAAAD4fgD/gHw/w+AAB8D4f8PB+AGAAAAAAAAAAAAA/wAAAP' +
                'g+Af/AfD/D4AAHwPh/48HwAAAAAAAAAAAAAAB/4AAA+D4B98A+P8PAAAfA+Hvjw+AAAAAAAAAAAA' +
                'AAAB/4AAD4PgH3wD4/x8AAB8H4e/PD4AAAAAAAAAAAAAAAB/8AAPh8A+PgPn/nwAAH//B5+8Pg/4' +
                'AH/j/x/4/8f+AA/8AA//wD4+A+eefAAAf/4Hj7w+D/gAf+P/H/j/x/4AA/wAD/+APj4B5554AAB/' +
                '/AeP/D4P+AB/4/8f+P/H/gAD/AAP/wB8HwH3nvgAAH/wB4f8Pw/4AH/j/x/4/8f+AA/8AA//AH//' +
                'Af+f+AAAfAAHg/wfAPgAAAAAAAAAAAAAf/AAD5+A//+B/w/4AAB8AAeD/B+A+AAAAAAAAAAAAAH/' +
                'gAAPj8D//4D/D/AAAHwAB4H8H+D4AAAAAAAAAAAAB/4AAA+H4P//gP8P8AAAfAAHgPwP//gAAAAA' +
                'AAAAAAAP8AAAD4fh+A/A/w/wAAB8AAeA/Af/+AAAAAAAAAAAAA/AAAAPg/HwB8B+B+AAAHwAB4B8' +
                'Af/4AAAAAAAAAAAADwAAAA+B+fAHwH4H4AAAfAAHgHwAf4AAAAAAAAAAAAAIAAAAD4H/8Afgfgfg' +
                'AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA' +
                'AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA' +
                'AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA' +
                'AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA' +
                'AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA' +
                'AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA' +
                'AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA' +
                'AAAAAAAAAAAAAAAAAAAAAAAAClAxLDEK'
        }
    ];

    qz.print(config, printData).catch(displayError);
}

/* Sample ZPL Only */
function printXML() {
    var config = getUpdatedConfig();
    var opts = getUpdatedOptions();

    var printData = [
        { type: 'raw', format: 'command', flavor: 'xml', data: 'assets/zpl_sample.xml', options: opts }
    ];

    qz.print(config, printData).catch(displayError);
}

function printHex() {
    var config = getUpdatedConfig();
    var lang = getUpdatedOptions().language; //print options not used with this flavor, just check language requested

    var printData;
    switch (lang) {
        case 'EPL':
            //defaults to 'type: raw' and 'format: command'
            printData = [
                { flavor: 'hex', data: '4e0d0a713630390d0a513230332c32360d0a42352c32362c3' },
                { flavor: 'hex', data: '02c31412c332c372c3135322c422c2239383736220d0a4133' },
                { flavor: 'hex', data: '31302c32362c302c332c312c312c4e2c22534b55203030303' },
                { flavor: 'hex', data: '030204d46472030303030220d0a413331302c35362c302c33' },
                { flavor: 'hex', data: '2c312c312c4e2c2254455354205052494e542053554343455' },
                { flavor: 'hex', data: '35346554c220d0a413331302c38362c302c332c312c312c4e' },
                { flavor: 'hex', data: '2c225553494e472068657820434f4d4d414e4453220d0a413' },
                { flavor: 'hex', data: '331302c3131362c302c332c312c312c4e2c225052494e5445' },
                { flavor: 'hex', data: '44205749544820515a2054524159220d0a50312c31' }
            ];
            break;
        case 'ZPL':
            printData = [
                { flavor: 'hex', data: '5e58410d0a5e464f35302c35300d0a5e41444e2c33362c3' },
                { flavor: 'hex', data: '2300d0a5e46445052494e544544205749544820515a2054' },
                { flavor: 'hex', data: '5241590d0a5e46445553494e472068657820434f4d4d414' },
                { flavor: 'hex', data: 'e44530d0a5e46530d0a5e585a' }
            ];
            break;
        case 'ESCPOS':
            printData = [
                { flavor: 'hex', data: '0d0a5052494e544544205749544820515a20545241590d0a0d0a' },
                { flavor: 'hex', data: '5553494e472068657820434f4d4d414e44530d0a0d0a0d0a0d0a' }
            ];
            break;
        default:
            displayError("Sample page cannot print hex commands using this printer language");
            break;
    }


    qz.print(config, printData).catch(displayError);
}

function printRawImage() {
    var config = getUpdatedConfig();
    var opts = getUpdatedOptions();

    var printData;
    switch (opts.language) {
        case 'EPL':
            printData = [
                '\nN\n',
                { type: 'raw', format: 'image', flavor: 'file', data: 'assets/img/image_sample_bw.png', options: opts },
                '\nP1,1\n'
            ];
            break;
        case 'ZPL':
            printData = [
                '^XA\n',
                { type: 'raw', format: 'image', flavor: 'file', data: 'assets/img/image_sample_bw.png', options: opts },
                '^XZ\n'
            ];
            break;
        case 'ESCPOS':
            printData = [
                //defaults to 'flavor: file'
                { type: 'raw', format: 'image', data: 'assets/img/image_sample_bw.png', options: opts },
            ];
            break;
        default:
            displayError("Cannot print images using this printer language");
            return;
    }

    qz.print(config, printData).catch(displayError);
}

function printRawPDF() {
    var config = getUpdatedConfig();
    var opts = getUpdatedOptions();

    var printData;
    switch (opts.language) {
        case 'EPL':
            printData = [
                '\nN\n',
                'q812\n',
                'Q1218,26\n',
                { type: 'raw', format: 'pdf', flavor: 'file', data: 'assets/pdf_sample.pdf', options: opts },
                '\nP1,1\n'
            ];
            break;
        case 'ZPL':
            printData = [
                '^XA\n',
                { type: 'raw', format: 'pdf', flavor: 'file', data: 'assets/pdf_sample.pdf', options: opts },
                '^XZ\n'
            ];
            break;
        case 'ESCPOS':
            printData = [
                { type: 'raw', format: 'pdf', flavor: 'file', data: 'assets/pdf_sample.pdf', options: opts }
            ];
            break;
        default:
            displayError("Cannot print PDFs using this printer language");
            break;
    }

    qz.print(config, printData).catch(displayError);
}

function printRawHTML() {
    var config = getUpdatedConfig();
    var opts = getUpdatedOptions();

    var printData;
    switch (opts.language) {
        case 'EPL':
            printData = [
                '\nN\n',
                'q812\n',
                'Q1218,26\n',
                { type: 'raw', format: 'html', flavor: 'file', data: 'https://qz.io/about/', options: opts },
                '\nP1,1\n'
            ];
            break;
        case 'ZPL':
            printData = [
                '^XA\n',
                { type: 'raw', format: 'html', flavor: 'file', data: 'https://qz.io/about/', options: opts },
                '^XZ\n'
            ];
            break;
        case 'ESCPOS':
            printData = [
                { type: 'raw', format: 'html', flavor: 'file', data: 'https://qz.io/about/', options: opts }
            ];
            break;
        default:
            displayError("Cannot print HTML using this printer language");
            break;
    }

    qz.print(config, printData).catch(displayError);
}

function printFile(file) {
    var config = getUpdatedConfig();
    //print options not used with this flavor

    var printData = [
        { type: 'raw', format: 'command', flavor: 'file', data: 'assets/' + file }
    ];

    qz.print(config, printData).catch(displayError);
}


/// Pixel Printers ///
function printHTML() {
    var config = getUpdatedConfig();
    var opts = getUpdatedOptions(true);

    var printData = [
        {
            type: 'pixel',
            format: 'html',
            flavor: 'plain',
            data: '<html>' +
                '<body>' +
                '  <table style="font-family: monospace; width: 100%">' +
                '    <tr>' +
                '      <td>' +
                '        <h2>* QZ Tray HTML Sample Print *</h2>' +
                '        <span style="color: #D00;">Version:</span> ' + qzVersion + '<br/>' +
                '        <span style="color: #D00;">Source:</span> https://qz.io/' +
                '      </td>' +
                '      <td align="right">' +
                '        <img src="' + getPath() + '/assets/img/image_sample.png" />' +
                '      </td>' +
                '    </tr>' +
                '  </table>' +
                '</body>' +
                '</html>',
            options: opts
        }
    ];

    qz.print(config, printData).catch(displayError);
}

function printPDF() {
    var config = getUpdatedConfig();
    var opts = getUpdatedOptions(true);

    var printData = [
        { type: 'pixel', format: 'pdf', flavor: 'file', data: 'assets/pdf_sample.pdf', options: opts }
    ];

    qz.print(config, printData).catch(displayError);
}

function printImage() {
    var config = getUpdatedConfig();
    //print options not used with this flavor

    var printData = [
        { type: 'pixel', format: 'image', flavor: 'file', data: 'assets/img/image_sample.png' }
        //also valid, as format and flavor will default to proper values:
        //             { type: 'pixel', data: 'assets/img/image_sample.png' }
    ];

    qz.print(config, printData).catch(displayError);
}


/// Serial ///
function listSerialPorts() {
    qz.serial.findPorts().then(function (data) {
        var list = '';
        for (var i = 0; i < data.length; i++) {
            list += "&nbsp; <code>" + data[i] + "</code>" + serialButton(["serialPort"], [data[i]]) + "<br/>";
        }

        displayMessage("<strong>Available serial ports:</strong><br/>" + list);
    }).catch(displayError);
}

function openSerialPort() {
    var options = getSerialOptions();

    qz.serial.openPort($("#serialPort").val(), options).then(function () {
        displayMessage("Serial port opened");
    }).catch(displayError);
}

function sendSerialData() {
    var options = getSerialOptions();

    var serialData = {
        type: $("input[name='serialType']:checked").val(),
        data: $("#serialCmd").val()
    };
    //allow some escape characters
    serialData.data = serialData.data.replace(/\\n/g, "\n").replace(/\\r/g, "\r").replace(/\\t/g, "\t");

    qz.serial.sendData($("#serialPort").val(), serialData, options).catch(displayError);
}

function closeSerialPort() {
    qz.serial.closePort($("#serialPort").val()).then(function () {
        displayMessage("Serial port closed");
    }).catch(displayError);
}


// Socket //
function openSocket() {
    qz.socket.open($("#socketHost").val(), $("#socketPort").val()).then(function () {
        displayMessage("Socket opened");
    }).catch(displayError);
}

function sendSocketData() {
    qz.socket.sendData($("#socketHost").val(), $("#socketPort").val(), $("#socketData").val()).catch(displayError);
}

function closeSocket() {
    qz.socket.close($("#socketHost").val(), $("#socketPort").val()).then(function () {
        displayMessage("Socket closed");
    }).catch(displayError);
}


/// USB ///
function listUsbDevices() {
    qz.usb.listDevices(true).then(function (data) {
        var list = '';
        for (var i = 0; i < data.length; i++) {
            var device = data[i];
            if (device.hub) { list += "USB Hub"; }

            list += "<p>" +
                "   VendorID: <code>0x" + device.vendorId + "</code>" +
                usbButton(["usbVendor", "usbProduct"], [device.vendorId, device.productId]) + "<br/>" +
                "   ProductID: <code>0x" + device.productId + "</code><br/>";

            if (device.manufacturer) { list += "   Manufacturer: <code>" + device.manufacturer + "</code><br/>"; }
            if (device.product) { list += "   Product: <code>" + device.product + "</code><br/>"; }

            list += "</p><hr/>";
        }

        pinMessage("<strong>Available usb devices:</strong><br/>" + list);
    }).catch(displayError);
}

function listUsbDeviceInterfaces() {
    qz.usb.listInterfaces({
        vendorId: $("#usbVendor").val(),
        productId: $("#usbProduct").val()
    })
        .then(function (data) {
            var list = '';
            for (var i = 0; i < data.length; i++) {
                list += "&nbsp; <code>0x" + data[i] + "</code>" + usbButton(["usbInterface"], [data[i]]) + "<br/>";
            }

            displayMessage("<strong>Available device interfaces:</strong><br/>" + list);
        }).catch(displayError);
}

function listUsbInterfaceEndpoints() {
    qz.usb.listEndpoints({
        vendorId: $("#usbVendor").val(),
        productId: $("#usbProduct").val(),
        interface: $("#usbInterface").val()
    })
        .then(function (data) {
            var list = '';
            for (var i = 0; i < data.length; i++) {
                list += "&nbsp; <code>0x" + data[i] + "</code>" + usbButton(["usbEndpoint"], [data[i]]) + "<br/>";
            }

            displayMessage("<strong>Available interface endpoints:</strong><br/>" + list);
        }).catch(displayError);
}

function claimUsbDevice() {
    qz.usb.claimDevice({
        vendorId: $("#usbVendor").val(),
        productId: $("#usbProduct").val(),
        interface: $("#usbInterface").val()
    })
        .then(function () {
            displayMessage("USB Device claimed");
        }).catch(displayError);
}

function checkUsbDevice() {
    qz.hid.isClaimed({
        vendorId: $("#usbVendor").val(),
        productId: $("#usbProduct").val()
    })
        .then(function (claimed) {
            displayMessage("USB Device is " + (claimed ? "" : "not ") + "claimed");
        }).catch(displayError);
}

function sendUsbData() {
    qz.usb.sendData({
        vendorId: $("#usbVendor").val(),
        productId: $("#usbProduct").val(),
        endpoint: $("#usbEndpoint").val(),
        data: $("#usbData").val()
    })
        .catch(displayError);
}

function readUsbData() {
    qz.usb.readData({
        vendorId: $("#usbVendor").val(),
        productId: $("#usbProduct").val(),
        endpoint: $("#usbEndpoint").val(),
        responseSize: $("#usbResponse").val()
    })
        .then(function (data) {
            displayMessage("<strong>Response:</strong> " + (window.readingWeight ? readScaleData(data) : data) + "<br/>");
        }).catch(displayError);
}

function openUsbStream() {
    qz.usb.openStream({
        vendorId: $("#usbVendor").val(),
        productId: $("#usbProduct").val(),
        endpoint: $("#usbEndpoint").val(),
        responseSize: $("#usbResponse").val(),
        interval: $("#usbStream").val()
    })
        .then(function () {
            pinMessage("Waiting on device", '' + $("#usbVendor").val() + $("#usbProduct").val());
        }).catch(displayError);
}

function closeUsbStream() {
    qz.usb.closeStream({
        vendorId: $("#usbVendor").val(),
        productId: $("#usbProduct").val(),
        endpoint: $("#usbEndpoint").val()
    })
        .then(function () {
            $('#' + $("#usbVendor").val() + $("#usbProduct").val()).attr('id', '').html("Stream closed");
        }).catch(displayError);
}

function releaseUsbDevice() {
    qz.usb.releaseDevice({
        vendorId: $("#usbVendor").val(),
        productId: $("#usbProduct").val()
    })
        .then(function () {
            displayMessage("USB Device released");
        }).catch(displayError);
}


/// HID ///
function listHidDevices() {
    qz.hid.listDevices().then(function (data) {
        var list = '';
        for (var i = 0; i < data.length; i++) {
            var device = data[i];

            list += "<p>" +
                "   VendorID: <code>0x" + device.vendorId + "</code>" +
                usbButton(["hidVendor", "hidProduct", "hidUsagePage", "hidSerial"],
                    [device.vendorId, device.productId, device.usagePage, device.serial]) + "<br/>" +
                "   ProductID: <code>0x" + device.productId + "</code><br/>" +
                (device.usagePage ? "   Usage Page: <code>0x" + device.usagePage + "</code><br/>" : "") +
                (device.serial ? "   Serial: <code>" + device.serial + "</code><br/>" : "") +
                (device.manufacturer ? "   Manufacturer: <code>" + device.manufacturer + "</code><br/>" : "") +
                (device.product ? "   Product: <code>" + device.product + "</code><br/>" : "") +
                "</p><hr/>";
        }

        pinMessage("<strong>Available hid devices:</strong><br/>" + list);
    }).catch(displayError);
}

function startHidListen() {
    qz.hid.startListening().then(function () {
        displayMessage("Started listening for HID events");
    }).catch(displayError);
}

function stopHidListen() {
    qz.hid.stopListening().then(function () {
        displayMessage("Stopped listening for HID events");
    }).catch(displayError);
}

function claimHidDevice() {
    qz.hid.claimDevice({
        vendorId: $("#hidVendor").val(),
        productId: $("#hidProduct").val(),
        usagePage: $("#hidUsagePage").val(),
        serial: $("#hidSerial").val()
    })
        .then(function () {
            displayMessage("HID Device claimed");
        }).catch(displayError);
}

function checkHidDevice() {
    qz.hid.isClaimed({
        vendorId: $("#hidVendor").val(),
        productId: $("#hidProduct").val(),
        usagePage: $("#hidUsagePage").val(),
        serial: $("#hidSerial").val()
    })
        .then(function (claimed) {
            displayMessage("HID Device is " + (claimed ? "" : "not ") + "claimed");
        }).catch(displayError);
}

function sendHidData() {
    qz.hid.sendData({
        vendorId: $("#hidVendor").val(),
        productId: $("#hidProduct").val(),
        usagePage: $("#hidUsagePage").val(),
        serial: $("#hidSerial").val(),
        data: $("#hidData").val(),
        endpoint: $("#hidReport").val()
    })
        .catch(displayError);
}

function readHidData() {
    qz.hid.readData({
        vendorId: $("#hidVendor").val(),
        productId: $("#hidProduct").val(),
        usagePage: $("#hidUsagePage").val(),
        serial: $("#hidSerial").val(),
        responseSize: $("#hidResponse").val()
    })
        .then(function (data) {
            displayMessage("<strong>Response:</strong> " + (window.readingWeight ? readScaleData(data) : data) + "<br/>");
        }).catch(displayError);
}

function openHidStream() {
    qz.hid.openStream({
        vendorId: $("#hidVendor").val(),
        productId: $("#hidProduct").val(),
        usagePage: $("#hidUsagePage").val(),
        serial: $("#hidSerial").val(),
        responseSize: $("#hidResponse").val(),
        interval: $("#hidStream").val()
    })
        .then(function () {
            pinMessage("Waiting on device", '' + $("#hidVendor").val() + $("#hidProduct").val());
        }).catch(displayError);
}

function closeHidStream() {
    qz.hid.closeStream({
        vendorId: $("#hidVendor").val(),
        productId: $("#hidProduct").val(),
        usagePage: $("#hidUsagePage").val(),
        serial: $("#hidSerial").val()
    })
        .then(function () {
            $('#' + $("#hidVendor").val() + $("#hidProduct").val()).attr('id', '').html("Stream closed");
        }).catch(displayError);
}

function releaseHidDevice() {
    qz.hid.releaseDevice({
        vendorId: $("#hidVendor").val(),
        productId: $("#hidProduct").val(),
        usagePage: $("#hidUsagePage").val(),
        serial: $("#hidSerial").val()
    })
        .then(function () {
            displayMessage("HID Device released");
        }).catch(displayError);
}


/// Status ///
function startPrintersListen(printerName) {
    if (printerName === "NONE") {
        return displayMessage("Please search for a valid printer first", "alert-warning");
    }
    qz.printers.stopListening().then(function () {
        clearPrintersLog();
        return qz.printers.startListening(printerName);
    }).then(function () {
        displayMessage("Started listening for " + (printerName ? printerName : "all") + " printer events");
    }).catch(displayError);
}

function getPrintersStatus() {
    qz.printers.getStatus().then(function () {
        displayMessage("Requesting all printer statuses for listened printers");
    }).catch(displayError);
}

function stopPrintersListen() {
    qz.printers.stopListening().then(function () {
        displayMessage("Stopped listening for printer events");
        clearPrintersLog();
    }).catch(displayError);
}


/// File ///
function listFiles() {
    var params = {
        sandbox: $("#fileSandbox").prop("checked"),
        shared: $("#fileShared").prop("checked")
    };

    qz.file.list($("#fileLocation").val(), params).then(function (data) {
        var files = "";
        for (var n = 0; n < data.length; n++) {
            files += data[n] + "\n";
        }
        displayMessage("File listing <strong><code>" + $("#fileLocation").val() + "</code></strong><pre>" + files + "</pre>", null, 15000);
    }).catch(displayError);
}

function readFile() {
    var params = {
        sandbox: $("#fileSandbox").prop("checked"),
        shared: $("#fileShared").prop("checked")
    };

    qz.file.read($("#fileLocation").val(), params).then(function (data) {
        displayMessage("Contents of <strong><code>" + $("#fileLocation").val() + "</code></strong><pre>" + data + "</pre>", null, 15000);
    }).catch(displayError);
}

function writeFile() {
    var params = {
        sandbox: $("#fileSandbox").prop("checked"),
        shared: $("#fileShared").prop("checked"),
        append: $("#fileAppend").prop('checked'),
        data: $("#fileData").val()
    };

    qz.file.write($("#fileLocation").val(), params).then(function () {
        displayMessage("File <strong><code>" + $("#fileLocation").val() + "</code></strong> written successfully");
    }).catch(displayError);
}

function deleteFile() {
    var params = {
        sandbox: $("#fileSandbox").prop("checked"),
        shared: $("#fileShared").prop("checked")
    };

    qz.file.remove($("#fileLocation").val(), params).then(function () {
        displayMessage("File <strong><code>" + $("#fileLocation").val() + "</code></strong> deleted");
    }).catch(displayError);
}

function startFileListen() {
    var params = {
        sandbox: $("#fileSandbox").prop("checked"),
        shared: $("#fileShared").prop("checked"),
        include: $("#includePattern").val() == "" ? [] : $("#includePattern").val(),
        exclude: $("#excludePattern").val() == "" ? [] : $("#excludePattern").val(),
        ignoreCase: true,
        listener: {}
    };

    if (isChecked($("#fileListenerData"))) {
        params.listener.reverse = !!$("#fileDirEnd").prop("checked"); //else fileStartRadio checked

        var len = $("#fileLength").val();
        if (!!$("#fileTruncateLines").prop("checked")) { //else fileTruncateBytes checked
            params.listener.lines = len;
        } else {
            params.listener.bytes = len;
        }
    }

    qz.file.startListening($("#fileLocation").val(), params).then(function () {
        displayMessage("Started listening for <strong><code>" + ($("#fileLocation").val() || "./") + "</code></strong> events");
    }).catch(displayError);
}

function stopFileListen() {
    var params = {
        sandbox: $("#fileSandbox").prop("checked"),
        shared: $("#fileShared").prop("checked")
    };

    qz.file.stopListening($("#fileLocation").val(), params).then(function () {
        displayMessage("Stopped listening for <strong><code>" + ($("#fileLocation").val() || "./") + "</code></strong> events");
    }).catch(displayError);
}

function stopAllFileListeners() {
    qz.file.stopListening().then(function () {
        displayMessage("Stopped listening for <strong>all</strong> file events");
    }).catch(displayError);
}


/// Resets ///
function resetGeneralOptions() {
    //connection
    $("#connectionHost").val('localhost');
    $("#connectionUsingSecure").prop('disabled', location.protocol !== 'https:');
}

function resetRawOptions() {
    //config
    $("#rawSpoolSize").val(1);
    $("#rawEncoding").val(null);
    $("#rawSpoolEnd").val(null);
    $("#rawAltPrinting").prop('checked', false);
    $("#rawCopies").val(1);

    //printer
    $("#pLangEPL").prop('checked', true);
    $("#pX").val('0');
    $("#pY").val('0');
    $("#pDotDensity").val('single');
    $("#pXml").val('v7:Image');
    $("#pRawWidth").val('480');
    $("#pRawHeight").val('');
}

function resetPixelOptions() {
    //config
    $("#pxlColorType").val("color");
    $("#pxlCopies").val(1);
    $("#pxlDuplex").val("");
    $("#pxlInterpolation").val("");
    $("#pxlJobName").val("");
    $("#pxlLegacy").prop('checked', false);
    $("#pxlOrientation").val("");
    $("#pxlPaperThickness").val(null);
    $("#pxlPrinterTray").val(null);
    $("#pxlRasterize").prop('checked', false);
    $("#pxlRotation").val(0);
    $("#pxlSpoolSize").val("");
    $("#pxlScale").prop('checked', true);
    $("#pxlUnitsIN").prop('checked', true);

    $("#pxlDensity").val('').css('display', '');
    $("#pxlCrossDensity").val('');
    $("#pxlFeedDensity").val('');
    $("#pxlDensityAsymm").prop('checked', false);
    $("#pxlDensityGroup").css('display', 'none');

    $("#pxlMargins").val(0).css('display', '');
    $("#pxlMarginsTop").val(0);
    $("#pxlMarginsRight").val(0);
    $("#pxlMarginsBottom").val(0);
    $("#pxlMarginsLeft").val(0);
    $("#pxlMarginsActive").prop('checked', false);
    $("#pxlMarginsGroup").css('display', 'none');

    $("#pxlSizeWidth").val('');
    $("#pxlSizeHeight").val('');
    $("#pxlSizeActive").prop('checked', false);
    $("#pxlSizeGroup").css('display', 'none');

    $("#pxlBoundX").val(0);
    $("#pxlBoundY").val(0);
    $("#pxlBoundWidth").val('');
    $("#pxlBoundHeight").val('');
    $("#pxlBoundsActive").prop('checked', false);
    $("#pxlBoundsGroup").css('display', 'none');

    //printer
    $("#pPxlWidth").val('');
    $("#pPxlHeight").val('');

    //connection
    $("#connectionGroup").css('display', 'none');

    $("#pxlContent").find(".dirty").removeClass("dirty");
}

function resetSerialOptions() {
    $("#serialPort").val('');
    $("#serialBaud").val(9600);
    $("#serialData").val(8);
    $("#serialStop").val(1);
    $("#serialParity").val('NONE');
    $("#serialFlow").val('NONE');

    $("#serialCmd").val('');
    $("#serialPlainRadio").prop('checked', true);
    $("#serialEncoding").val("UTF-8");

    $("#serialStart").val('');
    $("#serialEnd").val('');
    $("#serialWidth").val('');
    $("#serialHeader").prop('checked', false);
    $("#serialRespEncoding").val('');
    $("#serialLenIndex").val('0');
    $("#serialLenLength").val('1');
    $("#serialLenEndianBig").prop('checked', true);
    $("#serialLengthGroup").css('display', 'none');
    $("#serialCrcIndex").val('0');
    $("#serialCrcLength").val('1');
    $("#serialCrcGroup").css('display', 'none');

    // M/T PS60 - 9600, 7, 1, EVEN, NONE
}

function resetUsbOptions() {
    $("#usbVendor").val('');
    $("#usbProduct").val('');

    $("#usbInterface").val('');
    $("#usbEndpoint").val('');
    $("#usbData").val('');
    $("#usbResponse").val(8);
    $("#usbStream").val(100);

    // M/T PS60 - V:0x0EB8 P:0xF000, I:0x0 E:0x81
    // Dymo S100 - V:0x0922 P:0x8009, I:0x0 E:0x82
}

function resetHidOptions() {
    $("#hidVendor").val('');
    $("#hidProduct").val('');
    $("#hidUsagePage").val('');
    $("#hidSerial").val('');

    $("#hidInterface").val('');
    $("#hidEndpoint").val('');
    $("#hidData").val('');
    $("#hidReport").val('');
    $("#hidResponse").val(8);
    $("#hidStream").val(100);
}

function clearPrintersLog() {
    $("#printersLog").html("");
}

function resetFileOptions() {
    $("#fileLocation").val('');
    $("#fileData").val('');
    $("#fileShared").prop('checked', true);
    $("#fileSandbox").prop('checked', true);
    $("#fileAppend").prop('checked', false);

    $("#fileListenerData").prop('checked', true);
    $("#fileDirEnd").prop('checked', true);
    $("#fileTruncateLines").prop('checked', true);
    $("#fileLength").val('10');
}


/// Page load ///
$(document).ready(function () {
    window.readingWeight = false;

    resetGeneralOptions();
    resetRawOptions();
    resetPixelOptions();
    resetSerialOptions();
    resetUsbOptions();
    resetHidOptions();
    resetFileOptions();

    startConnection();
    //$("#disconnect-qz").hide();

    $("#printerSearch").on('keyup', function (e) {
        if (e.which == 13 || e.keyCode == 13) {
            findPrinter($('#printerSearch').val(), true);
            return false;
        }
    });

    $("#fileButton").on('change', function (e) {
        if (this.files && this.files[0]) {
            $("#fileLocation").val(this.files[0]['name']);
        }
    });

    $('a[data-toggle="tab"]').on('shown.bs.tab', function () {
        if (window.readingWeight) {
            $("#usbWeightRadio").click();
            $("#hidWeightRadio").click();
        } else {
            $("#usbRawRadio").click();
            $("#hidRawRadio").click();
        }
    });

    if (location.hash) {
        $(".nav-tabs a[href='" + location.hash + "']").tab('show');
    }
    $(".nav-tabs a").on("click", function (e) {
        location.hash = this.hash;
    });

    $("#usbRawRadio").click(function () { window.readingWeight = false; });
    $("#usbWeightRadio").click(function () { window.readingWeight = true; });
    $("#hidRawRadio").click(function () { window.readingWeight = false; });
    $("#hidWeightRadio").click(function () { window.readingWeight = true; });

    $("[data-toggle='tooltip']").tooltip();
});

qz.websocket.setClosedCallbacks(function (evt) {
    updateState('Inactive', 'default');
    console.log(evt);

    if (evt.reason) {
        displayMessage("<strong>Connection closed:</strong> " + evt.reason, 'alert-warning');
    }
});

qz.websocket.setErrorCallbacks(handleConnectionError);

qz.serial.setSerialCallbacks(function (streamEvent) {
    if (streamEvent.type !== 'ERROR') {
        console.log('Serial', streamEvent.portName, 'received output', streamEvent.output);
        displayMessage("Received output from serial port [" + streamEvent.portName + "]: <em>" + streamEvent.output + "</em>");
    } else {
        console.log(streamEvent.exception);
        displayMessage("Received an error from serial port [" + streamEvent.portName + "]: <em>" + streamEvent.exception + "</em>", 'alert-error');
    }
});

qz.socket.setSocketCallbacks(function (socketEvent) {
    if (socketEvent.type !== 'ERROR') {
        console.log('Socket', socketEvent.host, socketEvent.port, 'received response', socketEvent.response);
        displayMessage("Received output from network socket [" + socketEvent.host + ":" + socketEvent.port + "]: <em>" + socketEvent.response + "</em>");
    } else {
        console.log(socketEvent.exception);
        displayMessage("Received error from network socket [" + socketEvent.host + ":" + socketEvent.port + "]: <em>" + socketEvent.exception + "</em>", 'alert-error');
    }
});

qz.usb.setUsbCallbacks(function (streamEvent) {
    var vendor = streamEvent.vendorId;
    var product = streamEvent.productId;

    if (vendor.substring(0, 2) != '0x') { vendor = '0x' + vendor; }
    if (product.substring(0, 2) != '0x') { product = '0x' + product; }
    var $pin = $('#' + vendor + product);

    if (streamEvent.type !== 'ERROR') {
        if (window.readingWeight) {
            $pin.html("<strong>Weight:</strong> " + readScaleData(streamEvent.output));
        } else {
            $pin.html("<strong>Raw data:</strong> " + streamEvent.output);
        }
    } else {
        console.log(streamEvent.exception);
        $pin.html("<strong>Error:</strong> " + streamEvent.exception);
    }
});

qz.hid.setHidCallbacks(function (streamEvent) {
    var vendor = streamEvent.vendorId;
    var product = streamEvent.productId;

    if (vendor.substring(0, 2) != '0x') { vendor = '0x' + vendor; }
    if (product.substring(0, 2) != '0x') { product = '0x' + product; }
    var $pin = $('#' + vendor + product);

    if (streamEvent.type === 'RECEIVE') {
        if (window.readingWeight) {
            var weight = readScaleData(streamEvent.output);
            if (weight) {
                $pin.html("<strong>Weight:</strong> " + weight);
            }
        } else {
            $pin.html("<strong>Raw data:</strong> " + streamEvent.output);
        }
    } else if (streamEvent.type === 'ACTION') {
        displayMessage("<strong>Device status changed:</strong> " + "[v:" + vendor + " p:" + product + "] - " + streamEvent.actionType);
    } else { //ERROR type
        console.log(streamEvent.exception);
        $pin.html("<strong>Error:</strong> " + streamEvent.exception);
    }
});

qz.printers.setPrinterCallbacks(function (streamEvent) {
    addPrintersLog(streamEvent);
});

qz.file.setFileCallbacks(function (streamEvent) {
    if (streamEvent.type !== 'ERROR') {
        var text = "<h5>File IO Event</h5>" +
            "<strong>File:</strong> <code>" + streamEvent.file + "</code><br/>" +
            "<strong>Event:</strong><code>" + streamEvent.eventType + "</code><br/>";

        if (streamEvent.fileData) {
            text += "<strong>Data:</strong><br/><pre>" + streamEvent.fileData.replace(/\r?\n/g, "<br/>") + "</pre>";
        }

        displayMessage(text);
    } else {
        displayError("<strong>Error:</strong> " + streamEvent.message);
    }
});

var qzVersion = 0;
function findVersion() {
    qz.api.getVersion().then(function (data) {
        $("#qz-version").html(data);
        qzVersion = data;
    }).catch(displayError);
}

$("#askFileModal").on("shown.bs.modal", function () {
    $("#askFile").focus().select();
});
$("#askHostModal").on("shown.bs.modal", function () {
    $("#askHost").focus().select();
});

//make dirty when changed
$("input").add("select").on('change', function () {
    $(this).addClass("dirty");
});


/// Helpers ///
function handleConnectionError(err) {
    updateState('Error', 'danger');

    if (err.target != undefined) {
        if (err.target.readyState >= 2) { //if CLOSING or CLOSED
            displayError("Connection to QZ Tray was closed");
        } else {
            displayError("A connection error occurred, check log for details");
            console.error(err);
        }
    } else {
        displayError(err);
    }
}

function displayError(err) {
    //console.error(err);
    displayMessage(err, 'alert-danger');
}

function displayMessage(msg, css, time) {
    if (css == undefined) { css = 'alert-info'; }

    var timeout = setTimeout(function () { $('#' + timeout).alert('close'); }, time ? time : 5000);

    //var alert = $("<div/>").addClass('alert alert-dismissible fade in ' + css)
    //    .css('max-height', '20em').css('overflow', 'auto')
    //    .attr('id', timeout).attr('role', 'alert');
        var alert = $("<div/>").addClass('alert alert-warning')
        //.css('max-height', '20em').css('overflow', 'auto')
        .attr('id', timeout).attr('role', 'alert');
    alert.html("<button type='button' class='close' data-dismiss='alert'>&times;</button>" + msg);

    $("#qz-alert").append(alert);
}

//checkId = toggle checkbox, groupId = group/element to show, toggleId = group/element to hide
function checkGroupActive(checkId, groupId, toggleId) {
    var checkBox = $("#" + checkId);
    var useVisibility = false;
    var invisible = false;
    if (typeof checkBox.prop("checked") === 'undefined') {
        // if we're not dealing with a checkbox, rely blindly on visibility instead
        useVisibility = true;
        invisible = ($("#" + groupId).css('display') == 'none');
        if (invisible) {
            checkBox.addClass("active");
        } else {
            checkBox.removeClass('active');
        }
    }
    if (isChecked(checkBox) || (useVisibility && invisible)) {
        $("#" + groupId).css('display', '');
        if (toggleId) { $("#" + toggleId).css('display', 'none'); }
    } else {
        $("#" + groupId).css('display', 'none');
        if (toggleId) { $("#" + toggleId).css('display', ''); }
    }
}

//checkId = toggle checkbox, itemsArray = elements to disable
function checkItemsDisabled(checkId, itemsArray) {
    var disabled = isChecked($("#" + checkId));
    for (var index in itemsArray) {
        $("#" + itemsArray[index]).prop("disabled", disabled);
    }
}

function addPrintersLog(streamEvent) {
    var icon = '<span class="fa ' + (streamEvent.eventType == 'JOB' ? 'fa-exchange' : 'fa-print') + '"></span>&nbsp;';
    var msg = '<p class="' + (streamEvent.severity || "") + ' text-nowrap">' + icon + new Date().toString() + ": " + streamEvent.message + "</p>";
    var $log = $("#printersLog");
    $log.html($log.html() + msg);
}

function pinMessage(msg, id, css) {
    if (css == undefined) { css = 'alert-info'; }

    var alert = $("<div/>").addClass('alert alert-dismissible fade in ' + css)
        .css('max-height', '20em').css('overflow', 'auto').attr('role', 'alert')
        .html("<button type='button' class='close' data-dismiss='alert'>&times;</button>");

    var text = $("<div/>").html(msg);
    if (id != undefined) { text.attr('id', id); }

    alert.append(text);

    $("#qz-pin").append(alert);
}

function updateState(text, css) {
    $("#qz-status").html(text);
    $("#qz-connection").removeClass().addClass('panel panel-' + css);

    if (text === "Inactive" || text === "Error") {
        $("#launch").show();
    } else {
        $("#launch").hide();
    }
}

function getPath() {
    var path = window.location.href;
    return path.substring(0, path.lastIndexOf("/"));
}

function isChecked(checkElm, ifClean) {
    if (!checkElm.hasClass("dirty")) {
        if (ifClean !== undefined) {
            var lbl = checkElm.siblings("label").text();
            displayMessage("Forced " + lbl + " " + ifClean + ".", 'alert-warning');

            return ifClean;
        }
    }

    return checkElm.prop("checked");
}

function includedValue(element, value) {
    if (value != null) {
        return value;
    } else if (element.hasClass("dirty")) {
        return element.val();
    } else {
        return undefined;
    }
}

function usbButton(ids, data) {
    var click = "";
    for (var i = 0; i < ids.length; i++) {
        var $id = "$('#" + ids[i] + "')";
        click += $id + ".val('" + (data[i] ? data[i] : "") + "');" + $id + ".blur();" + $id + ".fadeOut(300).fadeIn(500);";
    }
    return '<button class="btn btn-default btn-xs" onclick="' + click + '" data-dismiss="alert">Use This</button>';
}

function serialButton(serialPort, data) {
    var click = "";
    for (var i = 0; i < serialPort.length; i++) {
        click += "$('#" + serialPort[i] + "').val('" + data[i] + "');$('#" + serialPort[i] + "').fadeOut(300).fadeIn(500);";
    }
    return '<button class="btn btn-default btn-xs" onclick="' + click + '" data-dismiss="alert">Use This</button>';
}

function formatHexInput(inputId) {
    var $input = $('#' + inputId);
    var val = $input.val();

    if (val.length > 0 && val.substring(0, 2) != '0x') {
        val = '0x' + val;
    }

    $input.val(val.toLowerCase());
}

/** Attempts to parse scale reading from USB raw output */
function readScaleData(data) {
    // Filter erroneous data
    if (data.length < 4 || data.slice(2, 8).join('') == "000000000000") {
        return null;
    }

    // Get status
    var status = parseInt(data[1], 16);
    switch (status) {
        case 1: // fault
        case 5: // underweight
        case 6: // overweight
        case 7: // calibrate
        case 8: // re-zero
            status = 'Error';
            break;
        case 3: // busy
            status = 'Busy';
            break;
        case 2: // stable at zero
        case 4: // stable non-zero
        default:
            status = 'Stable';
    }

    // Get precision
    var precision = parseInt(data[3], 16);
    precision = precision ^ -256; //unsigned to signed

    // xor on 0 causes issues
    if (precision == -256) { precision = 0; }

    // Get units
    var units = parseInt(data[2], 16);
    switch (units) {
        case 2:
            units = 'g';
            break;
        case 3:
            units = 'kg';
            break;
        case 11:
            units = 'oz';
            break;
        case 12:
        default:
            units = 'lbs';
    }

    // Get weight
    data.splice(0, 4);
    data.reverse();
    var weight = parseInt(data.join(''), 16);

    weight *= Math.pow(10, precision);
    weight = weight.toFixed(Math.abs(precision));

    return weight + units + ' - ' + status;
}


/// QZ Config ///
var cfg = null;
function getUpdatedConfig(cleanConditions) {
    if (cfg == null) {
        cfg = qz.configs.create(null);
    }

    updateConfig(cleanConditions || {});
    return cfg
}

function updateConfig(cleanConditions) {
    var pxlSize = null;
    if (isChecked($("#pxlSizeActive"), cleanConditions['pxlSizeActive']) && (($("#pxlSizeWidth").val() !== '') || ($("#pxlSizeHeight").val() !== ''))) {
        pxlSize = {
            width: $("#pxlSizeWidth").val(),
            height: $("#pxlSizeHeight").val()
        };
    }

    var pxlBounds = null;
    if (isChecked($("#pxlBoundsActive"), cleanConditions['pxlBoundsActive'])) {
        pxlBounds = {
            x: $("#pxlBoundX").val(),
            y: $("#pxlBoundY").val(),
            width: $("#pxlBoundWidth").val(),
            height: $("#pxlBoundHeight").val()
        };
    }

    var pxlDensity = includedValue($("#pxlDensity"));
    if (isChecked($("#pxlDensityAsymm"), cleanConditions['pxlDensityAsymm'])) {
        pxlDensity = {
            cross: $("#pxlCrossDensity").val(),
            feed: $("#pxlFeedDensity").val()
        };
    }

    var pxlMargins = includedValue($("#pxlMargins"));
    if (isChecked($("#pxlMarginsActive"), cleanConditions['pxlMarginsActive'])) {
        pxlMargins = {
            top: $("#pxlMarginsTop").val(),
            right: $("#pxlMarginsRight").val(),
            bottom: $("#pxlMarginsBottom").val(),
            left: $("#pxlMarginsLeft").val()
        };
    }

    var copies = 1;
    var spoolSize = null;
    var jobName = null;
    if ($("#rawTab").hasClass("active")) {
        copies = includedValue($("#rawCopies"));
        spoolSize = includedValue($("#rawSpoolSize"));
        jobName = includedValue($("#rawJobName"));
    } else {
        copies = includedValue($("#pxlCopies"));
        spoolSize = includedValue($("#pxlSpoolSize"));
        jobName = includedValue($("#pxlJobName"));
    }

    cfg.reconfigure({
        altPrinting: includedValue($("#rawAltPrinting"), isChecked($("#rawAltPrinting"), cleanConditions['rawAltPrinting'])),
        encoding: includedValue($("#rawEncoding")),
        spool: { size: spoolSize, end: includedValue($("#rawSpoolEnd")) },

        bounds: pxlBounds,
        colorType: includedValue($("#pxlColorType")),
        copies: copies,
        density: pxlDensity,
        duplex: includedValue($("#pxlDuplex")),
        interpolation: includedValue($("#pxlInterpolation")),
        jobName: jobName,
        margins: pxlMargins,
        orientation: includedValue($("#pxlOrientation")),
        paperThickness: includedValue($("#pxlPaperThickness")),
        printerTray: includedValue($("#pxlPrinterTray")),
        rasterize: includedValue($("#pxlRasterize"), isChecked($("#pxlRasterize"), cleanConditions['pxlRasterize'])),
        rotation: includedValue($("#pxlRotation")),
        scaleContent: includedValue($("#pxlScale"), isChecked($("#pxlScale"), cleanConditions['pxlScale'])),
        size: pxlSize,
        units: includedValue($("input[name='pxlUnits']:checked"))
    });
}

function getUpdatedOptions(onlyPixel) {
    if (onlyPixel) {
        return {
            pageWidth: $("#pPxlWidth").val(),
            pageHeight: $("#pPxlHeight").val()
        };
    } else {
        return {
            language: $("input[name='pLanguage']:checked").val(),
            x: $("#pX").val(),
            y: $("#pY").val(),
            dotDensity: $("#pDotDensity").val(),
            xmlTag: $("#pXml").val(),
            pageWidth: $("#pRawWidth").val(),
            pageHeight: $("#pRawHeight").val()
        };
    }
}

function getSerialOptions() {
    var options = {
        baudRate: $("#serialBaud").val(),
        dataBits: $("#serialData").val(),
        stopBits: $("#serialStop").val(),
        parity: $("#serialParity").val(),
        flowControl: $("#serialFlow").val(),
        encoding: $("#serialEncoding").val(),
        rx: {
            start: $("#serialStart").val(),
            end: $("#serialEnd").val() || null,
            width: $("#serialWidth").val() || null,
            untilNewline: $("#serialNewline").prop('checked'),
            lengthBytes: null,
            crcBytes: null,
            includeHeader: $("#serialHeader").prop('checked'),
            encoding: $("#serialRespEncoding").val()
        }
    };
    if (isChecked($("#serialLengthActive"))) {
        options.rx.lengthBytes = {
            index: $("#serialLenIndex").val(),
            length: $("#serialLenLength").val(),
            endian: $("input[name='serialLenEndian']:checked").val()
        };
    }
    if (isChecked($("#serialCrcActive"))) {
        options.rx.crcBytes = {
            index: $("#serialCrcIndex").val(),
            length: $("#serialCrcLength").val()
        };
    }

    return options;
}

function setPrintFile() {
    setPrinter({ file: $("#askFile").val() });
    $("#askFileModal").modal('hide');
}

function setPrintHost() {
    setPrinter({ host: $("#askHost").val(), port: $("#askPort").val() });
    $("#askHostModal").modal('hide');
}

function setPrinter(printer) {
    var cf = getUpdatedConfig();
    cf.setPrinter(printer);

    if (printer && typeof printer === 'object' && printer.name == undefined) {
        var shown;
        if (printer.file != undefined) {
            shown = "<em>FILE:</em> " + printer.file;
        }
        if (printer.host != undefined) {
            shown = "<em>HOST:</em> " + printer.host + ":" + printer.port;
        }

        $("#configPrinter").html(shown);
        $("#setPrinter").val(shown);
    } else {
        if (printer && printer.name != undefined) {
            printer = printer.name;
        }

        if (printer == undefined) {
            printer = null;
        }
        $("#configPrinter").html(printer);
        $("#setPrinter").val(printer);
    }
}