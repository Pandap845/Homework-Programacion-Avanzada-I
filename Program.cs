// See https://aka.ms/new-console-template for more information
using System.Security.Cryptography;
using static System.IO.Directory; // CRUD Directories

using static System.IO.Path; //CREATE URL's C://Pipe//documents

using static System.Environment;

using Software;

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


//Tiene que verificar que sea xml, que exista, que tenga algo, etc.
if(Login.ingreso(dir2))
{
   
}
else{

   
}

