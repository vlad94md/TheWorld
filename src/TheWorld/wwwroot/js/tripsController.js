// tripsController.js

(function() {
    "use strict";

    angular.module("app-trips").controller("tripsController", tripsController);

    function tripsController($http) {
        var vm = this;

        vm.trips = [];
        vm.errorMessage = "";
        vm.isBusy = true;

        $http.get("/api/trips")
            .then(function (response) {
                // sucess
                angular.copy(response.data, vm.trips);
            }, function(error) {
                // failure
                vm.errorMessage = "Failed to load data:" + error;
            }).finally(function() {
                vm.isBusy = false;
            });

        vm.newTrip = {};

        vm.addTrip = function () {
            vm.isBusy = true;
            debugger;
            $http.post("/api/trips", vm.newTrip)
                .then(function (response) {
                    // sucess
                    vm.trips.push(response.data);
                    vm.newTrip.name = "";
                }, function (error) {
                    // failure
                    vm.errorMessage = "Failed to save new trip:" + error;
                })
                .finally(function() {
                    vm.isBusy = false;
                });

            vm.trips.push({ name: vm.newTrip.name, created: new Date() });
            vm.newTrip.name = "";
        }
    };
})();