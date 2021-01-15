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

export default colorationColorIcon;