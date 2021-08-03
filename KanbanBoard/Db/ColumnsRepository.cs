using System.Collections.Generic;
using System.Linq;
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

        public override Task<List<Column>> GetItems()
        {
            var columns = database.Table<Column>().ToList();
            var cards = database.Table<Card>().ToList();
            foreach (var column in columns)
            {
                foreach (var card in cards)
                {
                    if (card.ColumnId == column.Id)
                    {
                        column.Cards.Add(card);
                    }
                }
            }

            return Task.FromResult(columns);
        }
    }
}