using Test.Entity.Models;

namespace Test.Service.Interfaces;

public interface ITicketService
{
    Task<TicketView> GetTicketByIdAsync(int id);
    
    Task<int> AddTicketAsync(TicketView Ticket);

    Task<List<TicketView>> GetAllTicketsByUserIdAsync(int userId);
    
}
