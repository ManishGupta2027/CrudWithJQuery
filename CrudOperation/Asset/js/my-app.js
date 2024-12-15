var myApp = angular.module('myApp', ['ngRoute', 'bw.paging', 'ngFileUpload', 'ngBootbox']);

// Define the alerts service
myApp.factory('alerts', function () {
    return {
        showAlert: function (message) {
            alert(message);
        }
         
    };
 });
    //myApp.factory('alerts', function ($http, $q, BASE_URL) {
    //    var isRunning = false;
    //    var alertContainer = $(".alert-container");

    //    var template = _.template("<div class='alert <%= alertClass %> alert-dismissable'>" +
    //        "<button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;</button>" +
    //        "<%= message %>" +
    //        "</div>");

    //    function showAlert(alert) {
    //        var alertElement = $(template(alert));
    //        if (isRunning == false) {
    //            alertContainer.append(alertElement);
    //            isRunning = true;
    //        }
    //        window.setTimeout(function () {
    //            isRunning = false;
    //            alertElement.fadeOut();
    //        }, 5000);
    //    }
    //    function success(message) {
    //        showAlert({ alertClass: "alert-success", message: message });
    //    }

    //    function info(message) {
    //        showAlert({ alertClass: "alert-info", message: message });
    //    }

    //    function warning(message) {
    //        showAlert({ alertClass: "alert-warning", message: message });
    //    }

    //    function error(message) {
    //        showAlert({ alertClass: "alert-danger", message: message });
    //    }

    //    return {
    //        showAlert: showAlert,
    //        success: success,
    //        info: info,
    //        warning: warning,
    //        error: error
    //    };
    //});
