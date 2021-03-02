/* Collect all prices in basket page and count total sum*/
const countTotal = (priceAmount, amountTotal) => {
    let price = document.querySelectorAll(priceAmount);
    let total = document.querySelector(amountTotal);

    let _total = 0;
    for (var i = 0; i < price.length; i++) {
        _total += parseInt(price[i].innerText);
    }

    total.innerText = _total;
}

export default countTotal;