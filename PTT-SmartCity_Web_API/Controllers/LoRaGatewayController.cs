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
using PTT_SmartCity_Web_API.Models;

namespace PTT_SmartCity_Web_API.Controllers
{
    public class LoRaGatewayController : ApiController
    {
        private dbEnvironmentLoRaContext db = new dbEnvironmentLoRaContext();

        // GET: api/LoRaGateway
        public IQueryable<tbLoRaGateway> GettbLoRaGateway()
        {
            return db.tbLoRaGateway;
        }

        // GET: api/LoRaGateway/5
        [ResponseType(typeof(tbLoRaGateway))]
        public async Task<IHttpActionResult> GettbLoRaGateway(DateTime id)
        {
            tbLoRaGateway tbLoRaGateway = await db.tbLoRaGateway.FindAsync(id);
            if (tbLoRaGateway == null)
            {
                return NotFound();
            }

            return Ok(tbLoRaGateway);
        }

        // PUT: api/LoRaGateway/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttbLoRaGateway(DateTime id, tbLoRaGateway tbLoRaGateway)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbLoRaGateway.Date)
            {
                return BadRequest();
            }

            db.Entry(tbLoRaGateway).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbLoRaGatewayExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/LoRaGateway
        [ResponseType(typeof(tbLoRaGateway))]
        public async Task<IHttpActionResult> PosttbLoRaGateway(tbLoRaGateway tbLoRaGateway)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbLoRaGateway.Add(tbLoRaGateway);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (tbLoRaGatewayExists(tbLoRaGateway.Date))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tbLoRaGateway.Date }, tbLoRaGateway);
        }

        // DELETE: api/LoRaGateway/5
        [ResponseType(typeof(tbLoRaGateway))]
        public async Task<IHttpActionResult> DeletetbLoRaGateway(DateTime id)
        {
            tbLoRaGateway tbLoRaGateway = await db.tbLoRaGateway.FindAsync(id);
            if (tbLoRaGateway == null)
            {
                return NotFound();
            }

            db.tbLoRaGateway.Remove(tbLoRaGateway);
            await db.SaveChangesAsync();

            return Ok(tbLoRaGateway);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbLoRaGatewayExists(DateTime id)
        {
            return db.tbLoRaGateway.Count(e => e.Date == id) > 0;
        }
    }
}