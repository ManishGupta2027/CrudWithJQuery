(function () {
    'use strict';
    /* Service: GlobalData
     * Defines the methods related to global data across the app
     */
    btAppAdmin.factory('GlobalData', function ($http, $q, BASE_URL) {
        var menus = {};
        return {
            searchStockCode: function (qry, searchType) {
                var deferred = $q.defer();
                $http.get(BASE_URL + '/Product/SearchStockCode/' + qry + '/' + searchType).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            generatePDF: function (id, domainId, templateType, trackingNo) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Content/GeneratePDF/' + id + '/' + domainId + '/' + templateType + '/' + trackingNo).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            }
        };
    });

}());