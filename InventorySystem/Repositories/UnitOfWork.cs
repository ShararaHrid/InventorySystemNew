using InventorySystem.Data;
using InventorySystem.Models;

namespace InventorySystem.Repositories
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Products = new Repository<Product>(_context);
            Stocks = new Repository<Stock>(_context);
        }

        public IRepository<Product> Products { get; private set; }
        public IRepository<Stock> Stocks { get; private set; }

        public void Save() => _context.SaveChanges();
    }
}
