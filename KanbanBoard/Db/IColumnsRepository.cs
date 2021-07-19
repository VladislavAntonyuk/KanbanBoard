using System.Collections.Generic;
using KanbanBoard.Models;

namespace KanbanBoard.Db
{
    public interface IColumnsRepository
    {
        int DeleteItem(int id);
        Column GetItem(int id);
        IEnumerable<Column> GetItems();
        int SaveItem(Column item);
    }
}