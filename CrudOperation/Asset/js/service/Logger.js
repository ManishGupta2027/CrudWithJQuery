(function () {'use strict';

/* Service: Logger
 * Enables collecting, retrieving and displaying log messages
 * for debugging and/or bug-reporting purposes.
 * (See at the end of the document for detailed documentation and usage examles.) */
    window.app.factory('Logger', function ($log) {
    /* Variables */
    var LOG_LEVEL = {
        DEBUG: 'debug',
        ERROR: 'error',
        INFO:  'info',
        LOG:   'log',
        WARN:  'warn'
    };
    var DEFAULT_LOG_LEVEL = LOG_LEVEL.LOG;

    var messages = [];

    /* Functions - Private */
    function addMessage(logLevel, content, issuer) {
        messages.push({
            level:     sanitizeLogLevel(logLevel),
            timestamp: (new Date()).toUTCString(),
            issuer:    issuer || undefined,
            content:   content
        });
    }

    function getUniqueSanitizedIssuers(issuerOrIssuers) {
        var issuersArr = angular.isArray(issuerOrIssuers) ?
                issuerOrIssuers : (issuerOrIssuers ?
                        [issuerOrIssuers] :
                        []);
        return issuersArr.filter(function (issuer, idx, thisArray) {
            return thisArray.indexOf(issuer) === idx;
        });
    }

    function getUniqueSanitizedLevels(levelOrLevels) {
        return sanitizeLogLevelArray(angular.isArray(levelOrLevels) ?
                levelOrLevels : (levelOrLevels ?
                        [levelOrLevels] :
                        []));
    }

    function sanitizeLogLevel(level) {
        var sanitized = DEFAULT_LOG_LEVEL;
        Object.keys(LOG_LEVEL).some(function (key) {
            if (level === LOG_LEVEL[key]) {
                sanitized = level;
                return true;
            }
        });
        return sanitized;
    }

    function sanitizeLogLevelArray(levels) {
        return (levels || []).map(function (level1) {
            return sanitizeLogLevel(level1);
        }).filter(function (level2, idx, thisArray) {
            return thisArray.indexOf(level2) === idx;
        });
    }

    /* Functions - Public */
    function countAll(levelOrLevels, issuerOrIssuers) {
        var uniqueSanitizedLevels  = getUniqueSanitizedLevels(levelOrLevels);
        var uniqueSanitizedIssuers = getUniqueSanitizedIssuers(issuerOrIssuers);

        var count = 0;
        if ((uniqueSanitizedLevels.length === 0) && (uniqueSanitizedIssuers.length === 0)) {
            count = messages.length;
        } else {
            var filterLevelFunc = (uniqueSanitizedLevels.length === 0) ?
                    function (msg) {
                        return true;
                    } :
                    function (msg) {
                        return uniqueSanitizedLevels.indexOf(msg.level) !== -1;
                    };
            var filterIssuerFunc = (uniqueSanitizedIssuers.length === 0) ?
                    function (msg) {
                        return true;
                    } :
                    function (msg) {
                        return msg.issuer && (uniqueSanitizedIssuers.indexOf(msg.issuer) !== -1);
                    };
            messages.forEach(function (msg) {
                if (filterLevelFunc(msg) && filterIssuerFunc(msg)) {
                    count++;
                }
            });
        }

        return count;
    }

    function displayMessages(messages) {
        if (!messages || !messages.length) { messages = []; }

        var separatorHard = '==================================================';
        var separatorSoft = '--------------------------------------------------';

        $log.log(separatorHard);
        $log.log('  ~Logger Service~     (number of messages: ' + messages.length + ')');
        $log.log(separatorSoft);

        messages.forEach(function (msg) {
            var arg1 = msg.level.toUpperCase() + '\t(' + msg.timestamp + '):' +
                       (msg.issuer ? ' `' + msg.issuer + '`:' : '');
            var args = [arg1].concat(angular.isArray(msg.content) ? msg.content : [msg.content]);
            $log.log.apply($log, args);
        });
        $log.log(separatorHard);
    }

    function getLast(numMessages, levelOrLevels, issuerOrIssuers, reverse) {
        numMessages = (numMessages || numMessages === 0) ? numMessages : -1;
        reverse     = !!reverse;
        var uniqueSanitizedLevels  = getUniqueSanitizedLevels(levelOrLevels);
        var uniqueSanitizedIssuers = getUniqueSanitizedIssuers(issuerOrIssuers);

        var retMessages = [];
        var addFunc = reverse ? retMessages.push.bind(retMessages) :
                                retMessages.unshift.bind(retMessages);
        var filterLevelFunc = (uniqueSanitizedLevels.length === 0) ?
                function (msg) {
                    return true;
                } :
                function (msg) {
                    return uniqueSanitizedLevels.indexOf(msg.level) !== -1;
                };
        var filterIssuerFunc = (uniqueSanitizedIssuers.length === 0) ?
                function (msg) {
                    return true;
                } :
                function (msg) {
                    return msg.issuer && (uniqueSanitizedIssuers.indexOf(msg.issuer) !== -1);
                };
        var filterFunc = function (msg) {
            return filterLevelFunc(msg) && filterIssuerFunc(msg);
        };

        for (var i = messages.length - 1; i >= 0; i--) {
            var msg = messages[i];
            if (filterFunc(msg)) {
                addFunc(msg);
                numMessages--;
                if (numMessages === 0) { break; }
            }
        }

        return retMessages;
    }

    /* Service object */
    var serviceObj = {
        LOG_LEVEL: LOG_LEVEL,   // Available only in original `serviceObj`

        debug: addMessage.bind(null, LOG_LEVEL.DEBUG),
        error: addMessage.bind(null, LOG_LEVEL.ERROR),
        info:  addMessage.bind(null, LOG_LEVEL.INFO),
        log:   addMessage.bind(null, LOG_LEVEL.LOG),
        warn:  addMessage.bind(null, LOG_LEVEL.WARN),

        countAll: countAll,   // Available only in original `serviceObj`
        getAll:   getLast.bind(null, -1),
        getLast:  getLast,

        displayMessages: displayMessages   // Available only in original `serviceObj`
    };
    serviceObj.forIssuer = function (issuer) {
        if (!issuer) {
            serviceObj.warn('Requested logger for empty issuer !', 'Logger');
            return serviceObj;
        }

        var loggerForIssuer = {};
        ['debug', 'error', 'info', 'log', 'warn'].forEach(function (funcName) {
            loggerForIssuer[funcName] = function (content) {
                serviceObj[funcName](content, issuer);
            };
        });
        loggerForIssuer.getLast = function (numMessages, levelOrLevels, reverse) {
            return serviceObj.getLast(numMessages, levelOrLevels, issuer, reverse);
        };
        loggerForIssuer.getAll = loggerForIssuer.getLast.bind(null, -1);

        return loggerForIssuer;
    };

    return serviceObj;
});

}());

//================================================================================================//
//  DOCUMENTATION  AND  EXAMPLES                                                                  //
//================================================================================================//

/*
Notes:
- Function arguments enclosed in square brackets (`[]`) are otpional.
- "Truthy"/"Falsy" refer to values that when interpreted as booleans by JavaScript evaluate to
  true/false.
*/

/**************/
/*  Entities  */
/**************/

/**
 * Entity: Message
 * ---------------
 * An object representing a log entry.
 *
 * Properties
 * ----------
 * @prop level : string (one of Logger.LOG_LEVEL)
 *         Indicates the severity level of the log entry.
 * @prop timestamp : string (created by `Date.toUTCString()`)
 *         The date/time of logging this entry (in UTC).
 * @prop issuer : string | undefined
 *         An arbitrary string specifying the issuer (e.g. service, controller etc) that generated
 *         this log entry. Can be used to "group" log entries together for easier accessing/analyzing.
 *         This property is optional and can be `undefined` (although this is NOT recommended).
 * @prop content : *
 *         The actual content of the log entry. It can be of any type (string, number, boolean, array,
 *         object, function etc). Strings are best for portability, but when it is useful to log
 *         more complex objects, wrapping in an array is the best option, e.g. in order to include a
 *         descriptive message along with an object.
 */
// Message

//----------------------------------------------------------------------------//

/**
 * Entity: Logger
 * --------------
 * The service object storing the log entries and providing utility methods for easy logging and
 * retrieving/querying/displaying logged messages.
 * NOTE: In order to access it outside of the context of an Angular app (e.g. from the console), you
 *       need access to an element that is part of the app (i.e. the $rootElement or a descendant).
 *       Assuming the `<body>` is the root-element:
 *       var Logger = angular.element(document.body).injector().get('Logger');
 *
 * Properties
 * ----------
 * @prop LOG_LEVEL : object<string,string>
 *         This is an enumeration of the supported severity levels for Messages.
 *         Currently, the following levels are supported:
 *             DEBUG, LOG, INFO, WARN, ERROR
 *
 * Methods
 * -------
 * @func debug / log / info / warn / error : function (content, [issuer])
 *         Methods used for logging messages. Each method's name denotes the severity level of
 *         messages logged using that method.
 * @func countAll : function ([levelOrLevels], [issuerOrIssuers])
 *         Returns the total number of log entries, optionally filtered by severity level and/or
 *         issuer.
 * @func getAll : function ([levelOrLevels], [issuerOrIssuers], [reverse])
 *         Returns an array of all log entries, optionally filtered by severity level and/or issuer.
 *         The log entries are returned in chronological order, unless the 3rd argument is specified
 *         and truthy.
 * @func getLast : function ([numMessages], [levelOrLevels], [issuerOrIssuers], [reverse])
 *         Returns an array of the last `numMessages` log entries, optionally filtered by severity
 *         level and/or issuer. The log entries are returned in chronological order, unless the 4th
 *         argument is specified and truthy.
 *         A negative or non-numerical value for `numMessages` makes this method behave the same as
 *         `Logger.getAll()`.
 * @func displayMessages : function (messages)
 *         Utility method for displaying a list of `Message` objects in the console in a human
 *         readable format. More specifically, each log entry is formatted as follows:
 *         <severity_level> (<timestamp>): `<issuer>`: <content>
 * @func forIssuer : function (issuer)
 *         Utility method for creating a `LoggerForIssuer` object, which can be used for easily logging
 *         and retrieving messages for a specific issuer.
 *
 * (See below for more details regarding the methods (e.g. argument types etc).)
 */
// Logger

//----------------------------------------------------------------------------//

/**
 * Entiry: LoggerForIssuer
 * -----------------------
 * Contains a subset of `Logger`'s methods bound to a specific issuer.
 * Enables easy logging and message retrieval for the specified issuer (i.e. without having to pass
 * the issuer as argument every time).
 * (`LoggerForIssuer` objects are constructed using `Logger.forIssuer()`.)
 *
 * Methods
 * -------
 * @func debug / log / info / warn / error : function (content)
 *         Methods used for logging messages associated with this instance's issuer. Each method's
 *         name denotes the severity level of messages logged using that method.
 * @func getAll : function ([levelOrLevels], [reverse])
 *         Returns an array of all log entries associated with this instance's issuer, optionally
 *         filtered by severity level.
 *         The log entries are returned in chronological order, unless the 2nd argument is specified
 *         and truthy.
 * @func getLast : function ([numMessages], [levelOrLevels], [reverse])
 *         Returns an array of the last `numMessages` log entries associated with this instance's
 *         issuer, optionally filtered by severity level. The log entries are returned in
 *         chronological order, unless the 3rd argument is specified and truthy.
 *         A negative or non-numerical value for `numMessages` makes this method behave the same as
 *         `LoggerForIssuer.getAll()`.
 *
 * (See below for more details regarding the methods (e.g. argument types etc).)
 */
// LoggerForIssuer

//================================================================================================//

/*************/
/*  Methods  */
/*************/

/**
 * Method: Logger.debug / log / info / warn / error
 * ------------------------------------------------
 * Methods used for logging messages. Each methods's name denotes the severity level of messages
 * logged using that method.
 *
 * @param content : *
 *         The actual content of the log entry. It can be of any type (string, number, boolean, array,
 *         object, function etc). Strings are best for portability, but when it is useful to log more
 *         complex objects, wrapping in an array is the best option, e.g. in order to include a
 *         descriptive message along with an object.
 * @param issuer : string|undefined [OPTIONAL]
 *         An arbitrary string specifying the issuer (e.g. service, controller etc) that generated
 *         this log entry. Can be used to "group" log entries together for easier accessing/analyzing.
 *         This property is optional and can be `undefined` (although this is NOT recommended).
 *
 * @return : void
 */
// Logger.debug(content, [issuer])
// Logger.log  (content, [issuer])
// Logger.info (content, [issuer])
// Logger.warn (content, [issuer])
// Logger.error(content, [issuer])

//----------------------------------------------------------------------------//

/**
 * Method: Logger.countAll
 * -----------------------
 * Returns the total number of log entries, optionally filtered by severity level and/or issuer.
 * Both arguments can be either falsy (to skip filtering by the corresponding attribute), a single
 * string value or an array of string values.
 *
 * @param levelOrLevels : string | Array<string> [OPTIONAL]
 *         A string (or array of strings) representing severity levels (as defined in
 *         `Logger.LOG_LEVEL`) to filter the log entries by.
 *         If it is not specified or falsy, the log entries are not filtered by severity level.
 * @param issuerOrIssuers : string | Array<string> [OPTIONAL]
 *         A string (or array of strings) representing issuers (arbitrary strings) to filter the log
 *         entries by.
 *         If it is not specified or falsy, the log entries are not filtered by issuer.
 *
 * @return : integer
 *         The total number of log entries that meet the filter criteria (if any).
 */
// Logger.countAll([levelOrLevels], [issuerOrIssuers])

//----------------------------------------------------------------------------//

/**
 * Method: Logger.getAll
 * ---------------------
 * Returns an array of all log entries, optionally filtered by severity level and/or issuer.
 * The log entries are returned in chronological order, unless `reverse` is specified and truthy.
 * The first two arguments can be either falsy (to skip filtering by the corresponding attribute),
 * a single string value or an array of string values.
 *
 * @param levelOrLevels : string | Array<string> [OPTIONAL]
 *         A string (or array of strings) representing severity levels (as defined in
 *         `Logger.LOG_LEVEL`) to filter the log entries by.
 *         If it is not specified or falsy, the log entries are not filtered by severity level.
 * @param issuerOrIssuers : string | Array<string> [OPTIONAL]
 *         A string (or array of strings) representing issuers (arbitrary strings) to filter the log
 *         entries by.
 *         If it is not specified or falsy, the log entries are not filtered by issuer.
 * @param reverse : boolean [OPTIONAL]
 *         Specifies whether the log entries should be returned in reverse chronological order or not.
 *         By default, the log entries are returned in chronological order.
 *
 * @return : Array<Message>
 *         An array of log entries that meet the filter criteria (if any) in the specified order.
 */
// Logger.getAll([levelOrLevels], [issuerOrIssuers], [reverse])

//----------------------------------------------------------------------------//

/**
 * Method: Logger.getLast
 * ----------------------
 * Returns an array of the last `numMessages` log entries, optionally filtered by severity level
 * and/or issuer. The log entries are returned in chronological order, unless `reverse` is specified
 * and truthy.
 * A negative or non-numerical value for `numMessages` makes this function behave the same as
 * `Logger.getAll()`.
 * The second and third arguments can be either falsy (to skip filtering by the corresponding
 * attribute), a single string value or an array of string values.
 *
 * @param numMessages : integer [OPTIONAL]
 *         The maximum number of log entries to return. If the total number of log entries is not
 *         less than `numMessages` (in which case all log entries are returned), only the last
 *         `numMessages` log entries (that match the specified criteria) are returned.
 * @param levelOrLevels : string | Array<string> [OPTIONAL]
 *         A string (or array of strings) representing severity levels (as defined in
 *         `Logger.LOG_LEVEL`) to filter the log entries by.
 *         If it is not specified or falsy, the log entries are not filtered by severity level.
 * @param issuerOrIssuers : string | Array<string> [OPTIONAL]
 *         A string (or array of strings) representing issuers (arbitrary strings) to filter the log
 *         entries by.
 *         If it is not specified or falsy, the log entries are not filtered by issuer.
 * @param reverse : boolean [OPTIONAL]
 *         Specifies whether the log entries should be returned in reverse chronological order or not.
 *         By default, the log entries are returned in chronological order.
 *
 * @return : Array<Message>
 *         An array of (max. `numMessages`) log entries that meet the filter criteria (if any) in
 *         the specified order.
 */
// Logger.getLast([numMessages], [levelOrLevels], [issuerOrIssuers], [reverse])

//----------------------------------------------------------------------------//

/**
 * Method: Logger.displayMessages
 * ------------------------------
 * Helps visualize the logged messages by displaying a list of `Message` objects in the console in a
 * human readable format. More specifically, each log entry is formatted as follows:
 *     <severity_level> (<timestamp>): `<issuer>`: <content>
 *
 * @param messages : Array<Message>
 *         An array of `Message` objects to display.
 *
 * @return : void
 */
// Logger.displayMessages(messages)

//----------------------------------------------------------------------------//

/**
 * Method: Logger.forIssuer
 * ------------------------
 * Creates and returns a `LoggerForIssuer` object, which contains a subset of `Logger`'s methods
 * bound to a specific issuer. It can be used for easily logging and retrieving messages for a
 * specific issuer (i.e. without having to pass the issuer as argument every time).
 *
 * @param issuer : string
 *         A string specifying the issuer. (Can be an arbitrary string.)
 *
 * @return : LoggerForIssuer
 *         The created `LoggerForIssuer` object for easy logging and message retrieval for the
 *         specified issuer.
 */
// Logger.forIssuer(issuer)

//----------------------------------------------------------------------------//

/**
 * Method: LoggerForIssuer.debug / log / info / warn / error
 * ---------------------------------------------------------
 * Methods used for logging messages associated with this instance's issuer. Each methods's name
 * denotes the severity level of messages logged using that method.
 *
 * @param content : *
 *         The actual content of the log entry. It can be of any type (string, number, boolean, array,
 *         object, function etc). Strings are best for portability, but when it is useful to log more
 *         complex objects, wrapping in an array is the best option, e.g. in order to include a
 *         descriptive message along with an object.
 *
 * @return : void
 */
// LoggerForIssuer.debug(content)
// LoggerForIssuer.log  (content)
// LoggerForIssuer.info (content)
// LoggerForIssuer.warn (content)
// LoggerForIssuer.error(content)

//----------------------------------------------------------------------------//

/**
 * Method: LoggerForIssuer.getAll
 * ------------------------------
 * Returns an array of all log entries associated with this instance's issuer, optionally filtered
 * by severity level. The log entries are returned in chronological order, unless `reverse` is
 * specified and truthy.
 * The first argument can be either falsy (to skip filtering by severity level), a single string value
 * or an array of string values.
 *
 * @param levelOrLevels : string | Array<string> [OPTIONAL]
 *         A string (or array of strings) representing severity levels (as defined in
 *         `Logger.LOG_LEVEL`) to filter the log entries by.
 *         If it is not specified or falsy, the log entries are not filtered by severity level.
 * @param reverse : boolean [OPTIONAL]
 *         Specifies whether the log entries should be returned in reverse chronological order or not.
 *         By default, the log entries are returned in chronological order.
 *
 * @return : Array<Message>
 *         An array of log entries (associated with this instance's issuer) that meet the filter
 *         criteria (if any) in the specified order.
 */
// LoggerForIssuer.getAll([levelOrLevels], [reverse])

//----------------------------------------------------------------------------//

/**
 * Method: LoggerForIssuer.getLast
 *--------------------------------
 * Returns an array of the last `numMessages` log entries associated with this instance's issuer,
 * optionally filtered by severity level. The log entries are returned in chronological order, unless
 * `reverse` is specified and truthy.
 * A negative or non-numerical value for `numMessages` makes this method behave the same as
 * `LoggerForIssuer.getAll()`.
 * The second argument can be either falsy (to skip filtering by severity level), a single string
 * value or an array of string values.
 *
 * @param numMessages : integer [OPTIONAL]
 *         The maximum number of log entries to return. If the total number of log entries associated
 *         with this instance's issuer is not less than `numMessages`, only the last `numMessages`
 *         log entries (that match the specified criteria) are returned.
 * @param levelOrLevels : string | Array<string> [OPTIONAL]
 *         A string (or array of strings) representing severity levels (as defined in
 *         `Logger.LOG_LEVEL`) to filter the log entries by.
 *         If it is not specified or falsy, the log entries are not filtered by severity level.
 * @param reverse : boolean [OPTIONAL]
 *         Specifies whether the log entries should be returned in reverse chronological order or not.
 *         By default, the log entries are returned in chronological order.
 *
 * @return : Array<Message>
 *         An array of (max. `numMessages`) log entries (associated with this instance's issuer) that
 *         meet the filter criteria (if any) in the specified order.
 */
// LoggerForIssuer.getLast([numMessages], [levelOrLevels], [reverse])

//================================================================================================//

/*********************/
/*  Usage  Examples  */
/*********************//*

Below are some common usage examples of the `Logger` service.

When using the service in the context of an AngularJS app, you can get a reference to it by means of
dependency injection, e.g.:

    .service('SomeService', function (Logger) {
        // Logger.info(...);
    });

When using the service outside of the context of an AngularJS app (e.g. in the DevTools' console),
you can get a reference to it by first accessing the app's injector and calling its `get()` method.
You can access the app's injector using any element that is inside the context of the app (i.e. any
element that is the root-element or a descendant).
Assuming `<body>` is inside the context of the app, you can get a reference to the `Logger` service
like this:

    var Logger = angular.element(document.body).injector().get('Logger');
    // Logger.warn(...);

//----------------------------------------------------------------------------//

 1. Log a message for a specific issuer

    Logger.log('Hello, world !', 'MainController');

//----------------------------------------------------------------------------//

 2. Log a message for a specific issuer (feat. LoggerForIssuer)

    var logger = Logger.forIssuer('MainController');
    logger.debug('I have an implicit issuer ! Yay !!!');

//----------------------------------------------------------------------------//

 3. Count messages by criteria

    // Count the number of WARN or ERROR messages issued by `MyService`
    Logger.countAll([Logger.LOG_LEVEL.WARN, Logger.LOG_LEVEL.ERROR], 'MyService');
    // ->  99

//----------------------------------------------------------------------------//

 4. Retrieve all messages

    Logger.getAll();
    // ->  [Message1, Message2, Message3, ...]

//----------------------------------------------------------------------------//

 5. Retrieve messages filtered by severity level

    // Retrieve all DEBUG messages
    Logger.getAll(Logger.LOG_LEVEL.DEBUG);
    // ->  [DebugMessage1, DebugMessage2, DebugMessage3, ...]

//----------------------------------------------------------------------------//

 6. Retrieve messages filtered by issuer

    // Retrieve all messages issued by `UserService`
    Logger.getAll(null, 'UserService');
    // ->  [MessageByUserService1, MessageByUserService2, MessageByUserService3, ...]

//----------------------------------------------------------------------------//

 7. Retrieve messages filtered by issuer (feat. LoggerForIssuer)

    // Retrieve all messages issued by `UserService`
    var logger = Logger.forIssuer('UserService');
    logger.getAll();
    // ->  [MessageByUserService1, MessageByUserService2, MessageByUserService3, ...]

//----------------------------------------------------------------------------//

 8. Retrieve the last 10 messages filtered by severity level and issuer in reverse chronological order

    // Retrieve the last 10 WARN or ERROR messages issued by `YourService` (from newest to oldest)
    Logger.getLast(10, [Logger.LOG_LEVEL.WARN, Logger.LOG_LEVEL.ERROR], 'YourService', true);
    // ->  [Message10, Message9, Message8, ...]

//----------------------------------------------------------------------------//

 9. Display messages

    // Retrieve and display the last 5 ERROR messages issued by `HttpService`
    Logger.displayMessage(Logger.getLast(5, Logger.LOG_LEVEL.ERROR, 'HttpService'));
    // ->  Nicely formatted output :)

//----------------------------------------------------------------------------//

10. Display messages (feat. LoggerForIssuer)

    // Retrieve and display the last 5 ERROR messages issued by `HttpService`
    var logger = Logger.forIssuer('HttpService');
    Logger.displayMessages(logger.getLast(5, Logger.LOG_LEVEL.ERROR));
    // ->  Nicely formatted output :)
    // Notice the use of `Logger.displayMessages` and `Logger.LOG_LEVEL`, since those objects are
    // only accessible on the `Logger` service (not on `LoggerForIssuer` instances).
*/
//================================================================================================//
