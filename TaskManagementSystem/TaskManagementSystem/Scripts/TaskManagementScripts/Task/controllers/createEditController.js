//Controller must be attached to module

var task = angular.module('taskModule');

task.controller('createEditTaskCtr', ['$rootScope', '$scope', '$http', '$log', '$timeout', 'createEditTaskService', 'createEditNotifiAlertService', function ($rootScope, $scope, $http, $log, $timeout, createEditTaskService, createEditNotifiAlertService) {
    //
    //debugger;
    //debugger;
    //debugger;

    $scope.model = {};
    $scope.model.Dict_Users = Dict_Users;
    $scope.checklist = [];
    $scope.TaskComments = [];
    $scope.showResult = false;
    $scope.listResultItems = [];
    $scope.quicklistResultItems
    $scope.quickshowResult = false;
    var nCheckListFromServer = 0;
    var ListUserResult = [];
    $scope.model.UserIDs = [];

    //singalR



    //debugger;
    $scope.userCanCreateEdit = true;
    
    $scope.usercannotEditTask = true;
    $scope.usercanEditTask = false;
    //$scope.model.addQuickTaskDateTime = "04:00 PM";
    //showing save button of descriptiond
    $scope.today = today;
    $scope.mindate = getFullDateTimeForCalendar(new Date(today));
    //debugger;
    var tod = new Date(today);
    tod.setHours(16, 0, 0);
    $scope.model.addQuickTaskDate = new Date(tod);

    var loadtasklist = function () {

        //debugger;

        var cal = calendarDate;

        //var tasks = createEditTaskService.GetTaskList(today);
        if (mytask === "False") {
            var tasks = createEditTaskService.GetCalendarTaskList(cal);
            tasks = tasks.Data;
        }
        else {
            var tasks = createEditTaskService.GetCalendarTaskListPerUser(cal, $rootScope.userDetails.USERT_ID)
            tasks = tasks.Data;
        }
        for (var i = 0 ; i < tasks.length; i++) {
            //var datestring = (tasks[i].TASK_ExpectedEndDate).substring(6, tasks[i].TASK_ExpectedEndDate.length - 2);
            var Enddate = new Date(tasks[i].TASK_ExpectedEndDate);
            tasks[i].TASK_ExpectedEndDate = getMonthDayYear(Enddate);

            //datestring = (tasks[i].TASK_ExpectedStartDate).substring(6, tasks[i].TASK_ExpectedStartDate.length - 2);
            var Startdate = new Date(tasks[i].TASK_ExpectedStartDate);
            tasks[i].TASK_ExpectedStartDate = getMonthDayYear(Startdate);


            var differencebetweenEndNStart = daysBetween(Startdate, Enddate);
            var differencebetweenTodayNEndDate = daysBetween(new Date(today), Enddate);
            var perc = (differencebetweenTodayNEndDate / differencebetweenEndNStart).toFixed(2);

            //debugger;

            if (Startdate > Enddate) {
                tasks[i].datecolor = 'due-date-sticker yellow-sticker';
                tasks[i].color = 'equipment-yellow';
            }
            else if (new Date(today) > Enddate) {
                tasks[i].datecolor = 'due-date-sticker red-sticker';
                tasks[i].color = 'equipment-red';
            }

            else if (differencebetweenEndNStart === 0) {

                tasks[i].datecolor = 'due-date-sticker orange-sticker';
                tasks[i].color = 'equipment-orange';
            }
            else if (perc < 0.5) {
                tasks[i].datecolor = 'due-date-sticker orange-sticker';
                tasks[i].color = 'equipment-orange';
            }
            else if (perc >= 0.5) {
                tasks[i].datecolor = 'due-date-sticker green-sticker';
                tasks[i].color = 'equipment-green';
            }

            if (tasks[i].TotWeight === null) {
                tasks[i].width = "0%";
            }
            else {
                if (tasks[i].TotWeightNotCompleted === null) {
                    tasks[i].width = "100%";
                }
                if (tasks[i].TotWeightNotCompleted !== null && tasks[i].TotWeight !== null) {
                    var com = parseInt(tasks[i].TotWeight) - parseInt(tasks[i].TotWeightNotCompleted);
                    var tot = parseInt(tasks[i].TotWeight);
                    var perc = (com * 100) / tot;
                    tasks[i].width = perc.toString() + "%";

                }
            }
        }
        
        $scope.tasks = tasks;

    }

    loadtasklist();

    $scope.addQuickTask = function () {

        //debugger;

        

        var model = $scope.model;
        var time = $scope.model.addQuickTaskDateTime;
        model.name = model.addQuickTaskDescription;
        model.expectedStart = new Date(today);
        model.expectedEnd = model.addQuickTaskDate;
        model.isPrivate = 0;
        model.priorityLevel = 5;
        model.creator = $rootScope.userDetails.USERT_ID;
        model.status = 5;
        model.isCompleted = 0;
        model.isRepeated = 0;
        //model.UserIDs = ['F87B2BA7-1BDF-4FE1-B1DE-06432528E0AA', '24507517-168A-4AA3-A841-13F062E55FB4'];
        model.repeatIntervals = 1;
        createEditTaskService.CreateEdit(model)
        .then(function (success) {
           

            //debugger;

            $scope.model.addQuickTaskDate = new Date(tod);
            $scope.model.addQuickTaskDescription = "";
            $scope.model.UserIDs = [];
            loadtasklist();
            $scope.$parent.sendSignalRMessage("new task");
            var notif = {};
            notif.sender_ID = $rootScope.userDetails.USERT_ID;
            notif.typeID = 1;
            notif.message = "Created task " + model.name;

            //debugger;

            createEditNotifiAlertService.CreateNotification(notif)
            .then(function (success) {
                if (success.status === 201) {
                    $scope.emitNewNotifiction("newNotification");
                    $scope.$parent.sendSignalRMessage("NewNotification");
                    $scope.$parent.sendSignalRMessage("NewNotification");
                }
            },function(error){
            });
        }, function (error) {

        });
        

    }

    $scope.details = function (taskID) {
        $scope.model.searchTerm = "";
        $scope.model.quickSearchTerm = "";
        $scope.checklist = [];
        $scope.TaskComments = [];

        //debugger;

        var taskDetails = createEditTaskService.DetailsForEdit(taskID);
        taskDetails = taskDetails.Data;
        //debugger;
        $scope.model.TASK_ActualEndDate = taskDetails.taskDetails.TASK_ActualEndDate;
        $scope.model.TASK_ActualStartDate = taskDetails.taskDetails.TASK_ActualStartDate;
        $scope.model.TASK_CheckListCompletedNo = taskDetails.taskDetails.TASK_CheckListCompletedNo;

        $scope.model.TASK_Creator = taskDetails.taskDetails.TASK_Creator;
        if ($scope.model.TASK_Creator === $rootScope.userDetails.USERT_ID) {
            $scope.usercannotEditTask = false;
            $scope.usercanEditTask = true;
        }

        else {
            $scope.usercannotEditTask = true;
            $scope.usercanEditTask = false;
        }

        $scope.model.TASK_Desc = taskDetails.taskDetails.TASK_Desc;

        //debugger;
        //$scope.model.TASK_ExpectedStartDate = (taskDetails.taskDetails.TASK_ExpectedStartDate).substring(6, (taskDetails.taskDetails.TASK_ExpectedStartDate).length - 2);
        $scope.model.TASK_ExpectedStartDate = new Date(taskDetails.taskDetails.TASK_ExpectedStartDate);

        //$scope.model.TASK_ExpectedEndDate = (taskDetails.taskDetails.TASK_ExpectedEndDate).substring(6, (taskDetails.taskDetails.TASK_ExpectedEndDate).length - 2);
        var da =new Date( taskDetails.taskDetails.TASK_ExpectedEndDate);
        
        $scope.model.TASK_ExpectedEndDate = new Date(da.getFullYear(), da.getMonth(), da.getDate(), da.getHours(), da.getMinutes());
        $scope.model.TASK_FolderPath = taskDetails.taskDetails.TASK_FolderPath;
        $scope.model.TASK_ID = taskDetails.taskDetails.TASK_ID;
        $scope.model.TASK_IsCompleted = taskDetails.taskDetails.TASK_IsCompleted;
        $scope.model.TASK_IsPrivate = taskDetails.taskDetails.TASK_IsPrivate;
        $scope.model.TASK_IsRepeated = taskDetails.taskDetails.TASK_IsRepeated;
        $scope.model.TASK_Name = taskDetails.taskDetails.TASK_Name;
        $scope.model.TASK_NumberOfComments = taskDetails.taskDetails.TASK_NumberOfComments;
        $scope.model.TASK_PriorityLevel = taskDetails.taskDetails.TASK_PriorityLevel;
        $scope.model.TASK_ProjectID = taskDetails.taskDetails.TASK_ProjectID;
        $scope.model.TASK_RepeatInterval = taskDetails.taskDetails.TASK_RepeatInterval;
        $scope.model.TASK_Status = taskDetails.taskDetails.TASK_Status;
        $scope.model.TASK_Weight = taskDetails.taskDetails.TASK_Weight;
        $scope.model.serverCheckList = taskDetails.checkLists;
        $scope.TaskComments = taskDetails.comments;
        $scope.usersDetails = taskDetails.users;

        //debugger;
        
        for (var i = 0 ; i < (taskDetails.checkLists).length; i++) {
            $scope.checklist.push(
                {
                    check: taskDetails.checkLists[i].CHECKLIST_Description,
                    id: taskDetails.checkLists[i].CHECKLIST_ID,
                    isCompleted: taskDetails.checkLists[i].CHECKLIST_IsCompleted,
                    spanForChecklist: taskDetails.checkLists[i].CHECKLIST_IsCompleted === false ? 'text-decoration: none' : 'text-decoration:line-through',
                    weight: taskDetails.checkLists[i].CHECKLIST_Weight,
                    canedit: $scope.usercanEditTask
                    //checked: taskDetails.checkLists[i].CHECKLIST_IsCompleted === false ? "" : "checked='checked'"
                });

            
            
            nCheckListFromServer += 1;
        }
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

        for (var i = 0; i < ($scope.usersDetails).length; i++) {

            //debugger;

            if ($scope.usersDetails[i].USERT_PhotoLink !== undefined && $scope.usersDetails[i].USERT_PhotoLink !== null && $scope.usersDetails[i].USERT_PhotoLink !== "") {
                
                $scope.usersDetails[i].images = ('/TaskManagementImages/' + $scope.usersDetails[i].USERT_ID + '/' + $scope.usersDetails[i].USERT_PhotoLink);
            }
            else {
                $scope.usersDetails[i].images = ('/TaskManagementImages/defaultUser.png');
            }

            
            if ($scope.usersDetails[i].USERT_ID === $scope.model.TASK_Creator) {
                $scope.usersDetails[i].displaymember = false;
                $scope.model.TASK_CreatorImage = $scope.usersDetails[i].images;
                $scope.model.TASK_CreatorName = $scope.usersDetails[i].USERT_Name;
            }
            else {
                $scope.usersDetails[i].displaymember = true;
            }
        }

        var timer = $timeout(function () {

           
            for (var i = 0 ; i < ($scope.checklist).length; i++) {
                if($scope.checklist[i].isCompleted)
                    $("#checkbox1_"+i.toString()).prop("checked", true);
            }
            $timeout.cancel(timer);
        }, 0);


    }

    $scope.addQuiCheckList = function () {

        //debugger;

        $scope.newchecklist = [];
        $scope.newchecklistLevel = [];

        $scope.newchecklistLevel.push(1);
        $scope.newchecklist.push($scope.model.QuickCheckList);
        $scope.model.QuickCheckList = "";
        

        var model = {};
        model.id = $scope.model.TASK_ID;
        model.CheckListDescriptions = $scope.newchecklist;
        model.CheckListLevels = $scope.newchecklistLevel;
        model.UserIDs = [];
        model.name = $scope.model.TASK_Name;
        model.expectedStart = $scope.model.TASK_ExpectedEndDate;
        model.expectedEnd = $scope.model.TASK_ExpectedEndDate;
        model.isPrivate = 0;
        model.priorityLevel = 5;
        model.creator = $scope.model.TASK_Creator;
        model.status = 5;
        model.isCompleted = 0;
        model.isRepeated = 0;
        model.projectID = $scope.model.TASK_ProjectID;
        model.desc = $scope.model.TASK_Desc;
        model.repeatIntervals = 1;
        createEditTaskService.UpdateTask(model)
       .then(function (success) {
           loadtasklist();
           $scope.newchecklist = [];
           $scope.details($scope.model.TASK_ID);
       }, function (error) {

       });

    }

    $scope.deleteOneCheck = function (key) {

        var model = {};
        model.id = $scope.checklist[key].id;
        model.taskID = $scope.model.TASK_ID;

        createEditTaskService.DeleteCheckList(model)
        .then(function (success) {
            $scope.checklist.splice(key, 1);
            loadtasklist();
        }, function (error) {

        });
        

        //debugger;
    }

    $scope.updateOneCheck = function (key) {

        //debugger;

        if ($scope.checklist[key].id === 'NOID') {
            $scope.checklist[key].spanForChecklist = 'text-decoration:none';

            $("#checkbox1_" + key.toString()).prop("checked", false);
            //$scope.checklist[key].checked = "";
        }
        else {

            if ($scope.checklist[key].isCompleted === false) {
                var model = {};
                model.id = $scope.checklist[key].id;
                model.taskID = $scope.model.TASK_ID;
                model.isCompleted = true;
                createEditTaskService.UpdateCheckList(model)
                .then(function (success) {
                    $scope.checklist[key].spanForChecklist = 'text-decoration:line-through';
                    $scope.checklist[key].isCompleted = true;
                    loadtasklist();
                    //$scope.checklist[key].checked = "checked='checked'";
                }, function (error) {

                });
            }
            else {
                var model = {};
                model.id = $scope.checklist[key].id;
                model.taskID = $scope.model.TASK_ID;
                model.isCompleted = false;
                createEditTaskService.UpdateCheckList(model)
                .then(function (success) {
                    $scope.checklist[key].spanForChecklist = 'text-decoration:none';
                    $scope.checklist[key].isCompleted = false;
                    loadtasklist();
                    //$scope.checklist[key].checked = "";
                }, function (error) {

                });
            }
        }


    }

    $scope.addTaskComment = function () {
        //debugger;

        if ($scope.model.TaskComment !== undefined && $scope.model.TaskComment !== null && $scope.model.TaskComment !== "") {
            var model = {};
            model.taskID = $scope.model.TASK_ID;
            model.userID = $rootScope.userDetails.USERT_ID;
            model.desc = $scope.model.TaskComment;
            model.datePosted = today;
            createEditTaskService.CreateComment(model)
               .then(function (success) {
                   //debugger;
                   //$scope.checklist[key].checked = "";
                   $scope.model.TaskComment = "";
                   var taskID = $scope.model.TASK_ID;
                   loadtasklist();
                   $scope.details(taskID);

                   $scope.$parent.sendSignalRMessage("new task");
                   var notif = {};
                   notif.sender_ID = $rootScope.userDetails.USERT_ID;
                   notif.typeID = 4;
                   notif.message ="Commented  task " + $scope.model.TASK_Name;

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
    }

    $scope.CompleteTask = function () {

        //debugger;

        var model = {};
        model.id = $scope.model.TASK_ID;
        model.isCompleted = 1;
        model.actualEnd = new Date(today);
        createEditTaskService.CompleteTask(model)
       .then(function (success) {

           //debugger;
           loadtasklist();
           $rootScope.$broadcast('oneTaskCompleted');
           $scope.$parent.sendSignalRMessage("completed task");
           var notif = {};
           notif.sender_ID = $rootScope.userDetails.USERT_ID;
           notif.typeID = 4;
           notif.message ="Completed  task " + $scope.model.TASK_Name;

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


    $scope.memberSearchClick = function (searchTerm) {

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
                $scope.listResultItems = ListUserResult.map(function (item) {
                    return item.User_Name;
                });
                if (($scope.listResultItems).length > 0) {
                    $scope.showResult = true;
                }
                else {
                    $scope.showResult = false;
                }

                //debugger;

            });
        }
        else {
            $scope.showResult = false;
        }

    }

    $scope.ItemSelected = function (index) {

        //debugger;

        $scope.selectedUser = ListUserResult[index];

        $scope.model.searchTerm = "";

        var isFound = false;
        for (var i = 0; i < ($scope.usersDetails).length; i++) {
            if ($scope.usersDetails[i].USERT_ID === $scope.selectedUser.UserID) {
                isFound = true;
                break;
            }
        }
        if ($scope.selectedUser.UserID === $scope.model.TASK_Creator) {
            isFound = true;
        }

        if (!isFound) {
            var model = {};
            model.id = $scope.model.TASK_ID;
            model.CheckListDescriptions = [];
            model.UserIDs = $scope.selectedUser.UserID;
            model.name = $scope.model.TASK_Name;
            model.expectedStart = $scope.model.TASK_ExpectedEndDate;
            model.expectedEnd = $scope.model.TASK_ExpectedEndDate;
            model.isPrivate = 0;
            model.priorityLevel = 5;
            model.creator = $scope.model.TASK_Creator;
            model.status = 5;
            model.isCompleted = 0;
            model.isRepeated = 0;
            model.projectID = $scope.model.TASK_ProjectID;
            model.desc = $scope.model.TASK_Desc;
            model.repeatIntervals = 1;
            createEditTaskService.UpdateTask(model)
           .then(function (success) {
               loadtasklist();
               $scope.details($scope.model.TASK_ID);
               $scope.$parent.sendSignalRMessage("new task");
               var notif = {};
               notif.sender_ID = $rootScope.userDetails.USERT_ID;
               notif.typeID = 4;
               notif.message = "Added user " + $scope.selectedUser.User_UserName + " to task " + $scope.model.TASK_Name;

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

        $scope.showResult = false;
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

                if (($scope.quicklistResultItems).length > 0) {
                    $scope.quickshowResult = true;
                }
                else {
                    $scope.quickshowResult = false;
                }

                //debugger;

            });
        }
        else {
            $scope.quickshowResult = false;
        }

    }

    $scope.quickItemSelected = function (index) {

        //debugger;
        var isFound = false;

        $scope.selectedUser = ListUserResult[index];
        $scope.model.quickSearchTerm = "";
        $scope.quickshowResult = false;
        for (var i = 0 ; i < ($scope.model.UserIDs).length; i++) {
            if ($scope.selectedUser.UserID === $scope.model.UserIDs[i]) {
                isFound = true;
                break;
            }
                
        }
        if ($scope.selectedUser.UserID === $rootScope.userDetails.USERT_ID) {
            isFound = true;
        }
        if(!isFound)
            $scope.model.UserIDs.push($scope.selectedUser.UserID);      
        
    }

    $scope.cancelQuickAddTAsk = function () {
        $scope.model.UserIDs = [];
        $scope.model.addQuickTaskDescription = "";
        $scope.model.addQuickTaskDate = new Date(tod);
    }

    $scope.deleteuser = function (key) {

        //debugger;

        var deleteduser = [];
        deleteduser.push($scope.usersDetails[key].USERT_ID)
        createEditTaskService.UnAssignUserToTask($scope.model.TASK_ID,deleteduser)
              .then(function (success) {
                  loadtasklist();
                  $scope.details($scope.model.TASK_ID);
              }, function (error) {

              });
        
    }

    $scope.SaveDescription = function () {

        //debugger;

        var model = {};
        model.id = $scope.model.TASK_ID;
        model.CheckListDescriptions = $scope.newchecklist;
        model.UserIDs = [];
        model.name = $scope.model.TASK_Name;
        model.expectedStart = $scope.model.TASK_ExpectedEndDate;
        model.expectedEnd = $scope.model.TASK_ExpectedEndDate;
        model.isPrivate = 0;
        model.priorityLevel = 5;
        model.creator = $scope.model.TASK_Creator;
        model.status = 5;
        model.isCompleted = 0;
        model.isRepeated = 0;
        model.projectID = $scope.model.TASK_ProjectID;
        model.desc = $scope.model.TASK_Desc;
        model.repeatIntervals = 1;
        createEditTaskService.UpdateTask(model)
       .then(function (success) {
           loadtasklist();

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

    $scope.saveDateChanged = function () {

        //debugger;
        var model = {};
        model.id = $scope.model.TASK_ID;
        model.CheckListDescriptions = $scope.newchecklist;
        model.UserIDs = [];
        model.name = $scope.model.TASK_Name;
        model.expectedStart = $scope.model.TASK_ExpectedEndDate;
        model.expectedEnd = $scope.model.TASK_ExpectedEndDate;
        model.isPrivate = 0;
        model.priorityLevel = 5;
        model.creator = $scope.model.TASK_Creator;
        model.status = 5;
        model.isCompleted = 0;
        model.isRepeated = 0;
        model.projectID = $scope.model.TASK_ProjectID;
        model.desc = $scope.model.TASK_Desc;
        model.repeatIntervals = 1;
        createEditTaskService.UpdateTask(model)
       .then(function (success) {
           loadtasklist();
           $scope.$parent.sendSignalRMessage("new task");
           var notif = {};
           notif.sender_ID = $rootScope.userDetails.USERT_ID;
           notif.typeID = 4;
           notif.message ="Changed Due Date on task " + $scope.model.TASK_Name;

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

    //Handle Messaging service for sending and receiving notiifcation.

    // Establishing connection to the server.


    //debugger;

    //var timer = setTimeout(function () {

    //    var message = "Hello from client with ID " + MessageService.getClientID();
    //    MessageService.sendNoticiation(message);

    //}, 1500)

    $scope.$on('newTask', function () {

        //debugger;
        loadtasklist();
        $scope.$digest();

    });

    $scope.emitNewNotifiction = function (notifmsg) {
        $scope.$emit(notifmsg);
    }

}]);