import colorationColorIcon from './colorationColorIcon.js';
import changeQuantity from './changeQuantity.js';
import slickSlider from './slickSlider.js';
import countTotal from './countTotal.js';

colorationColorIcon();
changeQuantity();
slickSlider();

if (window.location.pathname == '/Basket/Index') {
    countTotal();
}

