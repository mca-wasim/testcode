(function (angular) {
    'use strict';

    angular.module('EvolentApp').factory('commonservice', commonservice);

    //@ngInject
    function commonservice($window) {
        var commonserviceVm = {};

        commonserviceVm.getDisplayName = function (type) {
            var displayName = "";
            if (type === "tokenId") {
                displayName = "Token ID";
            }
            else if (type === "tokenSchema") {
                displayName = "Token Schema";
            }
            else if (type === "tokenTable") {
                displayName = "Token Table";
            }
            else if (type === "tokenColumn") {
                displayName = "Token Column";
            }
            return displayName;
        }

       
        return commonserviceVm;
    }

})(angular);