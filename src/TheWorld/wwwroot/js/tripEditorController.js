// tripEditorController.js

(function () {
    "use strict";

    angular.module("app-trips").controller("tripEditorController", tripEditorController);

    function tripEditorController($routeParams, $http) {
        var vm = this;

        vm.tripName = $routeParams.tripName;
        vm.errorMessage = "";
        vm.isBusy = true;
        vm.stops = [];
        vm.newStop = {};

        var url = "/api/trips/" + vm.tripName + "/stops";

        $http.get(url)
            .then(function (response) {
                //success
                angular.copy(response.data, vm.stops);
                showMap(vm.stops);
            }, function (error) {
                //failure
                vm.errorMessage = "Failed to load stops " + error;
            }).finally(function () {
                vm.isBusy = false;
            });

        vm.addStop = function () {
            vm.isBusy = true;

            $http.post(url, vm.newStop)
                .then(function (response) {
                    //success
                    vm.stops.push(response.data);
                    showMap(vm.stops);
                }, function (error) {
                    //failure
                    vm.errorMessage = "Failed to add new stop " + error;
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        }
    }

    function showMap(stops) {
        if (stops && stops.length > 0) {

            var mapStops = _.map(stops, function (item) {
                return {
                    lat: item.latitude,
                    long: item.longitude,
                    info: item.name
                };
            });

            //Show Map
            debugger;
            travelMap.createMap({
                stops: mapStops,
                selector: "#map",
                currentStop: 0,
                initialZoom: 3
            });

            $("#map").css("height", 600);
        }
    }

})();