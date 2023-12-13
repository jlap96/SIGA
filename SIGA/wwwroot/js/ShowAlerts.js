
const alertSuccess = document.querySelector('.successAlert');


    alertSuccess.classList.remove('hide');
    alertSuccess.classList.add('show');

    setTimeout(function () {
        alertSuccess.classList.remove('show');
        alertSuccess.classList.add('hide');
    }, 4000);
