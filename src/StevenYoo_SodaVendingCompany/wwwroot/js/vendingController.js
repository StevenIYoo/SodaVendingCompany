(function () {
    "use strict";

    angular.module("app-reports")
        .controller("vendingController", vendingController);

    function vendingController($http, $scope) {

        $scope.sodaSelection = {
            name: "Pepsi",
            cost: ""
        };

        var viewModel = this;

        viewModel.successStatus = "";
        viewModel.sodas = [];

        viewModel.value = 1;

        viewModel.errorMessage = "";
        viewModel.isBusy = true;

        $http.get("/api/consumerVendingMachine/sodas")
            .then(function (response) {
                //Success
                angular.copy(response.data, viewModel.sodas);

            }, function (error) {
                //Failure

                viewModel.errorMessage = "Failed to load data: " + error;
            })
            .finally(function () {
                viewModel.isBusy = false;
            });

        viewModel.vendSoda = function (sodaName) {

            viewModel.isBusy = true;

            viewModel.sodas = [];

            $http.post("/api/consumerVendingMachine/sodas/" + sodaName + "/count")
                .then(function (response) {
                    $http.get("/api/consumerVendingMachine/sodas")
                    .then(function (response) {
                        //Success
                        angular.copy(response.data, viewModel.sodas);

                        viewModel.successStatus = "You successfully bought a " + sodaName + "!";

                    }, function (error) {
                        //Failure

                        viewModel.errorMessage = "Failed to load data: " + error;
                    })
                    .finally(function () {
                        viewModel.isBusy = false;
                    })
                }, function (error) {
                    viewModel.errorMessage = "Failed to vend";
                }).finally(function () {
                    viewModel.isBusy = false;
                })
        }
    }
  
})();