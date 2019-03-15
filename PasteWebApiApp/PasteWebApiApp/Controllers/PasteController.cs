using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

using PasteWebApiApp.DataAccess;
using PasteWebApiApp.Services;

using log4net;
//using Ninject;


namespace PasteWebApiApp.Controllers
{
    public class PasteController : ApiController
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(PasteController));
        private readonly IPasteService _pasteService;


        public PasteController()
        {
            //IKernel ninjectKernel = new StandardKernel();
            //ninjectKernel.Bind<IPasteService>().To<PasteService>();
            //_pasteService = ninjectKernel.Get<IPasteService>();
        }


        public PasteController(IPasteService pasteService)
        {
            _pasteService = pasteService;
        }


        // GET api/Paste
        public async Task<IHttpActionResult> GetPastes()
        {
            Task<List<Paste>> pasteTask = _pasteService.GetAllPastes();
            var pastes = await pasteTask;
            return Ok(pastes);
        }


        // GET api/Paste/5
        [ResponseType(typeof(Paste))]
        public async Task<HttpResponseMessage> GetPaste(string id)
        {
            Paste paste = await _pasteService.GetPaste(id);
            var response = Request.CreateResponse(HttpStatusCode.OK, paste);
            response.Headers.Add("Last-Access", paste.AccessDate.ToShortDateString());

            logger.Info("Paste requested with id: " + id);

            return response;
        }


        // DELETE api/Paste/5
        [ResponseType(typeof(Paste))]
        public async Task<IHttpActionResult> DeletePaste(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _pasteService.DeletePaste(id);
                logger.Info("Paste deleted with id: " + id);
                return Ok(id);
            }
            //catch (EntityNotFoundException ex)
            catch (Exception e)
            {
                logger.Error(e.Message);
                return NotFound();
            }
        }


        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}