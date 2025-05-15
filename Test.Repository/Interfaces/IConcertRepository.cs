using Test.Entity.Data;

namespace Test.Repository.Interfaces;

public interface IConcertRepository
{
    IEnumerable<Concert> GetAll();
    
    (IEnumerable<Concert>,int) GetConcertsAsync(string? search, int currentPage, int pageSize, string? sortColumn, string? sortDirection);

    Task<Concert?> GetConcertByIdAsync(int id);

    Task<int> AddAsync(Concert Concert);

    Task UpdateAsync(Concert Concert);

    bool IsConcertExistAsync(string title);
}
