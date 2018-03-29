'use strict';

(function () {

    var appHealth_sharedDataService = angular.module('sharedDataServiceModule');
    appHealth_sharedDataService.factory('sharedDataService_Ver2', ['$http', '$log', function ($http, $log) {

        var shareModel = null;
        // Getters and Setters
        var setShareModel = function (subSystem, key, value) {
            if (shareModel === undefined || shareModel === null)
                shareModel = {};
            if (shareModel[subSystem] === undefined || shareModel[subSystem] === null)
                shareModel[subSystem] = {};
            shareModel[subSystem][key] = value;
        };
        var getShareModel = function (subSystem, key) {
            if (shareModel === undefined || shareModel === null)
                return null;
            if (shareModel[subSystem] === undefined || shareModel[subSystem] === null)
                return null;
            return shareModel[subSystem][key] === undefined ? null : shareModel[subSystem][key];
        };

        var CleanUpService = function () {
            if (shareModel !== undefined && shareModel !== null && _.isObject(shareModel)) {
                for (var subSystem in shareModel) {
                    delete shareModel[subSystem];
                }
            }
            shareModel = null;
        };

        var CleanUpServiceKey = function (subSystem, key) {
            if (shareModel !== null) {
                delete shareModel[subSystem][key];
            }
        }

        var CleanUpServiceSubsystem = function (subSystem) {
            if (shareModel !== null) {
                delete shareModel[subSystem];
            }
        }

        var CleanUpServiceKeyExcept = function (subSystem, key) {
            if (shareModel !== undefined && shareModel !== null && _.isObject(shareModel)) {
                if (key !== undefined && key !== null) {
                    var subSystemPtr = shareModel[subSystem];
                    for (var k in subSystemPtr) {
                        if (k !== key)
                            delete shareModel[subSystem][k];
                    }
                } else {
                    CleanUpServiceSubsystem(subSystem)
                }
            }
        }

        // Returning an Object with callable methods.
        return {
            setShareModel: setShareModel,
            getShareModel: getShareModel,
            CleanUpService: CleanUpService,
            CleanUpServiceKey: CleanUpServiceKey,
            CleanUpServiceKeyExcept: CleanUpServiceKeyExcept,
            CleanUpServiceSubsystem: CleanUpServiceSubsystem
        };
    }]);
}());