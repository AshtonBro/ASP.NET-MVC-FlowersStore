/* Change quantity shopping card */
const changeQuantity = () => {
    const plusBtnList = document.querySelectorAll('.counter-plus'),
        minusBtnList = document.querySelectorAll('.counter-minus');

    for (let plusBtn of plusBtnList) {
        plusBtn.onclick = () => {
            let currentSpan = plusBtn.previousElementSibling;
            if (+(currentSpan.innerHTML) < 101) {
                currentSpan.innerHTML = +(currentSpan.innerHTML) + 1;
            }
        }
    }

    for (let minusBtn of minusBtnList) {
        minusBtn.onclick = () => {
            let currentSpan = minusBtn.nextElementSibling;
            if (+(currentSpan.innerHTML) > 1) {
                currentSpan.innerHTML = +(currentSpan.innerHTML) - 1;
            }
        }
    }
};

export default changeQuantity;