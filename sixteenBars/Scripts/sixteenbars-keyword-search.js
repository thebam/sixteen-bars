$("body").on("click", ".word", function (e) {
    $("#init-loading").show();
    $("#btn-more-quotes").hide();
    var txt = "";
    var attr = $(e.target).attr("class");
    if (attr != undefined) {
        txt = attr.replace(" word", "");
    }
    getQuoteByKeyword(txt);
});





function getQuoteByKeyword(keyword) {
    $.getJSON(siteURL + "/api/SearchAPI/Search?searchTerm=" + keyword + "&searchType=quote&wordLink=true&filter="+getCookie(), function (data) {

        $("#init-loading").hide();
        $("#btn-more-quotes").show();
        var cnt = 1;
        $(".quote-blocks .col-md-4").html("");

        $.each(data.Data, function (key, val) {

            var artistName = "";
            var artistLink = "";
            $.each(this.AdditionalURLS, function () {
                
                if (this.Type == "artist") {
                    artistName = this.Text;
                    artistLink = this.Link;
                }
            });
            var block = "<div class='quote-block'><div class='artist-info'><a href='" + siteURL +"/"+ artistLink + "'>"+artistName+"</a></div><div class='quote-info'><span class='quote'>" + this.Text + "</span><br><a class='btn btn-standard' href='" + siteURL + "/" + this.URL + "'>quote details</a></div><div class='clearfix'></div></div>";

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