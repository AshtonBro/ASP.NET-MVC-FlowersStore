// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Methods from Max


function ajaxPost(url, formId, callBack) {
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

function ajaxGet(url, params, callBack) {
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

function ajaxPostJsonResult(e, url, formId, successCallBack, errorCallBack) {
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


function ajaxDefaultPost(e, key, url) {
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

function openDialog(url, formId = null) {
    startLoading(null);

    ajaxPost(url, formId, function (data) {
        $("#dialogBox").html(data);
        $("#linkInput").val(window.location.origin + $("#linkInput").val());
        showDialog();
    });

}


var _checkMouseIsOver = function () {
    var target = event.target;
    //console.log('target: ', target);          
    if (!target.closest("#dialogBox")) {
        hideDialog();
    } 
}

function showDialog() {
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

function hideDialog() {
    $('#dialogBox').css("visibility", "hidden");
    document.getElementById('cover').removeEventListener('mousedown', _checkMouseIsOver, true);
    document.querySelector('.close').removeEventListener('click', hideDialog)
    document.querySelector('.Hidable').removeEventListener('click', hideDialog)
    endLoading();
}


function startLoading() {
    document.getElementById('dual-ring').style.display = 'block';
    document.getElementById('cover').style.display = 'block';
    // document.getElementById('input-container').style.zIndex = 0;
}

function endLoading() {
    document.getElementById('dual-ring').style.display = 'none';
    document.getElementById('cover').style.display = 'none';
    // document.getElementById('input-container').style.zIndex = 0;
}

function dialogPost(e, key, url) {
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

//Add handlers to update text based on value changes.

//document.querySelectorAll('[source="value"]').forEach(ctrl => {
//    ctrl.addEventListener("input",
//        (e) => {
//            var data = {
//                point: e.target.getAttribute("point"),
//                value: e.target.value
//            };
//            $.ajax({
//                url: "/Tags/UpdateText",
//                type: "post",
//                contentType: 'application/x-www-form-urlencoded',
//                data: data,
//                success: function (result) {
//                    console.log(result);
//                    var valueInput = document.querySelector('[point="' + result.point + '"]');
//                    var textInput = document.getElementById(valueInput.id + "_text");
//                    if (textInput !== null) {
//                        textInput.value = result.text;
//                    }
//                }
//            });
//        });
//});


// * Slider Slick jQuery path of code
$(document).ready(function () {
    $(".sl").slick({
        arrows: false,
        fade: true,
        cssEase: 'linear',
        slidesToShow: 1,
        slidesToScroll: 1,
        autoplay: true,
        autoplaySpeed: 5000,
        speed: 1000,
        infinite: true
    });

    $("#singl1").fancybox({
        helpers: {
            title: {
                type: 'float'
            }
        }
    });

    $("#singl2").fancybox({
        openEffect: 'elastic',
        closeEffect: 'elastic',

        helpers: {
            title: {
                type: 'inside'
            }
        }
    });

    $("#singl3").fancybox({
        openEffect: 'none',
        closeEffect: 'none',
        helpers: {
            title: {
                type: 'outside'
            }
        }
    });

    colorationColorIcon();
    changeQuantity();
});


/* Coloration box */
const colorationColorIcon = () => {
    const cardColorIcon = document.querySelectorAll('.card-color-icon'),
        cardColor = document.querySelectorAll('.card-color');
    for (var i = 0; i < cardColor.length; i++) {
        switch (cardColor[i].innerHTML) {
            case 'Зелёный':
             cardColorIcon[i].style.backgroundColor = 'Green';
                continue;
            case 'Желтый':
                cardColorIcon[i].style.backgroundColor = 'Yellow';
                continue;
            case 'Красный':
                cardColorIcon[i].style.backgroundColor = 'Red';
                break;
        }
    };
};


/* Change quantity shopping card */
const changeQuantity = () => {
    const plusBtnList = document.querySelectorAll('.counter-plus'),
        minusBtnList = document.querySelectorAll('.counter-minus');

    for (let plusBtn of plusBtnList) {
        plusBtn.onclick = () => {
            let currentSpan = plusBtn.previousElementSibling;
            if (+(currentSpan.innerHTML) < 101)
            {
                currentSpan.innerHTML = +(currentSpan.innerHTML) + 1;
            }
        }
    }

    for (let minusBtn of minusBtnList) {
        minusBtn.onclick = () => {
            let currentSpan = minusBtn.nextElementSibling;
            if (+(currentSpan.innerHTML) > 0) {
                currentSpan.innerHTML = +(currentSpan.innerHTML) - 1;
            }
        }
    }
};
