/*Change quantity and Collect all prices in basket page and count total sum*/
export function showInfoBox(jsonMessage) {
    let msg = JSON.parse(jsonMessage);
    if (msg.message != undefined) {
        document.querySelector('.popup-content').innerHTML = msg.message;
    } else {
        document.querySelector('.popup-content').style.color = "Red";
        document.querySelector('.popup-content').innerHTML = msg.error;
    }
    let infoBox = document.querySelector('.popup-add-box');
    infoBox.classList.add("box-styles");
    infoBox.classList.add("box-show");
    setTimeout(() => {
        infoBox.classList.remove("box-show");
    }, 1000);
}