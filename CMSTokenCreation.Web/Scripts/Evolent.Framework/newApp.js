angular.module('EvolentApp', ['ui.router', 'ui.bootstrap', 'blockUI', 'toastr', 'ngAnimate', 'ngSanitize', 'angularMoment', 'ngTable']).config(config)
        .run(run);

function config($locationProvider, blockUIConfig) {
    $locationProvider.html5Mode(false);
    blockUIConfig.autoInjectBodyBlock = true;
 
}

function run($rootScope) {

   
}




