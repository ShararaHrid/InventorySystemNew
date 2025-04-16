using InventorySystem.Models;
using InventorySystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace InventorySystem.Controllers
{
    public class ProductController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly ILogger<ProductController> _logger;

        public ProductController(UnitOfWork unitOfWork, ILogger<ProductController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        // READ: List products
        public IActionResult Index()
        {
            var products = _unitOfWork.Products.GetAll();
            //Log.Information("Fetched all products");
            return View(products);
        }

        // CREATE: Show form
        public IActionResult Create()
        {
            return View();
        }

        // CREATE: Save new product
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Products.Add(product);
                _unitOfWork.Save();
                _logger.LogInformation("Product created: {@Product}", product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // UPDATE: Show form
        public IActionResult Edit(int id)
        {
            var product = _unitOfWork.Products.GetById(id);
            if (product == null)
            {
                //Log.Warning("Product not found for edit, ID: {Id}", id);
                return NotFound();
            }
            return View(product);
        }

        // UPDATE: Save product
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Products.Update(product);
                _unitOfWork.Save();
                _logger.LogInformation("Product updated: {@Product}", product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // DELETE: Confirm page
        public IActionResult Delete(int id)
        {
            var product = _unitOfWork.Products.GetById(id);
            if (product == null)
            {
                //Log.Warning("Product not found for delete, ID: {Id}", id);
                return NotFound();
            }
            return View(product);
        }

        // DELETE: Delete confirmed
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _unitOfWork.Products.GetById(id);
            if (product != null)
            {
                _unitOfWork.Products.Delete(product);
                _unitOfWork.Save();
                //Log.Information("Product deleted: {@Product}", product);
                _logger.LogInformation("Product deleted: {@Product}", product);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
