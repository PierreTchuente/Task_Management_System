//Controller must be attached to module

var task = angular.module('taskModule');

task.controller('calendarCtr', ['$rootScope', '$scope', '$http', '$log', '$timeout', 'createEditTaskService', function ($rootScope,$scope, $http, $log, $timeout, createEditTaskService) {
    //
    //debugger;
    //debugger;
    //debugger;

    var loadtasklist = function () {

        //debugger;

        var tasks = createEditTaskService.GetTaskList(today);
        for (var i = 0 ; i < tasks.length; i++) {
            var datestring = (tasks[i].TASK_ExpectedEndDate).substring(6, tasks[i].TASK_ExpectedEndDate.length - 2);
            var Enddate = new Date(parseInt(datestring));
            tasks[i].TASK_ExpectedEndDate = getMonthDayYear(Enddate);

            datestring = (tasks[i].TASK_ExpectedStartDate).substring(6, tasks[i].TASK_ExpectedStartDate.length - 2);
            var Startdate = new Date(parseInt(datestring));
            tasks[i].TASK_ExpectedStartDate = getMonthDayYear(Startdate);


            var differencebetweenEndNStart = daysBetween(Startdate, Enddate);
            var differencebetweenTodayNEndDate = daysBetween(new Date(today), Enddate);
            var perc = (differencebetweenTodayNEndDate / differencebetweenEndNStart).toFixed(2);

            if (Startdate > Enddate) {
                tasks[i].datecolor = 'due-date-sticker yellow-sticker'
            }
            else if (new Date(today) > Enddate) {
                tasks[i].datecolor = 'due-date-sticker red-sticker'
            }

            else if (perc < 0.5) {
                tasks[i].datecolor = 'due-date-sticker orange-sticker'
            }
            else if (perc >= 0.5) {
                tasks[i].datecolor = 'due-date-sticker green-sticker'
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

}]);