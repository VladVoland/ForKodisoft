'use strict';

var tempUserId = 0;
var collectionId = 0;

var app = angular.module('myApp');
app.controller('signIn', function ($scope, $http, $route) {
    if (tempUserId > 0) $scope.logged = true;

    $scope.check = function () {
        $http.get("http://localhost:58149/api/user/" + $scope.login + "/" + $scope.password).then(
            function (response) {
                tempUserId = response.data;
                $scope.login = "";
                $scope.password = "";
                $route.reload();

            },
            function (response) {
                $scope.showWarningMessage = true;
                if (!response.data.Message.includes('No HTTP resource was found') &&
                    !response.data.Message.includes('The request is invalid')) $scope.warningMessage = response.data.Message;
                else $scope.warningMessage = "Correct your entered data!"
            }
        );
    };
    $scope.logout = function () {
        tempUserId = 0;
        $scope.logged = false;
        window.location.href = "http://localhost:50400/index.html#!/signIn";
    };
});

app.controller('register', function ($scope, $http) {
    if (tempUserId > 0) $scope.logged = true;
    $scope.registerin = function () {
        var data = {
            "Name": $scope.name,
            "Surname": $scope.surname,
            "Patronymic": $scope.patronymic,
            "Login": $scope.login,
            "Password": $scope.password
        };

        $http.post('http://localhost:58149/api/user/newUser', data)
            .then(
            function (response) {
                    tempUserId = response.data;
                    alert("Registered");
                    $scope.name = "";
                    $scope.surname = "";
                    $scope.patronymic = "";
                    $scope.login = "";
                    $scope.password = "";
                },
                function (response) {
                    $scope.showWarningMessage = true;
                    if (!response.data.Message.includes('No HTTP resource was found') &&
                        !response.data.Message.includes('The request is invalid')) $scope.warningMessage = response.data.Message;
                    else $scope.warningMessage = "Correct your entered data!"
                });
    };
    $scope.logout = function () {
        tempUserId = 0;
        $scope.logged = false;
        window.location.href = "http://localhost:50400/index.html#!/signIn";
    };
});

app.controller('contentView', function ($scope, $http, $route) {
    if (tempUserId > 0) $scope.logged = true;
    
    $http.get("http://localhost:58149/api/feeds/" + collectionId).then(function (response) {
        $scope.items = response.data;
    });

    $scope.saveFeed = function () {
        var data = {
            "URL": $scope.URL
        };
        $http.post("http://localhost:58149/api/feeds/" + collectionId, data).then(function (response) {
            $scope.URL = "";
            $route.reload();
        },
            function (response) {
                $scope.URL = "";
                alert(response.data.Message);
            });
    };

    $scope.deleteFeed = function (Id) {
        $http.delete('http://localhost:58149/api/feeds/delete/' + Id).then(function (response) {
            $route.reload();
        });
    };

    $scope.logout = function () {
        tempUserId = 0;
        $scope.logged = false;
        window.location.href = "http://localhost:50400/index.html#!/signIn";
    };
});

app.controller('collectionsView', function ($scope, $http, $route) {
    if (tempUserId > 0) $scope.logged = true;

    $http.get("http://localhost:58149/api/contentcollections").then(function (response) {
        $scope.collections = response.data;
    });

    $scope.saveCollection = function () {
        $http.post("http://localhost:58149/api/contentcollections/" + $scope.Title).then(function (response) {
            $scope.Title = "";
            $route.reload();
        },
            function (response) {
                $scope.Title = "";
                alert(response.data.Message);
            });
    };

    $scope.showContent = function (id) {
        collectionId = id;
        window.location.href = "http://localhost:50400/#!/contentView";
    };

    $scope.deleteCollection = function (Id) {
        $http.delete('http://localhost:58149/api/contentcollections/delete/' + Id).then(function (response) {
            $route.reload();
        });
    };

    $scope.logout = function () {
        tempUserId = 0;
        $scope.logged = false;
        window.location.href = "http://localhost:50400/Index.html#!/signIn";
    };
});