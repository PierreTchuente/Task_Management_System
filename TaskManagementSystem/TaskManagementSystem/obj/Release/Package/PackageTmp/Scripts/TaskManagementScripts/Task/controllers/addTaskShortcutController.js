//Controller must be attached to module

var task = angular.module('taskModule');

task.controller('addTaskShortcutCtr', ['$rootScope', '$scope', '$http', '$log', '$timeout', 'createEditTaskService', 'createEditNotifiAlertService', function ($rootScope, $scope, $http, $log, $timeout, createEditTaskService, createEditNotifiAlertService) {
    //
    //debugger;
    //debugger;
    //debugger;
    
    $scope.model = {};
    $scope.showResult = false;
    $scope.today = today;
    $scope.mindate = (getFullDateTimeForCalendarver2(new Date(today))).toString();
    $scope.quicklistResultItems
    $scope.quickshowResult = false;
    var ListUserResult = [];
    $scope.model.Users = [];
    $scope.model.UserIDs = [];
    var tod = new Date(today);
    tod.setHours(23, 59, 59);
    $scope.model.addTaskShortcutDate = new Date(tod.getFullYear(),tod.getMonth(),tod.getDate(),tod.getHours(),tod.getMinutes());

    //debugger;
    $scope.userCanCreateEditHere =  true;
    $scope.addShortCutTask = function () {
        //debugger;
        angular.forEach($scope.addingatask.$error.required, function (field) {
            field.$setDirty();
        });
        if (!$scope.addingatask.$valid) return;

        //debugger;
        if (new Date($scope.model.addTaskShortcutDate) < new Date(today)) {
            $('#addTaskShortcutDate').focus();
            return;
        }

        //debugger;

        $scope.model.UserIDs.push($rootScope.userDetails.USERT_ID);
        var model = $scope.model;
        model.name = model.addTaskShortcutTitle;
        model.desc = model.addTaskShortcutDesc;
        model.expectedStart = today;
        model.expectedEnd = model.addTaskShortcutDate;
        model.isPrivate = 0;
        model.priorityLevel = 5;
        model.creator = $rootScope.userDetails.USERT_ID;
        model.status = 5;
        model.isCompleted = 0;
        model.isRepeated = 0;
        model.UserIDs = $scope.model.UserIDs;
        model.repeatIntervals = 1;
        createEditTaskService.CreateEdit(model)
        .then(function (success) {
            $('#addTask1').modal('hide');
            $scope.model.addTaskShortcutDate = new Date(tod.getFullYear(), tod.getMonth(), tod.getDate(), tod.getHours(), tod.getMinutes());
            model.addTaskShortcutDesc = "";
            model.addTaskShortcutTitle = "";
            $scope.model.Users = [];
            $scope.model.UserIDs = [];
            $rootScope.$broadcast('newTask');

            $scope.$parent.sendSignalRMessage("new task");
            var notif = {};
            notif.sender_ID = $rootScope.userDetails.USERT_ID;
            notif.typeID = 1;
            notif.message = "created task" + $scope.model.addTaskShortcutTitle;

            createEditNotifiAlertService.CreateNotification(notif)
            .then(function (success) {
                if (success.status === 201) {
                    $scope.emitNewNotifiction("newNotification");
                    $scope.$parent.sendSignalRMessage("NewNotification");
                    $scope.$parent.sendSignalRMessage("NewNotification");
                    
                }
            }, function (error) {
            });


        }, function (error) {

        });
        

    }

    $scope.quickmemberSearchClick = function (searchTerm) {

        //debugger;

        if (searchTerm === "" || searchTerm === undefined) {
            $log.log("do nothing");
        }
        else if (searchTerm.length >= 2) {

            $http({
                url: '/UserManagement/UserFullTextSearch',
                method: 'GET',
                params: { inputText: searchTerm }
            }).then(function (response) {

                ListUserResult = response.data;
                $scope.quicklistResultItems = ListUserResult.map(function (item) {
                    return item.User_Name;
                });

                $scope.quickshowResult = true;

                //debugger;

            });
        }

    }

    $scope.quickItemSelected = function (index) {

        //debugger;

        $scope.selectedUser = ListUserResult[index];
        $scope.model.quickSearchTerm = "";
        $scope.quickshowResult = false;

        var isFound = false;
        for (var i = 0; i < ($scope.model.UserIDs).length; i++) {
            if ($scope.model.UserIDs[i] === $scope.selectedUser.UserID) {
                isFound = true;
                break;
            }              
        }
        if ($scope.selectedUser.UserID === $rootScope.userDetails.USERT_ID) {
            isFound = true;
        }
        
        if (!isFound) {
            if ($scope.selectedUser.User_PictureName !== undefined && $scope.selectedUser.User_PictureName !== null && $scope.selectedUser.User_PictureName !== "") {
                $scope.selectedUser.finalUserPath = "/TaskManagementImages/" + $scope.selectedUser.UserID + "/" + $scope.selectedUser.User_PictureName;
            }
            else {
                $scope.selectedUser.finalUserPath = "/TaskManagementImages/defaultUser.png";
            }
            $scope.selectedUser.displayname = $scope.selectedUser.User_Name;

            $scope.model.Users.push($scope.selectedUser);
            $scope.model.UserIDs.push($scope.selectedUser.UserID);
        }

    }
    
    $scope.deleteuser = function (key) {
        //debugger;

        $scope.model.Users.splice(key, 1);
        $scope.model.UserIDs.splice(key, 1);
    }

    $scope.emitNewNotifiction = function (notifmsg) {
        $scope.$emit(notifmsg);
    }


}]);