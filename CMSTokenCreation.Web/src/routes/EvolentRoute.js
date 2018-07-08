
angular.module('EvolentApp').config(routeConfig);

angular.module('ngTable').run(['$templateCache', function ($templateCache) {
    $templateCache.put('ng-table/filters/text.html', '<input type="text" ng-model="params.filter()[name]" ng-if="filter==\'text\'" placeholder="Search By {{name}}" class="input-filter form-control"/>');
}]);

//@ngInject
function routeConfig($stateProvider, $urlRouterProvider, $httpProvider) {
    $urlRouterProvider.otherwise('/#');
    $httpProvider.defaults.headers.common['Access-Control-Allow-Origin'] = '*';
    $httpProvider.defaults.headers.common['Access-Control-Allow-Headers'] = 'Cache-Control, Pragma, Origin, Authorization, Content-Type, X-Requested-With';
    $httpProvider.defaults.headers.common['Access-Control-Allow-Methods'] = 'GET, PUT, POST, DELETE';
    $httpProvider.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';

    var homePage = {
        name: 'home',
        url: '/',
        templateUrl: 'src/Main/Main.html',
        controller: 'maincontroller',
        controllerAs: 'mainVm'
    }

    var upload = {
        name: 'upload',
        url: '/upload',
        templateUrl: 'src/TokenUpload/tokenupload.html',
        controller: 'tokenuploadcontroller',
        controllerAs: 'tokenuploadVM'
    }

    var tokendictionary = {
        name: 'tokendictionary',
        url: '/tokendictionary',
        templateUrl: 'src/VerifyAPI/tokendictionary.html',
        controller: 'tokendictionarycontroller',
        controllerAs: 'tokendictionaryVM'
    }

    var datadictionary = {
        name: 'datadictionary',
        url: '/datadictionary',
        templateUrl: 'src/VerifyAPI/datadictionary.html',
        controller: 'datadictionarycontroller',
        controllerAs: 'datadictionaryVM'
    }

    $stateProvider
            .state(homePage)
            .state(upload)
             .state(tokendictionary)
            .state(datadictionary);
}