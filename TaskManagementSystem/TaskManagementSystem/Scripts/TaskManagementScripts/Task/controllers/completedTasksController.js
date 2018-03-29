//Controller must be attached to module

var task = angular.module('taskModule');

task.controller('completedTaskCtr', ['$scope', '$http', '$log', '$timeout', 'createEditTaskService', function ($scope, $http, $log, $timeout, createEditTaskService) {
    //
    //debugger;
    //debugger;
    //debugger;

    $scope.model = {};
    //$scope.model.Dict_Users = Dict_Users;
    $scope.checklist = [];
    $scope.newchecklist = [];
    $scope.TaskComments = [];
    var nCheckListFromServer = 0;
    //showing save button of descriptiond

    var loadtasklist = function () {

        //debugger;

        var tasks = createEditTaskService.GetCompletedCalendarTaskList(calendarDate);
        tasks = tasks.Data;
        //debugger;

        for (var i = 0 ; i < tasks.length; i++) {
            //var datestring = (tasks[i].TASK_ExpectedEndDate).substring(6, tasks[i].TASK_ExpectedEndDate.length - 2);
            var date = new Date(tasks[i].TASK_ExpectedEndDate);
            tasks[i].TASK_ExpectedEndDate = getMonthDayYear(date);

            
        }

        $scope.Completedtasks = tasks;

    }

    loadtasklist();

    $scope.$on('oneTaskCompleted', function () {

        //debugger;
        loadtasklist();
        $scope.$digest();

    });

    $scope.CompletedDetails = function (taskID) {

        //debugger;

        $scope.TaskComments = [];
        $scope.checklist = [];
        //debugger;

      
        var taskDetails = createEditTaskService.DetailsForEdit(taskID);
        taskDetails = taskDetails.Data;

        $scope.model.CompletedDescription = taskDetails.taskDetails.TASK_Desc;
        $scope.model.CompletedTitle = taskDetails.taskDetails.TASK_Name;

        if (taskDetails.taskDetails.TASK_ActualEndDate != undefined && taskDetails.taskDetails.TASK_ActualEndDate != null && taskDetails.taskDetails.TASK_ActualEndDate != "") {
            //$scope.model.TASK_ActualEndDate = (taskDetails.taskDetails.TASK_ActualEndDate).substring(6, (taskDetails.taskDetails.TASK_ActualEndDate).length - 2);



            $scope.model.TASK_ActualEndDate = new Date(taskDetails.taskDetails.TASK_ActualEndDate);
            $scope.model.TASK_ActualEndDate = getMonthDayYear($scope.model.TASK_ActualEndDate);
        //var epochMicrotimeDiff = 2208988800000;
        //$scope.model.TASK_ActualEndDate = new Date(parseInt($scope.model.TASK_ActualEndDate) - epochMicrotimeDiff);
        //$scope.model.TASK_ActualEndDate = getMonthDayYear($scope.model.TASK_ActualEndDate);
        }

        //$scope.model.TASK_ExpectedStartDate = (taskDetails.taskDetails.TASK_ExpectedStartDate).substring(6, (taskDetails.taskDetails.TASK_ExpectedStartDate).length - 2);
        $scope.model.TASK_ExpectedStartDate = new Date(taskDetails.taskDetails.TASK_ExpectedStartDate);
        $scope.model.TASK_ExpectedStartDate = getMonthDayYear($scope.model.TASK_ExpectedStartDate);

        //$scope.model.TASK_ExpectedEndDate = (taskDetails.taskDetails.TASK_ExpectedEndDate).substring(6, (taskDetails.taskDetails.TASK_ExpectedEndDate).length - 2);
        $scope.model.TASK_ExpectedEndDate = new Date(taskDetails.taskDetails.TASK_ExpectedEndDate);
        $scope.model.TASK_ExpectedEndDate = getMonthDayYear($scope.model.TASK_ExpectedEndDate);

        $scope.TaskComments = taskDetails.comments;

        for (var i = 0; i < ($scope.TaskComments).length; i++) {

            //debugger;

            //$scope.TaskComments[i].COMMENT_DatePosted = ($scope.TaskComments[i].COMMENT_DatePosted).substring(6, ($scope.TaskComments[i].COMMENT_DatePosted).length - 2);
            $scope.TaskComments[i].COMMENT_DatePosted = new Date($scope.TaskComments[i].COMMENT_DatePosted);
            $scope.TaskComments[i].COMMENT_DatePosted = getMonthDayYear($scope.TaskComments[i].COMMENT_DatePosted);
            if ($scope.TaskComments[i].USERT_PhotoLink !== undefined && $scope.TaskComments[i].USERT_PhotoLink !== null && $scope.TaskComments[i].USERT_PhotoLink !== "") {

                $scope.TaskComments[i].USERT_PhotoLink = ('/TaskManagementImages/' + $scope.TaskComments[i].USERT_ID + '/' + $scope.TaskComments[i].USERT_PhotoLink);
            }
            else {
                $scope.TaskComments[i].USERT_PhotoLink = ('/TaskManagementImages/defaultUser.png');
            }

        }

        for (var i = 0 ; i < (taskDetails.checkLists).length; i++) {
            $scope.checklist.push(
                {
                    check: taskDetails.checkLists[i].CHECKLIST_Description,
                    id: taskDetails.checkLists[i].CHECKLIST_ID,
                    isCompleted: taskDetails.checkLists[i].CHECKLIST_IsCompleted,
                    spanForChecklist: taskDetails.checkLists[i].CHECKLIST_IsCompleted === false ? 'text-decoration: none' : 'text-decoration:line-through'
                    //checked: taskDetails.checkLists[i].CHECKLIST_IsCompleted === false ? "" : "checked='checked'"
                });
        }

        $scope.users = taskDetails.users;

        for (var i = 0; i < ($scope.users).length; i++) {
            if ($scope.users[i].USERT_PhotoLink !== undefined && $scope.users[i].USERT_PhotoLink !== null && $scope.users[i].USERT_PhotoLink !== "") {

                $scope.users[i].images = ('/TaskManagementImages/' + $scope.users[i].USERT_ID + '/' + $scope.users[i].USERT_PhotoLink);
            }
            else {
                $scope.users[i].images = ('/TaskManagementImages/defaultUser.png');
            }
        }


    }



}]);