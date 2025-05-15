using Test.Entity.Models;
using Test.Entity.Data;
using Test.Repository.Interfaces;
using Test.Service.Interfaces;
namespace Test.Service.Implementation;

public class ConcertService : IConcertService
{

    private readonly IConcertRepository _concertRepository;

    public ConcertService(IConcertRepository concertRepository)
    {
        _concertRepository = concertRepository;

    }


    public async Task<ConcertView> GetConcertByIdAsync(int id)
    {
        var Concert = await _concertRepository.GetConcertByIdAsync(id);
        return new ConcertView
        {
            ConcertId = Concert.ConcertId,
            Title = Concert.Title,
            ArtistName = Concert.ArtistName,
            Venue = Concert.Venue,
            ConcertDate = Concert.ConcertDate,
            Price = Concert.Price,
            TotalSeat = Concert.TotalSeat
        };
    }



    public async Task<int> AddConcertAsync(ConcertView Concert)
    {

        var existConcert = _concertRepository.IsConcertExistAsync(Concert.Title);

        if (existConcert == true)
        {
            throw new InvalidOperationException("A Concert with this title already exists.");
        }
        try
        {
            var addConcert = new Concert
            {
                Title = Concert.Title,
                ArtistName = Concert.ArtistName,
                Venue = Concert.Venue,
                ConcertDate = DateTime.UtcNow,
                Price = Concert.Price,
                TotalSeat = Concert.TotalSeat
            };
            int id = await _concertRepository.AddAsync(addConcert);
            return id;
        }
        catch (System.Exception)
        {
            throw;
        }
    }



    public async Task UpdateConcertAsync(ConcertView Concert)
    {
        try
        {
            Concert existConcert = await _concertRepository.GetConcertByIdAsync(Concert.ConcertId);
            existConcert.Title = Concert.Title;
            existConcert.ArtistName = Concert.ArtistName;
            existConcert.Venue = Concert.Venue;
            existConcert.ConcertDate = Concert.ConcertDate;
            
            existConcert.Price = Concert.Price;
            existConcert.TotalSeat = Concert.TotalSeat;

            await _concertRepository.UpdateAsync(existConcert);
        }
        catch (System.Exception)
        {

            throw;
        }

    }




    public async Task DeleteConcertAsync(int id)
    {
        Concert ConcertModel = await _concertRepository.GetConcertByIdAsync(id);
        ConcertModel.IsDeleted = true;
        await _concertRepository.UpdateAsync(ConcertModel);
    }


    public async Task<ConcertPagination> GetConcertsAsync(string? search, int currentPage, int pageSize, string? sortColumn, string? sortDirection)
    {
        var Concerts = _concertRepository.GetConcertsAsync(search,currentPage,pageSize,sortColumn,sortDirection);

        if(Concerts.Item1 == null){
            Pagination temppagination = new Pagination
            {
                CurrentPage = (int)currentPage,
                TotalRecords = Concerts.Item2,
                PageSize = (int)pageSize
            };

            return new ConcertPagination
            {
                Concerts = null,
                Pagination = temppagination
            };
        }

        var ConcertView = Concerts.Item1.Select(Concert => new ConcertView
        {
            ConcertId = Concert.ConcertId,
            Title = Concert.Title,
            ArtistName = Concert.ArtistName,
            Venue = Concert.Venue,
            ConcertDate = Concert.ConcertDate,
            Price = Concert.Price,
            TotalSeat = Concert.TotalSeat
        }).ToList();

        Pagination pagination = new Pagination
        {
            CurrentPage = (int)currentPage,
            TotalRecords = Concerts.Item2,
            PageSize = (int)pageSize
        };

        return new ConcertPagination
        {
            Concerts = ConcertView,
            Pagination = pagination
        };
    }




}

