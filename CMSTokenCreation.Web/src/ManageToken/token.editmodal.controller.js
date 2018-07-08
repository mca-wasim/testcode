(function () {
    'use strict';

    angular
        .module('EvolentApp')
        .controller('tokeneditmodalcontroller', tokeneditmodalcontroller);

    tokeneditmodalcontroller.$inject = ['$uibModalInstance', '$timeout', '$log', '$scope', 'commonData', 'manageTokenService', 'moment', 'user', 'toastr', 'mainservice', 'NgTableParams', '$rootScope', 'commonservice', '$document'];

    function tokeneditmodalcontroller($uibModalInstance, $timeout, $log, $scope, commonData, manageTokenService, moment, user, toastr, mainservice, NgTableParams, $rootScope, commonservice, $document) {
        /* jshint validthis:true */
        var vm = this;

        vm.Statuses = [
            {
                name: '--Select--',
                value: ''
            },
            {
                name: 'active',
                value: 'active'
            }, {
                name: 'inactive',
                value: 'inactive'
            }];  

        vm.title = 'tokeneditmodalcontroller';
        vm.save = updateTokenData;
        vm.cancel = cancel;
        vm.update = commonData.Is_Update;

        vm.tokenModule = 'Edit Contact';
        vm.cancelText = 'Cancel'

        vm.data = [];
        vm.updateModel = [];
        vm.TokenRequestor = null;

        activate();
        var date = new Date();
        // $scope.ModifiedDate = date.getFullYear() + '-' + ('0' + (date.getMonth() + 1)).slice(-2) + '-' + ('0' + date.getDate()).slice(-2);

        $document.on('keydown', function (e) {
            if (e.which === 8 && (e.target.nodeName !== "INPUT" && e.target.nodeName !== "SELECT" && e.target.nodeName !== "TEXTAREA")) { // you can add others here inside brackets.
                e.preventDefault();
            }
        });

        function updateTokenData() {
            if ($scope.CMSEditForm.$valid) {

                bindModel();

                manageTokenService.saveContactData(vm.updateModel)
                    .then(function (response) {

                            toastr.success("Contact Updated successfully.");
                            //close the pop-up and reload the ng-table
                            cancel();


                            mainservice.getAllContact().then(function (response) {

                                $rootScope.tableParam = new NgTableParams({ count: 10 },
                                    {
                                        dataset: response.data
                                    });

                                return;
                            });
                        

                        $log.info(vm.data);
                    }, function (error) {
                        if (vm.alerts.length == 1) {
                            vm.alerts.splice(0, 1);
                        }
                        vm.status = 'Error';
                        $log.error(error);
                    })

            }
            else {
                var message = "";

                if ($scope.CMSEditForm.$error.required != undefined) {
                    angular.forEach($scope.CMSEditForm.$error.required, function (item) {
                        if (message == "") {
                            message = "\"" + item.$name + '"  REQUIRED';
                        }
                        else {
                            message = message + "\n" + "\"" + item.$name + '"  REQUIRED';
                        }
                    });
                }
                if ($scope.CMSEditForm.$error.pattern != undefined) {
                    angular.forEach($scope.CMSEditForm.$error.pattern, function (item) {
                        if (message == "") {
                            message = "\"" + item.$name + '"  IS NOT VALID';
                        }
                        else {
                            message = message + "\n" + "\"" + item.$name + '"  IS NOT VALID';
                        }
                    });
                }
                if (message === "") {
                    message = 'ERROR OCCURRED';
                }

                if (message !== "") {
                    alert(message);
                    return false;
                }
            }
        };

        function bindModel() {
            commonData.FirstName = vm.data.FirstName;
            commonData.LastName = vm.data.LastName;
            commonData.PhoneNumber = vm.data.PhoneNumber;
            commonData.Status = vm.data.Status;
            commonData.Email = vm.data.Email;
            commonData.ContactId = vm.data.ContactId;
            vm.updateModel = commonData;
        }


        function activate() {

            manageTokenService.getTokenData(commonData.row)
                .then(function (response) {
                    vm.data = response.data;

                    // vm.tokenModule = vm.tokenModule + " [" + vm.data.TokenKey + "]";


                    //$scope.ModifiedDate = utcDateFormatter(vm.data.ChangeDate);
                    $log.info(vm.data);
                }, function (error) {
                    if (vm.alerts.length == 1) {
                        vm.alerts.splice(0, 1);
                    }
                    vm.status = 'Error';
                    $log.error(error);
                })

        };


        function cancel() {
            $uibModalInstance.dismiss('cancel');
        };

        //Date Formater
        function utcDateFormatter(value) {
            if (value) {
                var date = moment(value).format("YYYY-MM-DD HH:mm:ss");
                var gmtDateTime = moment.utc(date, "YYYY-MM-DD HH:mm:ss")
                return gmtDateTime.local().format('MM/DD/YYYY h:mm A');
            } else {
                return '';
            }
        }
    }
})();
