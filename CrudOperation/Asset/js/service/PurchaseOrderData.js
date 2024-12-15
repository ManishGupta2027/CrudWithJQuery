(function () {
    'use strict';
    /* Service: OrderData
     * Defines the methods related to global data across the app
     */
    btAppAdmin.factory('PurchaseOrderData', function ($http, $q, BASE_URL) {
        return {
            getPurchaseOrders: function (purchaseOrderFilter) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Purchase/FilterOrder', purchaseOrderFilter).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            updatePOStatus: function (statusType, selectedId, note, expectedDate, promisedDeliveryDate,grnNo,pickListNo) {
                var deferred = $q.defer();
                var data = { statusType: statusType, selectedId: selectedId, note: note, expectedDate: expectedDate, promisedDeliveryDate: promisedDeliveryDate, grnNo: grnNo, pickListNo: pickListNo}
                $http.post(BASE_URL + '/Purchase/UpdatePurchaseOrderStatus', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getLine: function (stockCode) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Purchase/GetOrderLineByStockCode', stockCode).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            deleteOrderLine: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Purchase/DeleteOrderLine', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            addOrderLine: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Purchase/AddOrderLine', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            updateGoodReceivedLine: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Purchase/UpdateGoodReceivedLine', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getVendorEmail: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Purchase/GetVendorEmail', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            sendEmail: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Purchase/SendEmail', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getDeliveryCentreLocationName: function (Id) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Purchase/GetDeliveryCenterLocation/' + Id).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
             fetchUpsertSupplierDNote: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Purchase/FetchUpsertSupplierDNote', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
             getSupplierDNoteLines: function (Id) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Purchase/GetSupplierDNoteLines/' + Id).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getGoodReceivedLine: function (Id) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Purchase/GetGoodReceivedLine/' + Id).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getGoodReceivedOrders: function (Id) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Purchase/GetGoodReceivedOrders/' + Id).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getCreatePODetails: function () {
                var deferred = $q.defer();
                $http.get(BASE_URL + '/Purchase/GetCreatePODetails/').success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            createPoWithoutSo: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Purchase/CreatePOWithoutSO', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            saveExchangeRate: function (recordId, exchangeRate) {
                var deferred = $q.defer();
                var data = { recordId: recordId, exchangeRate: exchangeRate }
                $http.post(BASE_URL + '/Purchase/SaveExchangeRate', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            showStatusUpdate : function (currentStatus) {
                var deferred = $q.defer();
                var data = { currentStatus: currentStatus}
                $http.post(BASE_URL + '/Purchase/ShowStatusUpdate', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getInventoryType: function () {
                var deferred = $q.defer();
                $http.get(BASE_URL + '/Purchase/GetInventoryType/').success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },

            saveReceivingTolerance: function (recordId, receivingTolerance) {
                var deferred = $q.defer();
                var data = { recordId: recordId, receivingTolerance: receivingTolerance }
                $http.post(BASE_URL + '/Purchase/SaveReceivingTolerance', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },

            getCartonPackage: function (cartons) {
                var deferred = $q.defer();
                var data = { cartons: cartons }
                $http.post(BASE_URL + '/Purchase/GetCartonPackage', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },

            getSupplierDNotesForReceive: function (purchaseOrderId) {
                var deferred = $q.defer();
                var data = { purchaseOrderId: purchaseOrderId }
                $http.post(BASE_URL + '/Purchase/GetSupplierDNotesForReceive', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            updateNarrative: function (data) {
                var deferred = $q.defer();               
                $http.post(BASE_URL + '/Purchase/updateNarrative', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            saveShippingCost: function (recordId, shippingCost) {
                var deferred = $q.defer();
                var data = { purchaseOrderId: recordId, shippingCost: shippingCost }
                $http.post(BASE_URL + '/Purchase/SaveShippingCost', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },

             updateSoReference: function (recordId, soReference) {
                var deferred = $q.defer();
                var data = { purchaseOrderId: recordId, soReference: soReference }
                $http.post(BASE_URL + '/Purchase/UpdateSOReference', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
             copyPO: function (data) {
                 var deferred = $q.defer();
                 $http.post(BASE_URL + '/Purchase/CopyPO', data).success(deferred.resolve).error(deferred.reject);
                 return deferred.promise;
            },
             getProductsListForTransfer: function (recordId, deliveryCenterId) {
                var deferred = $q.defer();
                var data = { recordId: recordId, deliveryCenterId: deliveryCenterId };
                $http.post(BASE_URL + '/Purchase/GetProductsListForTransfer', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getPONotes: function (orderId) {
                var deferred = $q.defer();
                $http.get(BASE_URL + '/Purchase/GetPONotes?orderId=' + orderId).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            removePONote: function (noteId, orderId) {
                var deferred = $q.defer();
                $http.get(BASE_URL + '/Purchase/RemovePONote?noteId=' + noteId + '&orderId=' + orderId).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            addPONote: function (model) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Purchase/AddPONote', model).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            uploadAttachment: function (model) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Purchase/UploadAttachment', model).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            removeAttachment: function (model) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Purchase/RemoveAttachment', model).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getAttachments: function (id) {
                var deferred = $q.defer();
                $http.get(BASE_URL + '/Purchase/GetAttachments?orderId=' + id).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getPurchaseOrderLineBySKU: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Purchase/GetPurchaseOrderLineBySKU', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            updatePODeliveryCenter: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Purchase/UpdatePoDeliveryCenter', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            updatePoDueDate: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Purchase/UpdatePoDueDate', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            updatePoEstimatedDate: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Purchase/UpdatePoEstimatedDate', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getPOChangeLog: function (purchaseOrderId) {
                var deferred = $q.defer();
                var data = { purchaseOrderId: purchaseOrderId }
                $http.post(BASE_URL + '/Purchase/GetPOChangeLog', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getPOListingGrid: function () {
                var deferred = $q.defer();
                //var data = { productId: productId, deliveryCenterId: deliveryCenterId, locationName: locationName, inventoryType: inventoryType, stockCode: stockCode }
                var data = {};
                $http.post(BASE_URL + '/Purchase/POListingGrid', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getSupplierPOFeeds: function (purchaseOrderFilter) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Purchase/FilterSupplierPOFeeds', purchaseOrderFilter).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getSuppliersForFeed: function () {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Purchase/GetSupplierForPOFeed').success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            generateManualFeed: function (ids) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Purchase/GenerateManualFeed', ids).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getGoodsWithBatchGroupForReceive: function (Id) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Purchase/GetGoodsWithBatchGroupForReceive/' + Id).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            updateGoodReceivedLotLine: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Purchase/UpdateGoodReceivedLotLine', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getGoodReceivedLotOrders: function (Id) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Purchase/GetGoodReceivedLotOrders/' + Id).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            unallocateReservedSalesOrder: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Purchase/UnallocateReservedSalesOrder', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getEmailPreviewForPOFeed: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Purchase/GetEmailPreviewForPOFeed', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            sendPOFeedEmail: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Purchase/SendPOFeedEmail', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
        };
    });


}());