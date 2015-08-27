app.factory('autoCompleteFactory', function ($http) {
    var obj = {};
    obj.artistName = function (name) {
        return $http.get(siteURL+'/api/ArtistAPI/AutoCompleteName/' + name);
    };
    

    obj.albumTitle = function (title, name) {
        var baseUrl = siteURL+"/api/AlbumAPI/AlbumAutoComplete?title="+title;
        if (name != "") {
            baseUrl += "&artist=" + name;
        }
        return $http.get(baseUrl);
    };

    obj.trackTitle = function () { };
    return obj;
});