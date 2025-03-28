(function () {
    'use strict';
    /* Service: OrderData
     * Defines the methods related to global data across the app
     */
    btAppAdmin.factory('DashboardData', function ($http, $q, BASE_URL) {
        return {
            getDashboardInfo: function () {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Home/GetDashboardInfo').success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getDashboardWidget: function (widgetCode,startDate,endDate) {
                var deferred = $q.defer();
                $http.get(BASE_URL + '/Home/GetDashboardWidget?widgetCode=' + widgetCode + "&startDate=" + startDate + "&endDate=" + endDate).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            }
        };
    });


}());