/// <reference path="../../sixteenbars/scripts/angular.js" />
/// <reference path="../../sixteenbars/scripts/angular-mocks.js" />
/// <reference path="../../sixteenbars/scripts/sixteenbars-artist-autocomplete.js" />





//http://www.benlesh.com/2013/06/angular-js-unit-testing-services.html
describe('httpBasedService', function () {
    var httpBasedService,
        httpBackend;

    beforeEach(function () {
        // load the module.
        module('TrackApp');

        // get your service, also get $httpBackend
        // $httpBackend will be a mock, thanks to angular-mocks.js
        inject(function ($httpBackend, _httpBasedService_) {
            httpBasedService = _httpBasedService_;
            httpBackend = $httpBackend;
        });
    });

    // make sure no expectations were missed in your tests.
    // (e.g. expectGET or expectPOST)
    afterEach(function () {
        httpBackend.verifyNoOutstandingExpectation();
        httpBackend.verifyNoOutstandingRequest();
    });

    it('should send the msg and return the response.', function () {
        // set up some data for the http call to return and test later.
        var returnData = {
                            ContentEncoding: null, ContentType: null, Data: [{Id: 42, Name: "Drake", DateCreated: "2015-05-18T06:24:09.307", DateModified: "2015-05-18T06:24:09.307", Enabled: true}], JsonRequestBehavior: 0, MaxJsonLength: null, RecursionLimit: null 
                        };

        // expectGET to make sure this is called once.
        httpBackend.expectGET('/api/ArtistAPI/AutoCompleteName/drak').respond(returnData);

        // make the call.
        var returnedPromise = httpBasedService.autoCompleteArtist("drak");

        // set up a handler for the response, that will put the result
        // into a variable in this scope for you to test.
        var result;
        returnedPromise.then(function (response) {
            result = response;
        });

        // flush the backend to "execute" the request to do the expectedGET assertion.
        httpBackend.flush();

        // check the result. 
        // (after Angular 1.2.5: be sure to use `toEqual` and not `toBe`
        // as the object will be a copy and not the same instance.)
        expect(result).toEqual(returnData);
    });
});





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

//describe('with httpBackend', function () {
//    beforeEach(module('TrackApp'));
//    var TrackController, $scope;
//    beforeEach(inject(function ($controller, $rootScope, $httpBackend) {
//        $scope = $rootScope.$new();
//        $httpBackend.when('GET', '/api/ArtistAPI/AutoCompleteName/drake')
//            .respond({
//                ContentEncoding: null, ContentType: null, Data: [{Id: 42, Name: "Drake", DateCreated: "2015-05-18T06:24:09.307", DateModified: "2015-05-18T06:24:09.307", Enabled: true}], JsonRequestBehavior: 0, MaxJsonLength: null, RecursionLimit: null 
//            });
//        $scope.artistName = "drake";
//        TrackController = $controller('TrackController', { $scope: $scope });
        
        
//    }));
//    it('should set data to "Drake"', function () {
//        expect($scope.artists).toBeUndefined();
       
//        $scope.autoCompleteArtist('artistName');
//        $httpBackend.flush();
//        expect($scope.artists).toEqual({
//            ContentEncoding: null, ContentType: null, Data: [{ Id: 42, Name: "Drake", DateCreated: "2015-05-18T06:24:09.307", DateModified: "2015-05-18T06:24:09.307", Enabled: true }], JsonRequestBehavior: 0, MaxJsonLength: null, RecursionLimit: null
//        });
//    });
//});


