using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using static System.IO.Directory; // CRUD Directories

using static System.IO.Path; //CREATE URL's C://Pipe//documents

using static System.Environment;

using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

//Alias


using System.Formats.Asn1;

namespace Software


{


    public class Login
    {

       
        static public bool ingreso( string? FilePath)
        {

            Almacenista almacenista= new();
         
           List<Almacenista> lista;
           string? contraAlmacenista, NombreAlmacenista;

        

            if (Path.Exists(FilePath)) //verifica que exista y sea un archivo xml
            {
                WriteLine("Almacenista");
                Write("Ingrese nombre de usuario:");
                NombreAlmacenista = ReadLine();

                Write("\nIngrese la contraseña:");
                contraAlmacenista = ReadLine();

                if(contraAlmacenista is not null)
                {
                contraAlmacenista = Encrypt.getSHA1(contraAlmacenista);
                }
                else{
                    return false;
                }

                //deserializar el archivo xml que tiene a los almacenistas.

               lista =  almacenista.xmlDeSerial<Almacenista>(FilePath);

  //Verificar si corresponde con alguna contraseña
                    foreach(var cuenta in lista)
                    {
                        if (cuenta.password ==  contraAlmacenista && cuenta.nombreCompleto == NombreAlmacenista)
                        {
                            return true;
                        }

                    }

                    return false;

              
            }
            else
            {

                return false;

            }








        }
    }


    public class Encrypt
    {

        public static string getSHA1(string str) //Metodo que permite la encriptación en SHA1
        {

            SHA1 sha1 = SHA1.Create();

            ASCIIEncoding encoding = new ASCIIEncoding(); //dar formato en ASCII

            byte[]? stream = null; //almacena el hash del SHA1
            StringBuilder sb = new StringBuilder(); //Mejor que un string normal 

            stream = sha1.ComputeHash(encoding.GetBytes(str));
            //Encriptación con el método SHA1. Como hace uso de un arreglo de bytes, es necesario el casteo

            foreach (var variable in stream)
            {
                sb.AppendFormat($"{variable:x2}");
            }

            return sb.ToString();

        }
    }




    public interface ISerializar
    {

        public int jsonSerial<T>(string? Name, List<T> lista); //Interfaz

        public int xmlSerial<T>(string? Name, List<T> lista);


        public List<T> xmlDeSerial<T>(string FilePath);

    }


    public class Almacenista : ISerializar
    {
        public string? nombreCompleto { get; set; } //Nombre completo del almacenista

        public string? password { get; set; }  //Contraseña del almacenista





        public int jsonSerial<Almacenista>(string Name, List<Almacenista> lista)  //Implementación de la interfaz, que permite serializar en json
        {
            if (!Path.Exists(Name) && Name is null)
            {

                return 0;
            }
            else
            {


                //specifie where is gonna be the file
              

                using (StreamWriter jsonStream = File.CreateText(Name))
                {
                    //Someone Who speak JSON
                    Newtonsoft.Json.JsonSerializer jss = new();

                    //Serialize
                    jss.Serialize(jsonStream, lista);

                }
                WriteLine(File.ReadAllText(Name));
                //EL objeto se crea dentro de los corchetes o paréntesis.

            }
            return 1;

        }


        public int xmlSerial<Almacenista>(string? Name, List<Almacenista> lista)
        {
            if (!Path.Exists(Name) && Name is null )
            {
                return 0;
            }
            else
            {



                //The XML Serializer NEEDS to know what type of data is going to be seralized.
                XmlSerializer xs = new(type: lista.GetType()); //Se le indica el //Necesita saber que tipo de dato ocupa leer



                //Implicit declaration

                using (FileStream stream = File.Create(Name))
                {

                    xs.Serialize(stream, lista);
                } //Llama automáticamente el close(), crear el archivo y abrir el archivo

                //1: take care of open on stream declaration
                //2: When } gets hit, Close file is called
                //3: Create file on declaration on Create(path)


                WriteLine($"\n{File.ReadAllText(Name)}");


            }

            return 0;
        }


        public List<Almacenista> xmlDeSerial<Almacenista>(string FilePath)
        {
            List<Almacenista>? lista = new();
            if (Path.Exists(FilePath))
            {
                using (FileStream xmlLoad = File.Open(FilePath, FileMode.Open))
                {
                    XmlSerializer xs = new(type: lista.GetType());
                    //Desearilzar en una lista 

                    lista = xs.Deserialize(xmlLoad) as List<Almacenista>;


                }
            }

            return lista;

        }



        

    }

    public class Profesor
    {

        public string? nombreCompleto { get; set; }

        public string? Nomina { get; set; }

        public string? password { get; set; }

        public List<string> materias = new(){

               "Computacion", "electronica", "Informatica", "Sistemas Digitales"
        };

        public string? division;


    }

    public class Salon
    {

        string? grupo;

        Profesor? profesor;

    }
}