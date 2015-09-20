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
    //siteURL = "http://www.invisiblefury.com/sixteenbars";
    siteURL = "http://rhyme4rhyme.azurewebsites.net";
}

$("#nav-items>li").removeClass("active");


if (window.location.href.toLowerCase().indexOf(siteURL + "/artist") >= 0) {
    $("#artist-nav-item").addClass("active");
}
if (window.location.href.toLowerCase().indexOf("/artist/") >= 0) {
    $("#artist-nav-item").addClass("active");
}
if (window.location.href.toLowerCase().indexOf("/artists/") >= 0) {
    $("#artist-nav-item").addClass("active");
}

if (window.location.href.toLowerCase().indexOf(siteURL + "/album") >= 0) {
    $("#album-nav-item").addClass("active");
}
if (window.location.href.toLowerCase().indexOf("/album/") >= 0) {
    $("#album-nav-item").addClass("active");
}
if (window.location.href.toLowerCase().indexOf("/albums/") >= 0) {
    $("#album-nav-item").addClass("active");
}


if (window.location.href.toLowerCase().indexOf(siteURL + "/track") >= 0) {
    $("#track-nav-item").addClass("active");
}
if (window.location.href.toLowerCase().indexOf("/track/") >= 0) {
    $("#track-nav-item").addClass("active");
}
if (window.location.href.toLowerCase().indexOf("/tracks/") >= 0) {
    $("#track-nav-item").addClass("active");
}

if (window.location.href.toLowerCase().indexOf(siteURL+"/quote") >= 0) {
    $("#quote-nav-item").addClass("active");
}
if (window.location.href.toLowerCase().indexOf("/quote/") >= 0) {
    $("#quote-nav-item").addClass("active");
}
if (window.location.href.toLowerCase().indexOf("/quotes/") >= 0) {
    $("#quote-nav-item").addClass("active");
}


$(function () {
    $('input[type=text]').attr('autocomplete', 'off');
});