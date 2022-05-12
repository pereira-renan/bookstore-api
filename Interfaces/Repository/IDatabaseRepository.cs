
using WebApplication1.Models;

public interface IDatabaseRepository
{
    Task<BookDataResponse> InsertAll();

}