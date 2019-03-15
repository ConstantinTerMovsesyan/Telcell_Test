using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Ninject;
using PasteWebApiApp.DataAccess;


namespace PasteWebApiApp.Services
{
    public class PasteService : IPasteService
    {
        private readonly IPasteRepository _pasteRepository;
        private readonly PasteRESTfulApiClientService service = new PasteRESTfulApiClientService();


        public PasteService()
        {
            //IKernel ninjectKernel = new StandardKernel();
            //ninjectKernel.Bind<IPasteRepository>().To<PasteRepository>();
            //_pasteRepository = ninjectKernel.Get<IPasteRepository>();
        }


        public PasteService(IPasteRepository pasteRepository)
        {
            _pasteRepository = pasteRepository;
        }


        public async Task<List<Paste>> GetAllPastes()
        {
            Task<List<Paste>> pasteTask = _pasteRepository.GetAllAsync();
            return await pasteTask;
        }


        public async Task<Paste> GetPaste(string id)
        {
            Task<Paste> pasteTask = _pasteRepository.GetAsync(id);
            var paste = await pasteTask;

            if (paste == null)
            {
                DateTime now = DateTime.Now;
                paste = new Paste 
                { 
                    Id = id, 
                    Text = service.GetPasteById(id), 
                    CreatedDate = now, 
                    AccessDate = now 
                };
                
                await _pasteRepository.CreateAsync(paste);
            }
            else
            {
                paste.AccessDate = DateTime.Now;
                await _pasteRepository.UpdateAsync(paste);
            }

            return paste;
        }


        public async Task CreatePaste(Paste paste)
        {
            Task pasteTask = _pasteRepository.CreateAsync(paste);
            await pasteTask;
        }


        public async Task UpdatePaste(Paste paste)
        {
            bool exists = _pasteRepository.Exists(paste.Id);
            if (! exists)
            {
                //throw new EntityNotFoundException();
                throw new Exception("Entity Not Found");
            }

            Task pasteTask = _pasteRepository.UpdateAsync(paste);
            await pasteTask;
        }


        public async Task DeletePaste(string id)
        {
            Task<Paste> pasteTask = _pasteRepository.GetAsync(id);
            var paste = await pasteTask;

            if (paste == null)
            {
                //throw new EntityNotFoundException();
                throw new Exception("Entity Not Found");
            }

            Task deleteTask = _pasteRepository.DeleteAsync(paste);
            await deleteTask;
        }
    }
}