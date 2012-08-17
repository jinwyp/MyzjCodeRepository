var UserAgents = function () {
    var ua = navigator.userAgent.toLowerCase();
    if (ua.indexOf('windows') != -1) {
        return 'windows';
    } else if (ua.indexOf('ipad') != -1) {
        return 'ipad';
    }
    else if (ua.indexOf('ipod') != -1) {
        return 'ipod';
    }
    else if (ua.indexOf('iphone') != -1) {
        return 'iphone';
    }
    else if (ua.indexOf('mac') != -1) {
        return 'mac';
    }
    else if (ua.indexOf('android') != -1) {
        return 'android';
    }
    else if (ua.indexOf('linux') != -1) {
        return 'linux';
    }
    else if (ua.indexOf('nokia') != -1) {
        return 'nokia';
    }
    else if (ua.indexOf('blackberry') != -1) {
        return 'blackberry';
    }
    else if (ua.indexOf('freebsd') != -1) {
        return 'freebsd';
    }
    else if (ua.indexOf('openbsd') != -1) {
        return 'openbsd';
    }
    else if (ua.indexOf('netbsd') != -1) {
        return 'netbsd';
    }
    else if (ua.indexOf('opensolaris') != -1) {
        return 'opensolaris';
    }
    else if (ua.indexOf('sunos') != -1) {
        return 'sunos';
    }
    else if (ua.indexOf('os\/2') != -1) {
        return 'os2';
    }
    else if (ua.indexOf('beos') != -1) {
        return 'beos';
    }
    else if (ua.indexOf('win') != -1) {
        return 'windows';
    }
} ();
var baseAg = function () {
    this.objectName = UserAgents;
    //alert(this.objectName);
    if (this.objectName === "iphone") {
        window.location.href = "http://m.muyingzhijia.me/index.aspx?refer=mobile";
    } else if (this.objectName === "windows") {
        window.location.href = "http://www.baidu.com";
    }
} ();
