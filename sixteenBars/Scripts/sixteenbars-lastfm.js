/* Create a cache object */
var cache = new LastFMCache();

/* Create a LastFM object */
var lastfm = new LastFM({
    apiKey: '3c370219a92b83bf8804de92f4ec26d4',
    apiSecret: 'c6feb385db08a59a0f0b848b941fb65a',
    cache: cache
});

var blnMakingLastFMAlbumRequest = false;
var blnMakingLastFMArtistRequest = false;
var blnMakingLastFMTrackRequest = false;


$(document).ready(function () {
    $("#ArtistId").change(function () {
        if (blnMakingLastFMArtistRequest == false) {
            var artistName = $("#ArtistId option:selected").text();
            if (artistName !== "" && artistName !== "Add New Artist") {
                blnMakingLastFMArtistRequest = true;
                getArtistInfo(artistName, "artistInfo","");
            }
        }
    });

    $("#ArtistName").blur(function () {
        if (blnMakingLastFMArtistRequest == false) {
            blnMakingLastFMArtistRequest = true;
            getArtistInfo($("#ArtistName").val(), "artistInfo", "ArtistName");
        }
    });

    $("#AlbumArtistId").change(function () {
        if (blnMakingLastFMArtistRequest == false) {
            var artistName = $("#AlbumArtistId option:selected").text();
            if (artistName !== "" && artistName !== "Add New Artist") {
                blnMakingLastFMArtistRequest = true;
                getArtistInfo($("#AlbumArtistId option:selected").text(), "albumArtistInfo","");
            }
        }
    });

    $("#AlbumArtistName").blur(function () {
        if (blnMakingLastFMArtistRequest == false) {
            blnMakingLastFMArtistRequest = true;
            getArtistInfo($("#AlbumArtistName").val(), "albumArtistInfo", "AlbumArtistName");
        }
    });

    $("#TrackId").change(function () {
        if (blnMakingLastFMTrackRequest == false) {
            var artistId = $("#AlbumArtistId option:selected").val();
            var artistName="";

            if (artistId != "") {
                if (artistId != -1) {
                    artistName = $("#AlbumArtistId option:selected").text();
                } else {
                    artistName = $("#AlbumArtistName").val();
                }

                if (artistName.length > 0) {
                    blnMakingLastFMTrackRequest = true;
                    getTrackInfo($("#TrackId option:selected").text(), artistName, "artistInfo");
                }
            }
        }
    });

    $("#TrackName").change(function () {
        if (blnMakingLastFMTrackRequest == false) {
            var artistId = $("#AlbumArtistId option:selected").val();
            var artistName = "";

            if (artistId != "") {
                if (artistId != -1) {
                    artistName = $("#AlbumArtistId option:selected").text();
                } else {
                    artistName = $("#AlbumArtistName").val();
                }

                if(artistName.length>0){
                    blnMakingLastFMTrackRequest = true;
                    getTrackInfo($("#TrackName").text(), artistName, "artistInfo");
                }
            }
        }
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

function getArtistInfo(artistName, elementToUpdate, nameElement) {
    $("#" + elementToUpdate).html("");
    lastfm.artist.getInfo({ artist: artistName }, {
        success: function (data) {
            $("<img src='" + data.artist.image[2]["#text"] + "' />").appendTo("#" + elementToUpdate);
            $("<p>" + data.artist.bio.summary + "</p>").appendTo("#" + elementToUpdate);

            if (nameElement.length > 0) {
                $("#"+nameElement).val(data.artist.name);
            }
            blnMakingLastFMArtistRequest = false;
        }, error: function (code, message) {
            blnMakingLastFMArtistRequest = false;
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

function getTrackInfo(trackName, artistName, elementToUpdate) {
    $("#" + elementToUpdate).html("");
    lastfm.track.getTrackInfo({ track: trackName,artist: artistName }, {
        success: function (data) {
            //alert(data.artist.image[2]["#text"]);
            //$("<img src='" + data.album.image[2]["#text"] + "' />").appendTo("#" + elementToUpdate)


            alert(data.track.album.title);
            alert(data.track.album.image[2]["#text"]);
            blnMakingLastFMTrackRequest = false;
            /* Use data. */
        }, error: function (code, message) {
            blnMakingLastFMTrackRequest = false;
            /* Show error message. */
        }
    });
}