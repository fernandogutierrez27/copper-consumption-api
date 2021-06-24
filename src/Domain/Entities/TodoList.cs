using CopperConsumption.Domain.Common;
using System.Collections.Generic;

namespace CopperConsumption.Domain.Entities
{
    public class TodoList : AuditableEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }


        public IList<TodoItem> Items { get; private set; } = new List<TodoItem>();
    }
}
