namespace TicketingSystem.Models.EntityModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        private ICollection<Ticket> tickets;

        public Category()
        {
            this.tickets = new HashSet<Ticket>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Ticket> Tickets
        {
            get { return this.tickets; }
            set { this.tickets = value; }
        }
    }
}