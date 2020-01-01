using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace KanbanBoard
{
    public class Column
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int WIP { get; set; } = int.MaxValue;
        public virtual ICollection<Card> Cards { get; set; }
        public int Order { get; set; }

        public Column()
        {
            Cards = new ObservableCollection<Card>();
        }
    }
}
