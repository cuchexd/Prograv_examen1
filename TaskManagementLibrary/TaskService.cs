using System.Collections.Generic;
using System.Linq;

namespace TaskManagementLibrary
{
    public class TaskService
    {
        private List<Task> tasks = new List<Task>();
        private int nextId = 1;

        public Task AddTask(string title, string description)
        {
            if (string.IsNullOrWhiteSpace(title)) title = null;
            if (string.IsNullOrWhiteSpace(description)) description = null;

            title ??= "Default Title";
            description ??= "Default Description";

            var task = new Task
            {
                Id = nextId++,
                Title = title,
                Description = description,
                IsCompleted = false
            };
            tasks.Add(task);
            return task;
        }

        public List<Task> GetAllTasks()
        {
            return tasks;
        }

        public Task GetTaskById(int id)
        {
            return tasks.FirstOrDefault(t => t.Id == id);
        }

        public bool UpdateTask(int id, string title, string description, bool isCompleted)
        {
            var task = GetTaskById(id);
            if (task == null) return false;

            if (!string.IsNullOrWhiteSpace(title)) task.Title = title;
            if (!string.IsNullOrWhiteSpace(description)) task.Description = description;
            task.IsCompleted = isCompleted;
            return true;
        }

        public bool DeleteTask(int id)
        {
            var task = GetTaskById(id);
            if (task == null) return false;
            tasks.Remove(task);
            return true;
        }

        public bool CompleteTask(int id)
        {
            var task = GetTaskById(id);
            if (task == null) return false;
            task.IsCompleted = true;
            return true;
        }
    }
}
