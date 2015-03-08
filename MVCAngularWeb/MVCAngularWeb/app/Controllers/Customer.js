app.controller('CustomerController', function ($scope, CustomerService, $routeParams, $log) {

    $scope.data = CustomerService.data;

    
    $scope.$watch('data.sortOptions', function (newVal, oldVal) {
        $log.log("sortOptions changed: " + newVal);
        if (newVal !== oldVal) {
            $scope.data.pagingOptions.currentPage = 1;
            CustomerService.find();
        }
    }, true);

    $scope.$watch('data.filterOptions', function (newVal, oldVal) {
        $log.log("filters changed: " + newVal);
        if (newVal !== oldVal) {
            $scope.data.pagingOptions.currentPage = 1;
            CustomerService.find();
        }
    }, true);

    $scope.$watch('data.pagingOptions', function (newVal, oldVal) {
        $log.log("page changed: " + newVal);
        if (newVal !== oldVal) {
            CustomerService.find();
        }
    }, true);

    $scope.gridOptions = {
        data: 'data.customers.content',
        showFilter: false,
        multiSelect: false,
        selectedItems: $scope.data.selected,
        enablePaging: true,
        showFooter: true,
        totalServerItems: 'data.customers.totalRecords',
        pagingOptions: $scope.data.pagingOptions,
        filterOptions: $scope.data.filterOptions,
        useExternalSorting: true,
        sortInfo: $scope.data.sortOptions,
        plugins: [new ngGridFlexibleHeightPlugin()],
        columnDefs: [
                    { field: 'customerID', displayName: 'ID' },
                    { field: 'contactName', displayName: 'Contact Name' },
                    { field: 'contactTitle', displayName: 'Contact Title' },
                    { field: 'address', displayName: 'Address' },
                    { field: 'city', displayName: 'City' },
                    { field: 'postalCode', displayName: 'Postal Code' },
                    { field: 'country', displayName: 'Country' }
        ],
        afterSelectionChange: function (selection, event) {
            $log.log("selection: " + selection.entity.CustomerID);
            // $location.path("comments/" + selection.entity.commentId);
        }
    };

});
