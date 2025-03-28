(function () {
    'use strict';
    /* Service: SupplierSvc
     * Defines the methods related to global data across the app
     */
    btAppAdmin.factory('xeroSettingData', function ($http, $q, BASE_URL) {
        return {           
            pluginSettingsByCat: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Setting/GetDomainPluginSettingByCat/', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            InsertAccountingServiceAccount: function (account, accountType) {
                var deferred = $q.defer();
                var data = { account: account, accountType: accountType }
                $http.post(BASE_URL + '/Setting/InsertXeroAccount', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            saveDomainPluginsByCat: function (domainId, pluginCode, categoryCode, settings) {
                var deferred = $q.defer();
                var data = { domainId: domainId, pluginCode: pluginCode, categoryCode: categoryCode, settings: settings };
                $http.post(BASE_URL + '/Setting/SaveDomainPluginByCat', data).success(deferred.resolve).error(deferred.reject);
                //$http.post(BASE_URL + 'Setting/SaveDomainPluginByCat/' + domainId + '/' + pluginCode + '/' + categoryCode, settings).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
        }
    })
}());