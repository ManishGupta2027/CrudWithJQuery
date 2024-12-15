(function () {
    'use strict';
    /* Service: OrderData
     * Defines the methods related to global data across the app
     */
    btAppAdmin.factory('OrderData', function ($http, $q, BASE_URL) {
        return {
            getOrders: function (orderFilter) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Order/FilterOrder', orderFilter).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getQueueOrders: function (orderFilter) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/OrderQueue/FilterOrder', orderFilter).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            createPicklist: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Picklist/GeneratePicklist', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            createPO: function (orderModel) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Purchase/CreatePO', orderModel).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getShippingMethods: function () {
                var deferred = $q.defer();
                //var data = { shippingInfo: model, id: id }
                $http.post(BASE_URL + '/ShippingMethod/GetShippingMethods').success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
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

            getForwardOrder: function () {
                var deferred = $q.defer();
                $http.get(BASE_URL + '/Order/GetForwardOrder').success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },

            getProductForwardOrder: function (stockCode) {
                var deferred = $q.defer();
                $http.get(BASE_URL + '/Order/GetProductForwardOrder?stockCode=' + stockCode).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },

            generatePurchaseOrderforForwardOrder: function (model) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Order/GeneratePurchaseOrderforForwardOrder', model).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },

            getForwardOrdersPO: function (customNo) {
                var deferred = $q.defer();
                $http.get(BASE_URL + '/Order/GetForwardOrdersPO?customNo=' + customNo).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },

            pickedProduct: function (productId, picked, picklistId) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/OrderQueue/PickedProduct/', {productId: productId, picked: picked, picklistId: picklistId}).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            createPicklistQueue: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/PicklistV3/GeneratePicklist', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },

            getPackageMasterdata: function () {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Product/GetPackageMasterdata').success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            updatePackageinfoForProduct: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/OrderQueue/UpdateProductPackageinfo', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            updatePackageinfoForOrder: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/OrderQueue/UpdatePackageinfoForOrder', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            updateOrderPacked: function (deliveryPlanIds) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/OrderQueue/UpdateOrderPacked', { deliveryPlanIds: deliveryPlanIds}).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            pickItems: function (picklistId, data) {
                //var postdata = { picklistId: picklistId, products: data };
                var deferred = $q.defer();
                $http.post(BASE_URL + '/OrderQueue/PickedProduct/', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            generateShippingLable: function (orderData) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/OrderQueue/GenerateShippingLable/', orderData).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getCustomerOrder: function (userId, deliveryPlanId) {
                var deferred = $q.defer();
                $http.get(BASE_URL + '/OrderQueue/GetCustomerOrders?userId=' + userId + '&deliveryPlanId=' + deliveryPlanId).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            consolidateOrders: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/OrderQueue/ConsolidateOrders/', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            updateWeightAndBoxCount: function (deliveryNoteId, weight, boxCount) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/OrderQueue/UpdateDeliveryNoteWeightAndBoxCount/', { deliveryNoteId: deliveryNoteId, weight: weight, boxCount: boxCount }).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getPicklistProductOrders: function (picklistId, productIds) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/PickListV3/GetProductOrders/', { picklistId: picklistId, productIds: productIds }).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            moveOrderToException: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/OrderQueue/MoveOrderToException/', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },

            getPickers: function () {
                var deferred = $q.defer();
                $http.get(BASE_URL + '/OrderQueue/WareHousePickers').success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            updatePickListPicker: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/OrderQueue/UpdatePickListPicker/', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            updateTrackingNo: function (deliveryPlanId, trackingNo, shipWithoutTrackingNo) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/OrderQueue/UpdateTrackingNo/', { deliveryPlanId: deliveryPlanId, trackingNo: trackingNo, shipWithoutTrackingNo: shipWithoutTrackingNo }).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            updateSoReference: function (recordId, soReference) {
                var deferred = $q.defer();
                var data = { recordId: recordId, soReference: soReference };
                $http.post(BASE_URL + '/Order/UpdateSOReference', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            generateTodayDespatchManifest: function () {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/OrderQueue/GenerateTodayDespatchManifest').success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            editShippingAddress: function (orderId, address) {
                var deferred = $q.defer();
                var data = { orderId: orderId, address: address};
                $http.post(BASE_URL + '/OrderQueue/EditShippingAddress', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            saveInvoiceDate: function (invoiceId, domainId, invoiceDate, invoiceDueDate, salesChannel) {
                var deferred = $q.defer();
                var data = { invoiceId: invoiceId, domainId: domainId, invoiceDate: invoiceDate, invoiceDueDate: invoiceDueDate, salesChannel: salesChannel };
                $http.post(BASE_URL + '/OrderQueue/SaveInvoiceDate', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getSONotes: function (orderId) {
                var deferred = $q.defer();
                $http.get(BASE_URL + '/Order/GetSONotes?orderId='+orderId).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            removeSONote: function (noteId, orderId) {
                var deferred = $q.defer();
                $http.get(BASE_URL + '/Order/RemoveSONote?noteId=' + noteId + '&orderId=' + orderId).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            addSONote: function (model) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Order/AddSONote',model).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            uploadAttachment: function (model) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Order/UploadAttachment', model).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            removeAttachment: function (model) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Order/RemoveAttachment', model).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getAttachments: function (id) {
                var deferred = $q.defer();
                $http.get(BASE_URL + '/Order/GetAttachments?orderId=' + id).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            updateDeliveryPlanFulfilmentType: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/OrderQueue/UpdateDeliveryPlanFulfilmentType', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            filterInvoiceList: function (filter) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/OrderQueue/FilterInvoices', filter).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            updateShippingMethod: function (deliveryPlanId, shippingMethod) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/OrderQueue/UpdateShippingMethod', { deliveryPlanId: deliveryPlanId, shippingMethod: shippingMethod}).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getRuleShippingMethods: function (data) {
                var deferred = $q.defer();
                //var data = { shippingInfo: model, id: id }
                $http.post(BASE_URL + '/ShippingMethod/GetRuleShippingMethods', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            editCustomerEmail: function (orderId, customerEmail) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/OrderQueue/EditCustomerEmail', { orderId: orderId, customerEmail: customerEmail }).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            removeForwardOrderLineForPO: function (stockCode) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/Order/RemoveForwardOrderLineForPO', { stockCode: stockCode}).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            moveOrderPickListToNew: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/OrderQueue/MoveOrderPickListToNew/', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getOrdersReviewForPO: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/OrderQueue/GetOrdersReviewForPO/', { orders: data}).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            generatePOForSupplier: function (model) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/OrderQueue/GeneratePOForSupplier/', model).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getGeneratedDropshipAndBackOrderPO: function (filter) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/OrderQueue/GetGeneratedDropshipAndBackOrderPO/',filter).success(deferred.resolve);
                return deferred.promise;
            },
            sendDropshipOrderPoToSupplier: function (purchaseOrderId) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/OrderQueue/SendDropshipOrderPoToSupplier/', { purchaseOrderId: purchaseOrderId }).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            filterCancelOrders: function (orderFilter) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/OrderQueue/FilterCancelOrder', orderFilter).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
             filterOrdersReviewForPO: function (filter) {
                var deferred = $q.defer();
                 $http.post(BASE_URL + '/OrderQueue/FilterPendingOrderForPO', filter).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            uploadTrackingNo: function (model) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/OrderQueue/UploadTrackingNo', model).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            updateCarrier: function (deliveryPlanId, carrierKey, carrierServiceKey) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/OrderQueue/UpdateCarrier', { deliveryPlanId: deliveryPlanId, carrierKey: carrierKey, carrierServiceKey: carrierServiceKey }).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            }, moveOrdersToMongo: function (totalOrdersToMove) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/OrderQueue/MoveOrdersToMongo', { totalOrdersToMove: totalOrdersToMove }).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            splitMixedOrderDeliveryPlan: function (model) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/OrderQueue/SplitMixedOrderDeliveryPlan', model).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            moveToBackOrderFromDropShip: function (model) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/OrderQueue/MoveDeliveryPlanDropShipToBackOrder', model).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
        };
    });
}());