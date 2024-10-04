using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

class Program
{
    struct Estudiante
    {
        public string Nombre;
        public int IdEstudiante;
    }

    struct Asignatura
    {
        public string Nombre;
        public int IdAsignatura;
    }

    struct Calificacion
    {
        public int IdEstudiante;
        public int IdAsignatura;
        public float Nota;
    }

    static Estudiante[] listaEstudiantes = new Estudiante[3];
    static Asignatura[] listaAsignaturas = new Asignatura[3];
    static Calificacion[] listaCalificaciones = new Calificacion[9];
    static int totalEstudiantes = 0;
    static int totalAsignaturas = 0;
    static int totalCalificaciones = 0;

    static string archivo = "calificaciones.dat";

    static void Main(string[] args)
    {
        int opcion;
        do
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Agregar Estudiante");
            Console.WriteLine("2. Agregar Asignatura");
            Console.WriteLine("3. Agregar Calificación");
            Console.WriteLine("4. Mostrar Calificaciones");
            Console.WriteLine("5. Guardar en Disco");
            Console.WriteLine("6. Leer desde Disco");
            Console.WriteLine("0. Salir");
            Console.Write("Ingrese una opción: ");
            opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    AgregarEstudiante();
                    break;

                case 2:
                    AgregarAsignatura();
                    break;

                case 3:
                    AgregarCalificacion();
                    break;

                case 4:
                    MostrarCalificaciones();
                    break;

                case 5:
                    GuardarEnDisco();
                    Console.WriteLine("Datos guardados en disco.");
                    break;

                case 6:
                    LeerDesdeDisco();
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

    static void AgregarEstudiante()
    {
        if (totalEstudiantes < listaEstudiantes.Length)
        {
            Console.Write("Ingrese el nombre del estudiante: ");
            listaEstudiantes[totalEstudiantes].Nombre = Console.ReadLine();
            listaEstudiantes[totalEstudiantes].IdEstudiante = totalEstudiantes + 1;
            totalEstudiantes++;
        }
        else
        {
            Console.WriteLine("No hay espacio para más estudiantes.");
        }
    }

    static void AgregarAsignatura()
    {
        if (totalAsignaturas < listaAsignaturas.Length)
        {
            Console.Write("Ingrese el nombre de la asignatura: ");
            listaAsignaturas[totalAsignaturas].Nombre = Console.ReadLine();
            listaAsignaturas[totalAsignaturas].IdAsignatura = totalAsignaturas + 1;
            totalAsignaturas++;
        }
        else
        {
            Console.WriteLine("No hay espacio para más asignaturas.");
        }
    }

    static void AgregarCalificacion()
    {
        if (totalCalificaciones < listaCalificaciones.Length)
        {
            Console.WriteLine("\nEstudiantes:");
            for (int i = 0; i < totalEstudiantes; i++)
            {
                Console.WriteLine($"{listaEstudiantes[i].IdEstudiante}. {listaEstudiantes[i].Nombre}");
            }
            Console.Write("Seleccione el ID del estudiante: ");
            listaCalificaciones[totalCalificaciones].IdEstudiante = int.Parse(Console.ReadLine());

            Console.WriteLine("\nAsignaturas:");
            for (int i = 0; i < totalAsignaturas; i++)
            {
                Console.WriteLine($"{listaAsignaturas[i].IdAsignatura}. {listaAsignaturas[i].Nombre}");
            }
            Console.Write("Seleccione el ID de la asignatura: ");
            listaCalificaciones[totalCalificaciones].IdAsignatura = int.Parse(Console.ReadLine());

            Console.Write("Ingrese la calificación: ");
            listaCalificaciones[totalCalificaciones].Nota = float.Parse(Console.ReadLine());

            totalCalificaciones++;
        }
        else
        {
            Console.WriteLine("No hay espacio para más calificaciones.");
        }
    }

    static void MostrarCalificaciones()
    {
        Console.WriteLine("\nLista de Calificaciones:");
        for (int i = 0; i < totalCalificaciones; i++)
        {
            string nombreEstudiante = listaEstudiantes[listaCalificaciones[i].IdEstudiante - 1].Nombre;
            string nombreAsignatura = listaAsignaturas[listaCalificaciones[i].IdAsignatura - 1].Nombre;
            Console.WriteLine($"Estudiante: {nombreEstudiante}, Asignatura: {nombreAsignatura}, Nota: {listaCalificaciones[i].Nota}");
        }
    }

    static void GuardarEnDisco()
    {
        FileStream fileStream = new FileStream(archivo, FileMode.OpenOrCreate, FileAccess.Write);
        BinaryWriter writer = new BinaryWriter(fileStream);

        writer.Write(totalEstudiantes);
        for (int i = 0; i < totalEstudiantes; i++)
        {
            writer.Write(listaEstudiantes[i].Nombre);
            writer.Write(listaEstudiantes[i].IdEstudiante);
        }

        writer.Write(totalAsignaturas);
        for (int i = 0; i < totalAsignaturas; i++)
        {
            writer.Write(listaAsignaturas[i].Nombre);
            writer.Write(listaAsignaturas[i].IdAsignatura);
        }

        writer.Write(totalCalificaciones);
        for (int i = 0; i < totalCalificaciones; i++)
        {
            writer.Write(listaCalificaciones[i].IdEstudiante);
            writer.Write(listaCalificaciones[i].IdAsignatura);
            writer.Write(listaCalificaciones[i].Nota);
        }

        writer.Close();
        fileStream.Close();
    }

    static void LeerDesdeDisco()
    {
        if (File.Exists(archivo))
        {
            FileStream fileStream = new FileStream(archivo, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(fileStream);

            totalEstudiantes = reader.ReadInt32();
            for (int i = 0; i < totalEstudiantes; i++)
            {
                listaEstudiantes[i].Nombre = reader.ReadString();
                listaEstudiantes[i].IdEstudiante = reader.ReadInt32();
            }

            totalAsignaturas = reader.ReadInt32();
            for (int i = 0; i < totalAsignaturas; i++)
            {
                listaAsignaturas[i].Nombre = reader.ReadString();
                listaAsignaturas[i].IdAsignatura = reader.ReadInt32();
            }

            totalCalificaciones = reader.ReadInt32();
            for (int i = 0; i < totalCalificaciones; i++)
            {
                listaCalificaciones[i].IdEstudiante = reader.ReadInt32();
                listaCalificaciones[i].IdAsignatura = reader.ReadInt32();
                listaCalificaciones[i].Nota = reader.ReadSingle();
            }

            reader.Close();
            fileStream.Close();
        }
        else
        {
            Console.WriteLine("El archivo no existe.");
        }
    }
}


