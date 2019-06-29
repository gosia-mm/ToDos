using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace ToDos.Models
{
    public class ToDoSGroup : List<ToDo>
    {
        public string Title { get; set; }
        public string ShortTitle { get; set; }
    }

    public class ToDoSGroups
    {
        public static List<ToDoSGroup> ToDosGroupsList(List<ToDo> toDos)
        {
            var toDosGroups = new List<ToDoSGroup>();

            var groupsNames = toDos.Select(t => t.Category).Distinct().OrderBy(t => t);

            foreach (var groupName in groupsNames)  // przejście po nazwach kategorii
            {
                var todoSinGroup = toDos.Where(t => t.Category == groupName);   // pobranie 
                var newGroup = new ToDoSGroup()     // utworzenie grupy
                {
                    Title = groupName == null ? "Brak" : groupName,
                    ShortTitle = groupName == null ? "Brak" : groupName,
                };
                newGroup.AddRange(todoSinGroup);    // dodanie todos z grupy
                toDosGroups.Add(newGroup);      // dodanie grupy do listy grup
            }

            return toDosGroups;
        }
    }
}
