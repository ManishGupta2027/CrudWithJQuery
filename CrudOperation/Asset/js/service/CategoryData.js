(function () {
    'use strict';
    /* Service: OrderData
     * Defines the methods related to global data across the app
     */
    btAppAdmin.factory('CategoryData', function ($http, $q, BASE_URL) {
        return {

            getBatchGroups: function () {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Category/GetBatchGroupList').success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },

            updateCategoryBatchGroup: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Category/UpdateCategoryBatchGroup', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },

        };
    });
}());