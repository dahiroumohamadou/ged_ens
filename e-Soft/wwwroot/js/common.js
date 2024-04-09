var AjaxManager = {
    GETAPI: function (serviceUrl, successCallback, errorCallback) {
        $.ajax({
            type: "GET",
            url: serviceUrl,
            async: false,
            cache: false,
            dataType: "json",
            success: successCallback,
            error: errorCallback
        });
    },
    POSTAPI: function (serviceUrl,jsonParams, successCallback, errorCallback) {
        $.ajax({
            type: "POST",
            url: serviceUrl,
            data: jsonParams,
            contentType: "application/json",
            success: successCallback,
            error: errorCallback
        });
    },
    PUTAPI: function (serviceUrl, jsonParams, successCallback, errorCallback) {
        $.ajax({
            type: "PUT",
            url: serviceUrl,
            data: jsonParams,
            contentType: "application/json",
            success: successCallback,
            error: errorCallback
        });
    },
    DELETEAPI: function (serviceUrl, successCallback, errorCallback) {
        $.ajax({
            type: "DELETE",
            url: serviceUrl,
            contentType: "application/json",
            success: successCallback,
            error: errorCallback
        });
    },
}