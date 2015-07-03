/// <reference path="../../sixteenbars/scripts/angular.js" />
/// <reference path="../../sixteenbars/scripts/angular-mocks.js" />
/// <reference path="PasswordController.js" />
describe('PasswordController', function () {
    beforeEach(module('app'));
    var $controller;
    beforeEach(inject(function (_$controller_) {
        $controller = _$controller_;
    }));

    describe('grade', function () {
        it('Strong', function () {
            var $scope = {};
            var controller = $controller('PasswordController', { $scope: $scope });
            $scope.password = 'longerthaneightchars';
            $scope.grade();
            expect($scope.strength).toEqual('strong');
        });

        it('Weak', function () {
            var $scope = {};
            var controller = $controller('PasswordController', { $scope: $scope });
            $scope.password = 'a';
            $scope.grade();
            expect($scope.strength).toEqual('weak');
        });
    });

});