
var app = angular.module('app', ['ngRoute', 'ui.bootstrap', 'ngGrid']);

app.config(
    function ($routeProvider, $httpProvider, $locationProvider) {

        $routeProvider
            .when('/customers', { templateUrl: 'app/views/customers/customerlist.html', controller: 'CustomerController' })

            .otherwise({ redirectTo: '/customers' });
    }
);