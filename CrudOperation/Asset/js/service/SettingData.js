(function () {
    'use strict';
    /* Service: SupplierSvc
     * Defines the methods related to global data across the app
     */
    btAppAdmin.factory('settingData', function ($http, $q, BASE_URL) {
        return {
            saveStoreSetting: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Setting/SaveStoreSetting', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            updateCarrier: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Setting/UpdateCarrier', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            saveSalesChannelDomainMapping: function (model) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Setting/UpsertSalesChannelDomainMapping', model).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            updateSalesChannelStatus: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Setting/UpdateSalesChannelStatus/', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            savePrinterSettings: function (model) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Setting/UpsertPrinterSettings', model).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            deletePrinterSetting: function (id) {
                var deferred = $q.defer();
                $http.get(BASE_URL + '/Setting/DeletePrinterSetting/' + id).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            upsertBarcodeTemplate: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Setting/UpsertBarcodeTemplate/', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            deleteBarcodeTemplate: function (id, tableId) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Setting/DeleteBarcodeTemplate/', { id: id, tableId: tableId }).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            resyncData: function (dataType, channelId) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Setting/ResyncSalesChannelData/', { dataType: dataType, channelId: channelId }).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            assignWareHouseToUser: function (model) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Setting/AssignWareHouseToUser', model).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            saveTaggingRule: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Setting/UpsertTaggingRule', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            saveBatchGroup: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Setting/UpsertInventoryBatchGroup', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
        }
    })
}());