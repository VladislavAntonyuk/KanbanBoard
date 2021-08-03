using System.Collections.Generic;
using System.Threading.Tasks;
using KanbanBoard.Models;
using SQLite;

namespace KanbanBoard.Db
{
    public class ColumnsRepository : BaseRepository<Column>, IColumnsRepository
    {
        public ColumnsRepository(IPath path):base(path)
        {
        }
    }
}