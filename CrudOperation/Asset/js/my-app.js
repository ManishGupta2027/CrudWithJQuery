var myApp = angular.module('myApp', ['ngRoute', 'bw.paging', 'ngFileUpload', 'ngBootbox']);

// Define the alerts service
myApp.factory('alerts', function () {
    var isRunning = false; // Prevent multiple alerts from displaying simultaneously
    var alertContainer = $(".alert-container");

    // Use _.template to define the alert HTML structure
    var template = _.template(`
        <div class="alert <%= alertClass %> alert-dismissable">
            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
            <%= message %>
        </div>
    `);

    // Function to display an alert
    function showAlert(alert) {
        var alertElement = $(template(alert)); // Render the alert using the template
        if (!isRunning) {
            alertContainer.append(alertElement);
            isRunning = true;
        }

        // Set a timeout to hide the alert after 5 seconds
        window.setTimeout(function () {
            isRunning = false;
            alertElement.fadeOut(function () {
                alertElement.remove(); // Remove the alert element after fading out
            });
        }, 5000);
    }

    // Shortcut functions for different alert types
    function success(message) {
        showAlert({ alertClass: "alert-success", message: message });
    }

    function info(message) {
        showAlert({ alertClass: "alert-info", message: message });
    }

    function warning(message) {
        showAlert({ alertClass: "alert-warning", message: message });
    }

    function error(message) {
        showAlert({ alertClass: "alert-danger", message: message });
    }

    // Expose the alert functions
    return {
        showAlert: showAlert,
        success: success,
        info: info,
        warning: warning,
        error: error
    };
});
