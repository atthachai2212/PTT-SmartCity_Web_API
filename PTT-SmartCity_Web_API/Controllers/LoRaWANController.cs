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
using PTT_SmartCity_Web_API.Entity;

namespace PTT_SmartCity_Web_API.Controllers
{
    public class LoRaWANController : ApiController
    {
        private dbEnvironmentLoRaContext db = new dbEnvironmentLoRaContext();

        // GET: api/LoRaWAN
        public IQueryable<tbLoRaWAN> GettbLoRaWAN()
        {
            return db.tbLoRaWAN;
        }

        // GET: api/LoRaWAN/5
        [ResponseType(typeof(tbLoRaWAN))]
        public async Task<IHttpActionResult> GettbLoRaWAN(DateTime id)
        {
            tbLoRaWAN tbLoRaWAN = await db.tbLoRaWAN.FindAsync(id);
            if (tbLoRaWAN == null)
            {
                return NotFound();
            }

            return Ok(tbLoRaWAN);
        }

        // PUT: api/LoRaWAN/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttbLoRaWAN(DateTime id, tbLoRaWAN tbLoRaWAN)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbLoRaWAN.Date)
            {
                return BadRequest();
            }

            db.Entry(tbLoRaWAN).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbLoRaWANExists(id))
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

        // POST: api/LoRaWAN
        [ResponseType(typeof(tbLoRaWAN))]
        public async Task<IHttpActionResult> PostLoRaWAN(tbLoRaWAN tbLoRaWAN)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbLoRaWAN.Add(tbLoRaWAN);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (tbLoRaWANExists(tbLoRaWAN.Date))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tbLoRaWAN.Date }, tbLoRaWAN);
        }

        // DELETE: api/LoRaWAN/5
        [ResponseType(typeof(tbLoRaWAN))]
        public async Task<IHttpActionResult> DeletetbLoRaWAN(DateTime id)
        {
            tbLoRaWAN tbLoRaWAN = await db.tbLoRaWAN.FindAsync(id);
            if (tbLoRaWAN == null)
            {
                return NotFound();
            }

            db.tbLoRaWAN.Remove(tbLoRaWAN);
            await db.SaveChangesAsync();

            return Ok(tbLoRaWAN);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbLoRaWANExists(DateTime id)
        {
            return db.tbLoRaWAN.Count(e => e.Date == id) > 0;
        }
    }
}