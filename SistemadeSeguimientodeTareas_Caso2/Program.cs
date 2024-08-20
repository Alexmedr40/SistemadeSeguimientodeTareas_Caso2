using System;
using System.Collections.Generic;

namespace SistemaSeguimientoTareas
{
    
    class Tarea
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaLimite { get; set; }

        public Tarea(string titulo, string descripcion, DateTime fechaLimite)
        {
            Titulo = titulo;
            Descripcion = descripcion;
            FechaLimite = fechaLimite;
        }

       
        public virtual void MostrarDetalles()
        {
            Console.WriteLine($"Título: {Titulo}");
            Console.WriteLine($"Descripción: {Descripcion}");
            Console.WriteLine($"Fecha Límite: {FechaLimite.ToShortDateString()}");
        }
    }

    
    class TareaUrgente : Tarea
    {
        public int NivelUrgencia { get; set; }

        public TareaUrgente(string titulo, string descripcion, DateTime fechaLimite, int nivelUrgencia)
            : base(titulo, descripcion, fechaLimite)
        {
            NivelUrgencia = nivelUrgencia;
        }

        
        public override void MostrarDetalles()
        {
            base.MostrarDetalles();
            Console.WriteLine($"Nivel de Urgencia: {NivelUrgencia}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Tarea> tareas = new List<Tarea>();
            bool continuar;

            do
            {
                Console.WriteLine("\nMenú:");
                Console.WriteLine("1. Agregar nueva tarea");
                Console.WriteLine("2. Mostrar todas las tareas");
                Console.WriteLine("3. Salir");
                Console.Write("Seleccione una opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        AgregarTarea(tareas);
                        break;
                    case "2":
                        MostrarTareas(tareas);
                        break;
                    case "3":
                        continuar = false;
                        continue; 
                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                        break;
                }

                Console.Write("¿Desea realizar otra acción? (S/N): ");
                continuar = Console.ReadLine().ToUpper() == "S";

            } while (continuar);
        }

        static void AgregarTarea(List<Tarea> tareas)
        {
            Console.Write("Ingrese el título de la tarea: ");
            string titulo = Console.ReadLine();

            Console.Write("Ingrese la descripción de la tarea: ");
            string descripcion = Console.ReadLine();

            DateTime fechaLimite;
            while (true)
            {
                Console.Write("Ingrese la fecha límite (dd/mm/aaaa): ");
                if (DateTime.TryParse(Console.ReadLine(), out fechaLimite))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Fecha no válida. Intente de nuevo.");
                }
            }

            Console.Write("¿Es una tarea urgente? (S/N): ");
            string esUrgente = Console.ReadLine().ToUpper();

            if (esUrgente == "S")
            {
                int nivelUrgencia;
                while (true)
                {
                    Console.Write("Ingrese el nivel de urgencia (1-5): ");
                    if (int.TryParse(Console.ReadLine(), out nivelUrgencia) && nivelUrgencia >= 1 && nivelUrgencia <= 5)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Nivel de urgencia no válido. Intente de nuevo.");
                    }
                }
                tareas.Add(new TareaUrgente(titulo, descripcion, fechaLimite, nivelUrgencia));
            }
            else if (esUrgente == "N")
            {
                tareas.Add(new Tarea(titulo, descripcion, fechaLimite));
            }
            else
            {
                Console.WriteLine("Entrada no válida. Tarea no agregada.");
            }
        }

        static void MostrarTareas(List<Tarea> tareas)
        {
            if (tareas.Count == 0)
            {
                Console.WriteLine("No hay tareas registradas.");
                return;
            }

            Console.WriteLine("\nLista de Tareas:");
            foreach (var tarea in tareas)
            {
                tarea.MostrarDetalles();
                Console.WriteLine(); 
            }
        }
    }
}
