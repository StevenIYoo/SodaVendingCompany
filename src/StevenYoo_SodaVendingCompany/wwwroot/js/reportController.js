// reportController.js

(function () {
    "use strict";

    angular.module("app-reports")
        .controller("reportController", reportController)
           

    function reportController($http, $scope, uiGridConstants) {

        var viewModel = this;

        viewModel.sodas = [];

        viewModel.errorMessage = "";
        viewModel.isBusy = true;

        $scope.reportGrid = {
            columnDefs: [
             { field: "sodaName", headerCellClass: $scope.highlightFilteredHeader },
             { field: "sodaPrice", cellFilter: "currency", headerCellClass: $scope.highlightFilteredHeader },
             { field: "transactionDate", cellFilter: "date:'MMM d, y @ H:mm:ss'", filterCellFiltered: true, headerCellClass: $scope.highlightFilteredHeader }
            ],   

            enableFiltering: true,
            enableGridMenu: true,
            enableSelectAll: true,

            exporterCsvFilename: 'myFile.csv',
            exporterPdfDefaultStyle: { fontSize: 12 },
            exporterPdfTableStyle: { margin: [30, 30, 30, 30] },
            exporterPdfTableHeaderStyle: { fontSize: 18, bold: true, italics: true,},
            exporterPdfHeader: { text: "Soda Transactions", style: 'headerStyle' },
            exporterPdfFooter: function (currentPage, pageCount) {
                return { text: currentPage.toString() + ' of ' + pageCount.toString(), style: 'footerStyle' };
            },
            exporterPdfCustomFormatter: function (docDefinition) {
                docDefinition.styles.headerStyle = { fontSize: 22, bold: true };
                docDefinition.styles.footerStyle = { fontSize: 10, bold: true };
                return docDefinition;
            },
            exporterPdfOrientation: 'portrait',
            exporterPdfPageSize: 'LETTER',
            exporterPdfMaxGridWidth: 500,
            exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),

            onRegisterApi: function (gridApi) {
                $scope.gridApi = gridApi;
            }
         
        }   

        $http.get("/api/reportVendingMachine/sodas")
            .then(function (response) {
                //Success
                angular.copy(response.data, viewModel.sodas);

                $scope.reportGrid.data = viewModel.sodas;


            }, function (error) {
                //Failure

                viewModel.errorMessage = "Failed to load data: " + error;
            })
            .finally(function () {
                viewModel.isBusy = false;
            });


        $scope.highlightFilteredHeader = function (row, rowRenderIndex, col, colRenderIndex) {
            if (col.filters[0].term) {
                return 'header-filtered';
            } else {
                return '';
            }
        };

        $scope.toggleFiltering = function () {
            $scope.reportGrid.enableFiltering = !$scope.reportGrid.enableFiltering;
            $scope.gridApi.core.notifyDataChange(uiGridConstants.dataChange.COLUMN);
        };
    }

})();