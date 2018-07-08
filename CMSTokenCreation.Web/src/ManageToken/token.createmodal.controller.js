(function () {
    'use strict';

    angular
        .module('EvolentApp')
        .controller('tokencreatemodalcontroller', tokencreatemodalcontroller);

    tokencreatemodalcontroller.$inject = ['$uibModalInstance', '$timeout', '$log', '$scope', 'commonData', 'manageTokenService', 'user', 'mainservice', 'toastr', 'NgTableParams', '$rootScope', 'commonservice', '$document'];

    function tokencreatemodalcontroller($uibModalInstance, $timeout, $log, $scope, commonData, manageTokenService, user, mainservice, toastr, NgTableParams, $rootScope, commonservice, $document) {
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


        vm.title = 'tokencreatemodalcontroller';
        vm.cancel = cancel;
        vm.save = saveTokenData;
        vm.update = commonData.Is_Update;
        vm.tokenModule = 'Create Contact';
        vm.data = [];
        vm.TokenRequestor = null;
        vm.cancelText = 'Cancel'
        activate();

        $document.on('keydown', function (e) {
            if (e.which === 8 && (e.target.nodeName !== "INPUT" && e.target.nodeName !== "SELECT" && e.target.nodeName !== "TEXTAREA")) { // you can add others here inside brackets.
                e.preventDefault();
            }
        });

        vm.saveModel = [];
        function activate() {
            vm.TokenRequestor = commonData.TokenRequestor;
        }

        function cancel() {
            $uibModalInstance.dismiss('cancel');
        };



        function saveTokenData() {
            if ($scope.CMSEditForm.$valid) {

                bindModel();

                manageTokenService.addContact(vm.saveModel)
                    .then(function (response) {


                        toastr.success("Contact Created Successfully.");

                        //close the pop-up and reload the ng-table
                        cancel();

                        mainservice.getAllContact().then(function (response) {

                            $rootScope.tableParam = new NgTableParams({ count: 10 },
                                {
                                    dataset: response.data
                                });

                            return;
                        });


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


            vm.saveModel = commonData;
        }
    }
})();
