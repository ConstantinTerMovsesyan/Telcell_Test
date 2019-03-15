using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Ninject;


namespace PasteWebApiApp.DataAccess
{
    public class PasteRepository : IPasteRepository
    {
        protected readonly TelcellDbContext _dbContext;


        //public PasteRepository()
        //{
        //    IKernel ninjectKernel = new StandardKernel();
        //    _dbContext = ninjectKernel.Get<TelcellDbContext>();
        //}

        public PasteRepository(TelcellDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public Task<List<Paste>> GetAllAsync()
        {
            return _dbContext.Pastes.AsNoTracking().ToListAsync();
        }


        public Task<Paste> GetAsync(string id)
        {
            return _dbContext.Pastes.FindAsync(id);
        }


        public Task CreateAsync(Paste item)
        {
            _dbContext.Pastes.Add(item);
            return _dbContext.SaveChangesAsync();
        }


        public Task UpdateAsync(Paste item)
        {
            _dbContext.Entry(item).State = EntityState.Modified;
            return _dbContext.SaveChangesAsync();
        }


        public Task DeleteAsync(Paste item)
        {
            _dbContext.Pastes.Remove(item);
            return _dbContext.SaveChangesAsync();
        }


        public bool Exists(string id)
        {
            return _dbContext.Pastes.Count(e => e.Id == id) > 0;
        }
    }
}