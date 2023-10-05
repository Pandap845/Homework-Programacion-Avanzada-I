using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using static System.IO.Directory; // CRUD Directories

using static System.IO.Path; //CREATE URL's C://Pipe//documents

using static System.Environment;
using Microsoft.VisualBasic; // Get the location, SpecialFolder, 
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;


namespace Software


{



   



    public interface ISerializar
    {

        public int jsonSerial<T>(string? Name, List<T> lista); //Interfaz

        public int xmlSerial<T>(string? Name, List<T> lista);
         
    }


    public class Almacenista: ISerializar
    {
       public string? nombreCompleto {get; set;} //Nombre completo del almacenista

        public  string ?password {get; set;}  //Contraseña del almacenista


          


        public int jsonSerial<Almacenista>(string? Name, List<Almacenista> lista)  //Implementación de la interfaz, que permite serializar en json
        {
            if(Name is null)
            {
 
                return 0;
            }
            else{
                //specifie where is gonna be the file
string jsonPath = Combine(GetFolderPath(SpecialFolder.Desktop), Name);

  using (StreamWriter jsonStream = File.CreateText(Name))
    {
        //Someone Who speak JSON
    Newtonsoft.Json.JsonSerializer jss = new();

    //Serialize
    jss.Serialize(jsonStream,  lista);

}
WriteLine(File.ReadAllText(Name));
//EL objeto se crea dentro de los corchetes o paréntesis.

        }
         return 1;

            }

          
        
  

        public int xmlSerial<Almacenista>(string? Name, List<Almacenista> lista)
        {
            if(Name is null)
            {
                return 0;
            }
            else{

            
            
//The XML Serializer NEEDS to know what type of data is going to be seralized.
        XmlSerializer xs = new(type: lista.GetType()); //Se le indica el //Necesita saber que tipo de dato ocupa leer


//Create a type
string path = Combine(CurrentDirectory, Name); //For now, it just a file

//Implicit declaration

using (FileStream stream = File.Create(path))
{

    xs.Serialize(stream, lista);
} //Llama automáticamente el close(), crear el archivo y abrir el archivo

//1: take care of open on stream declaration
//2: When } gets hit, Close file is called
//3: Create file on declaration on Create(path)


WriteLine($"\n{File.ReadAllText(path)}");

          
            }

            return 0;
        }
    }

    public class Profesor
    {

           private string ?nombreCompleto {get; set;}

           private  string ?Nomina {get; set;}

           private string ?password {get; set;}

            public List<string>? materias = new();

            public string? division;


    }

    public class Salon
    {

        string? grupo;

        Profesor? profesor;

    }
}