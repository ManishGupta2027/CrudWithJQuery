(function () {
    'use strict';
    btAppAdmin.directive('futuredatetimez', function () {

        var date = new Date();
        date.setDate(moment(date.getDate()));
         return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, element, attrs, ngModelCtrl) {
                element.datetimepicker({
                    dateFormat: 'dd/MM/yyyy',//DOMAIN_SETTINGS.SYSDATETIMEFORMAT,
                    //language: 'en',
                    pick12HourFormat: true,
                    //startDate: date,
                    minDate:0
                }).on('changeDate', function (e) {
                    //e.date.setHours(e.date.getHours());
                    //e.date.setMinutes(e.date.getMinutes() - 30);
                    //alert(e.date.toLocaleDateString('en-GB'))
                    //alert(e.date.toUTCString())
                    $('.bootstrap-datetimepicker-widget').hide();
                    var dd = e.localDate.getDate();
                    var mm = e.localDate.getMonth() + 1; //January is 0!
                    var yyyy = e.localDate.getFullYear();
                    var hh = e.localDate.getHours();
                    var min = e.localDate.getMinutes();

                    if (dd < 10) {
                        dd = '0' + dd
                    }

                    if (mm < 10) {
                        mm = '0' + mm
                    }
                    if (hh < 10) {
                        hh = '0' + hh
                    }
                    if (min < 10) {
                        min = '0' + min
                    }
                    e.date = yyyy + '-' + mm + '-' + dd + ' ' + hh + ':' + min;
                    //e.date = moment(e.localDate).format(DOMAIN_SETTINGS.SYSDATETIMEFORMAT)
                    ngModelCtrl.$setViewValue(e.date);
                    scope.$apply();
                });
            }
        };
    });
}());