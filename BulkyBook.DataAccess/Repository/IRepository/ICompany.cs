using BulkyBook.Models;

namespace BulkyBook.DataAccess.Repository.IRepository;

public interface ICompany :IRepository<Company>
{
    void Update(Company obj);
}
