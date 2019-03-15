using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasteWebApiApp.DataAccess;

namespace PasteWebApiApp.Services
{
    public interface IPasteService
    {
        Task<List<Paste>> GetAllPastes();
        Task<Paste> GetPaste(string id);
        Task CreatePaste(Paste paste);
        Task UpdatePaste(Paste paste);
        Task DeletePaste(string id);
    }
}
