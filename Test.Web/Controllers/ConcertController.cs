using Microsoft.AspNetCore.Mvc;
using Test.Entity.Models;
using Test.Service.Interfaces;
using Test.Web.Attributes;

namespace Test.Web.Controllers;

public class ConcertController : Controller
{
    private readonly IAuthService _authService;
    private readonly IJwtService _jwtService;

    private readonly IConcertService _concertService;

    private readonly ITicketService _ticketService;

    public ConcertController(IJwtService jwtService , IAuthService authService , IConcertService concertService, ITicketService ticketService)
    {
        _jwtService = jwtService;
        _authService = authService;
        _concertService = concertService;
        _ticketService = ticketService;
    }


[CustomAuthorize("Admin","User")]
    public IActionResult Index()
    {
        return View();
    }



[CustomAuthorize("Admin","User")]
    [HttpGet]
    public async Task<IActionResult> GetConcertFilterPartial(string? searchQuery, int currentPage, int pageSize, string? sortColumn, string? sortDirection)
    {
        var concerts = await _concertService.GetConcertsAsync(searchQuery, currentPage, pageSize, sortColumn, sortDirection);

        return PartialView("_ConcertPartial", concerts);
    }


[CustomAuthorize("Admin")]

[HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                // return Json(new{success = false});
                // return NotFound("id invalid");
            }
            var concert = await _concertService.GetConcertByIdAsync(id);
            if (concert == null)
            {
                // return Json(new{success = false});
                // return NotFound();
            }
            await _concertService.DeleteConcertAsync(id);
            return RedirectToAction("Index", "Concert");
            // return Json(new{success = true});
        }

[CustomAuthorize("Admin")]
        public IActionResult Add()
        {
            return View("AddConcertForm");
        }

[CustomAuthorize("Admin")]
        
        [HttpPost]
        public async Task<IActionResult> Add(ConcertView concert)
        {
            if (ModelState.IsValid)
            {
                await _concertService.AddConcertAsync(concert);
                return RedirectToAction(nameof(Index));
            }
            return View("AddConcertForm");
        }


[CustomAuthorize("Admin")]

        [Route("Concert/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            if (!ModelState.IsValid)
            {
                return View("ConcertForm");
            }
            var concert = await _concertService.GetConcertByIdAsync(id);
            if (concert == null)
            {
                return NotFound();
            }
            return View("ConcertForm",concert);
        }

[CustomAuthorize("Admin")]
        
        [HttpPost]
        public async Task<IActionResult> EditConcert(ConcertView concert)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Edit", concert);
                }

                await _concertService.UpdateConcertAsync(concert);

                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception)
            {
                throw;
            }
        }



[CustomAuthorize("User")]

        [Route("Concert/BookTicket/{id}")]
        public async Task<IActionResult> BookTicket(int id)
        {
            var model = new TicketView{
                ConcertId = id
            };
            return View("ConcertBookingForm" , model);

        }


[CustomAuthorize("User")]
        
        [HttpPost]
        public async Task<IActionResult> BookTicketSubmit(TicketView ticket)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("ConcertBookingForm", ticket);
                }

                await _ticketService.AddTicketAsync(ticket);

                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception)
            {
                throw;
            }
        }

}
