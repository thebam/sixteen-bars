app.factory('autoCompleteFactory', function ($http) {
    var obj = {};
    obj.artistName = function (name) {
        return $http.get('../api/ArtistAPI/AutoCompleteName/' + name);
    };

    obj.albumTitle = function (title, name) {
        var baseUrl = "../api/AlbumAPI/AlbumAutoComplete?title="+title;
        if (name != "") {
            baseUrl += "&artist=" + name;
        }
        return $http.get(baseUrl);
    };

    obj.trackTitle = function () { };
    return obj;
});