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
    siteURL = "http://www.rhyme4rhyme.com";
}

$("#nav-items>li").removeClass("active");

function getCookie() {
    var explicit = "clean";
    var name = "explicit=";
    var lang = document.cookie;
    var ca = lang.split(";");
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1);
        if (c.indexOf(name) == 0) {
            explicit = c.substring(name.length, c.length);
        }
    }
    if (explicit == "explicit") {
        return true;
    } else {
        return false;
    }
    
}
function setCookie(value) {
    document.cookie = "explicit="+value;
}


$(document).ready(function () {
    if (getCookie() == true) {
        $("#explicit").addClass("btn-on");
        $("#clean").addClass("btn-off");

        $("#explicit").removeClass("btn-off");
        $("#clean").removeClass("btn-on");
    } else {
        $("#explicit").addClass("btn-off");
        $("#clean").addClass("btn-on");

        $("#explicit").removeClass("btn-on");
        $("#clean").removeClass("btn-off");
    }

    $("#explicit").click(function () {
        setCookie("explicit");
        location.reload();
    });

    $("#clean").click(function () {
        setCookie("clean");
        location.reload();
    });

});


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