namespace SIGA.Models
{
    /*Asignamos los tipos de roles en una clase enum. 
      El valor de la constante es igual al asignado en la tabla "rol" de la base de datos*/
    public enum Role
    {
        DireccionGeneral = 1,
        DireccionMaternal = 2,
        DireccionPreescolar = 3,
        DireccionPrimaria = 4,
        CoordinacionAdministrativa = 5,
        AuxiliarAdministrativo = 6,
        Docente = 7,
        Alumno = 8
    }
}
