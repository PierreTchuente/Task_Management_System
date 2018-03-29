'use strict';

//debugger;

(function () {

    //debugger;

    var NotificationAlert = angular.module('NotificationAlert');
    NotificationAlert.factory('createEditNotifiAlertService', ['$rootScope', '$http', '$log', function ($rootScope, $http, $log) {


        var GetUserNotif = function (userID) {
            var data;
            $.ajax({
                url: '/Notification/GetUserNotif',
                type: "GET",
                data: {
                    userID: userID
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

        var GetListOfTodayNotitif = function () {
            var data;
            $.ajax({
                url: '/Notification/GetListOfTodayNotitif',
                type: "GET",
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
        var CreateNotification = function (model) {
            if (model !== undefined && model !== null) {
                return $http({
                    method: 'POST',
                    url: '/Notification/CreateNotification',
                    data: model
                })
            }
            else {
                return null;
            }
        }

        var CreateUserNotification = function (model) {
            if (model !== undefined && model !== null) {
                return $http({
                    method: 'POST',
                    url: '/Notification/CreateUserNotification',
                    data: model
                })
            }
            else {
                return null;
            }
        }

        var DeleteUserNotifications = function (userID) {

            //debugger;

            if (userID !== undefined && userID !== null) {
                return $http({
                    method: 'POST',
                    url: '/Notification/DeleteUserNotifications',
                    data: { id: userID }
                })
            }
            else {
                return null;
            }
        }

        //Alert functionalities ====================================

        var CreateUserAlert = function (model) {
            if (model !== undefined && model !== null) {
                return $http({
                    method: 'POST',
                    url: '/Alert/CreateUserAlert',
                    data: model
                })
            }
            else {
                return null;
            }
        }

        var GetListOfTodayAlert = function () {
            var data;
            $.ajax({
                url: '/Alert/GetListOfTodayAlert',
                type: "GET",
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
            CreateUserNotification: CreateUserNotification,
            GetUserNotif: GetUserNotif,
            CreateNotification: CreateNotification,
            GetListOfTodayNotitif: GetListOfTodayNotitif,
            DeleteUserNotifications: DeleteUserNotifications,
            CreateUserAlert: CreateUserAlert,
            GetListOfTodayAlert: GetListOfTodayAlert
        };
    }]);
}());