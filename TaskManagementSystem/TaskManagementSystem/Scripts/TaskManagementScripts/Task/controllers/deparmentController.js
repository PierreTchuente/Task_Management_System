//Controller must be attached to module

var task = angular.module('taskModule');

task.controller('departmentCtr', ['$rootScope', '$scope', '$http', '$log', '$timeout', 'departmentService', function ($rootScope, $scope, $http, $log, $timeout, departmentService) {

    debugger;
    var isEdit = false;
    $scope.model = {};
    $scope.model.id = 0;
    var loadDeparments = function () {

        $scope.deparments = departmentService.GetDeparmentList();
        $scope.users = departmentService.GetUserList();
        $scope.model.HOD = $scope.users[0];
        //debugger;

    }

    loadDeparments();
    $scope.create = function () {
        $scope.model.id = 0;
    }
    $scope.edit = function (id, desc, useriD) {

        //debugger;

        var user = {};
        for(var i = 0 ; i < $scope.users.length; i++){
            if ($scope.users[i].USERT_ID === useriD){
                user = $scope.users[i];
                break;
            }
        }
        $scope.model.id = id;
        $scope.model.Title = desc;
        $scope.model.HOD = user;
    }
    $scope.delete = function (id) {
        var model = {};
        model.id = id;
        departmentService.DeleteDepartment(model)
           .then(function (success) {
               loadDeparments();

           }, function (error) {

           });
    }
    $scope.createDepartment = function () {

        //debugger;

        var model = {};
        model.desc = $scope.model.Title;
        model.id = $scope.model.id;
        model.hod = $scope.model.HOD.USERT_ID;

        if (model.id === 0) {
            departmentService.CreateDeparment(model)
            .then(function (success) {
                $scope.model.Title = "";
                $('#addDept').modal('hide')
                loadDeparments();

            }, function (error) {

            });
        }
        else {
            departmentService.UpdateDepartment(model)
           .then(function (success) {
               $scope.model.Title = "";
               $('#addDept').modal('hide')
               loadDeparments();

           }, function (error) {

           });
        }

    }
}]);