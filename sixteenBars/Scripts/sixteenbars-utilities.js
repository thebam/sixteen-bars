var siteURL = "";
if (window.location.hostname.indexOf("localhost") >= 0) {
    siteURL = "http://" + window.location.hostname;

    var fullPath = window.location.href;
    fullPath = fullPath.replace(siteURL, "");
    var port = fullPath.split("/");
    if (port.length >= 1) {
        siteURL += port[0];
    }
} else {
    siteURL = "http://www.invisiblefury.com/sixteenbars";
}