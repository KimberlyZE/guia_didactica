using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Program
{
    struct Software
    {
        public string Fabricante;
        public string Nombre;
        public string Edicion;
        public string Version;
        public string Licenciamiento;
        public string Descripcion;
    }

    static void Main()
    {
        Software[] listaSoftware = new Software[3];
        int totalRegistros = 0;
        string archivo = "software.dat";

        int opcion;
        do
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Agregar Software");
            Console.WriteLine("2. Mostrar Software");
            Console.WriteLine("3. Actualizar Software");
            Console.WriteLine("4. Eliminar Software");
            Console.WriteLine("5. Guardar en disco");
            Console.WriteLine("6. Leer desde disco");
            Console.WriteLine("0. Salir");
            Console.Write("Ingrese una opción: ");
            opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1: // Agregar Software
                    if (totalRegistros < listaSoftware.Length)
                    {
                        Console.WriteLine("Agregar Software:");
                        Console.Write("Fabricante: ");
                        listaSoftware[totalRegistros].Fabricante = Console.ReadLine();
                        Console.Write("Nombre: ");
                        listaSoftware[totalRegistros].Nombre = Console.ReadLine();
                        Console.Write("Edición: ");
                        listaSoftware[totalRegistros].Edicion = Console.ReadLine();
                        Console.Write("Versión: ");
                        listaSoftware[totalRegistros].Version = Console.ReadLine();
                        Console.Write("Licenciamiento: ");
                        listaSoftware[totalRegistros].Licenciamiento = Console.ReadLine();
                        Console.Write("Descripción: ");
                        listaSoftware[totalRegistros].Descripcion = Console.ReadLine();
                        totalRegistros++;
                    }
                    else
                    {
                        Console.WriteLine("No hay espacio para más registros.");
                    }
                    break;

                case 2: // Mostrar Software
                    Console.WriteLine("\nLista de Software:");
                    for (int i = 0; i < totalRegistros; i++)
                    {
                        MostrarSoftware(listaSoftware[i]);
                    }
                    break;

                case 3: // Actualizar Software
                    Console.Write("Ingrese el número del registro a actualizar (0 a {0}): ", totalRegistros - 1);
                    int indice = int.Parse(Console.ReadLine());
                    if (indice >= 0 && indice < totalRegistros)
                    {
                        Console.WriteLine("Actualizar Software:");
                        Console.Write("Nuevo Fabricante: ");
                        listaSoftware[indice].Fabricante = Console.ReadLine();
                        Console.Write("Nuevo Nombre: ");
                        listaSoftware[indice].Nombre = Console.ReadLine();
                        Console.Write("Nueva Edición: ");
                        listaSoftware[indice].Edicion = Console.ReadLine();
                        Console.Write("Nueva Versión: ");
                        listaSoftware[indice].Version = Console.ReadLine();
                        Console.Write("Nuevo Licenciamiento: ");
                        listaSoftware[indice].Licenciamiento = Console.ReadLine();
                        Console.Write("Nueva Descripción: ");
                        listaSoftware[indice].Descripcion = Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Índice no válido.");
                    }
                    break;

                case 4: // Eliminar Software
                    Console.Write("Ingrese el número del registro a eliminar (0 a {0}): ", totalRegistros - 1);
                    int eliminarIndice = int.Parse(Console.ReadLine());
                    if (eliminarIndice >= 0 && eliminarIndice < totalRegistros)
                    {
                        for (int i = eliminarIndice; i < totalRegistros - 1; i++)
                        {
                            listaSoftware[i] = listaSoftware[i + 1];
                        }
                        totalRegistros--;
                        Console.WriteLine("Registro eliminado.");
                    }
                    else
                    {
                        Console.WriteLine("Índice no válido.");
                    }
                    break;

                case 5: // Guardar en disco
                    GuardarEnDisco(listaSoftware, totalRegistros, archivo);
                    Console.WriteLine("Datos guardados en disco.");
                    break;

                case 6: // Leer desde disco
                    totalRegistros = LeerDesdeDisco(listaSoftware, archivo);
                    Console.WriteLine("Datos cargados desde disco.");
                    break;

                case 0:
                    Console.WriteLine("Saliendo...");
                    break;

                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        } while (opcion != 0);
    }

    static void MostrarSoftware(Software software)
    {
        Console.WriteLine("Fabricante: " + software.Fabricante);
        Console.WriteLine("Nombre: " + software.Nombre);
        Console.WriteLine("Edición: " + software.Edicion);
        Console.WriteLine("Versión: " + software.Version);
        Console.WriteLine("Licenciamiento: " + software.Licenciamiento);
        Console.WriteLine("Descripción: " + software.Descripcion);
        Console.WriteLine();
    }

    static void GuardarEnDisco(Software[] listaSoftware, int totalRegistros, string archivo)
    {
        FileStream fileStream = new FileStream(archivo, FileMode.OpenOrCreate, FileAccess.Write);
        BinaryWriter writer = new BinaryWriter(fileStream);

        writer.Write(totalRegistros); // Guardamos la cantidad de registros
        for (int i = 0; i < totalRegistros; i++)
        {
            writer.Write(listaSoftware[i].Fabricante);
            writer.Write(listaSoftware[i].Nombre);
            writer.Write(listaSoftware[i].Edicion);
            writer.Write(listaSoftware[i].Version);
            writer.Write(listaSoftware[i].Licenciamiento);
            writer.Write(listaSoftware[i].Descripcion);
        }

        writer.Close();
        fileStream.Close();
    }

    static int LeerDesdeDisco(Software[] listaSoftware, string archivo)
    {
        if (File.Exists(archivo))
        {
            FileStream fileStream = new FileStream(archivo, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(fileStream);

            int totalRegistros = reader.ReadInt32(); // Leemos la cantidad de registros
            for (int i = 0; i < totalRegistros; i++)
            {
                listaSoftware[i].Fabricante = reader.ReadString();
                listaSoftware[i].Nombre = reader.ReadString();
                listaSoftware[i].Edicion = reader.ReadString();
                listaSoftware[i].Version = reader.ReadString();
                listaSoftware[i].Licenciamiento = reader.ReadString();
                listaSoftware[i].Descripcion = reader.ReadString();
            }

            reader.Close();
            fileStream.Close();
            return totalRegistros;
        }
        else
        {
            Console.WriteLine("El archivo no existe.");
            return 0;
        }
    }
}


