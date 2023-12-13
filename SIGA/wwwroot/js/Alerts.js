export class Alertas {
    mostrarAlerta(mensaje, tipo, referencia) {
        //Crear el div
        const divMensaje = document.createElement('div');
        divMensaje.classList.add('alert');

        //Agrear clase en base al tipo de error
        if (tipo === 'error') {
            divMensaje.classList.add('errorAlert');
        } else {
            divMensaje.classList.add('successAlert');
        }

        //Mensaje
        divMensaje.textContent = mensaje;

        //Insertar en el DOM

        referencia.appendChild(divMensaje);
        //Remover alerta después de unos segundos
        setTimeout( () => {
            divMensaje.remove();
        }, 5000 );

    }
}
