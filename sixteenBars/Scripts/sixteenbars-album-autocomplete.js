
app.controller("AlbumController", function ($scope, $http, autoCompleteFactory, existFactory) {
    $scope.autoCompleteAlbum = function () {
        //chariotsolutions.com/blog/post/angularjs-corner-using-promises-q-handle-asynchronous-calls
        if ($scope.albumTitle.length > 2) {
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
            },
            function (errorPayload) {
                $scope.albums = "";
            });
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


    $scope.autoCompleteArtist = function () {
        if ($scope.saidByArtistName.length > 2) {
            saidByArtistName = encodeURIComponent($scope.saidByArtistName);
            var promise = autoCompleteFactory.artistName(saidByArtistName);

            promise.then(function (payload) {
                $scope.artists = payload.data.Data;
            },
            function (errorPayload) {
                $scope.artists = "";
            });
        }
    };

    $scope.selectSuggestedArtist = function (artistName, fieldToPopulate) {
        if (fieldToPopulate === "artistName") {
            $scope.saidByArtistName = artistName;
        } else {
            $scope.saidByArtistName = artistName;
        }
        $scope.artists = "";
    }

    $scope.autoCompleteTrack = function () {
        //if ($scope.trackTitle.length > 2) {
        //    $http({
        //        url: '../api/TrackAPI/TrackAutoComplete?title=' + $scope.trackTitle,
        //        method: 'GET'
        //    }).success(function (data, status, headers, config) {
        //        $scope.tracks = data.Data;
        //    });
        //}
        if ($scope.trackTitle.length > 2) {
            trackTitle = encodeURIComponent($scope.trackTitle);
            var promise = autoCompleteFactory.trackTitle(trackTitle);

            promise.then(function (payload) {
                $scope.tracks = payload.data.Data;
            },
            function (errorPayload) {
                $scope.tracks = "";
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
    };

});
