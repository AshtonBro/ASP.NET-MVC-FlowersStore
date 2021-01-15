/* Collect all prices in basket page and count total sum*/
const countTotal = () => {
        let priceAmount = document.querySelectorAll(".basket-price-amount");
        let amountTotal = document.querySelector(".amount-total");

        let total = 0;
        for (var i = 0; i < priceAmount.length - 1; i++) {
            total += parseInt(priceAmount[i].innerText);
        }

        amountTotal.innerText = total;
}

export default countTotal;