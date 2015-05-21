/* Create a cache object */
var cache = new LastFMCache();

/* Create a LastFM object */
var lastfm = new LastFM({
    apiKey: '3c370219a92b83bf8804de92f4ec26d4',
    apiSecret: 'c6feb385db08a59a0f0b848b941fb65a',
    cache: cache
});


$(document).ready(function () {
    $("#ArtistId").change(function () {
        getArtistInfo($("#ArtistId option:selected").text(), "artistInfo");
    });

    $("#ArtistName").blur(function () {
        getArtistInfo($("#ArtistName").val(), "artistInfo");
    });

    $("#AlbumArtistId").change(function () {
        getArtistInfo($("#AlbumArtistId option:selected").text(), "albumArtistInfo");
    });

    $("#AlbumArtistName").blur(function () {
        getArtistInfo($("#AlbumArtistName").val(), "albumArtistInfo");
    });



    //TODO - You will need to get album artist for prepopulated items in album dropdown.
    //$("#AlbumId").change(function () {
    //    var artistId = $("#AlbumArtistId option:selected").val();
    //    var artistName;

    //    if(artistId != ""){
    //        if (artistId != -1) {
    //            artistName = $("#AlbumArtistId option:selected").text();
    //        } else {
    //            artistName = $("#AlbumArtistName").val();
    //        }
    //    }

    //    getAlbumInfo(artistName,$("#AlbumId option:selected").text(), "albumImage");
    //});

    //$("#AlbumName").blur(function () {
    //    getAlbumInfo($("#AlbumName").val(), "albumImage");
    //});

});

function getArtistInfo(artistName, elementToUpdate) {
    $("#" + elementToUpdate).html("");
    /* Load some artist info. */
    lastfm.artist.getInfo({ artist: artistName }, {
        success: function (data) {
            //alert(data.artist.image[2]["#text"]);
            $("<img src='" + data.artist.image[2]["#text"] + "' />").appendTo("#" + elementToUpdate);
            $("<p>" + data.artist.bio.summary + "</p>").appendTo("#" + elementToUpdate);

            /* Use data. */
        }, error: function (code, message) {
            /* Show error message. */
        }
    });
}



function getAlbumInfo(artistName, albumName, elementToUpdate) {
    $("#" + elementToUpdate).html("");
    /* Load some artist info. */
    lastfm.album.getInfo({ artist: artistName, album: albumName }, {
        success: function (data) {
            //alert(data.artist.image[2]["#text"]);
            $("<img src='" + data.album.image[2]["#text"] + "' />").appendTo("#" + elementToUpdate)


            /* Use data. */
        }, error: function (code, message) {
            /* Show error message. */
        }
    });
}