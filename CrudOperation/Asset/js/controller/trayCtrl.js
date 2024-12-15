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


        //traygroup start
        pm.trayGroup = {
            DeliveryCenterId: '',
            RecordId: null,
            Name: '',
            Capacity: '',
            //Code: '',
            BarCode: '',
            // Base64: ''//to show barcode img only
            TrayIds: [],
            Trays: []
        };
        pm.getTrayGroupList = getTrayGroupList;
        pm.trayGroups = []
        pm.initTrayGroups = initTrayGroups;
        pm.deleteTrayGroup = deleteTrayGroup;
        pm.saveTrayGroup = saveTrayGroup;
        pm.addTray = addTray;
        //pm.removeTray = removeTray;
        //pm.trayNotAdded = trayNotAdded;
        pm.removeSelectedTray = removeSelectedTray;
        pm.initTrayGroup = initTrayGroup;
        pm.initTrayGroupDetail = initTrayGroupDetail;
        pm.updateTrayGroup = updateTrayGroup;
        pm.getAvailableTrays = getAvailableTrays;
        // New tray name input
        //pm.newTrayName = '';
        //pm.traySearch = '';
        //traygroup end

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


        function initTrayGroups(deliveryCenterId) {
            pm.pageFilter.deliveryCenterId = deliveryCenterId;
            var data = { deliveryCenterId: deliveryCenterId }
            getTrayGroupList(data);
        }
        function getTrayGroupList(model) {
            $http.post('/Tray/TrayGroupList', model) // Assumes an API to get all trays
                .then(function (response) {
                    pm.trayGroups = response.data;
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
        function deleteTrayGroup(trayId) {


            $http.delete('/Tray/DeleteTrayGroup', { params: { id: trayId } })
                .then(function (response) {
                    alerts.success(response.data.message);
                    $window.location.reload();
                })
                .catch(function (error) {
                    alerts.error(TRAY_CONSTANTS.ERROR_MSG);
                });
        }

        function saveTrayGroup(deliveryCenterId) {
            pm.trayGroup.DeliveryCenterId = deliveryCenterId
            if (!pm.trayGroup.Name || !pm.trayGroup.Capacity) {
                alerts.error(TRAY_CONSTANTS.MANDATORY_MSG);
              //  alter(TRAY_CONSTANTS.MANDATORY_MSG);
                return;
            }
            if (!pm.trayGroup.TrayIds.length > 0) {
                alerts.error("Please add tray.");
                return;
            }
           // console.log(pm.trayGroup);
            $http.post('/Tray/SaveTrayGroup', pm.trayGroup)
                .then(function (response) {

                    if (response.data.isValid == true) {
                       // alerts.success(response.data.message);
                        // Redirect to the Detail view with the tray ID
                        var trayId = response.data.recordId;
                        $window.location.href = '/Tray/TrayGroupDetail?id=' + trayId;
                    } else {
                        alerts.error(response.data.message);
                        return;
                    }

                })
                .catch(function (error) {
                    alerts.error(error.data || 'An error occurred while saving the tray.');
                });
        }
        // Filter trays that are not already added
        //function trayNotAdded(tray) {
        //    return !pm.trayGroup.TrayId.some(function (addedTray) {
        //        return addedTray.recordId === tray.recordId;
        //    });
        //};
        // Add Tray Name Method
        function addTray(tray) {

            pm.trayGroup.TrayIds.push(tray.recordId);
            pm.trayGroup.Trays.push(tray);
            pm.trays = pm.trays.filter(x => x.recordId !== tray.recordId);
        };

        function removeSelectedTray(tray, index) {
            if (pm.trayGroup.TrayIds != null && pm.trayGroup.TrayIds.length > 0) {
                pm.trayGroup.TrayIds.splice(index, 1);

            }
            if (pm.trayGroup.Trays != null && pm.trayGroup.Trays.length > 0) {
                pm.trayGroup.Trays.splice(index, 1);

                // Add the tray back to pm.trays
                pm.trays.push(tray);

            }
        }
        // Remove a tray from the tray group
        //function removeTray(tray) {
        //    pm.trayGroup.TrayIds = pm.trayGroup.TrayIds.filter(function (addedTray) {
        //        return addedTray.recordId !== tray.recordId;
        //    });
        //};
    
        function initTrayGroup(deliveryCenterId) {
            var a = deliveryCenterId;
            pm.pageFilter.deliveryCenterId = deliveryCenterId;
            //$http.get('/Tray/GetTrayList') //Get all Warehouses
            //    .then(function (response) {
            //        pm.trays = response.data;
            //    })
            //    .catch(function (error) {
            //        alerts.error(TRAY_CONSTANTS.ERROR_MSG);
            //    });
            var model = { deliveryCenterId: deliveryCenterId }
            getAvailableTrays(model);
            //$http.post('/Tray/GetAvailableTrays', model) // Assumes an API to get all trays
            //    .then(function (response) {
            //        pm.trays = response.data;
            //        if (response != null && response != undefined && response.data.length > 0) {
            //            $scope.total = response.data[0].totalRecord;
            //            $scope.totalRecord = response.data[0].totalRecord;
            //            $scope.pageSize = response.data[0].pageSize;
            //        }
            //        else {
            //            $scope.total = 0;
            //            $scope.totalRecord = 0;
            //            $scope.pageSize = 1;
            //            $scope.page = 1;
            //        }
            //    })
            //    .catch(function (error) {
            //        //alerts.error(TRAY_CONSTANTS.ERROR_MSG);
            //    });
        }
        function getAvailableTrays(model) {
            $http.post('/Tray/GetAvailableTrays', model) // Assumes an API to get all trays
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
                    //alerts.error(TRAY_CONSTANTS.ERROR_MSG);
                });
        }
        function initTrayGroupDetail(model) {
           // pm.initTrayGroup();
            // console.log(model);
            pm.trayGroup.Name = model.name;
            pm.trayGroup.BarCode = model.barCode;
            pm.trayGroup.Base64 = model.base64;
            pm.trayGroup.RecordId = model.recordId;
            pm.trayGroup.DeliveryCenterId = model.deliveryCenterId;
            pm.trayGroup.Code = model.code;
            pm.trayGroup.Capacity = model.capacity;
            pm.trayGroup.Trays = model.trays;
        }

        function updateTrayGroup() {
            if (!pm.trayGroup.Name || !pm.trayGroup.DeliveryCenterId) {
                alerts.error(TRAY_CONSTANTS.MANDATORY_MSG);
                return;
            }

            $http.put('/Tray/UpdateTrayGroup', pm.trayGroup)
                .then(function (response) {

                    if (response.data.isValid == true) {
                       // alerts.success(response.data.message);
                        // Redirect to the Detail view with the tray ID
                        var trayId = response.data.recordId;
                        $window.location.href = '/Tray/TrayGroupDetail?id=' + trayId;
                    } else {
                        alerts.error(response.data.message);
                        return;
                    }

                })
                .catch(function (error) {
                    alerts.error(error.data || 'An error occurred while saving the tray.');
                });
        }

    }

})();