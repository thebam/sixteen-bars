/// <reference path="../../sixteenbars/scripts/angular.js" />
/// <reference path="../../sixteenbars/scripts/angular-route.js" />
/// <reference path="../../sixteenbars/scripts/angular-mocks.js" />
/// <reference path="../../sixteenbars/scripts/sixteenbars-track-app.js" />
/// <reference path="../../sixteenbars/scripts/sixteenbars-track-autocomplete.js" />

    beforeEach(module('TrackApp'));
    describe('TrackController', function () {
        var scope, $httpBackend, ctrl,
            lightTrackData = function () {
                return { ContentEncoding: null, ContentType: null, Data: [{ Album: { Artist: { Id: 42, Name: "Drake", DateCreated: "2015-05-18T06:24:09.307", DateModified: "2015-05-18T06:24:09.307", Enabled: true }, Id: 22, Title: "Thank Me Later", ReleaseDate: "2010-06-15T00:00:00", DateCreated: "2015-05-18T06:24:09.307", DateModified: "2015-05-18T06:24:09.307", Enabled: true }, Id: 23, Title: "Light Up", ReleaseDate: "2010-06-15T00:00:00", DateCreated: "2015-05-18T06:24:09.307", DateModified: "2015-05-18T06:24:09.307", Enabled: true }], JsonRequestBehavior: 0, MaxJsonLength: null, RecursionLimit: null }

                //{"ContentEncoding":null,"ContentType":null,"Data":[{"Album":{"Artist":{"Id":42,"Name":"Drake","DateCreated":"2015-05-18T06:24:09.307","DateModified":"2015-05-18T06:24:09.307","Enabled":true},"Id":22,"Title":"Thank Me Later","ReleaseDate":"2010-06-15T00:00:00","DateCreated":"2015-05-18T06:24:09.307","DateModified":"2015-05-18T06:24:09.307","Enabled":true},"Id":23,"Title":"Light Up","ReleaseDate":"2010-06-15T00:00:00","DateCreated":"2015-05-18T06:24:09.307","DateModified":"2015-05-18T06:24:09.307","Enabled":true}],"JsonRequestBehavior":0,"MaxJsonLength":null,"RecursionLimit":null}
            };
        beforeEach(inject(function(_$httpBackend_, $rootScope, $routeParams, $controller) {
            $httpBackend = _$httpBackend_;
            $httpBackend.expectGET('/api/TrackAPI/TrackAutoComplete?title=light').respond(lightTrackData());

            $routeParams.title = 'light';
            scope = $rootScope.$new();
            ctrl = $controller('TrackController', { $scope: scope });
        }));


        it('should fetch track detail', function() {
            expect(scope.tracks).toBeUndefined();
            $httpBackend.flush();

            expect(scope.tracks).toEqual(lightTrackData().Data);
            scope.tracks = "";
        });

        it('test2', function () {
  
            scope.trackAutoComplete("light");
            expect(scope.tracks).toEqual(lightTrackData().Data);
        });
    });
