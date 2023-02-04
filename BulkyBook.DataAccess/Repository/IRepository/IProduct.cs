using BulkyBook.Models;

namespace BulkyBook.DataAccess.Repository.IRepository;

public interface IProduct: IRepository<Product>
{
    void Update(Models.Product obj);
}
