


/// <reference path="../../sixteenbars/scripts/angular.js" />
/// <reference path="../../sixteenbars/scripts/sixteenbars-utilities.js" />
/// <reference path="../../sixteenbars/scripts/sixteenbars-track-app.js" />
/// <reference path="../../sixteenbars/scripts/sixteenbars-autocomplete-functions.js" />
/// <reference path="../../sixteenbars/scripts/sixteenbars-exist-functions.js" />
/// <reference path="../../sixteenbars/scripts/sixteenbars-album-autocomplete.js" />



describe('AlbumController', function () {
    beforeEach(module('TrackApp'));
    describe('autoCompleteAlbum', function () {
        it('returns "Because The Internet"', function () {
            var $scope = {};
            var $http = {};
            var autoCompleteFactory = {};
            var controller = $controller('AlbumController', { $scope:$scope, $http:$http, autoCompleteFactory:autoCompleteFactory });
            $scope.albumTitle = 'because';
            $scope.artistName = 'child';
            $scope.grade();
            expect($scope.strength).toEqual('strong');
        });
    });
});