using System; // Importar el espacio de nombres System
using TaskManagementLibrary; // Asegurarse de que TaskManagementLibrary esté referenciado correctamente en el proyecto

class Program
{
    static bool confirme(string accion)
    {
        Console.WriteLine("Confirme " + accion + " s/n");
        return Console.ReadLine() == "s";
    } 
    static void Main(string[] args)
    {
        var taskService = new TaskService();

        while (true)
        {
            Console.WriteLine("1. Agregar tarea");
            Console.WriteLine("2. Ver tareas");
            Console.WriteLine("3. Actualizar tarea");
            Console.WriteLine("4. Eliminar tarea");
            Console.WriteLine("5. Completar tarea");
            Console.WriteLine("6. Salir");
            Console.Write("Seleccione una opción: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    Console.Write("Titulo: ");
                    var title = Console.ReadLine().Trim();          
                    Console.Write("Descripcion: ");
                    var description = Console.ReadLine().Trim();
                    // Verificar si los datos son solo espacios vacíos y asignar nulo si es el caso
                    if (string.IsNullOrWhiteSpace(title)) title = null;
                    if (string.IsNullOrWhiteSpace(description)) description = null;
                    var task = taskService.AddTask(title, description);
                    Console.WriteLine($"Tarea agregada con Id: {task.Id}");
                    break;

                case "2":
                    var tasks = taskService.GetAllTasks();
                    Console.WriteLine("-------------------------------------------------");
                    foreach (var t in tasks)
                    {
                        Console.WriteLine($"ID: {t.Id}, Titulo: {t.Title}, Descripcion: {t.Description}, Completada: {t.IsCompleted}");
                    }
                    Console.WriteLine("-------------------------------------------------");
                    break;

                case "3":
                    Console.Write("Introduzca el Id de la tarea por actualizar: ");
                    var updateId = int.Parse(Console.ReadLine());
                    var updateTask = taskService.GetTaskById(updateId); // Reto 3: Obtener la tarea con el id indicado

                    if (updateTask == null)
                    {
                        Console.WriteLine("Tarea no encontrada.");
                        break;
                    }

                    Console.WriteLine($"Titulo actual: {updateTask.Title}"); // Reto 4: Imprimir el título de la tarea seleccionada
                    Console.Write("-> Nuevo titulo: ");
                    var newTitle = Console.ReadLine().Trim();

                    Console.WriteLine($"Descripcion actual: {updateTask.Description}"); // Reto 5: Imprimir la descripción de la tarea seleccionada
                    Console.Write("-> Nueva Descripcion: ");
                    var newDescription = Console.ReadLine().Trim();

                    Console.Write("Completada (true/false): ");
                    var isCompleted = bool.Parse(Console.ReadLine());

                    if (taskService.UpdateTask(updateId, newTitle, newDescription, isCompleted))
                    {
                        Console.WriteLine("Tarea actualizada exitosamente.");
                    }
                    else
                    {
                        Console.WriteLine("Tarea no encontrada.");
                    }
                    break;
                  
                case "4":
                    Console.Write("Introduzca el Id de la tarea a eliminar: ");
                    var deleteId = 0;
                    try
                    {
                        deleteId = int.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        break;
                    }
                    
                    var deleteTask = taskService.GetTaskById(deleteId); // Reto 7: Obtener la tarea con el id indicado
                    if (deleteTask == null)
                    {
                        Console.WriteLine("Tarea no encontrada.");
                        break;
                    }

                    Console.WriteLine("Tarea:");
                    Console.Write("     - ");
                    Console.WriteLine(deleteTask.Title);

                    if (confirme("eliminar"))
                    {
                        if (taskService.DeleteTask(deleteId))
                        {
                            Console.WriteLine("Tarea eliminada exitosamente.");
                        }
                        else
                        {
                            Console.WriteLine("Tarea no encontrada.");
                        }
                    }
                    break;

                case "5":
                    Console.Write("Introduzca el Id de la tarea a completar: ");
                    var completeId = int.Parse(Console.ReadLine());
                    var completeTask = taskService.GetTaskById(completeId); // Verificar que el Id exista

                    if (completeTask == null)
                    {
                        Console.WriteLine("Tarea no encontrada.");
                        break;
                    }

                    Console.WriteLine($"Titulo de la tarea: {completeTask.Title}");
                    if (taskService.CompleteTask(completeId))
                    {
                        Console.WriteLine("Tarea completada exitosamente.");
                    }
                    else
                    {
                        Console.WriteLine("No se pudo completar la tarea.");
                    }
                    break;

                case "6":
                    return;

                default:
                    Console.WriteLine("Opción inválida, intente de nuevo.");
                    break;
            }
        }
    }
}

