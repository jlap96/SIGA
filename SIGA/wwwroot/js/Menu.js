const submenuUsuarios = document.getElementById('submenuUsuarios'),
    submenuAdministracion = document.getElementById('submenuAdministracion'),
    submenuFinanciero = document.getElementById('submenuFinanciero'),
    submenuPagos = document.getElementById('submenuPagos'),
    submenuAlumnos = document.getElementById('submenuAlumnos'),
    opcionUsuarios = document.getElementById('opcionUsuarios'),
    opcionAdministracion = document.getElementById('opcionAdministracion'),
    opcionFinanciero = document.getElementById('opcionFinanciero'),
    opcionPagos = document.getElementById('opcionPagos'),
    opcionAlumnos = document.getElementById('opcionAlumnos');


opcionUsuarios.addEventListener('click', () => {
    if (submenuUsuarios.style.display === "none") {
        submenuUsuarios.style.display = "block"
    }
    else {
        submenuUsuarios.style.display = "none"
    }
})

opcionAdministracion.addEventListener('click', () => {
    if (submenuAdministracion.style.display === "none") {
        submenuAdministracion.style.display = "block"
    }
    else {
        submenuAdministracion.style.display = "none"
    }
})

opcionFinanciero.addEventListener('click', () => {
    if (submenuFinanciero.style.display === "none") {
        submenuFinanciero.style.display = "block"
    }
    else {
        submenuFinanciero.style.display = "none"
    }
})

opcionPagos.addEventListener('click', () => {
    if (submenuPagos.style.display === "none") {
        submenuPagos.style.display = "block"
    }
    else {
        submenuPagos.style.display = "none"
    }
})

opcionAlumnos.addEventListener('click', () => {
    if (submenuAlumnos.style.display === "none") {
        submenuAlumnos.style.display = "block"
    }
    else {
        submenuAlumnos.style.display = "none"
    }
})





