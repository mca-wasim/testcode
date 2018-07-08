angular.module('EvolentApp').factory('user', userService);

//@ngInject
function userService($http, $window,$rootScope) {
    var user = {};
    user.authorizationCompleted = false;
    user.info = {};
    user.getUserDetails = function() {
        return $http.get('api/v1/users/current').then(function (response) {
            if (response.data.IsAuthorized) {
                $window.sessionStorage.UserDetails = JSON.stringify(response.data);
                user.info = response.data;
                $rootScope.$broadcast('authorized', 'User Authorized');
            }
            else {
                window.location.href = "/UnauthorizedUser.html";
            }
        }).catch(function (response) {
            if (response.status == 401) {
                window.location.href = "/UnauthorizedUser.html";
            }
        });
    }
    return user;
}