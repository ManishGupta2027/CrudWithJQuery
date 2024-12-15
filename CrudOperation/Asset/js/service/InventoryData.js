(function () {
    'use strict';
    /* Service: SupplierSvc
     * Defines the methods related to global data across the app
     */
    btAppAdmin.factory('InventoryData', function ($http, $q, BASE_URL) {
        return {
            //POST REQUESTS
            getInventoryList: function (filter) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Inventory/GetInventoryList', filter).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getReservedInventoryList: function (filter) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Inventory/GetReservedInventoryList', filter).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            transferStock: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Inventory/TransferStock', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            adjustStock: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Inventory/AdjustStock', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },

            //GET REQUESTS
            getDeliveryCenters: function () {
                var deferred = $q.defer();
                $http.get(BASE_URL + '/Inventory/GetDeliveryCenters').success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getStockDeliveryCenters: function (data) {
                var deferred = $q.defer();              
                $http.post(BASE_URL + '/Inventory/GetStockDeliveryCenters/', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getDeliveryCenterInventory: function (filter) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Location/GetFilterInventory', filter).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            //getInventoryByLocation: function (stockCode, deliveryCenterId, inventoryType) {
            //    var deferred = $q.defer();
            //    var data = { stockCode: stockCode, deliveryCenterId: deliveryCenterId, inventoryType: inventoryType }
            //    $http.post(BASE_URL + '/Inventory/GetInventoryByLocation/', data).success(deferred.resolve).error(deferred.reject);
            //    return deferred.promise;
            //},
            getSourceLocationName: function (Id) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Inventory/GetLocationName/' + Id).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getDestinationLocationName: function (Id) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Inventory/GetLocationName/' + Id).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getReserveStock: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Inventory/GetReserveStock/', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            freeReserveStock: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Inventory/FreeReserveStock/', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getWarehouseAndInventoryType: function ()
            {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Inventory/GetWarehouseAndInventoryType/').success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            saveInventoryPool: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Inventory/SaveInventoryPool/', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getInventoryPoolList: function (filter)
            {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Inventory/GetInventoryPoolList/', { filter : filter}).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getInventoryPoolDetail: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Inventory/GetInventoryPoolDetail/',data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
             getOneViewPoolInventory: function (recordId, filter) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Inventory/GetOneViewPoolInventory/', { recordId: recordId, filter: filter}).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },

             getStockForTransfer: function (stockCode) {
                 var deferred = $q.defer();
                 $http.get(BASE_URL + '/Inventory/GetStockForTransfer?stockCode=' + stockCode).success(deferred.resolve).error(deferred.reject);
                 return deferred.promise;
             },

             stockTransfer: function (data) {
                 var deferred = $q.defer();
                 $http.post(BASE_URL + '/Inventory/StockTransfer/', data).success(deferred.resolve).error(deferred.reject);
                 return deferred.promise;
             },
             getStockTakeList: function (filter) {
                 var deferred = $q.defer();
                 $http.post(BASE_URL + '/Inventory/GetStockTakeList', filter).success(deferred.resolve).error(deferred.reject);
                 return deferred.promise;
             },
             updateStockTakeItemStatus: function (data) {
                 var deferred = $q.defer();
                 $http.post(BASE_URL + '/Inventory/UpdateStockTakeItemStatus/', data).success(deferred.resolve).error(deferred.reject);
                 return deferred.promise;
            }, deleteInventoryPool: function (id) {
                var deferred = $q.defer();
                 $http.get(BASE_URL + '/Inventory/DeleteInventoryPool/'+ id).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
             },
            getStockForAdjust: function (productId, deliveryCenterId, locationName, inventoryType,stockCode) {
                var deferred = $q.defer();
                var data = { productId: productId, deliveryCenterId: deliveryCenterId, locationName: locationName, inventoryType: inventoryType, stockCode: stockCode }
                $http.post(BASE_URL + '/Inventory/GetStockForAdjust', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getStockAdjustGrid: function (productId, deliveryCenterId, locationName, inventoryType, stockCode) {
                var deferred = $q.defer();
                var data = { productId: productId, deliveryCenterId: deliveryCenterId, locationName: locationName, inventoryType: inventoryType, stockCode: stockCode }
                $http.post(BASE_URL + '/Inventory/GetStockAdjustGrid', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            createStockTransfer: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Inventory/CreateStockTransfer', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            filterStockTransferList: function (filter) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Inventory/FilterStockTransferList', filter).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            addItemforStocktransfer: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Inventory/AddItemsForStockTransfer/', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            showStatusUpdate: function (currentStatus, documentType) {
                var deferred = $q.defer();
                var data = { currentStatus: currentStatus, documentType: documentType }
                $http.post(BASE_URL + '/Inventory/ShowStatusUpdate', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            updateStockTransferStatus: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Inventory/UpdateStockTransferStatus/', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getProductForTransfer: function (productId, deliveryCenterId) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Inventory/GetproductsForTransfer/', { productId: productId, deliveryCenterId: deliveryCenterId }).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            deleteStockTransferLine: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Inventory/DeleteStockTransferLine/', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getStockTransferLineforUpdate: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Inventory/GetStockTransferLineForUpdate/', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            updateStockTranferLine: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Inventory/UpdateStockTranferLine/', { transferLine: data }).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getInventoryType: function () {
                var deferred = $q.defer();
                $http.get(BASE_URL + '/Purchase/GetInventoryType/').success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getReturnBinInventory: function (filter) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Inventory/GetReturnBinInventory/', { filter: filter }).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            updateReturnBinInventoryLocation: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Inventory/UpdateReturnBinInventoryLocation/', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            uploadFile: function () {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/DeliveryCenterLocation/').success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getProposedLocationForReturnBinInventory: function (model) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Inventory/GetProposedLocationForReturnBinInventory/', { model: model }).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getStockTransferReceivedOrders: function (stockTransferId) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Inventory/GetStockTransferReceivedOrders/', { stockTransferId: stockTransferId }).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getStockTransferReceiveLines: function (stockTransferId) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Inventory/GetStockTransferReceivedLines/', { stockTransferId: stockTransferId }).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            updateStockTransferReceiveLines: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Inventory/UpdateStockTransferReceivedLines/', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            setLocationsForBarCodePrint: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/DeliveryCenterLocation/SetLocationsForBarCodePrint/', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            filterStockRequestList: function (filter) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Inventory/FilterStockRequestList', filter).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            createStockRequest: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Inventory/CreateStockRequest', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getStockForStockRequest: function (productId) {
                var deferred = $q.defer();
                $http.get(BASE_URL + '/Inventory/GetStockForStockRequest?productId=' + productId).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            addItemforStockRequest: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Inventory/AddItemForStockRequest/', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            updateStockRequestLine: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Inventory/UpdateStockRequestLine/', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            deleteStockRequestLine: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Inventory/DeleteStockRequestLine/', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            updateStockRequestStatus: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Inventory/UpdateStockRequestStatus/', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getStockRequstLineForApprove: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Inventory/GetStockRequestLineForApprove/', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            approveStockRequest: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Inventory/ApproveStockRequest/', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getStockTransferMismatchedLines: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Inventory/GetStockTransferMismatchedLines/', data).success(deferred.resolve).error(deferred.reject);                
                return deferred.promise;
            },
            resolveStockTransferMismatch: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Inventory/ResolveStockTransferMisMatch/', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            filterStockTransferMismatchList: function (filter) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Inventory/FilterStockTransferMisMatch', filter).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            generatePutawayList: function (model) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Inventory/GeneratePutawayList/', { model: model }).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            filterPutawayList: function (filter) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Inventory/FilterPutawayList', filter).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            updatePutawayListStatus: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Inventory/UpdatePutawayListStatus/', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getUserMappedDeliveryCenters: function () {
                var deferred = $q.defer();
                $http.get(BASE_URL + '/Inventory/GetUserMappedDeliveryCenters').success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            uploadStockTake: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Inventory/UploadStockTake/', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            updateStockTakeStatus: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Inventory/UpdateStockTakeStatus/', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
        }
    })
}());