using Test.Entity.Data;

namespace Test.Repository.Interfaces;

public interface ITicketRepository
{
    IEnumerable<Ticket> GetAll();
    Task<Ticket?> GetTicketByIdAsync(int id);

    IEnumerable<Ticket> GetTicketByUserIdAsync(int id);

    Task<int> AddAsync(Ticket Ticket);

    Task UpdateAsync(Ticket Ticket);

    bool IsTicketExistAsync(int userid , int concertid);
}
