using AcmeCorpStockApp.BLL.Interfaces;
using AcmeCorpStockApp.CustomElements;
using AcmeCorpStockApp.Dtos;
using AcmeCorpStockApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AcmeCorpStockApp.Controllers
{
    [CustomAuthAttribute]
    public class ProductController : Controller
    {
        private readonly IManageProductsService _manageProductsService;

        public ProductController(IManageProductsService manageProductsService)
        {
            _manageProductsService = manageProductsService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                Product newProduct = new Product
                {
                    Id = model.Id,
                    Name = model.Name,
                    Quantity = int.Parse(model.Quantity),
                    Price = model.Price ?? 0,
                    Manufactured = DateTime.Parse(model.Manufactured)
                };

                ResultState resultState = await _manageProductsService.AddNewProductAsync(newProduct);
                if (resultState.Status == ResultStatus.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }
                else
                {
                    ModelState.AddModelError("alreadyexist", resultState.Message);
                    return View(model);
                }
            }
        }

        [HttpGet]
        public IActionResult Find()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Find(string id)
        {
            Product product = await _manageProductsService.FindProductByIdAsync(id);
            if (product == null)
            {
                ModelState.AddModelError("notfound", "Product Not Found");
                return View();
            }
            else
            {
                ProductDTO model = new ProductDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    Quantity = product.Quantity.ToString(),
                    Price = product.Price,
                    Manufactured = product.Manufactured.ToString("yyyy-MM-dd")
                };
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Product/Find.cshtml", model);
            }
            else
            {
                Product product = new Product
                {
                    Id = model.Id,
                    Name = model.Name,
                    Quantity = int.Parse(model.Quantity),
                    Price = model.Price ?? 0,
                    Manufactured = DateTime.Parse(model.Manufactured).Date
                };

                ResultState result = await _manageProductsService.UpdateProductAsync(product);
                if (result.Status == ResultStatus.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }
                else
                {
                    ModelState.AddModelError("error", result.Message);
                    return View();
                }
            }
        }

        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FindForDelete(string id)
        {
            Product product = await _manageProductsService.FindProductByIdAsync(id);
            if (product == null)
            {
                ModelState.AddModelError("notfound", "Product Not Found");
                return View("~/Views/Product/Delete.cshtml");
            }
            else
            {
                ProductDTO model = new ProductDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    Quantity = product.Quantity.ToString(),
                    Price = product.Price,
                    Manufactured = product.Manufactured.ToString("yyyy-MM-dd")
                };
                return View("~/Views/Product/Delete.cshtml", model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            Product product = await _manageProductsService.FindProductByIdAsync(id);
            if (product == null)
            {
                ModelState.AddModelError("notfound", "Product Not Found");
                return View();
            }
            else
            {
                ResultState resultState = await _manageProductsService.DeleteProductAsync(product);
                if (resultState.Status == ResultStatus.Succeeded)
                {

                    return RedirectToAction("index", "home");
                }
                else
                {

                    ModelState.AddModelError("notfound", resultState.Message);
                    return View(id);
                }
            }
        }
    }
}