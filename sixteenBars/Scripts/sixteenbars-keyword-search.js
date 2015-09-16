﻿$("body").on("click", ".word", function (e) {
    var txt = "";
    var attr = $(e.target).attr("class");
    if (attr != undefined) {
        txt = attr.replace(" word", "");
    }
    getQuoteByKeyword(txt);
});


$(document).ready(function () {
    var queryKeyword = getParameterByName('word');
    if (queryKeyword != undefined) {
        if (queryKeyword.length >1) {
            getQuoteByKeyword(queryKeyword);
        }
    }
});


function getQuoteByKeyword(keyword) {
    $.getJSON(siteURL + "/api/SearchAPI/Search?searchTerm=" + keyword + "&searchType=quote&filter="+true, function (data) {
        var cnt = 1;
        $(".quote-blocks .col-md-4").html("");

        $.each(data.Data, function (key, val) {
            var block = "<div class='quote-block'><div class='artist-info'><a href='/Artist/details/" + this.Artist.Id + "'>" + this.Artist.Name + "</a></div><div class='quote-info'><span class='quote'>" + this.Text + "</span><br><a class='btn btn-standard' href='/Quote/details/" + this.Id + "'>details</a></div><div class='clearfix'></div></div>";

            $(block).appendTo(".quote-blocks .col-md-4:nth-of-type(" + cnt + ")");
            cnt++;
            if (cnt == 4) {
                cnt = 1;
            }
        });

    });
}

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}