using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PasteWebApiApp.DataAccess
{
    public interface IPasteRepository
    {
        Task<List<Paste>> GetAllAsync();
        Task<Paste> GetAsync(string id);
        Task CreateAsync(Paste item);
        Task UpdateAsync(Paste item);
        Task DeleteAsync(Paste item);
        bool Exists(string id);
    }
}
