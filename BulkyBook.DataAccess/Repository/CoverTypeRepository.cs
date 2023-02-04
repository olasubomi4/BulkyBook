using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBookWeb.Data;

namespace BulkyBook.DataAccess.Repository;

public class CoverTypeRepository : Repository<Models.CoverType>, ICoverType
{
    private ApplicationDbContext _db;

    public CoverTypeRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
    public void Update(CoverType obj)
    {
        _db.CoverTypes.Update(obj);
    }
}
