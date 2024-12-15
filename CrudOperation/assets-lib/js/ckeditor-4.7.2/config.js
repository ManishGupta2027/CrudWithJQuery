CKEDITOR.editorConfig = function (config) {
    config.toolbarGroups = [
        //{ name: 'editing', groups: ['find', 'selection', 'spellchecker', 'editing'] },
        { name: 'document', groups: ['mode'] },
        { name: 'styles', groups: ['styles'] },
        //{ name: 'clipboard', groups: ['clipboard', 'undo'] },
        //{ name: 'forms', groups: ['forms'] },
        { name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
        { name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi', 'paragraph'] },
        { name: 'links', groups: ['links'] },
        { name: 'insert', groups: ['insert'] },
        //{ name: 'colors', groups: ['colors'] },
        { name: 'tools', groups: ['tools'] }
        //{ name: 'others', groups: ['others'] },
        //{ name: 'about', groups: ['about'] }
    ];
    //config.contentsCss = CSS_PATH;
    // Advanced Content Filter (ACF) Configuration
    config.allowedContent = {
        p: { attributes: 'style' },
        h1: true,
        h2: true,
        h3: true,
        pre: true,
        ul: true,
        ol: true,
        li: true,
        table: true,
        tr: true,
        th: true,
        td: true,
        span: { attributes: 'style' },
        strong: true,
        em: true,
        img: { attributes: 'src,alt,width,height' },
        a: { attributes: 'href,target' }
    };

    // Disallowed content to block XSS
    config.disallowedContent = 'script; *[on*]; iframe; object; embed; form; input; button; textarea';

    // Set the most common block elements
    config.format_tags = 'p;h1;h2;h3;pre';

    // Enable full-page mode
    config.fullPage = true;

    // Remove unwanted buttons
    config.removeButtons = 'Save,NewPage,Preview,Print,Templates,Cut,Copy,Paste,PasteText,PasteFromWord,Find,Replace,SelectAll,Scayt,Form,Checkbox,Radio,TextField,Textarea,Select,Button,ImageButton,HiddenField,Subscript,Superscript,RemoveFormat,CopyFormatting,Outdent,Indent,CreateDiv,BidiLtr,BidiRtl,Language,Anchor,Flash,HorizontalRule,Smiley,SpecialChar,Iframe,Font,FontSize,TextColor,ShowBlocks,About,Styles,Maximize,BGColor';

    // Add docprops plugin
    config.extraPlugins = 'docprops';

    // Remove unwanted plugins
    config.removePlugins = 'image,table,tabletools,horizontalrule';
};