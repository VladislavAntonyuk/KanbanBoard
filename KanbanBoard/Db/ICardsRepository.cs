using System.Collections.Generic;
using KanbanBoard.Models;

namespace KanbanBoard.Db
{
    public interface ICardsRepository
    {
        int DeleteItem(int id);
        Card GetItem(int id);
        IEnumerable<Card> GetItems();
        int SaveItem(Card item);
    }
}