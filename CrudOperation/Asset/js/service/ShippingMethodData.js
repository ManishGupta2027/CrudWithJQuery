(function () {
    'use strict';
    /* Service: ShippingMethodData
     * Defines the methods related to global data across the app
     */
    btAppAdmin.factory('ShippingMethodData', function ($http, $q, BASE_URL) {
        return {
            getCarrierData: function () {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Carrier/GetShippingCarrier').success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getCarrierShippingMethodData: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Carrier/GetCarrierShippingMethod', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            saveShippingRule: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Setting/UpsertShippingRule', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            deleteShippingRule: function (id) {
                var deferred = $q.defer();
                $http.get(BASE_URL + '/Setting/DeleteShippingRule/' + id).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            }
        }
    })
}());