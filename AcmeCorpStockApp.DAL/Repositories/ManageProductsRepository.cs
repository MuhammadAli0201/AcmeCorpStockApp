using AcmeCorpStockApp.DAL;
using AcmeCorpStockApp.DAL.Repositories;
using AcmeCorpStockApp.Models;
using AcmeCorpStockApp.Services.Infrastructure;

namespace AcmeCorpStockApp.Services.Repositories
{
    public class ManageProductsRepository : GenericSQLRepo<Product>, IManageProductsRepository
    {
        private readonly AcmeCorpAppDBContext _acneCorpAppDBContext;
        public ManageProductsRepository(AcmeCorpAppDBContext acneCorpAppDBContext) : base(acneCorpAppDBContext)
        {
            _acneCorpAppDBContext = acneCorpAppDBContext;
        }
    }
}
