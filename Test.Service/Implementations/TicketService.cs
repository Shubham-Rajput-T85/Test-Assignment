using Test.Entity.Models;
using Test.Entity.Data;
using Test.Repository.Interfaces;
using Test.Service.Interfaces;
namespace Test.Service.Implementation;

public class TicketService : ITicketService
{

    private readonly ITicketRepository _ticketRepository;

    private readonly IConcertRepository _concertRepository;

    public TicketService(ITicketRepository ticketRepository, IConcertRepository concertRepository)
    {
        _ticketRepository = ticketRepository;
        _concertRepository = concertRepository;

    }


    public async Task<TicketView> GetTicketByIdAsync(int id)
    {
        var Ticket = await _ticketRepository.GetTicketByIdAsync(id);
        if(Ticket == null){
            return new TicketView();
        }
        return new TicketView
        {
            TicketId = Ticket.TicketId,
            UserId = Ticket.UserId,
            ConcertId = Ticket.ConcertId,
            BookTime = Ticket.BookTime,
            Seat = Ticket.Seat,
            Discount = Ticket.Discount,
            Amount = Ticket.Amount
        };
    }



    public async Task<int> AddTicketAsync(TicketView Ticket)
    {
        var ConcertModel = await _concertRepository.GetConcertByIdAsync(Ticket.ConcertId);
        var existTicket = _ticketRepository.IsTicketExistAsync(Ticket.UserId);

        if (existTicket == true)
        {
            throw new InvalidOperationException("A Ticket with this user already exists.");
        }
        try
        {
            if(Ticket.Seat >= 5){
                Ticket.Discount = 20;
            }

            Ticket.Amount =  (decimal)(Ticket.Seat *(ConcertModel.Price  *((100 - Ticket.Discount)/100))) ;

            var addTicket = new Ticket
            {
                UserId = Ticket.UserId,
                ConcertId = Ticket.ConcertId,
                BookTime = DateTime.UtcNow,
                Seat = Ticket.Seat,
                Discount = Ticket.Discount,
                Amount = Ticket.Amount
            };
            int id = await _ticketRepository.AddAsync(addTicket);
            return id;
        }
        catch (System.Exception)
        {
            throw;
        }
    }







    public async Task<List<TicketView>> GetAllTicketsByUserIdAsync(int userId)
    {
        var Tickets = _ticketRepository.GetTicketByUserIdAsync(userId);

        var TicketView = Tickets.Select(Ticket => new TicketView
        {
            TicketId = Ticket.TicketId,
            UserId = Ticket.UserId,
            ConcertId = Ticket.ConcertId,
            BookTime = Ticket.BookTime,
            Seat = Ticket.Seat,
            Discount = Ticket.Discount,
            Amount = Ticket.Amount
        }).ToList();

        return TicketView;
    }




}


