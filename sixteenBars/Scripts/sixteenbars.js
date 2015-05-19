var toggleNewArtist = function () {
    if ($("#ArtistId").val() === "-1") {
        $("#ArtistName").show();
    } else {
        $("#ArtistName").hide();
    }
};

var toggleNewTrack = function () {
    if ($("#TrackId").val() === "-1") {
        $("#TrackName").show();
    } else {
        $("#TrackName").hide();
    }
};

var toggleNewAlbum = function () {
    if ($("#AlbumId").val() === "-1") {
        $("#AlbumName").show();
        $("#albumInfo").show();
    } else {
        $("#AlbumName").hide();
        $("#albumInfo").hide();
    }
};

var toggleNewAlbumArtist = function () {
    if ($("#AlbumArtistId").val() === "-1") {
        $("#AlbumArtistName").show();
    } else {
        $("#AlbumArtistName").hide();
    }
};

$(document).ready(function () {
    toggleNewArtist();
    toggleNewTrack();
    toggleNewAlbum();
    toggleNewAlbumArtist();


    $("#TrackReleaseDate").datepicker();
    $("#AlbumReleaseDate").datepicker();
    today = new Date();
    $('#TrackReleaseDate').datepicker('setDate', today);
    $('#AlbumReleaseDate').datepicker('setDate', today);


    $("#ArtistId").change(function () {
        toggleNewArtist();
    });
    $("#TrackId").change(function () {
        toggleNewTrack();
    });
    $("#AlbumId").change(function () {
        toggleNewAlbum();
    });
    $("#AlbumArtistId").change(function () {
        toggleNewAlbumArtist();
    });
});


