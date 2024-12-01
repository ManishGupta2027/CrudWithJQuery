var myApp = angular.module('myApp', ['ngRoute', 'bw.paging', 'ngFileUpload', 'ngBootbox']);

// Define the alerts service
myApp.factory('alerts', function () {
    return {
        showAlert: function (message) {
            alert(message);
        }
    };
});