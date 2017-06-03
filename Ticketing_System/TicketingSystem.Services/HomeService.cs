namespace TicketingSystem.Services
{
    using Models.ViewModels.Home;
    using System.Linq;
    using AutoMapper;
    using Models.EntityModels;
    using System.Collections.Generic;

    public class HomeService : BaseService
    {
        public IList<TicketViewModel> GetIndexViewModel()
        {
            IEnumerable<Ticket> ticketsDb = this.Context.Tickets
                .OrderByDescending(c => c.Comments.Count())
                .Take(6)
                .ToList();

            IEnumerable<TicketViewModel> ticketsVm =
                Mapper.Map<IEnumerable<Ticket>, IEnumerable<TicketViewModel>>(ticketsDb);

            return ticketsVm.ToList();
        }
    }
}