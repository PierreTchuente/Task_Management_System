(function () {
    'use strict';

    var NotificationAlert = angular.module('NotificationAlert');

    NotificationAlert.controller('createEditNotifiAlertCtr', ['$rootScope', '$scope', '$http', '$log', '$timeout', 'createEditNotifiAlertService',
        function ($rootScope, $scope, $http, $log, $timeout, createEditNotifiAlertService) {

            $scope.model = {};
            var ListofNotiifcation = [];
            var tempList = [];

            UpdateTodayAlert();

            $scope.$on('UpdateAlert', function (event, args) {
                UpdateTodayAlert();
            });

            // When creating a notification in general only specifying the sender ID.
            var createNotification = function (model) {
                createEditNotifiAlertService.CreateNotification(model).then(function (response) {

                });
            }

            // When creating a user notification specify the user ID and the sender ID.
            var createUserNotification = function (model) {
                createEditNotifiAlertService.CreateNotification(model).then(function (response) {

                });
            }

            // Create alert
            $scope.addAlert = function () {

                if ($scope.model.Message === undefined || $scope.model.Message === "") {
                    //alert("Please Specify the Alert Message.");
                    $scope.model.isRequired = true;
                    return;
                }
                $('#createAlertModal').modal('hide');
                $scope.model.isRequired = false;

                var model = {
                    alertID: null,
                    alertCreatedByID: $rootScope.userDetails.USERT_ID,
                    alertDescription: $scope.model.Message
                }
                $scope.model.Message = "";
                createEditNotifiAlertService.CreateUserAlert(model).then(function (response) {
                    if (response.status === 201) {
                        UpdateTodayAlert();
                        var notif = {};
                        notif.sender_ID = $rootScope.userDetails.USERT_ID;
                        notif.typeID = 8;
                        notif.message = "New Alert";
                        createEditNotifiAlertService.CreateNotification(notif).then(function (response) {
                            if (response.status === 201) {
                                $scope.$emit("newNotification");
                                $scope.$parent.sendSignalRMessage("NewAlertCreated");
                            }
                        });
                    }
                });
            }

            //function to update today alert alert.
            function UpdateTodayAlert() {

                var list = createEditNotifiAlertService.GetListOfTodayAlert()
                tempList = JSON.parse(list);
                $.each(tempList, function (index, item) {
                    //item.alertDateTime = new Date(parseInt(item.alertDateTime.substring(6, (item.alertDateTime).length - 2)));
                    //item.alertDateTime = getFullDateTime(item.alertDateTime);
                    item.USERT_PhotoLink = "/TaskManagementImages/" + item.alertCreatedByID + "/" + item.USERT_PhotoLink;
                });
                $scope.ListOfTodayAlert = tempList;
                if (!$scope.$$phase) {
                    $scope.$digest();
                }
            }

        }]);
})();