namespace TicketingSystem.Services
{
    using TicketingSystem.Data;

    public abstract class BaseService
    {
        private TicketSystemDbContext context;

        protected BaseService()
        {
            this.context = new TicketSystemDbContext();
        }

        protected TicketSystemDbContext Context
        {
            get
            {
                return this.context;
            }
        }
    }
}