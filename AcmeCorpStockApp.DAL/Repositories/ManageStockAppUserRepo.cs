using AcmeCorpStockApp.DAL.Interfaces;
using AcmeCorpStockApp.Models;

namespace AcmeCorpStockApp.DAL.Repositories
{
    public class ManageStockAppUserRepo : GenericSQLRepo<StockAppUser>, IManageStockAppUserRepo
    {
        private readonly AcmeCorpAppDBContext _acneCorpAppDBContext;

        public ManageStockAppUserRepo(AcmeCorpAppDBContext acneCorpAppDBContext) : base(acneCorpAppDBContext)
        {
            _acneCorpAppDBContext = acneCorpAppDBContext;
        }
    }
}
