// app-reports.js
(function () {
    "use strict";

    angular.module("app-reports", ["ngMaterial", "ngMessages", "simpleControls", "ngRoute", "ngAnimate", "ui.grid", "ui.grid.selection", "ui.grid.exporter"])
        .config(function ($routeProvider) {

            $routeProvider.when("/", {
                controller: "vendingController",
                controllerAs: "viewModel",
                templateUrl: "/views/vendingView.html"
            });

            $routeProvider.when("/reports", {
                controller: "reportController",
                controllerAs: "viewModel",
                templateUrl: "/views/reportView.html"
            });

            $routeProvider.when("/admin", {
                controller: "adminController",
                controllerAs: "viewModel",
                templateUrl: "/views/adminView.html"
            });

            $routeProvider.otherwise({ redirectTo: "/" });

        });

})();