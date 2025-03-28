(function () {
    'use strict';
    /* Service: ProductData
     * Defines the methods related to global data across the app
     */
    btAppAdmin.factory('ProductData', function ($http, $q, BASE_URL) {
        return {
            getProducts: function (productFilter) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Product/FilterProduct', productFilter).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getInventoryByLocation: function (stockCode, deliveryCenterId, inventoryType) {
                var deferred = $q.defer();
                var data = { stockCode: stockCode, deliveryCenterId: deliveryCenterId, inventoryType: inventoryType}
                $http.post(BASE_URL + '/Inventory/GetInventoryByLocation/', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getSuppliersForProduct: function (stockCode) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Supplier/GetSuppliersForProduct?stockCode=' + stockCode).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getPackageMasterdata: function () {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Product/GetPackageMasterdata').success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            updateSupplierForProduct: function (selectedSupplierId,productId) {
                var deferred = $q.defer();
                var data = { supplierId: selectedSupplierId, productId: productId }
                $http.post(BASE_URL + '/Product/UpdateSupplierForProduct/', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;

            },
            updatePackageinfoForProduct: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Product/UpdateProductPackageinfo', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;

            },
            updateProductStorageSpace: function (productId, storageSpace) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Product/UpdateProductStorageSpace', productId, storageSpace).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            updateReorderInfo: function (productId, reorderLevel, reorderQty, infoType) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Product/UpdateReorderInfo', productId, reorderLevel, reorderQty, infoType).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },

            updateProductCaseInfo: function (model) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Product/UpdateProductCaseInfo', model).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;

            },
            updatePackagingType: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Product/UpdatePackagingType', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;

            },
            getVariantGroupItems: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Product/getVariantGroupItems/', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getStockStatement: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Product/GetStockStatement/', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
             printBarCode: function (data) {
                var deferred = $q.defer();
                 $http.post(BASE_URL + '/ReportViewer/Index/', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            setProductsForBarCodePrint: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Product/SetProductsForBarCodePrint/', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getInventoryPositionDetail: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Product/GetInventoryPositionDetail/', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getInventoryLogDetail: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Product/GetInventoryLogDetail/', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getProductBundleItems: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Product/GetProductBundleItems/', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            updateProductCategory: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Product/UpdateProductCategory/', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            updateProductManufacturer: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Product/UpdateProductManufacturer/', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            updateProductFields: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Product/UpdateProductFileds/', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
        }
    });


}());