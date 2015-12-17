
app.controller("AlbumController", function ($scope, $http, autoCompleteFactory, existFactory) {
    $scope.autoCompleteAlbum = function () {
        //chariotsolutions.com/blog/post/angularjs-corner-using-promises-q-handle-asynchronous-calls
        $(".suggestions").hide();
        if ($scope.albumTitle != undefined) {
            if ($scope.albumTitle.length > 2) {
                $(".album-suggestions").show();
                $(".album-suggestions .loading").show();
                var albumTitle, artistName;
                if ($scope.artistName != undefined) {
                    artistName = encodeURIComponent($scope.artistName);
                } else {
                    artistName = "";
                }

                albumTitle = encodeURIComponent($scope.albumTitle);
                var promise = autoCompleteFactory.albumTitle(albumTitle, artistName);

                promise.then(function (payload) {
                    $scope.albums = payload.data.Data;
                    if ($scope.albums.length > 0) {
                        $(".album-suggestions .empty").hide();
                    } else {
                        $(".album-suggestions .empty").show();
                    }
                    $(".album-suggestions .loading").hide();
                },
                function (errorPayload) {
                    $scope.albums = "";
                    $(".album-suggestions .loading").hide();
                    $(".album-suggestions .empty").show();
                });
            }
        }
    };

    $scope.selectSuggestedAlbum = function (albumName, releaseDate, artistName, titleFieldToPopulate, dateFieldToPopulate,artistFieldToPopulate) {
        if (titleFieldToPopulate === "albumTitle") {
            $scope.albumTitle = albumName;
            $scope.artistName = artistName;
            $('#' + dateFieldToPopulate).datepicker("setDate", new Date(releaseDate));
            $scope.existTrack();
        } else {
            $scope.albumTitle = albumName;
        }
        $scope.albums = "";
        $(".suggestions").hide();
    };

    $scope.existTrack = function () {
        //TO-DO : Turn this back on after add releasedate 


        //if ($scope.albumTitle.length > 2 && $scope.title.length > 2 && $scope.artistName.length) {
        //    var title, albumTitle, artistName;
        //    if ($scope.title != undefined) {
        //        title = encodeURIComponent($scope.title);
        //    }
        //    if ($scope.albumTitle != undefined) {
        //        albumTitle = encodeURIComponent($scope.albumTitle);
        //    }
        //    if ($scope.artistName != undefined) {
        //        artistName = encodeURIComponent($scope.artistName);
        //    }
            
        //    var promise = existFactory.track(title, albumTitle, artistName);
        //    promise.then(function (payload) {
        //        $scope.blnTrackExists = payload.data;
        //    },
        //    function (errorPayload) {
        //        $scope.blnTrackExists = true;
        //    });
        //}
    }


    $scope.autoCompleteArtist = function (type) {
        var saidByArtistName;
        var artistElement;
        if (type === "saidByArtistName") {
            saidByArtistName = encodeURIComponent($scope.saidByArtistName);
            artistElement = "speaker-suggestions";
        } else {
            saidByArtistName = encodeURIComponent($scope.artistName);
            artistElement = "artist-suggestions";
        }
        $(".suggestions").hide();

        if (saidByArtistName.length > 2) {

            $("." + artistElement).show();
            $("."+artistElement+" .loading").show();
            
            
            var promise = autoCompleteFactory.artistName(saidByArtistName);

            promise.then(function (payload) {
                if (payload.data.Data.length > 0) {
                    $("." + artistElement+" .empty").hide();
                } else {
                    $("." + artistElement+" .empty").show();
                }

                if (type === "saidByArtistName") {
                    $scope.speakers = payload.data.Data;
                } else {
                    $scope.artists = payload.data.Data;
                }
                $("." + artistElement+" .loading").hide();
            },
            function (errorPayload) {
                $scope.artists = "";
                $("." + artistElement+" .loading").hide();
                $("." + artistElement+" .empty").show();
            });
        } 
    };

    $scope.selectSuggestedArtist = function (artistName, fieldToPopulate) {
        if (fieldToPopulate === "artistName") {
            $scope.artistName = artistName;
        } else {
            $scope.saidByArtistName = artistName;
        }
        $scope.artists = "";
        $(".suggestions").hide();
    }

    $scope.autoCompleteTrack = function () {
        $(".suggestions").hide();
        if ($scope.trackTitle.length > 2) {
            $(".track-suggestions").show();
            $(".track-suggestions .loading").show();
            trackTitle = encodeURIComponent($scope.trackTitle);
            var promise = autoCompleteFactory.trackTitle(trackTitle);

            promise.then(function (payload) {
                $scope.tracks = payload.data.Data;
                if ($scope.tracks.length > 0) {
                    $(".track-suggestions .empty").hide();
                } else {
                    $(".track-suggestions .empty").show();
                }
                $(".track-suggestions .loading").hide();
            },
            function (errorPayload) {
                $scope.tracks = "";
                $(".track-suggestions .loading").hide();
                $(".track-suggestions .empty").show();
            });
        }


    };

    $scope.selectSuggestedTrack = function (trackName, albumName, releaseDate, artistName, titleFieldToPopulate, dateFieldToPopulate) {
        if (titleFieldToPopulate === "albumTitle") {
            $scope.trackTitle = trackName;
            $scope.albumTitle = albumName;
            $scope.artistName = artistName;
            $('#' + dateFieldToPopulate).datepicker("setDate", new Date(releaseDate));
            
        } else {
            $scope.trackTitle = trackName;
        }
        $scope.tracks = "";
        $(".suggestions").hide();
    };

    

    

});


$(".suggestions .close").click(function () {
    $(".suggestions").hide();
});