export function ajaxPost(url, formId, callBack) {
    //if ($("#" + formId).valid()) {
    var formData = new FormData($("#" + formId).get(0));
    $.ajax({
        url: url,
        type: "POST",
        dataType: "html",
        processData: false,
        contentType: false,
        data: formData,//$("#" + formId).serialize(),
        success: function (response) {
            callBack(response);
        },
        error: function (error) {
            //handleError(error);
        }
    });
    //}
}

export function ajaxGet(url, params, callBack) {
    $.ajax({
        url: url,
        type: "GET",
        dataType: "html",
        contentType: "application/json; charset=utf-8",
        data: params,
        async: true,
        success: function (response) {
            callBack(response);
        },
        error: function (error) {
            //handleError(error);
        }
    });
}

export function ajaxPostJsonResult(e, url, formId, successCallBack, errorCallBack) {
    e.preventDefault();
    var callback = function (data) {
        var result = JSON.parse(data);
        if (result['error'] != null) {
            errorCallBack(result['error']);
        } else {
            var link = result['link'];
            successCallBack(link);
            window.location.href = link;
        }
    }
    ajaxPost(url, formId, callback);
}


export function ajaxDefaultPost(e, key, url) {
    e.preventDefault();

    var formId = "frm_" + key;
    var msgId = "msg_" + key;
    var btnId = "btn_" + key;

    var btn = document.getElementById(btnId);
    var msg = document.getElementById(msgId);

    var btnVal = btn.value;

    var successCallBack = function (link) {
        btn.disabled = true;
        btn.value = "Redirecting....";
        if (msg != null) msg.innerText = null;
    }

    var errorCallBack = function (error) {
        msg.innerText = error;
        btn.disabled = false;
        btnVal.value = btnVal;
    }

    if (msg != null) msg.innerText = null;
    btn.disabled = true;

    btnVal.value = "Working...";

    ajaxPostJsonResult(e, url, formId, successCallBack, errorCallBack);
}
