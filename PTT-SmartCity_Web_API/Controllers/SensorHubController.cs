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
    public class SensorHubController : ApiController
    {
        private dbEnvironmentLoRaContext db = new dbEnvironmentLoRaContext();

        // GET: api/SensorHub
        public IQueryable<tbSensorHub> GettbSensorHub()
        {
            return db.tbSensorHub;
        }

        // GET: api/SensorHub/5
        [ResponseType(typeof(tbSensorHub))]
        public async Task<IHttpActionResult> GettbSensorHub(DateTime id)
        {
            tbSensorHub tbSensorHub = await db.tbSensorHub.FindAsync(id);
            if (tbSensorHub == null)
            {
                return NotFound();
            }

            return Ok(tbSensorHub);
        }

        // PUT: api/SensorHub/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttbSensorHub(DateTime id, tbSensorHub tbSensorHub)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbSensorHub.Date)
            {
                return BadRequest();
            }

            db.Entry(tbSensorHub).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbSensorHubExists(id))
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

        // POST: api/SensorHub
        [ResponseType(typeof(tbSensorHub))]
        public async Task<IHttpActionResult> PosttbSensorHub(tbSensorHub tbSensorHub)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbSensorHub.Add(tbSensorHub);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (tbSensorHubExists(tbSensorHub.Date))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tbSensorHub.Date }, tbSensorHub);
        }

        // DELETE: api/SensorHub/5
        [ResponseType(typeof(tbSensorHub))]
        public async Task<IHttpActionResult> DeletetbSensorHub(DateTime id)
        {
            tbSensorHub tbSensorHub = await db.tbSensorHub.FindAsync(id);
            if (tbSensorHub == null)
            {
                return NotFound();
            }

            db.tbSensorHub.Remove(tbSensorHub);
            await db.SaveChangesAsync();

            return Ok(tbSensorHub);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbSensorHubExists(DateTime id)
        {
            return db.tbSensorHub.Count(e => e.Date == id) > 0;
        }
    }
}