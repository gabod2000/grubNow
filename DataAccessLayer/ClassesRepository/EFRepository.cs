
using Models;
using System.Collections.Generic;

namespace DataAccessLayer
{
    public class EfRepository : IEfRepository
    {
        public readonly GrubNowDbContext _context;

       
        public EfRepository(GrubNowDbContext context)
        {
            _context = context;
        }

        #region Genaric
        public void Add(object entity)
        {
            _context.Add(entity);
        }


        public void Update(object entity)
        {
            _context.Update(entity);
        }


        public void Delete(object entity)
        {
            _context.Remove(entity);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
        #endregion


    }
}
    