using Microsoft.EntityFrameworkCore;
using Test.Entity.Data;
using Test.Repository.Interfaces;

namespace Test.Repository.Implementations;

public class TicketRepository : ITicketRepository
{
    private readonly Context _context;

    public TicketRepository(Context context)
    {
        _context = context;
    }

    public IEnumerable<Ticket> GetAll() => _context.Tickets;

    public async Task<Ticket?> GetTicketByIdAsync(int id)
    {
        return await _context.Tickets.FirstOrDefaultAsync(c=>c.TicketId == id);
    }

    public IEnumerable<Ticket> GetTicketByUserIdAsync(int id)
    {
        return _context.Tickets.Where(c=>c.UserId == id);
    }

    public async Task<int> AddAsync(Ticket Ticket)
    {
        _context.Tickets.Add(Ticket);
        await _context.SaveChangesAsync();
        return Ticket.TicketId;
    }

    public async Task UpdateAsync(Ticket Ticket)
    {
        // _context.ChangeTracker.Clear();
        // _context.Entry(Ticket).State = EntityState.Modified;
        _context.Update(Ticket);
        await _context.SaveChangesAsync();
    }

    public bool IsTicketExistAsync(int userid)
    {
        return _context.Tickets.Any(c => c.UserId == userid);
    }



}
