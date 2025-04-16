using Microsoft.AspNetCore.Mvc;
using InventorySystem.Models;
using InventorySystem.Repositories;

namespace InventorySystem.Controllers
{
    public class StockController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly ILogger<StockController> _logger;

        public StockController(UnitOfWork unitOfWork, ILogger<StockController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        // GET: /Stock/
        public IActionResult Index()
        {
            _logger.LogInformation("Accessed Stock Index page.");
            var stocks = _unitOfWork.Stocks.GetAll();
            return View(stocks);
        }

        // GET: /Stock/Details/5
        public IActionResult Details(int id)
        {
            var stock = _unitOfWork.Stocks.GetById(id);
            if (stock == null)
            {
                _logger.LogWarning("Stock not found with ID: {Id}", id);
                return NotFound();
            }
            return View(stock);
        }

        // GET: /Stock/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Stock/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Stock stock)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Stocks.Add(stock);
                _unitOfWork.Save();
                _logger.LogInformation("Created new stock with ID: {Id}", stock.Id);
                return RedirectToAction(nameof(Index));
            }
            return View(stock);
        }

        // GET: /Stock/Edit/5
        public IActionResult Edit(int id)
        {
            var stock = _unitOfWork.Stocks.GetById(id);
            if (stock == null)
            {
                _logger.LogWarning("Stock not found for Edit with ID: {Id}", id);
                return NotFound();
            }
            return View(stock);
        }

        // POST: /Stock/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Stock stock)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Stocks.Update(stock);
                _unitOfWork.Save();
                _logger.LogInformation("Updated stock with ID: {Id}", stock.Id);
                return RedirectToAction(nameof(Index));
            }
            return View(stock);
        }

        // GET: /Stock/Delete/5
        public IActionResult Delete(int id)
        {
            var stock = _unitOfWork.Stocks.GetById(id);
            if (stock == null)
            {
                _logger.LogWarning("Stock not found for Delete with ID: {Id}", id);
                return NotFound();
            }
            return View(stock);
        }

        // POST: /Stock/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var stock = _unitOfWork.Stocks.GetById(id);
            if (stock == null)
            {
                _logger.LogWarning("Stock not found for DeleteConfirmed with ID: {Id}", id);
                return NotFound();
            }

            _unitOfWork.Stocks.Delete(stock);
            _unitOfWork.Save();
            _logger.LogInformation("Deleted stock with ID: {Id}", stock.Id);

            return RedirectToAction(nameof(Index));
        }
    }
}
