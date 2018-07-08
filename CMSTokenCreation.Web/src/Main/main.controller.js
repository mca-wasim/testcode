(function () {
    'use strict';

    var cmsApp = angular.module('EvolentApp')
                 .controller('maincontroller', ['$http', '$window', '$rootScope', '$scope', 'toastr', 'user', 'NgTableParams', 'mainservice', '$uibModal', 'commonData','$document', 'commonservice',
        function ($http, $window, $rootScope, $scope, toastr, user, NgTableParams, mainservice, $uibModal, commonData, $document, commonservice) {
            var mainVm = this;
            mainVm.loadData = loadTokens();
            mainVm.data = [];
            mainVm.editRow = editRow;  
          
            mainVm.createToken = createToken;
           
            $document.on('keydown', function (e) {
                if (e.which === 8 && (e.target.nodeName !== "INPUT" && e.target.nodeName !== "SELECT" && e.target.nodeName !== "TEXTAREA")) { // you can add others here inside brackets.
                    e.preventDefault();
                }
            });

            $scope.cancel = function () {
                $uibModal.dismiss('cancel');
            };

            function editRow(row) {
                commonData.row = row;
                commonData.Is_Update = true;

                $uibModal.open({
                    name: 'edit',
                    url: '/',
                    templateUrl: 'src/ManageToken/token-modal.html',
                    controller: 'tokeneditmodalcontroller',
                    controllerAs: 'vm'
                });
            }

            function createToken() {
                commonData.Is_Update = false;

                $uibModal.open({
                    name: 'createToken',
                    url: '/',
                    templateUrl: 'src/ManageToken/token-modal.html',
                    controller: 'tokencreatemodalcontroller',
                    controllerAs: 'vm'
                });
            }


          
            function loadTokens() {
                
                    mainservice.getAllContact().then(function (response) {
                        mainVm.dataInitialized = true;
                        mainVm.data = [];
                        mainVm.data = response.data;
                        

                        $rootScope.tableParam = new NgTableParams({ count: 10 },
                            {
                                dataset: mainVm.data
                            });

                        return;
                    });
                
            }

            mainVm.deleteContact = function (id) {
                var ans = confirm('Are you sure to delete it?');
                if (ans) {
                    mainservice.deleteContact(id)
                        .then(function () {
                            loadTokens();
                        })
                }
            };
           
           
        }])


        //[PhoneNumber]
        //, [FirstName]
       // , [LastName]
       // , [Email]
       // , [Status]
    cmsApp.factory('commonData', function () {
        var commonData = {
            "row": String,
            "Is_Update": Boolean,
            "ContactId": Number,
            "PhoneNumber": String,
            "FirstName": String,
            "LastName": String,
            "Email": String,
            "Status": String
        };
        return commonData;
    });

}) ();

