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
    public class GpsTrackingController : ApiController
    {
        private dbSmartCityContext db = new dbSmartCityContext();

        // GET: api/GpsTracking
        public IQueryable<tbGPS_Realtime> GettbGPS_Realtime()
        {
            return db.tbGPS_Realtime;
        }

        // GET: api/GpsTracking/5
        [ResponseType(typeof(tbGPS_Realtime))]
        public async Task<IHttpActionResult> GettbGPS_Realtime(string id)
        {
            tbGPS_Realtime tbGPS_Realtime = await db.tbGPS_Realtime.FindAsync(id);
            if (tbGPS_Realtime == null)
            {
                return NotFound();
            }

            return Ok(tbGPS_Realtime);
        }

        // PUT: api/GpsTracking/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttbGPS_Realtime(string id, tbGPS_Realtime tbGPS_Realtime)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbGPS_Realtime.DevEUI)
            {
                return BadRequest();
            }

            db.Entry(tbGPS_Realtime).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbGPS_RealtimeExists(id))
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

        // POST: api/GpsTracking
        [ResponseType(typeof(tbGPS_Realtime))]
        public async Task<IHttpActionResult> PosttbGPS_Realtime(tbGPS_Realtime tbGPS_Realtime)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbGPS_Realtime.Add(tbGPS_Realtime);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (tbGPS_RealtimeExists(tbGPS_Realtime.DevEUI))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tbGPS_Realtime.DevEUI }, tbGPS_Realtime);
        }

        // DELETE: api/GpsTracking/5
        [ResponseType(typeof(tbGPS_Realtime))]
        public async Task<IHttpActionResult> DeletetbGPS_Realtime(string id)
        {
            tbGPS_Realtime tbGPS_Realtime = await db.tbGPS_Realtime.FindAsync(id);
            if (tbGPS_Realtime == null)
            {
                return NotFound();
            }

            db.tbGPS_Realtime.Remove(tbGPS_Realtime);
            await db.SaveChangesAsync();

            return Ok(tbGPS_Realtime);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbGPS_RealtimeExists(string id)
        {
            return db.tbGPS_Realtime.Count(e => e.DevEUI == id) > 0;
        }
    }
}