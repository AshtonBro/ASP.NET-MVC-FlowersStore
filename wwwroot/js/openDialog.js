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
    document.querySelector('.close').addEventListener('click', hideDialog)
    document.querySelector('.Hidable').addEventListener('click', hideDialog)
    document.querySelector('.loginBtn').addEventListener('click', (event) => {
        if (document.querySelector('.alert-danger').textContent !== null) {
            setTimeout(() => {
                document.querySelector('.alert-danger').textContent = '';
            }, 3000);
        }
    })
}

export function hideDialog() {
    $('#dialogBox').css("visibility", "hidden");
    document.getElementById('cover').removeEventListener('mousedown', _checkMouseIsOver, true);
    document.querySelector('.close').removeEventListener('click', hideDialog)
    document.querySelector('.Hidable').removeEventListener('click', hideDialog)
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
