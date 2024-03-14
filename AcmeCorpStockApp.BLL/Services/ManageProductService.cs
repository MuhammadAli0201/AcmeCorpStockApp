using AcmeCorpStockApp.BLL.Interfaces;
using AcmeCorpStockApp.Models;
using AcmeCorpStockApp.Services.Infrastructure;
using System.Threading.Tasks;

namespace AcmeCorpStockApp.BLL.Services
{
    public class ManageProductService : IManageProductsService
    {
        private readonly IManageProductsRepository _manageProductRepo;
        public ManageProductService(IManageProductsRepository manageProductsRepository)
        {
            _manageProductRepo = manageProductsRepository;
        }

        public async Task<ResultState> AddNewProductAsync(Product product)
        {
            Product findProduct = _manageProductRepo.GetByAttrAsync(p => p.Id == product.Id);
            if (findProduct != null)
            {
                return new ResultState
                {
                    Message = "Product Already Exist",
                    Status = ResultStatus.Error
                };
            }
            else
            {
                await _manageProductRepo.CreateAsync(product);
                return new ResultState
                {
                    Message = "Added",
                    Status = ResultStatus.Succeeded
                };
            }
        }

        public async Task<Product> FindProductByIdAsync(string id)
        {
            return _manageProductRepo.GetByAttrAsync(p => p.Id == id);
        }

        public async Task<ResultState> UpdateProductAsync(Product product)
        {
            Product getProduct = await FindProductByIdAsync(product.Id);
            if (getProduct == null)
            {
                return new ResultState
                {
                    Message = "Unable to Find Product",
                    Status = ResultStatus.Error
                };
            }
            else
            {
                Product updatedProduct = await _manageProductRepo.UpdateAsync(getProduct, product);
                if (updatedProduct != null)
                {
                    return new ResultState
                    {
                        Message = "updated",
                        Status = ResultStatus.Succeeded
                    };
                }
                else
                {
                    return new ResultState
                    {
                        Message = "Unable to Update Product",
                        Status = ResultStatus.Error
                    };
                }
            }

        }

        public async Task<ResultState> DeleteProductAsync(Product product)
        {
            bool getProductStatus = await _manageProductRepo.DeleteAsync(product);
            return getProductStatus ? new ResultState { Message = "Deleted", Status = ResultStatus.Succeeded } :
                new ResultState { Message = "Error In deletion", Status = ResultStatus.Error }
                ;
        }
    }
}
