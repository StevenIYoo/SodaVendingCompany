//adminController.js

(function () {
    "use strict";

    angular.module("app-reports")
        .controller("adminController", adminController);

    function adminController($http, $scope, $mdDialog, $filter) {

        var viewModel = this;

        viewModel.sodas = [];

        viewModel.newSoda = {};

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


        viewModel.decrement = function (soda, sodaName) {

            viewModel.successStatus = "";
            viewModel.warningStatus = "";


            $http.put("/api/adminVendingMachine/sodas/" + sodaName + "/decrement/count")
            .then(function (response) {

                soda.sodaCount = soda.sodaCount - 1;

                viewModel.successStatus = sodaName + " decremented to " + soda.sodaCount;

            }, function (error) {
                viewModel.errorMessage = "Failed to decrement: " + error;
            })
            .finally(function () {
                viewModel.isBusy = false;
            })
        }

        viewModel.increment = function (soda, sodaName) {

            viewModel.successStatus = "";
            viewModel.warningStatus = "";

            $http.put("/api/adminVendingMachine/sodas/" + sodaName + "/increment/count")
            .then(function (response) {

                soda.sodaCount = soda.sodaCount + 1;

                viewModel.successStatus = sodaName + " incremented to " + soda.sodaCount;

            }, function (error) {
                viewModel.errorMessage = "Failed to increment: " + error;
            })
            .finally(function () {
                viewModel.isBusy = false;
            })
        }

        viewModel.showPrompt = function (ev, soda) {

            viewModel.successStatus = "";
            viewModel.warningStatus = "";

            // Appending dialog to document.body to cover sidenav in docs app
            var confirm = $mdDialog.prompt()
              .title("Changing price for " + soda.name)
              .textContent("Only numbers and only two max decimal points allowed")
              .placeholder("Price")
              .ariaLabel("New Price")
              .initialValue()
              .targetEvent(ev)
              .ok("Change It")
              .cancel("Nevermind");

            $mdDialog.show(confirm).then(function (result) {

                var moneyRegex = /^[0-9]+(\.[0-9]{1,2})?$/;

                if (!moneyRegex.test(result)) {
                    viewModel.warningStatus = "Invalid Money Format";
                    return;
                }

                $http.post("api/adminVendingMachine/sodas/" + soda.name + "/" + result + "/priceChange")
                .then(function (response) {

                    viewModel.successStatus = "Price of " + soda.name + " changed to " + $filter('currency')(result);
                    soda.price = result;

                }, function (error) {
                    viewModel.errorMessage = "Failed to change price: " + error;
                })
                .finally(function () {
                    viewModel.isBusy = false;
                })
            }, function () {
                viewModel.warningStatus = "Change canceled";
            });

        };

        viewModel.createSoda = function () {

            viewModel.isBusy = true;
            viewModel.errorMessage = "";

            $http.post("/api/CreateSoda", viewModel.newSoda)
                .then(function (response) {
                    //viewModel.sodas.push(response.data);
                    viewModel.newSoda = {};

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

                    viewModel.successStatus = "New Soda Created";
                }, function (error) {
                    viewModel.errorMessage = "Failed to save new soda";
                })
                .finally(function () {
                    viewModel.isBusy = false;
                })

        };
    }

})();