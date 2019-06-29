using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ToDos.Models
{
    public class ToDo
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Category { get; set; }
    }
}
