
app.controller("AlbumController", function ($scope, $http, autoCompleteFactory) {
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
            //$scope.releaseDate = releaseDate;
            $scope.artistName = artistName;
            $('#releaseDate').datepicker("setDate", new Date(releaseDate));

        } else {
            $scope.albumTitle = albumName;
        }
        $scope.albums = "";
    };
});
