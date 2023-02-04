using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBookWeb.Data;

namespace BulkyBook.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    public ApplicationDbContext _db;

    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
        Category = new CategoryRepository(_db);
        CoverType = new CoverTypeRepository(_db);
        Product = new ProductRepository(_db);
        Company = new CompanyRepository(_db);
        ShoppingCart = new ShoppingCartRepository(_db);
        ApplicationUser = new ApplicationUserRepository(_db);
        OrderHeader = new OrderHeaderRepository(_db);
        OrderDetail = new OrderDetailRepository(_db)
            ;
    }
    public ICategoryRepository Category { get; private set; }
    public ICoverType CoverType { get; }
    public IProduct Product { get; }
    public ICompany Company { get; }
    public IApplicationUserRepository ApplicationUser { get; }
    public IShoppingCartRepository ShoppingCart { get; }
    public IOrderDetailRepository OrderDetail { get; }
    public IOrderHeaderRepository OrderHeader { get; }


    public void Save()
    {
        _db.SaveChanges();
    }
}