//Controller must be attached to module

var equipment = angular.module('equipmentModule');

equipment.controller('createEditViewEquipmentCtr', ['$rootScope', '$scope', '$http', '$log', 'createEditNotifiAlertService', function ($rootScope, $scope, $http, $log, createEditNotifiAlertService) {
    //debugger;
    //Attach a model in scope
    $scope.model = {};
    $scope.EquipmentOrders = {};
    var ifUserCan = false;
    var userLevel = $rootScope.userDetails.LEVELT_ID;
    if ((userLevel) === 1 || (userLevel === 2)) {
        ifUserCan = true;
        //debugger;
    }
    $scope.userCanCreateEdit = ifUserCan;
    //$scope.userCanCreateEdit = false;
    //Function to load Responsibility
    var loadpage = function () {
        //Empty the InputBox
        //$scope.model.Description = "";
        //var userid = '68FD7263-299B-4991-BC8A-C4F610D32CFB';
        //debugger;
        var ifUserCan = false;
        var userLevel = $rootScope.userDetails.LEVELT_ID;
        
        if ((userLevel) === 1 || (userLevel === 2)) {
            ifUserCan = true;
        }
        $scope.userCanCreateEdit = ifUserCan;
        $http({
            method: 'GET',
            url: '/EquipmentManagement/GetEquipments'
        }).then(function (response) {
            //debugger;
            $log.log(response);

            //Create new object in scope that will contain the list of returned objects

            var eqs = response.data;
            for (var i = 0; i < eqs.length; i++) {
                var available = eqs[i].EQ_Quantity - eqs[i].EQ_QuantityOrdered;
                var percentageAvail = (available / eqs[i].EQ_Quantity) * 100;
                if (percentageAvail <= 25.00) {
                    eqs[i].color = 'equipment-red';
                }
                else if (available >= eqs[i].EQ_Quantity) {
                    eqs[i].color = 'equipment-green';
                }
                else {
                    eqs[i].color = 'equipment-yellow';
                }
                eqs[i].qtyOrdered = 0;
                eqs[i].orderComment = '';
                eqs[i].isOrdered = false;
            }
            $scope.Equipments = eqs;

        });
        //debugger;
        var eish = $scope.Equipments;
        //Equipment Management Stuff Begin -----------------------------------------------
        $scope.equipmentManagementTitle = 'My Orders:';
        $scope.showMyOrders = true;
        $scope.showTodaysOrders = true;
        $scope.todaysOrdersClickable = false;
        if ((userLevel) === 1 || (userLevel === 2)) {
            $scope.todaysOrdersClickable = true;
        }
        
    };
    //debugger;
    loadpage();
    //Get list of status
    var loadStatus = function () {
        //debugger;
        var statuss = [];
        var status1 = {};
        status1.ID = 1002;
        status1.Text = 'Pending';
        statuss[0] = status1;
        var status2 = {};
        status2.ID = 1003;
        status2.Text = 'Returned';
        statuss[1] = status2;
        var status3 = {};
        status3.ID = 1004;
        status3.Text = 'Out';
        statuss[2] = status3;
        //debugger;
        $scope.statusT = statuss;
        // $log.log($scope.statusT);
    };
    loadStatus();
    //Function to add Equipment to DB
    $scope.addEquipment = function () {
        var userLevel = $rootScope.userDetails.LEVELT_ID;
        
        if ((userLevel) === 1 || (userLevel === 2)) {
            var eM = {};
            eM.name = $scope.model.AddEqName;
            eM.desc = $scope.model.AddEqDescription;
            eM.quantity = $scope.model.AddEqQuantity;
            if (userLevel === 2) {
                eM.isApproved = false;
                //SEND NOTIFICATION TO SUPER USER HERE===============================================================================
            } else {
                eM.isApproved = true;
            }
            //Empty the inputs
            $scope.model.AddEqName = '';
            $scope.model.AddEqDescription = '';
            $scope.model.AddEqQuantity = '';

            $http({
                method: 'POST',
                url: '/EquipmentManagement/CreateEquipment',
                data: eM
            }).then(function (response) {
                //debugger;
                $log.log(response);
                $(".collapse1").collapse('toggle');
                //Reload Div
                loadpage();
            });
        }
        $(".collapse1").collapse('toggle');
    };
    $scope.placeOrder = function (eqsss) {
        //debugger;
        var selectedDate = calendarDate;

        var orderM = [];
        
        var eqs = $scope.Equipments;
        for (var i = 0; i < eqs.length; i++) {
            var index = eqs[i].EQ_ID;
            if ($scope.model[eqs[i].EQ_ID] >= 1) {
                //debugger;
                var anOrder = {};
                anOrder.equipmentID = eqs[i].EQ_ID;
                anOrder.userID = $rootScope.userDetails.USERT_ID;
                anOrder.qtyOrdered = $scope.model[index];    //OrdersMade[eqs[i]].qty;
                anOrder.date = selectedDate;
                anOrder.comment = eqs[i].orderComment;

                orderM.push(anOrder);

                $scope.model[index] = 0;
                //debugger;
            }
            
            //Empty the inputs
            //$scope.model.PlaceOrderEqQuantity = '';
            //$scope.model.PlaceOrderComments = '';
        }

        $http({
                    method: 'POST',
                    url: '/EquipmentManagement/CreateOrderEquipment',
                    data: orderM
                }).then(function (response) {
                    //debugger;
                    $log.log(response);
                    $(".collapse1").collapse('toggle');
                   
                    //SEND NOTIFICATION BEGIN ===========================================
                    var msg = "New Orders Placed.";
                    var towho = null;
                    $scope.emitNewNotifiction("newOrder");
                    $scope.emitNewNotifiction("newNotification");
                    if ($rootScope.equipmentHOD_ID !== null) {
                        towho = $rootScope.equipmentHOD_ID;
                        sendNotification($rootScope.userDetails.USERT_ID, 5, msg, towho);
                    }
                });
        loadpage();

    };
    //Function to show details of an Equipment
    var loadOrderDetails = function (equipmentID, qtyOrdered) {
        //var res = {};
        //res.OrdersFromDB = [];
        var selectedDate = calendarDate;
        $http({
            method: 'GET',
            url: '/EquipmentManagement/OrdersPerEquipment',
            params: {
                eqID: equipmentID,
                date: selectedDate
            }
        }).then(function (response) {
            //debugger;
            $log.log(response);
            $(".collapse1").collapse('toggle');
            //Reload Div
            var orders = response.data;
            $scope.model.EquipName = equipmentID;
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
                } else if (orders[i].STATUST_ID === 1003) {
                    orders[i].STATUST_Desc = 'Returned';
                } else if (orders[i].STATUST_ID === 1004) {
                    orders[i].STATUST_Desc = 'Out';
                }
            }

            $scope.ordersOfTheEquipment = orders;

            //loadpage();
        });
    }
    $scope.equipmentOrderDetails = function (equipmentID, qtyAvailable, chosenID, qty, qtyOrdered, eqName) {
        loadOrderDetails(equipmentID, qtyOrdered);
        $scope.setChosenQtyAvailable(qtyAvailable, chosenID, qty, qtyOrdered, eqName);
    };
    $scope.disableDropdown = function (statusID) {
        //debugger;
        if (statusID === 1002) {
            //debugger;
            return false;
        } else if (statusID === 1003) {
            return true;
        } else if (statusID === 1004) {
            return false;
        }
        return false
    }
    $scope.setChosenQtyAvailable = function (qtyAvailable, chosenID, qty, qtyOrdered, eqName) {
        var qtyAvail = qtyAvailable;
        $scope.chosenQtyAvail = qtyAvail;
        $scope.chosenEquip = chosenID;
        $scope.quantity = qty;
        $scope.quantityOrdered = qtyOrdered;
        $scope.EQName = eqName;
    };
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
    $scope.changeStatus = function (chosenEquip, statusID, orderID, qty, statText, clientWhoPlacedOrder) {
        //debugger;
        //check if user is equipment manager or master user
        var userLevel = $rootScope.userDetails.LEVELT_ID;
        
        if ((userLevel) === 1 || (userLevel === 2)) {
            //call function to change status
            var ueM = {};
            ueM.eqID = chosenEquip;
            ueM.statusID = statusID;
            ueM.OrderEqID = orderID;
            ueM.time = new Date;
            $http({
                method: 'POST',
                url: '/EquipmentManagement/UpdateEquipmentStatus',
                data: ueM
            }).then(function (response) {
                //SEND NOTIFICATION BEGIN ===========================================
                var msg = "Your Order Is Now:   " + statText;
                //debugger;
                $scope.emitNewNotifiction("newOrder");
                $scope.emitNewNotifiction("newNotification");
                
                sendNotification($rootScope.userDetails.USERT_ID, 6, msg, clientWhoPlacedOrder);
                //sendNotification($rootScope.userDetails.USERT_ID, 6, msg, $rootScope.masterUser_ID);
                //SEND NOTIFICATION END ===========================================

                loadOrderDetails(chosenEquip, qty);
                loadpage();
            });
        }
        
        
    }
    var sendNotification = function (senderID, type, msg, toWhichUserID) {
        //SEND NOTIFICATION BEGIN ===========================================
        var notif = {};
        notif.sender_ID = senderID;
        notif.typeID = type;
        notif.message = msg;
        notif.userID = toWhichUserID;

        createEditNotifiAlertService.CreateUserNotification(notif)
        .then(function (success) {
            if (success.status === 201) {
                $scope.emitNewNotifiction("newOrder");
                $scope.emitNewNotifiction("newNotification");
                $scope.$parent.sendSignalRMessage("NewNotification");
                $scope.$parent.sendSignalRMessage("NewNotification");
            }
        }, function (error) {
        });
        //SEND NOTIFICATION END ===========================================
    };
    $scope.emitNewNotifiction = function (notifmsg) {
        $scope.$emit(notifmsg);
    }
    $scope.setParamsForDelete = function (orderID, EQName) {
        //debugger;
        $scope.deleteOrderOrderIDParam = orderID;
        $scope.deleteOrderEQNameParam = EQName;

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

            //SEND NOTIFICATION BEGIN =================================================================
            var sender_ID = $rootScope.userDetails.USERT_ID;
            var msg = "Order By: " + sender_ID + ", Placed On: " + name + ", Was Deleted";
            var typeID = 7;
            $scope.emitNewNotifiction("newNotification");
            $scope.emitNewNotifiction("newOrder");
            if ($rootScope.equipmentHOD_ID !== null) {
                var toWho = $rootScope.equipmentHOD_ID;
                $log.log(notif);
                sendNotification(sender_ID, typeID, msg, toWho);
            }
            //SEND NOTIFICATION BEGIN =================================================================

            // loadpage();
            fetchshoppincart();
        });
    };





































    //EQUIPMENT PAGE STUFF BEGIN: ======================================================================================================================
    var setShowOrHide = function (myOrders, eQManagerOrders) {
        //debugger;
        $scope.showMyOrders = myOrders;
        $scope.showTodaysOrders = eQManagerOrders;
    }
    var fetchshoppincart = function () {
        //debugger;
        $scope.equipmentManagementTitle = 'My Orders:';
        setShowOrHide(true, false);
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
    $scope.showMyOrdersDiv = function () {
        fetchshoppincart();
    }
    $scope.showAllOrdersDiv = function () {
        loadOrdersOfToday();
        loadTodaysOrdersAPICall();
    }
    var loadOrdersOfToday = function () {
        //debugger;
        setShowOrHide(false, true);
    };
    var loadEQsEQPage = function () {
        //Empty the InputBox
        //$scope.model.Description = "";
        //var userid = '68FD7263-299B-4991-BC8A-C4F610D32CFB';
       
        $http({
            method: 'GET',
            url: '/EquipmentManagement/GetEquipments'
        }).then(function (response) {
            //debugger;
           

            //Create new object in scope that will contain the list of returned objects

            var eqs = response.data;
            for (var i = 0; i < eqs.length; i++) {
                var available = eqs[i].EQ_Quantity - eqs[i].EQ_QuantityOrdered;
                var percentageAvail = (available / eqs[i].EQ_Quantity) * 100;
                if (percentageAvail <= 25.00) {
                    eqs[i].color = 'equipment-red';
                }
                else if (available >= eqs[i].EQ_Quantity) {
                    eqs[i].color = 'equipment-green';
                }
                else {
                    eqs[i].color = 'equipment-yellow';
                }
                eqs[i].qtyOrdered = 0;
                eqs[i].orderComment = '';
                eqs[i].isOrdered = false;
            }
            $scope.EquipmentsEQPage = eqs;
            //$log.log("length" + $scope.EquipmentsEQPage.length);
        });

        //Equipment Management Stuff Begin -----------------------------------------------
        
        
       
    };
    debugger;
    loadEQsEQPage();
    loadOrdersOfToday();
    fetchshoppincart();
    $scope.setParamsForDeleteEQPage = function (orderID, EQName) {
        //debugger;
        $scope.deleteOrderOrderIDParamEQPage = orderID;
        $scope.deleteOrderEQNameParamEQPage = EQName;

    };
    $scope.deleteOrderEQPage = function () {
        //debugger;
        $http({
            method: 'POST',
            url: '/EquipmentManagement/DeleteOrderEquipment',
            params: {
                id: $scope.deleteOrderOrderIDParamEQPage
            }
        }).then(function (response) {
            //debugger;
            $log.log(response);

            //SEND NOTIFICATION BEGIN =================================================================
            var sender_ID = $rootScope.userDetails.USERT_ID;
            var msg = "Order By: " + sender_ID + ", Placed On: " + name + ", Was Deleted";
            var typeID = 7;
            $scope.emitNewNotifiction("newNotification");
            $scope.emitNewNotifiction("newOrder");
            if ($rootScope.equipmentHOD_ID !== null) {
                var toWho = $rootScope.equipmentHOD_ID;
                $log.log(notif);
                sendNotification(sender_ID, typeID, msg, toWho);
            }
            //SEND NOTIFICATION BEGIN =================================================================

            // loadpage();
            //debugger;
            fetchshoppincart();
        });
    };
    var loadTodaysOrdersAPICall = function () {
        //debugger;
        $scope.equipmentManagementTitle = 'All Orders Placed Today:';
        // setShowOrHide(false, true);
        if ($rootScope.userDetails !== undefined) {
            //debugger;
            //Call the service for shopping cart
            var selectedDate = calendarDate;
            $scope.numberOfOrders = 0;
            $http({
                method: 'GET',
                url: '/EquipmentManagement/GetTodaysOrders',
                params: {
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

                    $scope.allOrders = orders;
                    $scope.numberOfOrders = orders.length;
                }

                //loadpage();
            });
        }

    };
    $scope.changeStatusEQPage = function (chosenEquip, statusID, orderID, qty, statText, clientWhoPlacedOrder) {
        //debugger;
        //check if user is equipment manager or master user
        var userLevel = $rootScope.userDetails.LEVELT_ID;

        if ((userLevel) === 1 || (userLevel === 2)) {
            //call function to change status
            var ueM = {};
            ueM.eqID = chosenEquip;
            ueM.statusID = statusID;
            ueM.OrderEqID = orderID;
            ueM.time = new Date;
            $http({
                method: 'POST',
                url: '/EquipmentManagement/UpdateEquipmentStatus',
                data: ueM
            }).then(function (response) {
                //SEND NOTIFICATION BEGIN ===========================================
                var msg = "Your Order Is Now:   " + statText;
                //debugger;
                $scope.emitNewNotifiction("newOrder");
                $scope.emitNewNotifiction("newNotification");

                sendNotification($rootScope.userDetails.USERT_ID, 6, msg, clientWhoPlacedOrder);
                //sendNotification($rootScope.userDetails.USERT_ID, 6, msg, $rootScope.masterUser_ID);
                //SEND NOTIFICATION END ===========================================
                loadTodaysOrdersAPICall();
                //loadOrderDetails(chosenEquip, qty);
                //loadpage();
            });
        }


    }
    
    //EQUIPMENT PAGE STUFF END: ======================================================================================================================












    $scope.validatequantityordered = function (max, ID) {

           if (parseInt($scope.model[ID]) < 0)
               $scope.model[ID] = 0;
           if (parseInt($scope.model[ID]) > parseInt(max)) {
               $scope.model[ID] = max;
           }

        

    }

}]);