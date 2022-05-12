
using WebApplication1.Models;

public interface IDatabaseBusiness
{
    Task<BookDataResponse> InsertAll();
}