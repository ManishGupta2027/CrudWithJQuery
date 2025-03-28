(function () {
    'use strict';
    /* Service: SupplierSvc
     * Defines the methods related to global data across the app
     */

    btAppAdmin.factory('deliveryTypeSLAData', function ($http, $q, BASE_URL) {
        return {

            saveDTypeSLA: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/deliveryTypeSLA/SaveDeliveryTypeSLA', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getSLAData: function (masterFilter) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/deliveryTypeSLA/Filterresult', masterFilter).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },


        }
    })
}());