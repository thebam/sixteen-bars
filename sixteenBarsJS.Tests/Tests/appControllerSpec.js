/// <reference path="../../sixteenbars/scripts/angular.js" />
/// <reference path="../../sixteenbars/scripts/angular-mocks.js" />
/// <reference path="../../sixteenbars/scripts/appcontroller.js" />
describe('When using appController ', function () {
    //initialize Angular
    beforeEach(module('app'));
    //parse out the scope for use in our unit tests.
    var scope;
    beforeEach(inject(function ($controller, $rootScope) {
        scope = $rootScope.$new();
        var ctrl = $controller('appController', { $scope: scope });
    }));

    it('initial value is 5', function () {
        expect(scope.value).toBe(5);
    });

    it('says hello', function () {
        expect(hello()).toBe("Hello world.");
    });



    //it("factor large numbers", function () {
    //    var calc = new Calculator();
    //    var answer = calc.factor(18973547201226, 28460320801839);

    //    waitsFor(function () {
    //        return calc.answerHasBeenCalculated();
    //    }, "took too long.", 10000);

    //    runs(function () {
    //        expect(answer).toEqual(9486773600613)
    //    });
    //});
});