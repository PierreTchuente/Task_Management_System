(function () {
    'use strict';

    angular.module('taskManagement').controller('MainController', ['$rootScope', '$scope', '$http', '$log', 'MessageService', 'createEditNotifiAlertService',
    function MainController($rootScope, $scope, $http, $log, MessageService, createEditNotifiAlertService) {

        debugger;

        //Handling User Notifications
        var olddept = "";
        var oldlevel = "";
        $scope.numberOfUserNotification = 0;
        if ($rootScope.userDetails !== undefined) {
            fetchnewNotifications();
        }

        $scope.$on('newNotification', function (event, args) {
            fetchnewNotifications();
            //$rootScope.$broadcast("NewNotificationCreated");
        });
        $scope.$on('newOrder', function (event, args) {
            fetchshoppincart();
        });


        //Manuella's Addition BEGIN ==================================================================
        var fetchshoppincart = function () {
            if ($rootScope.userDetails !== undefined) {

                //debugger;

                //Call the service for shopping cart
                var selectedDate = calendarDate;
                $scope.numberOfOrders = 0;
                $http({
                    method: 'GET',
                    url: '/EquipmentManagement/GetOrderShoppingCart',
                    params: {
                        userID: $rootScope.userDetails.USERT_ID,
                        date: selectedDate
                    }
                }).then(function (response) {
                    //debugger;
                    $log.log(response);
                    //$(".collapse1").collapse('toggle');
                    //Reload Div
                    var orders = response.data;
                    //$scope.model.EquipName = equipmentID;
                    if (orders !== null) {
                        for (var i = 0; i < orders.length; i++) {
                            orders[i].OE_DateOrdered = orders[i].OE_DateOrdered.substring(6, orders[i].OE_DateOrdered.length - 2);
                            orders[i].OE_DateOrdered = new Date(parseInt(orders[i].OE_DateOrdered));
                            orders[i].OE_DateOrdered = getFullTime(orders[i].OE_DateOrdered);

                            if (orders[i].OE_Out_Time !== null) {
                                orders[i].OE_Out_Time = orders[i].OE_Out_Time.substring(6, orders[i].OE_Out_Time.length - 2);
                                orders[i].OE_Out_Time = new Date(parseInt(orders[i].OE_Out_Time));
                                orders[i].OE_Out_Time = getFullTime(orders[i].OE_Out_Time);
                            }

                            if (orders[i].OE_Return_Time !== null) {
                                orders[i].OE_Return_Time = orders[i].OE_Return_Time.substring(6, orders[i].OE_Return_Time.length - 2);
                                orders[i].OE_Return_Time = new Date(parseInt(orders[i].OE_Return_Time));
                                orders[i].OE_Return_Time = getFullTime(orders[i].OE_Return_Time);
                            }

                            //debugger;
                            if (orders[i].STATUST_ID === 1002) {
                                orders[i].STATUST_Desc = 'Pending';
                                orders[i].statusCheck = true;
                            } else if (orders[i].STATUST_ID === 1003) {
                                orders[i].STATUST_Desc = 'Returned';
                                orders[i].statusCheck = false;
                            } else if (orders[i].STATUST_ID === 1004) {
                                orders[i].STATUST_Desc = 'Out';
                                orders[i].statusCheck = false;
                            }

                        }

                        $scope.shoppingCart = orders;
                        $scope.numberOfOrders = orders.length;
                    }

                    //loadpage();
                });
            }
        };
        fetchshoppincart();
        $scope.getStatusColor = function (statusID) {
            //debugger;
            if (statusID === 1002) {
                return 'due-date-sticker yellow-sticker';
            } else if (statusID === 1003) {
                return 'due-date-sticker green-sticker';
            } else if (statusID === 1004) {
                return 'due-date-sticker red-sticker';
            }
            return 'due-date-sticker red-sticker';
        };

        $scope.deleteOrder = function (orderID, name) {
            //debugger;
            $http({
                method: 'POST',
                url: '/EquipmentManagement/DeleteOrderEquipment',
                params: {
                    id: orderID
                }
            }).then(function (response) {
                //debugger;
                $log.log(response);
                var msg = "Order Placed On: " + name + "Was Deleted";
                var notif = {};
                notif.sender_ID = $rootScope.userDetails.USERT_ID;
                notif.typeID = 7;
                notif.message = msg;
                //reload cart
                $scope.emitNewNotifiction("newNotification");
                $scope.emitNewNotifiction("newOrder");
                if ($rootScope.equipmentHOD_ID !== null) {
                    //debugger;
                    notif.userID = $rootScope.equipmentHOD_ID;
                    //debugger;
                    $log.log(notif);
                    sendNotifEquipment(notif);
                }
                //if ($rootScope.masterUser_ID !== null) {
                //    notif.userID = $rootScope.masterUser_ID;
                //    sendNotifEquipment(notif);
                //    debugger;
                //}

                // loadpage();
            });
        };

        var sendNotifEquipment = function (notif) {
            //debugger;
            createEditNotifiAlertService.CreateUserNotification(notif)
               .then(function (success) {
                   if (success.status === 201) {
                       $scope.emitNewNotifiction("newNotification");
                       $scope.emitNewNotifiction("newOrder");
                       $scope.$parent.sendSignalRMessage("NewNotification");
                       $scope.$parent.sendSignalRMessage("NewNotification");
                   }
               }, function (error) {
               });
        }

        $scope.setParamsForDelete = function (orderID, EQName) {
            //debugger;
            $scope.deleteOrderOrderIDParam = orderID;
            $scope.deleteOrderEQNameParam = EQName;

        }

        $scope.emitNewNotifiction = function (notifmsg) {
            $scope.$emit(notifmsg);
        }

        if (Listlevel !== undefined && ListDepartment !== undefined && ListGender !== undefined) {
            $scope.Listlevel = Listlevel;
            $scope.ListDepartment = ListDepartment;
            $scope.ListGender = ListGender;
            $scope.model = {};
        }


        $scope.ActiDeact = function (user) {
            $http({
                method: 'POST',
                url: '/UserManagement/DeactivateActivateUser',
                params: {
                    id: user.USERT_ID
                }
            }).then(function (response) {
                if (response.status === 201) {
                    user.USERT_IsActive = (!user.USERT_IsActive);
                }
            });
        }

        var CurrentUser;
        $scope.EditUserDetails = function (user) {

            debugger;

            CurrentUser = user;

            $scope.model.name = user.USERT_Name;
            $scope.model.emailAddress = user.USERT_Email;
            $scope.model.PhoneNumber = user.USERT_PhoneNumber;
            $scope.model.Gender = { GENDER_ID: user.GENDER_ID, GENDER_Desc: Utilities_ExtractDictGender($scope.ListGender, user.GENDER_ID) }
            $scope.model.PhysycalAddress = user.USERT_Address;
            $scope.model.userID = user.USERT_ID;
            $scope.model.hashPass = user.USERT_HashPassword;
            $scope.model.USERT_IsActive = user.USERT_IsActive;
            $scope.model.USERT_MemberStatus = user.USERT_MemberStatus;
            $scope.model.USERT_Desc = user.USERT_Desc;
            $scope.model.USERT_OnlineStatus = user.USERT_OnlineStatus;
            $scope.model.USERT_MemberStatus = user.USERT_MemberStatus;
            $scope.model.USERT_PhotoLink = user.USERT_PhotoLink
            var DOBcopy = angular.copy(user.USERT_DOB);
            DOBcopy = DOBcopy.substring(6, user.USERT_DOB.length - 2);
            DOBcopy = new Date(parseInt(DOBcopy));
            $scope.model.DOB = DOBcopy;
            //USERT_DOB
            $scope.model.level = { LEVELT_ID: user.LEVELT_ID, LEVELT_Desc: Utilities_ExtractDictLevel($scope.Listlevel, user.LEVELT_ID) };
            $scope.model.dept = { DEPT_ID: user.DEPT_ID, DEPT_Desc: Utilities_ExtractDictDepart($scope.ListDepartment, user.DEPT_ID) };//user.DEPT_ID;

            debugger;

            oldlevel = $scope.model.level.LEVELT_ID;
            olddept = $scope.model.dept.DEPT_ID;
            alert(olddept);
            alert(oldlevel);
            $("#UserEditModal").modal('show');

        }
        $scope.cancel = function () {
            $("#UserEditModal").modal('hide');
        }

        $scope.SaveDetails = function () {

            debugger;

            if ($scope.model.name === undefined || $scope.model.name === null || $scope.model.name === "") {

                alert("Please Specify a name for the User.");
                
                debugger;

                return;
            } else if ($scope.model.emailAddress === undefined || $scope.model.emailAddress === null || $scope.model.emailAddress === "") {
                alert("Please Specify the User Email Addres.");
                return;
            } else if ($scope.model.PhoneNumber === undefined || $scope.model.PhoneNumber === null || $scope.model.PhoneNumber === "") {
                alert("Please Provide a valid phone number");
                return;
            } else if ($scope.model.Gender === undefined || $scope.model.Gender === null || $scope.model.Gender === "") {
                alert("Please Select the User Gender.");
                return;
            } else if ($scope.model.DOB === undefined || $scope.model.DOB === null || $scope.model.DOB === "") {
                alert("Please Specify the User Date of Birth.");
                return;
            } else if ($scope.model.level === undefined || $scope.model.level === null || $scope.model.level === "") {
                alert("Please Select the User Level.");
                return;
            } else if ($scope.model.PhysycalAddress === undefined || $scope.model.PhysycalAddress === null || $scope.model.PhysycalAddress === "") {
                alert("Please Specify the User Physical Address.");
                return;
            } else if ($scope.model.dept === undefined || $scope.model.dept === null || $scope.model.dept === "") {
                alert("Please Select the User Department.");
                return;
            }

            $("#UserEditModal").modal('hide');

            //debugger;

            debugger;

            var userModel = {
                id: $scope.model.userID,
                hashPass: $scope.model.hashPass,
                name: $scope.model.name,
                username: $scope.model.name,
                email: $scope.model.emailAddress,
                phone: $scope.model.PhoneNumber,
                genderID: $scope.model.Gender.GENDER_ID,
                DOB: $scope.model.DOB,
                address: $scope.model.PhysycalAddress,
                levelId: $scope.model.level.LEVELT_ID,
                deptID: $scope.model.dept.DEPT_ID,
                memberStatus: $scope.model.USERT_MemberStatus,
                isActive: $scope.model.USERT_IsActive,
                desc: $scope.model.USERT_Desc,
                onlineStatus: $scope.model.USERT_OnlineStatus,
                photoLink: $scope.model.USERT_PhotoLink,
                Department: $scope.model.dept.DEPT_Desc,
                olddeptID: olddept,
                oldlevelID: oldlevel
            };

            $http({
                method: 'POST',
                url: '/UserManagement/EditUser',
                data: userModel
            }).then(function (response) {

                //debugger;

                if (response.status === 201) {

                    CurrentUser.USERT_Name = $scope.model.name;
                    CurrentUser.USERT_Email = $scope.model.emailAddress;
                    CurrentUser.USERT_PhoneNumber = $scope.model.PhoneNumber;
                    CurrentUser.GENDER_ID = $scope.model.Gender.GENDER_ID;
                    CurrentUser.USERT_Address = $scope.model.PhysycalAddress;
                    CurrentUser.USERT_ID = $scope.model.userID;
                    CurrentUser.USERT_HashPassword = $scope.model.hashPass;
                    CurrentUser.USERT_IsActive = $scope.model.USERT_IsActive;
                    CurrentUser.USERT_MemberStatus = $scope.model.USERT_MemberStatus;
                    CurrentUser.USERT_Desc = $scope.model.USERT_Desc;
                    CurrentUser.USERT_OnlineStatus = $scope.model.USERT_OnlineStatus;
                    CurrentUser.USERT_MemberStatus = $scope.model.USERT_MemberStatus;
                    CurrentUser.USERT_DOB;
                    CurrentUser.LEVELT_ID = $scope.model.level.LEVELT_ID;
                    CurrentUser.DEPT_ID = $scope.model.dept.DEPT_ID
                }
            });
        }

        var Utilities_ExtractDictGender = function (Dict_Object, id) {
            var currencyObject = Dict_Object.filter(function (item) {
                return item.GENDER_ID === id;
            });
            return currencyObject[0].GENDER_Desc;
        }

        var Utilities_ExtractDictDepart = function (Dict_Object, id) {
            var currencyObject = Dict_Object.filter(function (item) {
                return item.DEPT_ID === id;
            });
            return currencyObject[0].DEPT_Desc;
        }

        var Utilities_ExtractDictLevel = function (Dict_Object, id) {
            var currencyObject = Dict_Object.filter(function (item) {
                return item.LEVELT_ID === id;
            });
            return currencyObject[0].LEVELT_Desc;
        }

        $scope.ChangePicture = function (e) {

            e.preventDefault();
            $("#upload:hidden").trigger('click');

            $('#upload:hidden').on('change', function (e) {
                SubmitButtonOnclick();
            });

            function SubmitButtonOnclick() {
                var formData = new FormData();
                var imagefile = document.getElementById("upload").files[0];
                formData.append("imageFile", imagefile);
                var xhr = new XMLHttpRequest();
                xhr.open("POST", "/UserManagement/UpdateUserPicture", true);
                xhr.addEventListener("load", function (evt) { UploadComplete(evt, imagefile.name); }, false);
                xhr.addEventListener("error", function (evt) { UploadFailed(evt); }, false);
                xhr.send(formData);
            }

            function UploadComplete(evt, fileName) {
                if (evt.target.status == 200) {

                    //debugger;

                    $("#UserPict").attr("src", "/TaskManagementImages/" + $rootScope.userDetails.USERT_ID + "/" + fileName);
                    $("#UserDropPict").attr("src", "/TaskManagementImages/" + $rootScope.userDetails.USERT_ID + "/" + fileName);
                    $("#UserBottPic").attr("src", "/TaskManagementImages/" + $rootScope.userDetails.USERT_ID + "/" + fileName);
                    $rootScope.userDetails.USERT_PhotoLink = fileName;
                }
                else {
                    alert("Error Uploading File");
                }
            }

            function UploadFailed(evt) {
                alert("There was an error attempting to upload the file.");

            }
        }

        //Manuella's Addition END ==================================================================

        $('#notificationMenu').on('hidden.bs.dropdown', function () {
            createEditNotifiAlertService.DeleteUserNotifications($rootScope.userDetails.USERT_ID).then(function (response) {
                $scope.numberOfUserNotification = 0;
            });
        });

        // SignalR connection
        MessageService.connectToserver();

        MessageService.setFunctionNameToRunOnMesRecv($scope, "receivedTaskNotification");

        MessageService.setFunctionNameToRunOnMesRecv($scope, "receivedReturnOrderNotification");

        $scope.receivedTaskNotification = function (message) {
            if (message !== undefined && message !== null && message !== "") {
                if (message === "new task") {
                    $rootScope.$broadcast('newTask');

                } else if (message === "completed task") {

                    $rootScope.$broadcast('newTask');
                    $rootScope.$broadcast('oneTaskCompleted');
                } else if (message === "NewNotification") {
                    fetchnewNotifications();
                    //MANU ADDEDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD
                    fetchshoppincart();
                    //MANU ADDEDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD
                    $rootScope.$broadcast("NewNotificationCreated");
                    $scope.$digest();
                } else if (message === "NewAlertCreated") {
                    fetchnewNotifications();
                    $rootScope.$broadcast("UpdateAlert");
                }
            }
        }

        $scope.receivedReturnOrderNotification = function (message) {
            if ($rootScope.userDetails !== undefined) {

                var listOfNonReturnOrd = JSON.parse(message);
                $log.log(listOfNonReturnOrd);

                var currentUser = listOfNonReturnOrd.filter(function (item) {
                    return item.USERT_ID === $rootScope.userDetails.USERT_ID
                });

                if (currentUser.length > 0) {

                    for (var i = 0; i < currentUser.length; i++) {
                        var msg = "Your stil have " + currentUser[i].OE_QuantityOrdered + " " + currentUser[i].EQ_Name + " to be Returned";
                        var notif = {};
                        notif.sender_ID = $rootScope.equipmentHOD_ID;
                        notif.typeID = 6;
                        notif.message = msg;
                        notif.userID = $rootScope.userDetails.USERT_ID;
                        createEditNotifiAlertService.CreateUserNotification(notif).then(function (success) {
                            if (success.status === 201) {
                                $log.log("Success!");
                            }
                        }, function (error) {
                            $log.log("Faillure!");
                        });
                    }
                    fetchnewNotifications();
                }
            }
        }

        $scope.sendSignalRMessage = function (message) {
            MessageService.sendNoticiation(message);
        }

        $scope.getUser = function (user) {
        }

        function fetchnewNotifications() {
            $scope.ListOfUserNotification = JSON.parse(createEditNotifiAlertService.GetUserNotif($rootScope.userDetails.USERT_ID));

            //debugger;

            $scope.numberOfUserNotification = $scope.ListOfUserNotification.length;

            //$.each($scope.ListOfUserNotification, function (index, item) {
            //    item.notificationDateTime = new Date(parseInt(item.notificationDateTime.substring(6, (item.notificationDateTime).length - 2)));
            //    item.notificationDateTime = getFullDateTime(item.notificationDateTime);
            //});

            if (!$scope.$$phase) {
                $scope.$digest();
            }
        }
    }]);
})();
