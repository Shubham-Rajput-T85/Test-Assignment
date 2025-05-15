using Microsoft.EntityFrameworkCore;
using Test.Entity.Data;
using Test.Repository.Interfaces;

namespace Test.Repository.Implementations;

public class ConcertRepository :IConcertRepository
{
    private readonly Context _context;

    public ConcertRepository(Context context)
    {
        _context = context;
    }

    public IEnumerable<Concert> GetAll() => _context.Concerts.Where(c=>c.IsDeleted == false);

    public (IEnumerable<Concert>,int) GetConcertsAsync(string? search, int currentPage, int pageSize, string? sortColumn, string? sortDirection)
    {
        IQueryable<Concert> query = _context.Concerts.Where(c=>c.IsDeleted == false);
        if(!query.Any()){
            return (null,0);
        }

        
        if(sortColumn == null){
            sortColumn = "";
        }
        switch (sortColumn.ToLower())
        {
            case "artistname":
                query = sortDirection == "asc" ? query.OrderBy(c => c.ArtistName) : query.OrderByDescending(c => c.ArtistName);
                break;
            case "concertdate":
                query = sortDirection == "asc" ? query.OrderBy(c => c.ConcertDate) : query.OrderByDescending(c => c.ConcertDate);
                break;
            case "venue":
                query = sortDirection == "asc" ? query.OrderBy(c => c.Venue) : query.OrderByDescending(c => c.Venue);
                break;
            default:
                query = query.OrderBy(c => c.ConcertId);
                break;

        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(c => c.ArtistName.ToLower().Contains(search.ToLower()) || c.Venue.ToLower().Contains(search.ToLower()) || c.Title.ToLower().Contains(search.ToLower()) );   // check for date
        }


        int totalRecords = query.Count();
        query = query.Skip((currentPage - 1) * pageSize).Take(pageSize);

        return (query,totalRecords);
    }

    public async Task<Concert?> GetConcertByIdAsync(int id)
    {
        return await _context.Concerts.FirstOrDefaultAsync(c=>c.ConcertId == id && c.IsDeleted == false);
    }

    public async Task<int> AddAsync(Concert Concert)
    {
        _context.Concerts.Add(Concert);
        await _context.SaveChangesAsync();
        return Concert.ConcertId;
    }

    public async Task UpdateAsync(Concert Concert)
    {
        // _context.ChangeTracker.Clear();
        // _context.Entry(Concert).State = EntityState.Modified;
        _context.Update(Concert);
        await _context.SaveChangesAsync();
    }

    public bool IsConcertExistAsync(string title)
    {
        return _context.Concerts.Any(c => c.Title == title && c.IsDeleted == false);
    }



}










/*

    public async Task<UserPagination> GetUsersAsync(string? search, int? currentPage, int? pageSize, string? sortColumn, string? sortDirection)
    {
        var users = await _userRepository.GetUsersAsync();

        if (!string.IsNullOrWhiteSpace(search))
        {
            // Check if search can be parsed to an integer
            users = users.Where(c => c.Username.ToLower().StartsWith(search.ToLower())).ToList();
        }

        switch (sortColumn.ToLower())
        {
            case "role":
                users = sortDirection == "asc" ? users.OrderBy(c => c.Roleid).ToList() : users.OrderByDescending(c => c.Roleid).ToList();
                break;
            case "name":
                users = sortDirection == "asc" ? users.OrderBy(c => c.Username).ToList() : users.OrderByDescending(c => c.Username).ToList();
                break;
        }

        int totalRecords = users.Count();
        users = users.Skip((int)((currentPage - 1) * pageSize)).Take((int)pageSize).ToList();

        var userView = users.Select(u => new UserView
            {
                Userid = u.Userid,
                Firstname = u.Firstname,
                Lastname = u.Lastname,
                Email = u.Email,
                Roleid = u.Roleid,
                Status = (bool)u.Status,
                Phone = u.Phone,
            }).ToList();

        Pagination pagination = new Pagination
        {
            CurrentPage = (int)currentPage,
            TotalRecords = totalRecords,
            PageSize = (int)pageSize
        };

        return new UserPagination
        {
            Users = userView,
            Pagination = pagination
        };
    }



*/
