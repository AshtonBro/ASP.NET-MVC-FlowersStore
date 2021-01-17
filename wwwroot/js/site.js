'use strict';

import colorationColorIcon from './colorationColorIcon.js';
import changeQuantity from './changeQuantity.js';
import slickSlider from './slickSlider.js';
import countTotal from './countTotal.js';
import { changeMinus } from './changeMinus.js';
import { changePlus } from './changePlus.js';
import { ajaxPost, ajaxGet, ajaxPostJsonResult, ajaxDefaultPost } from './ajaxHelper.js';
import { dialogPost, endLoading, hideDialog, openDialog, showDialog, startLoading} from './openDialog.js';

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

if (window.location.pathname == '/Basket/Index') {
    countTotal();
    window.changeMinus = changeMinus;
    window.changePlus = changePlus;
}