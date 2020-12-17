// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const modalLogin = document.getElementById('login'),
    btnLogin = document.querySelector('.btnClick');

console.log(inputs)

const togglePopup = () => {

    modalLogin.classList.remove('fade');
    modalLogin.classList.add('show');

    modalLogin.addEventListener('click', (event) => {
        let target = event.target;
        if (target.classList.contains('close') ||
            target.classList.contains('HidebtnModal') ||
            target.classList.contains('modal')) {

            modalLogin.classList.add('fade');
            modalLogin.classList.remove('show');
        }
    });
};


btnLogin.addEventListener('click', togglePopup);
