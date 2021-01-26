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

export function openDialog(url, formId = null) {
    startLoading(null);

    ajaxPost(url, formId, function (data) {
        $("#dialogBox").html(data);
        $("#linkInput").val(window.location.origin + $("#linkInput").val());
        showDialog();
    });

}


export var _checkMouseIsOver = function () {
    var target = event.target;
    //console.log('target: ', target);          
    if (!target.closest("#dialogBox")) {
        hideDialog();
    }
}

export function showDialog() {
    $('#dialogBox').css("visibility", "visible");
    startLoading();
    document.getElementById('cover').addEventListener('mousedown', _checkMouseIsOver, true);
  
    let authBtns = document.querySelectorAll('.auth');
    authBtns.forEach(btn => {
        btn.addEventListener('click', () => {
            let alertDanger = document.querySelectorAll('.alert-danger');
            alertDanger[0].textContent = '';
            alertDanger[1].textContent = '';
            //setTimeout(() => {
            //    clearTimeout(timerDeleteText);
            //    
            //    if (alertDanger[0].textContent !== null || alertDanger[1].textContent !== null) {
            //        var timerDeleteText = setTimeout(() => {
                        
            //        }, 3000);
            //    }
            //}, 100);
        })
    })
}

export function hideDialog() {
    $('#dialogBox').css("visibility", "hidden");
    document.getElementById('cover').removeEventListener('mousedown', _checkMouseIsOver, true);
    endLoading();
}


export function startLoading() {
    document.getElementById('dual-ring').style.display = 'block';
    document.getElementById('cover').style.display = 'block';
    // document.getElementById('input-container').style.zIndex = 0;
}

export function endLoading() {
    document.getElementById('dual-ring').style.display = 'none';
    document.getElementById('cover').style.display = 'none';
    // document.getElementById('input-container').style.zIndex = 0;
}

export function dialogPost(e, key, url) {
    e.preventDefault();

    var formId = key + "_F";
    var msgId = key + "_M";
    var btnId = key + "_B";

    var btn = document.getElementById(btnId);
    var msg = document.getElementById(msgId);

    var btnVal = btn.value;

    var successCallBack = function (link) {
        btn.disabled = true;
        btn.value = "Redirecting....";
        if (msg != null) msg.innerText = null;
        window.location.href = link;
    }

    var errorCallBack = function (error) {
        msg.innerText = error;
        btn.disabled = false;
        btn.value = btnVal;
    }

    if (msg != null) msg.innerText = null;
    btn.disabled = true;
    btn.value = "Working...";

    ajaxPostJsonResult(e, url, formId, successCallBack, errorCallBack);
}
