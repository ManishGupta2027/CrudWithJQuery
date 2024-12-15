(function () {
    'use strict';
    /* Service: SupplierSvc
     * Defines the methods related to global data across the app
     */
    btAppAdmin.factory('supplierData', function ($http, $q, BASE_URL) {
        return {
            getSupplierData: function (masterFilter) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/supplier/Filterresult', masterFilter).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            uploadFile: function () {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/supplier/').success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getPriceListForProduct: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/supplier/GetPriceListForProduct', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            deletePriceList: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/supplier/DeletePriceList', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getPriceList: function (priceListFilter) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/supplier/GetPriceList', priceListFilter).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            updatePriceList: function (addPricelist) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/supplier/UpdatePriceList', addPricelist).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getGoodRecievedList: function (goodsRecievedFilter) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/supplier/GetGoodsReceived', goodsRecievedFilter).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getPurOrdHistoryList: function (purOrdHistoryFilter) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/supplier/GetPurchaseOrdHistory', purOrdHistoryFilter).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            uploadAttachment: function (model) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Supplier/UploadAttachment', model).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            removeAttachment: function (model) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Supplier/RemoveAttachment', model).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getAttachments: function (id) {
                var deferred = $q.defer();
                $http.get(BASE_URL + '/Supplier/GetAttachments?id=' + id).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
             updateSupplierAccountmanager: function (data) {
                var deferred = $q.defer();
                 $http.post(BASE_URL + '/Supplier/UpdateSupplierAccountmanager', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            saveSupplierPOCsvFeedSetting: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/supplier/SaveSupplierPOCsvFeedSetting', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            saveSupplierPOConsolidationRule: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/supplier/SaveSupplierPOConsolidationRule', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getFeedAttributeMapping: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/supplier/FeedAttributeMapping', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
        }
    })
    }());