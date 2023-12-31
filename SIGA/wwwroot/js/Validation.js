﻿import { Alertas } from "./Alerts.js";

document.addEventListener('DOMContentLoaded', function () {

    //Select elements from the interface
    const inputUser = document.querySelector('#Email');
    const inputPassword = document.querySelector('#Password');

    //Add events
    inputUser.addEventListener('blur', validar);
    inputPassword.addEventListener('blur', validar);

    const alert = new Alertas;

    function validar(e) {
        if (e.target.value.trim() === '') {
            alert.mostrarAlerta(`El campo ${e.target.id} es obligatorio`, 'error', e.target.parentElement);
            //showAlert(`El campo ${e.target.id} es obligatorio`, e.target.parentElement); 
            return;
        }

        if (e.target.id === 'Email' && !validationEmail(e.target.value)) {
            alert.mostrarAlerta('El email no es válido', 'error', e.target.parentElement);
            //showAlert('El email no es válido', e.target.parentElement);
            return
        }

        cleanAlert(e.target.parentElement);
    }

    /*

    function showAlert(mensaje, referencia) {
        cleanAlert(referencia);

        //Add alert in HTML
        const error = document.createElement('div');
        error.textContent = mensaje;
        error.classList.add('errorAlert');


        //Inject error at form
        referencia.appendChild(error);
    }

    function cleanAlert(referencia) {
        const alert = referencia.querySelector('.errorAlert');
        if (alert) {
            alert.remove();
        }
    }
    */
    function validationEmail(email) {
        const regex = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/
        const resultado = regex.test(email)
        return resultado;
    }
});