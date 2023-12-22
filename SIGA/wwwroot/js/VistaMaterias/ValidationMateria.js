document.addEventListener('DOMContentLoaded', function () {

    //Select elements from the interface
    const inputNombre = document.querySelector('#Nombre');

    //Add events
    inputNombre.addEventListener('blur', validar);


    function validar(e) {
        if (e.target.value.trim() === '') {
            showAlert(`El campo ${e.target.id} es obligatorio`, e.target.parentElement); 
            return;
        }

        cleanAlert(e.target.parentElement);
    }

    function showAlert(mensaje, referencia) {
        cleanAlert(referencia);

        //Add alert in HTML
        const error = document.createElement('P');
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
});