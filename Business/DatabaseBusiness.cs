using WebApplication1.Models;

namespace WebApplication1.Business
{
    public class DatabaseBusiness : IDatabaseBusiness
    {
        private readonly IDatabaseRepository _databaseRepository;
        public DatabaseBusiness(IDatabaseRepository databaseRepository)
        {
            _databaseRepository = databaseRepository;
        }

        public async Task<BookDataResponse> InsertAll()
        {
            return await _databaseRepository.InsertAll();
        }
    }
}
