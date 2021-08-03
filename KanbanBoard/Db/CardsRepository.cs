using System.Collections.Generic;
using System.Threading.Tasks;
using KanbanBoard.Models;
using SQLite;
 
namespace KanbanBoard.Db
{
    public class CardsRepository : BaseRepository<Card>, ICardsRepository
    {
        public CardsRepository(IPath path):base(path)
        {

        }
    }
}