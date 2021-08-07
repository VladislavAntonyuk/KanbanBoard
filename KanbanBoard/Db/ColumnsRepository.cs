using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KanbanBoard.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;

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
            var result = columns.GroupJoin(cards, column => column.Id, card => card.ColumnId,
                        (column, columnCards) =>
                        {
                            column.Cards = columnCards.ToObservableCollection();
                            return column;
                        }).ToList();
            return Task.FromResult(result);
        }

        public Task DeleteColumnWithCards(Column column)
        {
            database.Delete(column, true);
            return Task.CompletedTask;

        }
    }
}