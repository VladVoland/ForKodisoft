'use strict';

var app = angular.module('myApp', [
    'ngRoute',
    'ngResource'
]);
app.config(function ($routeProvider) {
    $routeProvider.when('/collectionsView', { templateUrl: 'app/collectionsView.html', controller: 'collectionsView' });
    $routeProvider.when('/signIn', { templateUrl: 'app/signIn.html', controller: 'signIn' });
    $routeProvider.when('/register', { templateUrl: 'app/register.html', controller: 'register' });
    $routeProvider.when('/contentView', { templateUrl: 'app/contentView.html', controller: 'contentView' });
    $routeProvider.otherwise({ redirectTo: '/signIn' });
});