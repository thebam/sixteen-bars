/// <reference path="../../sixteenbars/scripts/angular.js" />
/// <reference path="../../sixteenbars/scripts/angular-mocks.js" />
/// <reference path="../../sixteenbars/scripts/sixteenbars-artist-autocomplete.js" />
//describe("TrackApp", function () {
//    beforeEach(module('TrackApp'));
//    var scope, httpBackEnd, http, controller;
//    beforeEach(inject(function ($rootScope, $controller, $httpBackend, $http) {
//        scope = $rootScope.$new();
//        httpBackEnd = $httpBackend;
//        http = $http;
//        controller = $controller;
//        httpBackEnd.when("GET", "/api/ArtistAPI/AutoCompleteName/dra").respond([{ "ContentEncoding": null, "ContentType": null, "Data": [{ "Id": 42, "Name": "Drake", "DateCreated": "2015-05-18T06:24:09.307", "DateModified": "2015-05-18T06:24:09.307", "Enabled": true }], "JsonRequestBehavior": 0, "MaxJsonLength": null, "RecursionLimit": null }]);
//        $controller('TrackController', {
//            $scope: scope,
//            $http:$http
//        });
//    }));

//    it("Find Drake", function () {
//        httpBackEnd.expectGET('/api/ArtistAPI/AutoCompleteName/dra');
//        controller('TrackController', {
//            $scope: scope,
//            $http: http
//        });
//        httpBackEnd.flush();
//    });
//});

//describe("TrackApp", function () {
//    beforeEach(module('TrackApp'));
//    var MainCtrl, $scope;

//    describe("with spies", function () {
//        beforeEach(inject(function ($controller, $rootScope, artist) {
//            $scope = $rootScope.$new();

//            spyOn(artist, "autoComplete").and.callFake(function () {
//                return {
//                    //success: function (callback) { callback([{ "ContentEncoding": null, "ContentType": null, "Data": [{ "Id": 42, "Name": "Drake", "DateCreated": "2015-05-18T06:24:09.307", "DateModified": "2015-05-18T06:24:09.307", "Enabled": true }], "JsonRequestBehavior": 0, "MaxJsonLength": null, "RecursionLimit": null }]) }
//                    success: function (callback) { callback({ things: 'and stuff' }) }
//                };
//            });

//            MainCtrl = $controller('TrackController', { $scope: $scope, artist: artist });
//        }));

//        it('should set data to "Drake"', function () {
//            //expect($scope.data).toEqual({ "Id": 42, "Name": "Drake", "DateCreated": "2015-05-18T06:24:09.307", "DateModified": "2015-05-18T06:24:09.307", "Enabled": true });
//            expect($scope.data).toEqual({ things: 'and stuff'});
//        });
//    });
//});

//describe('mocking service http call', function () {
//    beforeEach(module('TrackApp'));
//    var TrackController, $scope;
//    describe('with spies', function () {
//        beforeEach(inject(function ($controller, $rootScope, artist) {
//            $scope = $rootScope.$new();
//            spyOn(artist, 'autoComplete').and.callFake(function () {
//                return {
//                    success: function (callback) { callback({ Id: 42, Name: "Drake", DateCreated: "2015-05-18T06:24:09.307", DateModified: "2015-05-18T06:24:09.307", Enabled: true }) }
//                };
//            });
//            TrackController = $controller('TrackController', { $scope: $scope, artist: artist });
//        })); 

//        it('should set data to "Drake"', function () {
//            expect($scope.artists).toEqual({
//                Id: 42, Name: "Drake", DateCreated: "2015-05-18T06:24:09.307", DateModified: "2015-05-18T06:24:09.307", Enabled: true
//            });
//        });
//    });
//});

describe('with httpBackend', function () {
    beforeEach(module('TrackApp'));
    var TrackController, $scope;
    beforeEach(inject(function ($controller, $rootScope, $httpBackend) {
        $scope = $rootScope.$new();
        $httpBackend.when('GET', '/api/ArtistAPI/AutoCompleteName/drake')
            .respond({
                Id: 42, Name: "Drake", DateCreated: "2015-05-18T06:24:09.307", DateModified: "2015-05-18T06:24:09.307", Enabled: true
            });
        $scope.artistName = "drake";
        TrackController = $controller('TrackController', { $scope: $scope });
        
        $httpBackend.flush();
    }));
    it('should set data to "Drake"', function () {
        expect($scope.artists).toEqual({
            Id: 42, Name: "Drake", DateCreated: "2015-05-18T06:24:09.307", DateModified: "2015-05-18T06:24:09.307", Enabled: true
        });
    });
});


