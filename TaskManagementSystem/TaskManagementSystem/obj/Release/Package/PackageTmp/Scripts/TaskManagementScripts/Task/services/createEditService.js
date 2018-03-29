'use strict';

//debugger;

(function () {

    //debugger;

    var CreateEditTaskSerive = angular.module('taskModule');
    CreateEditTaskSerive.factory('createEditTaskService', ['$rootScope', '$http', '$log', 'sharedDataService_Ver2', function ($rootScope, $http, $log, sharedDataService_Ver2) {

        //debugger;

        //var shareModel;

        // Getters and Setters
        var setCreatedTask = function (subsystem, key, model) {
            sharedDataService_Ver2.setShareModel(subsystem, key, model);
        };
        var getCreatedTask = function (subsystem, key) {
            var shareModel = sharedDataService_Ver2.getShareModel(subsystem, key);
            return shareModel;
        };


        var CleanUpService = function () {
            //shareModel = {};
            //shareModel = null;
            sharedDataService_Ver2.CleanUpService();
        };

        // Performing a synchronous ajax call to the server in order to load all of the required data.
        var DetailsForEdit = function (TaskID) {
            var data;
            $.ajax({
                url: '/TaskManagement/Details',
                type: "GET",
                data: {
                    taskID: TaskID
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

        var GetTaskList = function (date) {
            var data;
            $.ajax({
                url: '/TaskManagement/ListTaskPerDay',
                type: "GET",
                data: {
                    date: date
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

        //ADDED KEVIN CODE FOR CALENDAR
        var GetCalendarTaskList = function (date) {
            var data;
            $.ajax({
                url: '/TaskManagement/ListTaskPerCalendarDay',
                type: "GET",
                data: {
                    date: date
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


        
        var GetCompletedCalendarTaskList = function (date) {
            var data;
            $.ajax({
                url: '/TaskManagement/ListCompletedCalendarTaskPerDay',
                type: "GET",
                data: {
                    date: date
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

        var GetCalendarTaskListPerUser = function (date, userid) {
            var data;
            $.ajax({
                url: '/TaskManagement/ListTaskPerCalendarDayPerUSer',
                type: "GET",
                data: {
                    date: date,
                    userid: userid
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

        
        
        //END ADDED KEVIN CODE
        var GetCompletedTaskList = function (date) {
            var data;
            $.ajax({
                url: '/TaskManagement/ListCompletedTaskPerDay',
                type: "GET",
                data: {
                    date: date
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
        var CreateEdit = function (model) {
            if (model !== undefined && model !== null) {
                return $http({
                    method: 'POST',
                    url: '/TaskManagement/Create',
                    data: model
                })
            }
            else {
                return null;
            }
        }

        var UpdateTask = function (model) {
            if (model !== undefined && model !== null) {
                return $http({
                    method: 'POST',
                    url: '/TaskManagement/Edit',
                    data: model
                })
            }
            else {
                return null;
            }
        }

        var CompleteTask = function (model) {
            if (model !== undefined && model !== null) {
                return $http({
                    method: 'POST',
                    url: '/TaskManagement/CompleteTask',
                    data: model
                })
            }
            else {
                return null;
            }
        }

        var DeleteCheckList= function (model) {
            if (model !== undefined && model !== null) {
                return $http({
                    method: 'POST',
                    url: '/TaskManagement/DeleteCheckList',
                    data: model
                })
            }
            else {
                return null;
            }
        }

        var CreateComment = function (model) {
            if (model !== undefined && model !== null) {
                return $http({
                    method: 'POST',
                    url: '/TaskManagement/CreateComment',
                    data: model
                })
            }
            else {
                return null;
            }
        }

        var UpdateCheckList  = function (model) {
            if (model !== undefined && model !== null) {
                return $http({
                    method: 'POST',
                    url: '/TaskManagement/UpdateChecklist',
                    data: model
                })
            }
            else {
                return null;
            }
        }

        var UnAssignUserToTask = function (taskID, userIDs) {
            if (taskID !== undefined && userIDs !== undefined && taskID !== null && userIDs !== null) {
                return $http({
                    method: 'POST',
                    url: '/TaskManagement/DeleteUsersAssignedToTask',
                    data: {
                        taskID: taskID,
                        userIDs: userIDs
                    }
                })
            }
            else {
                return null;
            }
        }
        


        return {

            UnAssignUserToTask: UnAssignUserToTask,

            UpdateTask: UpdateTask,

            CompleteTask: CompleteTask,

            CreateComment: CreateComment,

            UpdateCheckList: UpdateCheckList,

            DeleteCheckList: DeleteCheckList,

            setCreatedTask: setCreatedTask,

            getCreatedTask: getCreatedTask,

            DetailsForEdit: DetailsForEdit,

            CleanUpService: CleanUpService,

            CreateEdit: CreateEdit,

            GetTaskList: GetTaskList,

            GetCompletedTaskList: GetCompletedTaskList,

            //ADDED KEVIN
            GetCalendarTaskList: GetCalendarTaskList,

            GetCompletedCalendarTaskList: GetCompletedCalendarTaskList,

            GetCalendarTaskListPerUser: GetCalendarTaskListPerUser

        };
    }]);
}());