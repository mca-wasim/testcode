(function () {
    'use strict';

    angular
        .module('EvolentApp')
        .service('manageTokenService', ['$http', function ($http) {

            this.saveContactData = saveContactData;

            this.addContact = addContact;

            this.getTokenData = function (tokenKey) {

                return $http.get('api/v1/contact/' + tokenKey)
                    .success(function (data) {
                        return data;
                    }).error(function (error) {
                        alert(error.message);
                    }
                    );
            }

            function saveContactData(contact) {
                return $http({
                    method: "put",
                    url: "/api/v1/contact/" + contact.ContactId,
                    data: contact,
                    headers: { 'Content-type': 'application/json' }
                });
            }

            function addContact(contact) {

                return $http({
                    method: "post",
                    url: "/api/v1/contact/add",
                    data: contact,
                    headers: { 'Content-type': 'application/json' }
                });
            }
        }])
})();