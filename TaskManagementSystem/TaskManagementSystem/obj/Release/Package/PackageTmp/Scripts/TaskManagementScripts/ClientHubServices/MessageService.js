(function () {
    'use strict';

    var mainApp_messageService = angular.module('messages');
    mainApp_messageService.factory('MessageService', ['$rootScope', '$http', function ($rootScope, $http) {

        var client_ConnectionID;
        var responses;
        var functionNameToRunOnMessRecv;
        var currentSscope;

        // Return the ID connection of the current user.
        var getClientID = function () {
            return client_ConnectionID;
        }

        var connection = $.hubConnection();
        var messageServiceHubProxy = connection.createHubProxy('MessageServiceHub');

        var sendNoticiation = function (message) {
            messageServiceHubProxy.invoke('broadcastMessage', client_ConnectionID, message);
        }

        messageServiceHubProxy.on('receiveNotification', function (message) {
            currentSscope[functionNameToRunOnMessRecv](message);
        });

        messageServiceHubProxy.on('receivedMessage', function (message) {
            currentSscope[functionNameToRunOnMessRecv](message);
        });
        

        var connectToserver = function () {

            connection.start()
               .done(function () {
                   client_ConnectionID = connection.id;
                   $rootScope.client_ConnectionID = connection.id;
                   console.log('Now connected, connection ID =' + client_ConnectionID);
               })
               .fail(function () {
                   console.log('Could not connect. Something went wrong');
               });
        }

        var setFunctionNameToRunOnMesRecv = function (CurrentScope, functionName) {
            currentSscope = CurrentScope;
            functionNameToRunOnMessRecv = functionName;
        }

        return {
            connectToserver: connectToserver,
            sendNoticiation: sendNoticiation,
            setFunctionNameToRunOnMesRecv: setFunctionNameToRunOnMesRecv,
            getClientID: getClientID
        }

    }]);
})();