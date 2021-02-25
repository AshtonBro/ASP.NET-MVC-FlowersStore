'use strict';

import colorationColorIcon from './colorationColorIcon.js';
import changeQuantity from './changeQuantity.js';
import slickSlider from './slickSlider.js';
import countTotal from './countTotal.js';
import { changeMinus } from './changeMinus.js';
import { changePlus } from './changePlus.js';
import { showInfoBox } from './showInfoBox.js';
import {
    ajaxPost,
    ajaxGet,
    ajaxPostJsonResult,
    ajaxDefaultPost,
    dialogPost,
    endLoading,
    hideDialog,
    openDialog,
    showDialog,
    startLoading
} from './ajaxDialogPosts.js';

slickSlider();
colorationColorIcon();
changeQuantity();
window.ajaxPost = ajaxPost;
window.ajaxGet = ajaxGet;
window.ajaxPostJsonResult = ajaxPostJsonResult;
window.ajaxDefaultPost = ajaxDefaultPost;
window.dialogPost = dialogPost;
window.endLoading = endLoading;
window.hideDialog = hideDialog;
window.openDialog = openDialog;
window.showDialog = showDialog;
window.startLoading = startLoading;
window.showInfoBox = showInfoBox;

if (window.location.pathname == '/Basket/Index') {
    countTotal(".basket-price-amount", ".amount-total");
    window.changeMinus = changeMinus;
    window.changePlus = changePlus;
}

if (window.location.pathname == '/Checkout/Index') {
    console.log('checkout page loaded');
}