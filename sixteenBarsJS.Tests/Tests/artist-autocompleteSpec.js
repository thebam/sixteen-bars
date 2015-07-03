/// <reference path="../../sixteenbars/scripts/angular.js" />
/// <reference path="../../sixteenbars/scripts/angular-mocks.js" />
/// <reference path="../../sixteenbars/scripts/sixteenbars-artist-autocomplete.js" />
describe("TrackApp", function () {
    beforeEach(module('TrackApp'));
    var scope, httpBackEnd, http, controller;
    beforeEach(inject(function ($rootScope, $controller, $httpBackend, $http) {
        scope = $rootScope.$new();
        httpBackEnd = $httpBackend;
        http = $http;
        controller = $controller;
        httpBackEnd.when("GET", "/api/ArtistAPI/AutoCompleteName/dra").respond([{ "ContentEncoding": null, "ContentType": null, "Data": [{ "Id": 42, "Name": "Drake", "DateCreated": "2015-05-18T06:24:09.307", "DateModified": "2015-05-18T06:24:09.307", "Enabled": true }], "JsonRequestBehavior": 0, "MaxJsonLength": null, "RecursionLimit": null }]);
        $controller('TrackController', {
            $scope: scope,
            $http:$http
        });
    }));

    it("Find Drake", function () {
        httpBackEnd.expectGET('/api/ArtistAPI/AutoCompleteName/dra');
        controller('TrackController', {
            $scope: scope,
            $http: http
        });
        httpBackEnd.flush();
    });
});