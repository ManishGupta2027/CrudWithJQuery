(function () {
    'use strict';

    /**
     * autocomplete
     * Autocomplete directive for AngularJS
     * By Daryl Rowland
     * https://github.com/darylrowland/angucomplete
     */

    angular.module('btAutoComplete', [])
        .directive('btAutoComplete', function ($parse, $http, $sce, $timeout) {
            return {
                restrict: 'EA',
                scope: {
                    "id": "@id",
                    "placeholder": "@placeholder",
                    "selectedObject": "=selectedobject",
                    "url": "@url",
                    "dataField": "@datafield",
                    "titleField": "@titlefield",
                    "descriptionField": "@descriptionfield",
                    "imageField": "@imagefield",
                    "imageUri": "@imageuri",
                    "inputClass": "@inputclass",
                    "userPause": "@pause",
                    "localData": "=localdata",
                    "searchFields": "@searchfields",
                    "minLengthUser": "@minlength",
                    "matchClass": "@matchclass"
                },
                // template: '<div class="autocomplete-holder"><input id="{{id}}_value" ng-keyup="$event.keyCode == 13 && headerSearch()"  ng-model="searchStr" type="text" placeholder="{{placeholder}}" class="{{inputClass}}" onmouseup="this.select();" ng-focus="resetHideResults()" ng-blur="hideResults()" />  <input type="button" ng-click="headerSearch()" class="search-icon-grey sprite" /> <div id="{{id}}_dropdown" class="autocomplete-dropdown" ng-if="showDropdown"><div class="autocomplete-searching" ng-show="searching">Searching...</div><div class="autocomplete-searching" ng-show="!searching && (!results || results.length == 0)">No results found</div><div class="autocomplete-row" ng-repeat="result in results" ng-mouseover="hoverRow()" ng-class="{\'autocomplete-selected-row\': $index == currentIndex}"><a href="{{result.originalObject.detailUrl}}"><div ng-if="imageField" class="autocomplete-image-holder"><img ng-if="result.image && result.image != \'\'" ng-src="{{result.image}}" class="autocomplete-image"/><div ng-if="!result.image && result.image != \'\'" class="autocomplete-image-default"></div></div><div class="autocomplete-title" ng-if="matchClass" ng-bind-html="result.title"></div><div class="autocomplete-title" ng-if="!matchClass">{{ result.title }}</div><div ng-if="result.description && result.description != \'\'" class="autocomplete-description">{{result.description}}</div></a></div></div></div>',
                template: '<div class="autocomplete-holder">' +
                                '<input id="{{id}}_value"  ng-model="searchStr" type="text" placeholder="{{placeholder}}" class="{{inputClass}}" onmouseup="this.select();" ng-focus="resetHideResults()" ng-blur="hideResults()" />' +
                                //'<input type="button" ng-click="headerSearch()" class="search-icon-grey sprite" />' +
                                '<div id="{{id}}_dropdown" class="autocomplete-dropdown" ng-if="showDropdown">' +
                                    '<div class="autocomplete-searching" ng-show="searching">Searching...</div>' +
                                    '<div class="autocomplete-searching" ng-show="!searching && (!results || results.length == 0)">No results found</div>' +
                                    '<div ng-repeat="result in results"  class="resultData" ng-click="selectedObject=result" >' +
                                        //'<div ng-if="result.originalObject.id==0"><h4>{{result.originalObject.headerTitle}}</h4></div>' +
                                        '<div class="autocomplete-row"  ng-mouseover="hoverRow()" ng-class="{\'autocomplete-selected-row\': $index == currentIndex}">' +
                                          // '<a  href="#" >' +
                                                //'<div ng-if="imageField" class="autocomplete-image-holder">' +
                                                //    '<img ng-if="result.image && result.image != \'\'" ng-src="{{result.image}}" class="autocomplete-image"/>' +
                                                //    '<div ng-if="!result.image && result.image != \'\'" class="autocomplete-image-default"></div>' +
                                                //'</div>' +
                                                '<div class="autocomplete-title" ng-click="selectResult(result)" ng-if="matchClass" ng-bind-html="result.title">{{result.title}}</div>' +
                                                '<div class="autocomplete-title" ng-if="!matchClass">{{ result.title }}</div>' +
                                                //'<div ng-if="result.title && result.title != \'\'" class="autocomplete-description">{{result.title}}</div>' +
                                            //'</a>' +
                                         '</div>' +
                                    '</div>' +
                                 '</div>' +
                              '</div>',

                link: function ($scope, elem, attrs) {
                    $scope.lastSearchTerm = null;
                    $scope.currentIndex = null;
                    $scope.justChanged = false;
                    $scope.searchTimer = null;
                    $scope.hideTimer = null;
                    $scope.searching = false;
                    $scope.pause = 500;
                    $scope.minLength = 3;
                    $scope.searchStr = null;
                    $scope.searchRecordId = "";

                    if ($scope.minLengthUser && $scope.minLengthUser != "") {
                        $scope.minLength = $scope.minLengthUser;
                    };

                    if ($scope.userPause) {
                        $scope.pause = $scope.userPause;
                    };

                    $scope.isNewSearchNeeded = function (newTerm, oldTerm) {
                        return newTerm.length >= $scope.minLength && newTerm != oldTerm;
                    };
                    $scope.headerSearch = function () {

                        if ($scope.searchStr != null && $scope.searchStr != "") {
                            window.location = "/search?q=" + $scope.searchStr;
                        }
                    };
                    $scope.processResults = function (responseData, str) {
                        if (responseData && responseData.length > 0) {
                            $scope.results = [];
                            //console.log($scope.titleField);
                            var titleFields = [];
                            if ($scope.titleField && $scope.titleField != "") {
                                titleFields = $scope.titleField.split(",");
                            }
                            //console.log(responseData.length);
                            for (var i = 0; i < responseData.length; i++) {
                                // Get title variables
                                var titleCode = [];
                                //console.log(titleFields.length);
                                for (var t = 0; t < titleFields.length; t++) {
                                    titleCode.push(responseData[i][titleFields[t]]);
                                }
                                //console.log(titleCode);
                                var description = "";
                                if ($scope.descriptionField) {
                                    description = responseData[i][$scope.descriptionField];
                                }
                                //console.log(description);
                                var imageUri = "";
                                if ($scope.imageUri) {
                                    imageUri = $scope.imageUri;
                                }
                                //console.log(imageUri);
                                var image = "";
                                if ($scope.imageField) {
                                    image = imageUri + responseData[i][$scope.imageField];
                                }

                                var text = titleCode.join(' ');
                                if ($scope.matchClass) {
                                    var re = new RegExp(str, 'i');
                                    //console.log(re);
                                    //var strPart = text.match(re)[0];
                                    var strPart = text.match(re);
                                    if (strPart === null) {
                                        text = $sce.trustAsHtml(text);
                                    } else {
                                        text = $sce.trustAsHtml(text.replace(re, '<span class="' + $scope.matchClass + '">' + strPart[0] + '</span>'));
                                    }
                                }
                                //console.log("after image");
                                var resultRow = {
                                    title: text,
                                    description: description,
                                    image: image,
                                    originalObject: responseData[i]
                                }
                                //console.log("populated result row");
                                //console.log($scope.results.length);
                                $scope.results[$scope.results.length] = resultRow;
                            }


                        } else {
                            $scope.results = [];
                        }
                    };

                    $scope.searchTimerComplete = function (str) {
                        // Begin the search

                        if (str.length >= $scope.minLength) {
                            if ($scope.localData) {
                                var searchFields = $scope.searchFields.split(",");

                                var matches = [];

                                for (var i = 0; i < $scope.localData.length; i++) {
                                    var match = false;

                                    for (var s = 0; s < searchFields.length; s++) {
                                        match = match || (typeof $scope.localData[i][searchFields[s]] === 'string' && typeof str === 'string' && $scope.localData[i][searchFields[s]].toLowerCase().indexOf(str.toLowerCase()) >= 0);
                                    }

                                    if (match) {
                                        matches[matches.length] = $scope.localData[i];
                                    }
                                }

                                $scope.searching = false;
                                $scope.processResults(matches, str);

                            } else {
                                $http.get($scope.url + str, {}).
                                    success(function (responseData, status, headers, config) {
                                        $scope.searching = false;
                                        $scope.processResults((($scope.dataField) ? responseData[$scope.dataField] : responseData), str);
                                    }).
                                    error(function (data, status, headers, config) {
                                        console.log("error");
                                    });
                            }
                        }
                    };

                    //$scope.hideResults = function () {
                    //    $scope.hideTimer = $timeout(function () {
                    //        $scope.showDropdown = false;
                    //    }, 500);
                    //};

                    //$scope.resetHideResults = function () {
                    //    if ($scope.hideTimer) {
                    //        $timeout.cancel($scope.hideTimer);
                    //    };
                    //};

                    $scope.hoverRow = function (index) {
                        $scope.currentIndex = index;
                    };

                    $scope.keyPressed = function (event) {
                        if (!(event.which == 38 || event.which == 40 || event.which == 13)) {
                            if (!$scope.searchStr || $scope.searchStr == "") {
                                $scope.showDropdown = false;
                                $scope.lastSearchTerm = null;
                            } else if ($scope.isNewSearchNeeded($scope.searchStr, $scope.lastSearchTerm)) {
                                $scope.lastSearchTerm = $scope.searchStr;
                                $scope.showDropdown = true;
                                $scope.currentIndex = -1;
                                $scope.results = [];

                                if ($scope.searchTimer) {
                                    $timeout.cancel($scope.searchTimer);
                                }

                                $scope.searching = true;

                                $scope.searchTimer = $timeout(function () {
                                    $scope.searchTimerComplete($scope.searchStr);
                                }, $scope.pause);
                            }
                        } else {
                            event.preventDefault();
                        }
                    };

                    $scope.selectResult = function (result) {
                        //window.location = "../products/" + result.originalObject.seName + "-" + result.originalObject.id + ".aspx";
                        //window.location = result.originalObject.detailUrl;
                        console.log(result);
                        if ($scope.matchClass) {
                            result.title = result.title.toString().replace(/(<([^>]+)>)/ig, '');
                        }
                        $scope.searchStr = $scope.lastSearchTerm = result.title;
                        $scope.selectedObject = result;

                        $scope.showDropdown = false;
                        $scope.results = [];

                        //<a href="../products/{{result.originalObject.seName}}-{{result.originalObject.id}}.aspx" >
                        //$scope.$apply();
                    };

                    var inputField = elem.find('input');

                    inputField.on('keyup', $scope.keyPressed);

                    elem.on("keyup", function (event) {
                        if (event.which === 40) {
                            if ($scope.results && ($scope.currentIndex + 1) < $scope.results.length) {
                                $scope.currentIndex++;
                                if ($scope.results[$scope.currentIndex].originalObject.id == 0) {
                                    $scope.currentIndex++;
                                }
                                $scope.$apply();
                                event.preventDefault();
                                event.stopPropagation();
                            }

                            $scope.$apply();
                        } else if (event.which == 38) {
                            if ($scope.currentIndex >= 1) {
                                $scope.currentIndex--;
                                if ($scope.results[$scope.currentIndex].originalObject.id == 0) {
                                    $scope.currentIndex--;
                                }
                                $scope.$apply();
                                event.preventDefault();
                                event.stopPropagation();
                            }

                        } else if (event.which == 13) {
                            if ($scope.results && $scope.currentIndex >= 0 && $scope.currentIndex < $scope.results.length) {
                                $scope.selectResult($scope.results[$scope.currentIndex]);
                                $scope.$apply();
                                event.preventDefault();
                                event.stopPropagation();
                            } else {
                                $scope.results = [];
                                $scope.$apply();
                                event.preventDefault();
                                event.stopPropagation();
                            }

                        } else if (event.which == 27) {
                            $scope.results = [];
                            $scope.showDropdown = false;
                            $scope.$apply();
                        } else if (event.which == 8) {
                            $scope.selectedObject = null;
                            $scope.$apply();
                        }
                    });

                }
            };
        });

}());