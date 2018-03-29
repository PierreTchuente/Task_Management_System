'use strict';

//debugger;

(function () {

    //debugger;

    var CreateEditTaskSerive = angular.module('taskModule');
    CreateEditTaskSerive.factory('departmentService', ['$rootScope', '$http', '$log', 'sharedDataService_Ver2', function ($rootScope, $http, $log, sharedDataService_Ver2) {

 

        var GetDeparmentList = function () {
            var data;
            $.ajax({
                url: '/DepartmentManagement/GetListOfDepartments',
                type: "GET",
                data: {
                },
                async: false,
                cache: false,
                dataType: "json",
                success: function (result) {
                    data = result;
                },
                error: function (err) {
                    $log.log("failure message: " + err);
                }
            });
            return data;
        }


        // Send complete model to Server
        var CreateDeparment = function (model) {
            if (model !== undefined && model !== null) {
                return $http({
                    method: 'POST',
                    url: '/DepartmentManagement/Create',
                    data: model
                })
            }
            else {
                return null;
            }
        }

        var UpdateDepartment = function (model) {
            if (model !== undefined && model !== null) {
                return $http({
                    method: 'POST',
                    url: '/DepartmentManagement/Update',
                    data: model
                })
            }
            else {
                return null;
            }
        }

        var DeleteDepartment= function (model) {
            if (model !== undefined && model !== null) {
                return $http({
                    method: 'POST',
                    url: '/DepartmentManagement/DeleteDepartment',
                    data: model
                })
            }
            else {
                return null;
            }
        }

        var GetUserList = function () {
            var data;
            $.ajax({
                url: '/UserManagement/GetListOfUsers',
                type: "GET",
                data: {
                },
                async: false,
                cache: false,
                dataType: "json",
                success: function (result) {
                    data = result;
                },
                error: function (err) {
                    $log.log("failure message: " + err);
                }
            });
            return data;
        }


        return {

            GetDeparmentList: GetDeparmentList,

            CreateDeparment: CreateDeparment,

            UpdateDepartment: UpdateDepartment,

            DeleteDepartment: DeleteDepartment,

            GetUserList: GetUserList
        };
    }]);
}());