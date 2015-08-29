app.factory('existFactory', function ($http) {
    var obj = {};
    obj.track = function (name, albumTitle, artistName) {
        //return $http.get(siteURL + '/api/TrackAPI/TrackExists/' + name, albumTitle, artistName);
        return $http.get(siteURL + '/api/TrackAPI/TrackExists?trackTitle=' + name + '&albumTitle=' + albumTitle + '&artistName=' + artistName);
    };

    obj.artist = function (artistName) {
        return $http.get(siteURL + '/api/TrackAPI/TrackExists/' + artistName);
    };

    obj.album = function (albumTitle, artistName) {
        return $http.get(siteURL + '/api/AlbumAPI/AlbumExists/' + albumTitle, artistName);
    };

    obj.quote = function (quote,name, albumTitle, artistName) {
        return $http.get(siteURL + '/api/TrackAPI/TrackExists/' + quote, name, albumTitle, artistName);
    };

    return obj;
});