

var artistController = app.controller("ArtistController", function ($scope, $http) {
    $scope.artistName;
    $scope.artistExists = false;
    $scope.artistExistsSubmit = true;
    $scope.existsArtistName = function () {
        if ($scope.artistName != undefined) {
            if ($scope.artistName.length > 2) {
                $http({
                    url: siteURL+'/api/ArtistAPI/ArtistExists',
                    data: "'" + $scope.artistName + "'",
                    method: 'POST'
                })
                .success(function (data, status, headers, config) {
                    if (data === true) {
                        $scope.artistExists = true;
                        $scope.artistExistsSubmit = false;
                    } else {
                        $scope.artistExists = false;
                        $scope.artistExistsSubmit = true;
                    }
                });
            } else {
                $scope.artistExists = false;
                $scope.artistExistsSubmit = true;
            }
        }
    };
});
