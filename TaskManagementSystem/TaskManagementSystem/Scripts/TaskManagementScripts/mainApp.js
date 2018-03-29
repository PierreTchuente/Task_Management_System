
var taskManagementApp = angular.module('taskManagement', ['messages', 'sharedDataServiceModule', 'responsibilitiesModule', 'taskModule', 'equipmentModule', 'NotificationAlert']);

// The run function
taskManagementApp.run(['$rootScope', '$log', function ($rootScope, $log) {

    $log.log("Hello main app");

    debugger;

    if (userDetails !== undefined && userDetails !== "" && userDetails !== null) {
        $rootScope.userDetails = JSON.parse(userDetails.replace(/&quot;/g, '"'));
        $rootScope.getUserPicturel = function () {

            //debugger;

            if ($rootScope.userDetails.USERT_PhotoLink !== undefined && $rootScope.userDetails.USERT_PhotoLink !== "" && $rootScope.userDetails.USERT_PhotoLink !== null)
                return "/TaskManagementImages/" + $rootScope.userDetails.USERT_ID + "/" + $rootScope.userDetails.USERT_PhotoLink;
            else
                return "/TaskManagementImages/DefaultImage/defaultUser.png";
        }

            // Request list of users.
            $.ajax({
                url: '/UserManagement/GetListOfUsers',
                type: "GET",
                async: true,
                cache: false,
                dataType: "json",
                success: function (result) {
                    $rootScope.listOfusers = result;
                    var equipmentHOD_ID = null;
                    var masterUser_ID = null;
                    for (var i = 0; i < $rootScope.listOfusers.length; i++) {
                        if (equipmentHOD_ID === null) {
                            if ($rootScope.listOfusers[i].LEVELT_ID === 2) { //i.e. equipment manager
                                equipmentHOD_ID = $rootScope.listOfusers[i].USERT_ID;
                            }
                        }
                        if (masterUser_ID === null) {
                            if ($rootScope.listOfusers[i].LEVELT_ID === 1) { //i.e. master user
                                masterUser_ID = $rootScope.listOfusers[i].USERT_ID;
                            }
                        }
                    }
                    $rootScope.equipmentHOD_ID = equipmentHOD_ID;
                    $rootScope.masterUser_ID = masterUser_ID;
                },
                error: function (err) {
                    $log.log("failure message: " + err);
                }
            });

            $rootScope.getUserPict = function (user) {

                //debugger;

            if (user.USERT_PhotoLink !== undefined && user.USERT_PhotoLink !== "" && user.USERT_PhotoLink !== null)
                return "/TaskManagementImages/" + user.USERT_ID + "/" + user.USERT_PhotoLink;
            else
                return "/TaskManagementImages/DefaultImage/defaultUser.png";
        }
    }
}]);