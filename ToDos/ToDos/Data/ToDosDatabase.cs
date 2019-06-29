using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using ToDos.Models;

namespace ToDos.Data
{
    public class ToDosDatabase
    {
        private readonly SQLiteAsyncConnection connection; // przechowywanie połączenia z bazą danych

        public ToDosDatabase(string dbPath)
        {
            connection = new SQLiteAsyncConnection(dbPath); // utworzenie połączenia do bazy
            connection.CreateTableAsync<ToDo>().Wait(); // utworzenie tabelki w bazie jeśli nie istnieje
        }

        public Task<List<ToDo>> getToDosAsync() // zwrócenie zadania - asynchronicznie
        {
            return connection.Table<ToDo>().ToListAsync(); // zwrócenie zadania zwracającego listę ToDo
        }


        public Task<int> addOrUpdateToDoAsync(ToDo toDo)
        {
            if (toDo.Id == 0)
                return connection.InsertAsync(toDo);
            else
                return connection.UpdateAsync(toDo);
        }
    }
}
