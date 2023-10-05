// See https://aka.ms/new-console-template for more information
using Software;

Almacenista jefe = new();

List<Almacenista> almacenistasPro = new()
{
            new()
            {
                nombreCompleto = "Pedro",
                password = "123"
                
            },

            new()
            {
                nombreCompleto = "Paco",
                password = "321"
                

            }

};


jefe.jsonSerial("prueba.json",almacenistasPro);
