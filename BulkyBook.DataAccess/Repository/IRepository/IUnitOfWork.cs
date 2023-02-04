namespace BulkyBook.DataAccess.Repository.IRepository;

public interface IUnitOfWork
{
    ICategoryRepository Category{ get; }
    ICoverType CoverType { get; }
    IProduct Product { get; }
    ICompany Company { get; }
    IApplicationUserRepository ApplicationUser{get;}
    IShoppingCartRepository ShoppingCart { get; }
    IOrderDetailRepository OrderDetail { get; }
    IOrderHeaderRepository OrderHeader { get; }
    void Save();
}