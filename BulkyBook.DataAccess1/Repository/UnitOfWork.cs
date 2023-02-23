using System;
using BulkyBook.DataAccess1.Repository.IRepository;

namespace BulkyBook.DataAccess1.Repository
{
	public class UnitOfWork:IUnitOfWork
	{
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            CoverType = new CoverTypeRepository(_db);
        }
        public ICategoryRepository Category { get; private set; } = null!;

        public ICoverTypeRepository CoverType { get; private set; } = null!;

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}