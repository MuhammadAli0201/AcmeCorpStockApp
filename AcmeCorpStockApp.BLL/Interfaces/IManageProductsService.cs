using AcmeCorpStockApp.Models;
using System.Threading.Tasks;

namespace AcmeCorpStockApp.BLL.Interfaces
{
    public interface IManageProductsService
    {
        public Task<ResultState> AddNewProductAsync(Product product);
        public Task<Product> FindProductByIdAsync(string id);
        public Task<ResultState> UpdateProductAsync(Product product);
        public Task<ResultState> DeleteProductAsync(Product product);
    }
}
