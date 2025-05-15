using Test.Entity.Models;
using Test.Entity.Data;
using Test.Repository.Interfaces;

namespace Test.Service.Interfaces;

public interface IConcertService
{

    Task<ConcertView> GetConcertByIdAsync(int id);



    Task<int> AddConcertAsync(ConcertView Concert);



    Task UpdateConcertAsync(ConcertView Concert);




    Task DeleteConcertAsync(int id);



    Task<ConcertPagination> GetConcertsAsync(string? search, int currentPage, int pageSize, string? sortColumn, string? sortDirection);




}
