//Controller must be attached to module

var responsibility = angular.module('responsibilitiesModule');

responsibility.controller('createEditResponsibilitiesCtr', ['$rootScope', '$scope', '$http', '$log', function ($rootScope, $scope, $http, $log) {
    //debugger;
    //Attach a model in scope
    $scope.model = {};
    $scope.listResultItems = [];
    $scope.quicklistResultItems;
    $scope.quickshowResult = false;
    //Function to load Responsibility
    var loadpage = function () {
        //Empty the InputBox
        $scope.model.Description = "";
        //var userid = '68FD7263-299B-4991-BC8A-C4F610D32CFB';
       
        $http({
            method: 'GET',
            url: '/Responsibility/DepartmentList'
        }).then(function (response) {
            //debugger;
            $log.log(response);
            //Create new object in scope that will contain the list of returned objects
            $scope.Departments = response.data;
        });
    };


    loadpage();
    //Function to add Responsibility to DB
    $scope.DepartmentResponsibilityDetails = function (DeptID) {

       
        //Check If User is Manager to access this
        //
        /// var resMembers;
        
        var userLevel = $rootScope.userDetails.LEVELT_ID;
        if (userLevel <= 3) {
            //debugger;
            $("#deptRespDetailsModal").modal().show();
            //debugger;
            //Call API
            $http({
                method: 'GET',
                url: '/Responsibility/DepartmentDetails',
                params: {
                    deptID: DeptID
                }
            }).then(function (response) {
                //debugger;
                $log.log(response);
                //Load the newly added responsibility
                $scope.DeptDetails = response.data;
                //var resMembers = response.data.otherMembers;
                //resMembers = response.data
                //$scope.Responsibilities.push({ RESPONSIBILITY_Desc: $scope.model.Description, 
                //    complete: "50%"...});
                //Empty the InputBox
                //$scope.model.Description = "";
                //Hide AddResponsibility Div
                //$scope.members = [];
                //for (var i = 0; i < $scope.DeptDetails.otherMembers.length; i++) {

                //    $scope.members[i].pic = $scope.DeptDetails.otherMembers[i].USERT_PhotoLink;
                //}
                $scope.setDeptID(DeptID, $scope.DeptDetails.deptDetails.Dept_HOD);
                for (var i = 0; i < ($scope.DeptDetails.otherMembers).length; i++) {
                    if ($scope.DeptDetails.otherMembers[i].USERT_PhotoLink !== undefined && $scope.DeptDetails.otherMembers[i].USERT_PhotoLink !== null && $scope.DeptDetails.otherMembers[i].USERT_PhotoLink !== "") {
                        $scope.DeptDetails.otherMembers[i].images = ('/TaskManagementImages/' + $scope.DeptDetails.otherMembers[i].USERT_ID + '/' + $scope.DeptDetails.otherMembers[i].USERT_PhotoLink);

                    }
                    else {
                        $scope.DeptDetails.otherMembers[i].images = ('/TaskManagementImages/defaultUser.png');
                    }
                }
                $(".collapse1").collapse('toggle');
                
            });
            //var resMembers = $scope.DeptDetails;
            //for (var i = 0; i < $scope.DeptDetails.otherMembers.length; i++) {
            //    $scope.members[i].pic = resMembers[i].otherMembers.USERT_PhotoLink;
            //}
            $scope.DeptID;
        }
        //debugger;



        //var rM = {};
        ////rM.id 
        //rM.Title = 'Responsibility';
        //rM.Description = $scope.model.Description;
        //rM.userID = 'F87B2BA7-1BDF-4FE1-B1DE-06432528E0AA';

        
        //Load all responsibilities
    }





    //Function to load Responsibility
    var reloadResps = function (DeptID) {
        $http({
            method: 'GET',
            url: '/Responsibility/DepartmentDetails',
            params: {
                deptID: DeptID
            }
        }).then(function (response) {
            //debugger;
            $log.log(response);
            //Load the newly added responsibility
            $scope.DeptDetails = response.data;
            //Hide AddResponsibility Div
            $(".collapse1").collapse('toggle');
            //debugger;
        });
    };
    //Function to load Members
    var reloadMembers = function (DeptID) {
        //debugger;
        var userLevel = $rootScope.userDetails.LEVELT_ID;
        if (userLevel <= 3) {
            //debugger;
            $("#deptRespDetailsModal").modal().show();

            //Call API
            $http({
                method: 'GET',
                url: '/Responsibility/DepartmentDetails',
                params: {
                    deptID: DeptID
                }
            }).then(function (response) {
                //debugger;
                $log.log(response);

                //Load the newly added responsibility
                $scope.DeptDetails = response.data;

                //$scope.Responsibilities.push({ RESPONSIBILITY_Desc: $scope.model.Description, 
                //    complete: "50%"...});
                //Empty the InputBox
                //$scope.model.Description = "";
                //Hide AddResponsibility Div
                $(".collapse1").collapse('toggle');
                //debugger;
            });
            $scope.DeptID;
        }
    };
    //Remove Responsibility From List
    $scope.DeleteResponsibility = function (respID, DeptID) {
        var userLevel = $rootScope.userDetails.LEVELT_ID;
        var userDeptID = $rootScope.userDetails.DEPT_ID;
        var proceed = false;
        if (userDeptID === 1)
            proceed = true;
        if ((userLevel <= 3) && (userDeptID === DeptID))
            proceed = true;
        if(proceed){
            var id = respID;
            $http({
                method: 'POST',
                url: '/Responsibility/Remove',
                params: { id: respID }
            }).then(function (response) {
                //debugger;
                $log.log(response);
                //Load the newly added responsibility
                reloadResps(DeptID);
            });
        }
        
    };












    //Function to add Responsibility to DB
    $scope.addResponisbility = function (deptID) {

        //debugger;
        var userLevel = $rootScope.userDetails.LEVELT_ID;
        var userDeptID = $rootScope.userDetails.DEPT_ID;
        var proceed = false;
        if (userLevel === 1)
            proceed = true;
        if ((userLevel <= 3) && (userDeptID === deptID))
            proceed = true;

        if(proceed){
            var rM = {};
            //rM.id 
            rM.Title = 'Responsibility';
            rM.Description = $scope.model.AddRespDescription;
            rM.deptID = deptID;
            //debugger;
            $http({
                method: 'POST',
                url: '/Responsibility/CreateResponsibility',
                data: rM
            }).then(function (response) {
                //debugger;
                $log.log(response);

                //Load the newly added responsibility
                $scope.Responsibilities

                //$scope.Responsibilities.push({ RESPONSIBILITY_Desc: $scope.model.Description, 
                //    complete: "50%"...});
                //Empty the InputBox
                $scope.model.AddRespDescription = "";
                //Hide AddResponsibility Div
                //$(".collapse1").collapse('toggle');
                //Reload Div
                reloadResps(deptID);


            });
        }
        
        $scope.model.AddRespDescription = "";
        //Load all responsibilities
    }

    //$scope.UpdateResponsibilityID = function (userId) {
    //    //$scope.selectedUserId = userId;
    //    var rM = {};
    //    //rM.id 
    //    rM.Title = 'Responsibility';
    //    rM.Description = $scope.model.Description;
    //    rM.userID = '68FD7263-299B-4991-BC8A-C4F610D32CFB';

    //    $http({
    //        method: 'POST',
    //        url: '/Responsibility/CreateResponsibility',
    //        data: rM
    //    }).then(function (response) {
    //        //debugger;
    //        $log.log(response);

    //        //Load the newly added responsibility
    //        loadpage('68FD7263-299B-4991-BC8A-C4F610D32CFB');
    //    });
    //};

    
    $scope.setDeptID = function (id, chosenHOD) {
        $scope.chosenID = id;
        $scope.chosenHODID = chosenHOD;
    };


    //$scope.SelectedUserForAddMemberToResps
   // $scope.addMemberToResponsibility = function (deptID)













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

    $scope.quickItemSelected = function (index, deptID) {

        //debugger;
        var isFound = false;

        $scope.selectedUser = ListUserResult[index];
        $scope.model.quickSearchTerm = "";
        $scope.quickshowResult = false;
        $scope.SelectedUserForAddMemberToResps = $scope.selectedUser;
        var userIDChosen = $scope.SelectedUserForAddMemberToResps;



        for (var i = 0 ; i < ($scope.DeptDetails.otherMembers).length; i++) {
            if (userIDChosen.UserID === $scope.DeptDetails.otherMembers[i].USERT_ID) {
                isFound = true;
                break;
            }
        }



        if (!isFound) {
            //Check if user wanting to add is a Manager
            //debugger;
            var userLevel = $rootScope.userDetails.LEVELT_ID;
            if (userIDChosen.UserID !== $scope.chosenHODID){
                //debugger;
                var aM = {};
                aM.userID = userIDChosen.UserID;
                aM.deptID = deptID;
                $http({
                    method: 'POST',
                    url: '/Responsibility/AddMemberToDepartmentResponsibilitiesController',
                    data: aM
                }).then(function (response) {
                    //debugger;
                    $log.log(response);

                    reloadMembers(deptID);

                });
            }
            else {
                alert("Chosen user is already a member.")
            }
           
        }
        
        
        //for (var i = 0 ; i < ($scope.model.UserIDs).length; i++) {
        //    if ($scope.selectedUser.UserID === $scope.model.UserIDs[i]) {
        //        isFound = true;
        //        break;
        //    }

        //}
        //if (!isFound)
        //    $scope.model.UserIDs.push($scope.selectedUser.UserID);

    }

    $scope.removeMember = function (key, deptID) {
        var id = $scope.DeptDetails.otherMembers[key].USERT_ID;
        var userLevel = $rootScope.userDetails.LEVELT_ID;
        if (userLevel === 1) {
            var aM = {};
            aM.userID = id;
            aM.deptID = deptID;
            $http({
                method: 'POST',
                url: '/Responsibility/RemoveMemberFromDepartmentResponsibilitiesController',
                data: aM
            }).then(function (response) {
                //debugger;
                $log.log(response);

                reloadMembers(deptID);

            });
        }
        
    }
    
    $scope.keypressed = function ($event) {

        //debugger;

        var eve = event;
        if (eve.keyCode === 13) {
            $scope.addResponisbility($scope.chosenID);
        }
    }
    //debugger;
}]);