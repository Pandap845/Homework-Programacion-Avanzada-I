// See https://aka.ms/new-console-template for more information
using System.Security.Cryptography;
using static System.IO.Directory; // CRUD Directories

using static System.IO.Path; //CREATE URL's C://Pipe//documents

using static System.Environment;

using Software;

const int rango = -10;

#region Inicializacion 

Almacenista jefe = new();

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

string dir = Combine(CurrentDirectory, "Almacenistas.json");
string dir2 = Combine(CurrentDirectory, "Almacenistas.xml");

jefe.jsonSerial<Almacenista>(dir, almacenistasPro);

jefe.xmlSerial<Almacenista>(dir2, almacenistasPro);


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
                        Profesor profesor = new(nombre, nomina, passwordd, nomina, division);


                        profesor.Agregar(profesor);

                    }
                    WriteLine($"{""}");
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
                    string? categoria = ReadLine();

                    if(categoria is not null)
                    {
                        Profesor reporte = new();
                        if(reporte.reportes(categoria, dirPro))
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

