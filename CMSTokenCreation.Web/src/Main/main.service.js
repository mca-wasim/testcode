(function (angular) {
    'use strict';

    angular.module('EvolentApp').factory('mainservice', mainservice);

    //@ngInject
    function mainservice($http) {
        var mainserviceVm = {};

        mainserviceVm.getAllContact = function () {
         
            return $http({
                method: 'Get',
                url: 'api/contact/'
            })

        }

        mainserviceVm.deleteContact =function (id) {
            return $http({
                method: "delete",
                url: "/api/v1/contact/" + id,
                headers: { 'Content-type': 'application/json' }
            });
        }



        return mainserviceVm;
    }

})(angular);