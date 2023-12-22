const eliminarRegistro = async (id) => {
    var formData = new FormData();
    formData.append('id', id);
    console.log(id);
    const requestOptions = {
        method: 'POST',
        body: formData
    };
   
    const api = await fetch(url, requestOptions)

    const resultado = await api.json();

    if (resultado.status === 200) {
        const deleteAlert = `
                    <div class="alertaEliminar">
                        <div class="eliminar">
                            <h2>¡Correcto!</h2>
                            <p>El nivel educativo se ha eliminado exitosamente</p>
                            <div class="acciones">
                                   <button class="btn secondary" onclick="window.location.reload()">Aceptar</button>
                            </div>
                        </div>
                    </div>
                `;

        document.getElementById('deleteAlert').innerHTML = deleteAlert
        /*setTimeout(function(){
            window.location.reload();
        }, 2000);*/
        //window.location.reload();
    }

}

const cancelar = async (id) => {


    document.getElementById('alert').innerHTML = '';

}

const eliminar = (id) => {
    const alert = `
            <div class="modalContainer">
                <div class="modal">
                    <h2>Eliminar nivel educativo</h2>
                    <p>¿Está seguro de eliminar el nivel educativo</p>
                    <div class="acciones">
                        <button class="btn secondary" onclick="cancelar()">Cancelar</button>
                        <button class="btn danger" onclick='eliminarRegistro(${id})'>Eliminar</button>
                    </div>
                </div>
            </div>
         `;
    document.getElementById('alert').innerHTML = alert;
    console.log('id', id);
}
