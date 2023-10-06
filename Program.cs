// See https://aka.ms/new-console-template for more information
using System.Security.Cryptography;
using static System.IO.Directory; // CRUD Directories

using static System.IO.Path; //CREATE URL's C://Pipe//documents

using static System.Environment;

using Software;
using Microsoft.Win32;

const int rango = -10;

#region Inicializacion 

Almacenista jefe = new();
Salon SalonMaestro = new();

List<Almacenista> almacenistasPro = new()
{
            new()
            {
                nombreCompleto = "Pedro",
                password = Encrypt.getSHA1("123")

            },

            new()
            {
                nombreCompleto = "Paco",
                password = Encrypt.getSHA1("321")


            }

};

//lista de alumnos

List<Salon> salons = new()
{

        new()
        {
            grupo = "7F1",
            profesor = null
        },

        new()
        {
            grupo = "4A1",
            profesor = null
        },

        new()
        {
            grupo = "5S2",
            profesor = null
        }
};

List<Alumno> alumnos = new()
{
        new()
        {
            registro = 20300695,
            nombreCompleto = "Victor Emiliano",
            salon = null,
            password = "Caz"

                

       
        }
};

string dir = Combine(CurrentDirectory, "Almacenistas.json");
string dir2 = Combine(CurrentDirectory, "Almacenistas.xml");

string dirSalonJSON = Combine(CurrentDirectory, "Salones.json");
string dirSalonXML = Combine(CurrentDirectory, "Salones.xml");



jefe.jsonSerial<Almacenista>(dir, almacenistasPro);

jefe.xmlSerial<Almacenista>(dir2, almacenistasPro);

SalonMaestro.xmlSerial<Salon>(dirSalonXML, salons);

SalonMaestro.jsonSerial<Salon>(dirSalonJSON, salons);


#endregion

string dirPro = Combine(CurrentDirectory, "Profesores.xml");
//Tiene que verificar que sea xml, que exista, que tenga algo, etc.

int posicion= Login.ingreso(dir2);
if (posicion>=0)
{
    string? eleccion = "";

    while (eleccion != "6")
    {
        WriteLine("\n\n\n Congratulations. Ahora, seleccione que desea hacer:");
        WriteLine($"{"1.-Agregar profesor",rango}\n{"2.-Eliminar profesor",rango}");
        WriteLine($"{"3.-Editar profesor",rango}\n{"4.-Cambiar contraseña propia"}\n{"5.-Reportes",rango}\n{"6.-Salir",rango} ");
        eleccion = ReadLine();



        if (eleccion is not null)
        {

            switch (eleccion)
            {
                case "1":
                    Clear();
                    WriteLine("Muy bien!!, ingrese la información del profesor:");
                    Write($"{"Nombre Completo:",rango}");
                    string? nombre = ReadLine();
                    Write($"{"Password:",rango}");
                    string? passwordd = ReadLine();
                    Write($"{"Nomina:",rango}");
                    string? nomina = ReadLine();
                    Write($"{"materia:",rango}");
                    string? materia = ReadLine();
                    Write($"{"Division:",rango}");
                    string? division = ReadLine();

                    if (nombre is not null && nomina is not null && materia is not null && division is not null && passwordd is not null)
                    {

                        if(passwordd.Length>8)
                        {

                        
                        Profesor profesor = new(nombre, nomina, passwordd, nomina, division);
                          profesor.Agregar(profesor);
                        }
                        else{

                            WriteLine($"Contraseña menor a 8. Repita");
                        }


                      

                        //Por si decide agregar más de un 

                    }
                    WriteLine($"Agregue valores a cada uno");
                    break;


                case "2":
                    Clear();

                    //Editar un campo de algún profesor.
                    WriteLine("Muy bien!!, ¿qué profesor desea matar (ingrese su nómina)?: ");
                    nomina = ReadLine();

                    Profesor pro = new();
                    if (pro.Eliminar(nomina))
                    {
                        WriteLine("Se pudo eliminar");
                    }
                    else
                    {
                        WriteLine("No se pudo");
                    }


                    break;



                case "3":
                Clear();

                    WriteLine("Muy bien!!, ¿qué profesor desea editar (ingrese su nómina): ");
                    nomina = ReadLine();




                    break;

                case "4":
                Clear();
                Write("Cambiar password propia: ");
                    passwordd = ReadLine();

                    Almacenista alma = new();
                   if( alma.editarPassword(passwordd, posicion, dir2))
                   {

                    WriteLine("Se logró");
                   }
                   else{

                    WriteLine("No se logró");
                   }

                break;

                case "5":
                    Clear();
                    WriteLine("Excelente!!, ¿por cuál categoría desea organizar?");
                    WriteLine($"{"1.-Nomina", rango}\n{"2.-Nombre",rango}\n{"3.-Division", rango}\n{"4.-Materias",rango}\n{"5.-Password", rango}");
                    string? categoria = ReadLine();


                    if(int.TryParse(categoria, out int intCategoria))
                    {
                        Profesor reporte = new();
                        if(reporte.reportes(intCategoria, dirPro))
                        {
                            WriteLine("Se logró");
                        }
                        else
                        {
                            WriteLine("No se logró");
                        }
                    }

                    break;

                case "6":
                    WriteLine("Ciao");
                    break;

                default:

                    WriteLine("Error 404: not found");
                    break;
            }
        }
    }

}
else
{


}

