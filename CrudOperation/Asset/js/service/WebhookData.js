(function () {
    'use strict';
    /* Service: WebhookData
     * Defines the methods related to global data across the app
     */
    btAppAdmin.factory('WebhookData', function ($http, $q, BASE_URL) {
        return {
            getWebhookById: function (id) {
                var deferred = $q.defer();
                $http.get(BASE_URL + '/webhooks/getWebhook/'+ id).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            saveWebhook: function (data) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/webhooks/saveWebhook', data).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            deleteWebhook: function (id) {
                var deferred = $q.defer();
                $http.get(BASE_URL + '/api/deleteCustomerGroup/' + id).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getEventByEntity: function (entityType) {
            var deferred = $q.defer();
            $http.get(BASE_URL + '/webhooks/getEventType/' + entityType).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
            },
            getEntityList: function () {
                var deferred = $q.defer();
                $http.get(BASE_URL + '/webhooks/GetEntities' ).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getWebHooksByFilter: function (filter) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/webhooks/getWebhookByFilter/', filter).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getWebhookChangeLog: function (id) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/webhooks/GetWebhookChangeLog/' + id).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            },
            getWebhookFireLog: function (id) {
                var deferred = $q.defer();
                $http.post(BASE_URL + '/webhooks/GetWebhookFireLog/' + id).success(deferred.resolve).error(deferred.reject);
                return deferred.promise;
            }
        };
    });

}());