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


namespace Software


{

   



    public interface ISerializar
    {

        public int jsonSerial(string? Name, List<Task> lista);
        public int xmlSerial();
    }


    public class Almacenista 
    {
       public string? nombreCompleto {get; set;} //Nombre completo del almacenista

        public  string ?password {get; set;}  //Contraseña del almacenista


          


        public int jsonSerial(string? Name, List<Almacenista> lista)  //Implementación de la interfaz, que permite serializar en json
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

          
        
  

        public int xmlSerial(string? Name, List<Almacenista> lista)
        {
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