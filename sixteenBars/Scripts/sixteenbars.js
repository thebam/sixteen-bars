var toggleNewArtist = function () {
    if ($("#ArtistId").val() === "-1") {
        $("#newArtistName").show();
    } else {
        $("#newArtistName").hide();
    }
};

var toggleNewTrack = function () {
    if ($("#TrackId").val() === "-1") {
        $("#newTrackName").show();
    } else {
        $("#newTrackName").hide();
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

    var minimumDate = new Date(1978, 1 - 1, 1);

    var today = new Date();

    var maximumDate = new Date(today.getFullYear(),today.getMonth(), today.getDate());


    $("#TrackReleaseDate").datepicker({ minDate: minimumDate, maxDate: maximumDate });
    $("#AlbumReleaseDate").datepicker({ minDate: minimumDate, maxDate: maximumDate });
    if(blnEditMode==false){
        $('#TrackReleaseDate').datepicker('setDate', today);
        $('#AlbumReleaseDate').datepicker('setDate', today);
    }

    $("#ArtistId").change(function () {
        toggleNewArtist();
    });

    $("#ArtistName").blur(function () {
        $("#AlbumArtistId").append($("<option></option>").val("-2").html($("#ArtistName").val()));
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


