$("body").on("click", ".word", function (e) {
    var txt = $(e.target).text();
   
    $.getJSON("/api/SearchAPI/Search?searchTerm=" + txt + "&searchType=quote", function (data) {
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

});

