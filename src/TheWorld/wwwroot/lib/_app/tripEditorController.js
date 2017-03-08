!function(){"use strict";function t(t,n){var s=this;s.tripName=t.tripName,s.errorMessage="",s.isBusy=!0,s.stops=[],s.newStop={};var a="/api/trips/"+s.tripName+"/stops";n.get(a).then(function(t){angular.copy(t.data,s.stops),o(s.stops)},function(t){s.errorMessage="Failed to load stops "+t}).finally(function(){s.isBusy=!1}),s.addStop=function(){s.isBusy=!0,n.post(a,s.newStop).then(function(t){s.stops.push(t.data),o(s.stops)},function(t){s.errorMessage="Failed to add new stop "+t}).finally(function(){s.isBusy=!1})}}function o(t){if(t&&t.length>0){var o=_.map(t,function(t){return{lat:t.latitude,long:t.longitude,info:t.name}});travelMap.createMap({stops:o,selector:"#map",currentStop:0,initialZoom:3}),$("#map").css("height",600)}}t.$inject=["$routeParams","$http"],angular.module("app-trips").controller("tripEditorController",t)}();