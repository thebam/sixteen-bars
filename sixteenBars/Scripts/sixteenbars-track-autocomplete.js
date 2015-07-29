
TrackApp.controller('TrackController', ['$scope', '$routeParams', '$http',
  function ($scope, $routeParams, $http) {
      $http.get('/api/TrackAPI/TrackAutoComplete?title=' + $routeParams.title).success(function (data) {
          $scope.tracks = data.Data;
      });

      $scope.trackAutoComplete = function (title) {
          $http.get('/api/TrackAPI/TrackAutoComplete?title=' + title).success(function (data) {
              $scope.tracks = data.Data;
          });
      };
  }]);