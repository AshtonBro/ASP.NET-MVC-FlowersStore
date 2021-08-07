/*Change quantity and Collect all prices in basket page and count total sum*/
export function changePlus(e) {
    let id = e.id.replace("_P", "");
    let quantityBasket = id + "_Q";
    let productPrice = id + "_Pr";
    let subTotal = id + "_St";
    let basketTotal = "SumTotalBasket";

    let quantity = document.getElementById(quantityBasket);
    if (parseInt(quantity.innerText) > 101) {
        return;
    }

    quantity.innerText++;

    let subSumTotal = parseInt(document.getElementById(productPrice).innerText) * parseInt(quantity.innerText);
    document.getElementById(subTotal).innerText = subSumTotal;

    let total = parseInt(document.getElementById(basketTotal).innerText) + parseInt(document.getElementById(productPrice).innerText);

    document.getElementById(basketTotal).innerText = total;
}