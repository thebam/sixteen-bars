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

$(document).ready(function () {
    toggleNewArtist();
    toggleNewTrack();
    $("#ArtistId").change(function () {
        toggleNewArtist();
    });
    $("#TrackId").change(function () {
        toggleNewTrack();
    });
});


