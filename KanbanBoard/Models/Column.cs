﻿using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace KanbanBoard.Models
{
    public sealed class Column
    {
        public Column()
        {
            Cards = new ObservableCollection<Card>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Wip { get; set; } = int.MaxValue;
        public ICollection<Card> Cards { get; set; }
        public int Order { get; set; }
    }
}