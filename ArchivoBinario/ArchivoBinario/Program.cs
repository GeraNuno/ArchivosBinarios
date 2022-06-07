using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace ArchivoBinario
{
    class Program
    {
        class ArchivoBinarioEmpleados
        {
            //Declaración de flujos
            BinaryWriter bw = null; //flujo salida - escritura de datos
            BinaryReader br = null; //flujo entrada - lectura de datos

            //Campos de la clase
            string Nombre, Direccion;
            long Telefono;
            int NumEmp, DiasTrabajados;
            float SalarioDiario;

            public void CrearArchivo(String Archivo)
            {
                //Variable local método
                char resp;
                try
                {
                    //Creación del flujo para esribir datos al archivo
                    bw = new BinaryWriter(new FileStream(Archivo, FileMode.Create, FileAccess.Write));

                    //Captura de datos
                    do
                    {
                        Console.Clear();
                        Console.Write("Número del Empleado: ");
                        NumEmp = int.Parse(Console.ReadLine());
                        Console.Write("Nombre del Empleado: ");
                        Nombre = Console.ReadLine();
                        Console.Write("Dirección del Empleado: ");
                        Direccion = Console.ReadLine();
                        Console.Write("Teléfono del Empleado: ");
                        Telefono = long.Parse(Console.ReadLine());
                        Console.Write("Días Trabajados del Empleado: ");
                        DiasTrabajados = int.Parse(Console.ReadLine());
                        Console.Write("Salario Diario del Empleado: ");
                        SalarioDiario = float.Parse(Console.ReadLine());

                        //Escribe los datos al Archivo
                        bw.Write(NumEmp);
                        bw.Write(Nombre);
                        bw.Write(Direccion);
                        bw.Write(Telefono);
                        bw.Write(DiasTrabajados);
                        bw.Write(SalarioDiario);

                        Console.Write("\n\nDeseas Almacenar otro Registro (s/n)? ");
                        resp = char.Parse(Console.ReadLine());
                    } while ((resp == 's') || (resp == 'S'));
                }
                catch (IOException e)
                {
                    Console.WriteLine("\nError: " + e.Message);
                    Console.WriteLine("\nRuta: " + e.StackTrace);
                }
                finally
                {
                    if (bw != null) bw.Close(); //Cierra el flujo - escritura
                    Console.Write("\nPresione <ENTER> para terminar la Escritura de Datos y regresar al Menu.");
                    Console.ReadKey();
                }
            }

            public void MostrarArchivo(string Archivo)
            {
                try
                {
                    //Verificación si existe el Archivo
                    if (File.Exists(Archivo))
                    {
                        //Creación del Flujo para leer datos del archivo
                        br = new BinaryReader(new FileStream(Archivo, FileMode.Open, FileAccess.Read));

                        //Despliegue de datos en pantalla
                        Console.Clear();
                        do
                        {
                            //Lectura de registros mientras no llegue a EndOfFile
                            NumEmp = br.ReadInt32();
                            Nombre = br.ReadString();
                            Direccion = br.ReadString();
                            Telefono = br.ReadInt64();
                            DiasTrabajados = br.ReadInt32();
                            SalarioDiario = br.ReadSingle();

                            //Muestra los datos
                            Console.WriteLine("Número de Empleado: " + NumEmp);
                            Console.WriteLine("Nombre del Empleado: " + Nombre);
                            Console.WriteLine("Dirección del Empleado: " + Direccion);
                            Console.WriteLine("Teléfono del Empleado: " + Telefono);
                            Console.WriteLine("Dias Trabajados del Empleado: " + DiasTrabajados);
                            Console.WriteLine("Salario Diario del Empleado: {0:C}", SalarioDiario);
                            Console.WriteLine("SUELDO TOTAL del Empleado: {0:C}", (DiasTrabajados * SalarioDiario));
                            Console.WriteLine("\n");
                        } while (true);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("\n\nEl Archivo " + Archivo + " No Existe en el Disco!!");
                        Console.Write("\nPresione <ENTER> para Continuar...");
                        Console.ReadKey();
                    }
                }
                catch (EndOfStreamException)
                {
                    Console.WriteLine("\n\nFin del Listado del Empleado");
                    Console.Write("\nPresione <ENTER> para Continuar...");
                    Console.ReadKey();
                }
                finally
                {
                    if (br != null) br.Close(); //Cierra flujo
                    Console.Write("\nPresoine <ENTER> para terminar la Lectura de Datos y regresar al Menú.");
                    Console.ReadKey();
                }
            }
        }
        static void Main(string[] args)
        {
            //Declaración variables auxiliares
            string Arch = null;
            int opcion;

            //Creacion del objeto
            ArchivoBinarioEmpleados Al = new ArchivoBinarioEmpleados();

            //Menú de Opciones
            do
            {
                Console.Clear();
                Console.WriteLine("\n*** ARCHIVO BINARIO EMPLEADOS ***");
                Console.WriteLine("1.- Creación de un Archivo.");
                Console.WriteLine("2.- Lectura de un Archivo");
                Console.WriteLine("3.- Salida del Programa");
                Console.Write("\nQué opción deseas: ");
                opcion = Int16.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        //Bloque de escritura
                        try
                        {
                            //captura nombre archivo
                            Console.Write("\nAlimenta el Nombre del Archivo a Crear: ");
                            Arch = Console.ReadLine();

                            //Verifica si existe el archivo
                            char resp = 's';
                            if (File.Exists(Arch))
                            {
                                Console.Write("\nEl Archivo Existe!!!, Deseas Sobreescribirlo (s/n)? ");
                                resp = char.Parse(Console.ReadLine());
                            }
                            if ((resp == 's') || (resp == 'S'))
                            {
                                Al.CrearArchivo(Arch);
                            }
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError: " + e.Message);
                            Console.WriteLine("\nRuta: " + e.StackTrace);
                        }
                        break;
                    case 2:
                        //bloque de lectura
                        try
                        {
                            //Captura nombre archivo
                            Console.Write("\nAlimenta el Nombre del Archivo que deseas leer: ");
                            Arch = Console.ReadLine();
                            Al.MostrarArchivo(Arch);
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError: " + e.Message);
                            Console.WriteLine("\nRuta: " + e.StackTrace);
                        }
                        break;
                    case 3:
                        Console.Write("\nPresione <ENTER> para Salir del Programa...");
                        Console.ReadKey();
                        break;
                    default:
                        Console.Write("\nEsa Opción no Existe !!, Presione <ENTER> para Continuar...");
                        Console.ReadKey();
                        break;
                }
            } while (opcion != 3);
        }
    }
}
