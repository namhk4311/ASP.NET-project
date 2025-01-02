// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const show = document.querySelector('.show-popup');
const popupContainer = document.querySelector('.popup-container');
const closeBtn = document.querySelector('.close-btn');
const hideLine = document.querySelector('.line-separate');
const blurBackground = document.querySelector('.blurbackground');

function popupnow() {
    popupContainer.classList.add('active');
    blurBackground.classList.add('active');
    //body.classList.add('active');
    if (hideLine) {
        hideLine.classList.add('active');
    }
}

//closeBtn.onclick = () => {
//    popupContainer.classList.remove('active');
//}
