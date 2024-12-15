(function () {
    'use strict';
    /* Service: SupplierSvc
     * Defines the methods related to global data across the app
     */
    btAppAdmin.factory('templateData', function ($http, $q, BASE_URL) {
        return {
            saveEmailTemplateContentLiquid: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Setting/SaveEmailTemplateContent', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
        }
    })
}());