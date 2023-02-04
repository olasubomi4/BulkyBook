using BulkyBook.Models;

namespace BulkyBook.DataAccess.Repository.IRepository;

public interface ICoverType:IRepository<CoverType>
{
    void Update(CoverType obj);
}