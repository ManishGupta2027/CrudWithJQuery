(function () {
    'use strict';

    // this is not working in this version later test
    //myApp.constant('TRAY_CONSTANTS', {
    //    'SUCCESS_MSG': 'Operation completed successfully.',
    //    'DELETE_MSG': 'Deleted successfully.',
    //    'MANDATORY_MSG': 'Please fill all mandatory fields.',
    //    'ERROR_MSG': 'An error occurred. Please try again.'
    //});

    //myApp.controller('trayCtrl', trayCtrl);
    //trayCtrl.$inject = ['$scope', '$http', '$timeout', '$window', 'alerts', 'TRAY_CONSTANTS'];


    angular.module('myApp')
        .constant('TRAY_CONSTANTS', {
            'SUCCESS_MSG': 'Operation completed successfully.',
            'DELETE_MSG': 'Deleted successfully.',
            'MANDATORY_MSG': 'Please fill all mandatory fields.',
            'ERROR_MSG': 'An error occurred. Please try again.'
        })
        .controller('trayCtrl', trayCtrl);

    trayCtrl.$inject = ['$scope', '$http', '$timeout', '$window', 'alerts', 'TRAY_CONSTANTS'];

    function trayCtrl($scope, $http, $timeout, $window, alerts, TRAY_CONSTANTS) {
        var pm = this;
        pm.tray = {
            DeliveryCenterId: '',
            RecordId: null,
            Name: '',
            Code: '',
            BarCode: '',
            Base64: ''//to show barcode img only
        };

        pm.pageFilter = {};
        pm.trays = [];
        pm.warehouses = [];
        pm.saveTray = saveTray;
        pm.updateTray = updateTray;
        pm.initTrayDetail = initTrayDetail;
        pm.initTrays = initTrays;
        pm.deleteTray = deleteTray;
        pm.getTrayList = getTrayList;
        pm.initAddTray = initAddTray;

        pm.trayforBarCodePrint = [];
        pm.showBulkBarCodePopUp = showBulkBarCodePopUp;
        pm.clearSelectedTray = clearSelectedTray;
        pm.selectAllLocation = selectAllLocation;
        pm.removeAlltrayForBarCode = removeAlltrayForBarCode;
        pm.selectSingletray = selectSingletray;
        pm.applytoAllNoofBarCode = applytoAllNoofBarCode;
        pm.printBarCode = printBarCode;

        function initAddTray() {
            $http.get('/Tray/GetAllWarehouses') //Get all Warehouses
                .then(function (response) {
                    pm.warehouses = response.data;
                })
                .catch(function (error) {
                    alerts.error(TRAY_CONSTANTS.ERROR_MSG);
                });
        }
        function initTrayDetail(model) {
            pm.initAddTray();
            // console.log(model);
            pm.tray.Name = model.name;
            pm.tray.BarCode = model.barCode;
            pm.tray.Base64 = model.base64;
            pm.tray.RecordId = model.recordId;
            pm.tray.DeliveryCenterId = model.deliveryCenterId;
            pm.tray.Code = model.code;
        }
        /*
            * Initialize trays list (for list view)
        */
        function initTrays() {
            pm.initAddTray();
            $http.post('/Tray/GetTrays') // Assumes an API to get all trays
                .then(function (response) {
                     console.log(response.data);

                    pm.pageFilter.currentPage = 1;
                    pm.pageFilter.pageSize = 0;

                    pm.trays = response.data;
                    if (response != null && response != undefined && response.data.length > 0) {
                        $scope.total = response.data[0].totalRecord;
                        $scope.totalRecord = response.data[0].totalRecord;
                        $scope.pageSize = response.data[0].pageSize;
                        angular.forEach(pm.trays, function (loc) {
                            loc.isChecked = false;
                        });
                    }
                })
                .catch(function (error) {
                    alerts.error(TRAY_CONSTANTS.ERROR_MSG);
                });
        }
        function getTrayList(model) {
            $http.post('/Tray/GetTrays', model) // Assumes an API to get all trays
                .then(function (response) {
                    pm.trays = response.data;
                    if (response != null && response != undefined && response.data.length > 0) {
                        $scope.total = response.data[0].totalRecord;
                        $scope.totalRecord = response.data[0].totalRecord;
                        $scope.pageSize = response.data[0].pageSize;
                    }
                    else {
                        $scope.total = 0;
                        $scope.totalRecord = 0;
                        $scope.pageSize = 1;
                        $scope.page = 1;
                    }
                })
                .catch(function (error) {
                    alerts.error(TRAY_CONSTANTS.ERROR_MSG);
                });
        }

        function saveTray() {
            if (!pm.tray.Name || !pm.tray.DeliveryCenterId) {
                alerts.error(TRAY_CONSTANTS.MANDATORY_MSG);
                return;
            }

            $http.post('/Tray/SaveTray', pm.tray)
                .then(function (response) {

                    if (response.data.isValid == true) {
                       // alerts.success(response.data.message);
                        // Redirect to the Detail view with the tray ID
                        var trayId = response.data.recordId;
                        $window.location.href = '/Tray/DetailTray?id=' + trayId;
                    } else {
                        alerts.error(response.data.message);
                        return;
                    }

                })
                .catch(function (error) {
                    alerts.error(error.data || 'An error occurred while saving the tray.');
                });
        }
        function updateTray() {
            if (!pm.tray.Name || !pm.tray.DeliveryCenterId) {
                alerts.error(TRAY_CONSTANTS.MANDATORY_MSG);
                return;
            }

            $http.put('/Setting/UpdateTray', pm.tray)
                .then(function (response) {

                    if (response.data.isValid == true) {
                        alerts.success(response.data.message);
                        // Redirect to the Detail view with the tray ID
                        var trayId = response.data.recordId;
                        $window.location.href = '/Setting/DetailTray?id=' + trayId;
                    } else {
                        alerts.error(response.data.message);
                        return;
                    }

                })
                .catch(function (error) {
                    alerts.error(error.data || 'An error occurred while saving the tray.');
                });
        }
        /**
        * Delete a tray (for list view)
        */
        function deleteTray(trayId) {

            $http.delete('/Setting/DeleteTray', { params: { id: trayId } })
                .then(function (response) {
                    alerts.success(response.data.message);
                    $window.location.reload();
                })
                .catch(function (error) {
                    alerts.error(TRAY_CONSTANTS.ERROR_MSG);
                });
        }


        function selectAllLocation(tray, selectAll, locationLimit) {
            if (pm.trayforBarCodePrint.length > locationLimit) {
                alerts.error("Max limit of (" + locationLimit + ") exceeded");
                return false;
            }
            if (selectAll == true) {
                angular.forEach(tray, function (loc) {
                    if (pm.trayforBarCodePrint.length < locationLimit) {
                        if (!loc.isChecked) {
                            pm.trayforBarCodePrint.push({ recordId: loc.recordId, code: loc.code, isChecked: true });
                            loc.isChecked = true;
                        }
                    }
                    else {
                        alerts.error("Max limit of (" + locationLimit + ") exceeded");
                        return false;
                    }

                });
            }
            else {
                // $scope.updateLocations = [];
                angular.forEach(tray, function (loc) {
                    angular.forEach(pm.trayforBarCodePrint, function (val) {
                        if (val.recordId.toLowerCase() == loc.recordId.toLowerCase()) {
                            loc.isChecked = false;
                            val.isChecked = false;
                            // $('#' + loc.locationName).prop("checked", false);
                        }
                    });
                });
                pm.selectedLocationList = pm.trayforBarCodePrint;
                pm.trayforBarCodePrint = [];
                angular.forEach(pm.selectedLocationList, function (selloc, key) {
                    if (selloc.isChecked)
                        pm.trayforBarCodePrint.push(selloc);
                });
            }
        }

        function showBulkBarCodePopUp() {
            pm.trayForBarCode = [];
            if (pm.trayforBarCodePrint && pm.trayforBarCodePrint.length > 0) {
                angular.forEach(pm.trayforBarCodePrint, function (loc) {
                    pm.trayForBarCode.push({ code: loc.code, printQty: 1 });
                });
            }
            $("#barCodePopUpModal").modal();
        }
        function applytoAllNoofBarCode() {
            if (pm.trayForBarCode && pm.trayForBarCode.length > 0) {
                angular.forEach(pm.trayForBarCode, function (loc, key) {
                    loc.printQty = pm.trayForBarCode[0].printQty;
                });
            }
        }

        function removeAlltrayForBarCode() {
            if (pm.trayforBarCodePrint != null && pm.trayforBarCodePrint.length > 0) {
                angular.forEach(pm.trays, function (loc) {
                    angular.forEach(pm.trayforBarCodePrint, function (val) {
                        if (loc.recordId.toLowerCase() == val.recordId.toLowerCase()) {
                            loc.isChecked = false;
                        }
                    });
                });
                pm.trayforBarCodePrint = [];
                $scope.selectAll = false;
            }
        }

        function clearSelectedTray(recordId, index) {
            if (pm.trayforBarCodePrint != null && pm.trayforBarCodePrint.length > 0) {
                pm.trayforBarCodePrint.splice(index, 1);
                angular.forEach(pm.trays, function (loc) {
                    if (loc.recordId.toLowerCase() == recordId.toLowerCase()) {
                        loc.isChecked = false;
                    }
                });
            }
            if (pm.trayforBarCodePrint != null && pm.trayforBarCodePrint.length == 0) {
                $scope.selectAll = false;
            }

        }


        function selectSingletray(tray, isSelected, locationLimit) {
            if (pm.trayforBarCodePrint.length > locationLimit) {
                alerts.error("Max limit of (" + locationLimit + ") exceeded");
                return false;
            }
            if (isSelected == true) {
                if (pm.trayforBarCodePrint.length < locationLimit) {
                    pm.trayforBarCodePrint.push({ recordId: tray.recordId, code: tray.code, isChecked: true });
                }
                else {
                    tray.isChecked = false;
                    alerts.error("Max limit of (" + locationLimit + ") exceeded");
                    return false;
                }
            }
            else {
                for (var i = pm.trayforBarCodePrint.length - 1; i >= 0; i--) {
                    if (pm.trayforBarCodePrint[i].recordId == tray.recordId) {
                        pm.trayforBarCodePrint.splice(i, 1);
                    }
                }
            }
            if (pm.trayforBarCodePrint != null && pm.trayforBarCodePrint.length == 0) {
                $scope.selectAll = false;
            }
        }
        function printBarCode(documentType) {
            var data = { tray: pm.trayForBarCode }

            $http.post('/Tray/SetTrayForBarCodePrint', data)
                .then(function (resp) {

                    if (resp.data.result.isValid) {
                        $("#barCodePopUpModal").modal("hide");
                        $(".modal-backdrop").remove();
                        var pdfUrl = "";
                        pm.showPdfReportIframe = true;
                        pdfUrl = "/Content/DownloadPdf?documentType=" + documentType + "&id=";
                        pm.jsReportTitle = "Tray";

                        $("#iframeJSPDF").attr('src', 'about:blank');
                        $("#printJsReportPDF").modal("show");
                        setTimeout(function () {
                            $("#iframeJSPDF").attr("src", pdfUrl + 'a');
                        }, 100);
                    }
                    else {
                        alerts.error(resp.result.message);
                    }
                })
                .catch(function (error) {
                    alerts.error(error.data || 'An error occurred while saving the tray.');
                });
        }


    }

})();