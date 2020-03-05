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
    public class WasteBinSensorController : ApiController
    {
        private dbSmartCityContext db = new dbSmartCityContext();

        // GET: api/WasteBinSensor
        public IQueryable<tbWasteBinSensor> GettbWasteBinSensor()
        {
            return db.tbWasteBinSensor;
        }

        // GET: api/WasteBinSensor/5
        [ResponseType(typeof(tbWasteBinSensor))]
        public async Task<IHttpActionResult> GettbWasteBinSensor(DateTime id)
        {
            tbWasteBinSensor tbWasteBinSensor = await db.tbWasteBinSensor.FindAsync(id);
            if (tbWasteBinSensor == null)
            {
                return NotFound();
            }

            return Ok(tbWasteBinSensor);
        }

        // PUT: api/WasteBinSensor/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttbWasteBinSensor(DateTime id, tbWasteBinSensor tbWasteBinSensor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbWasteBinSensor.Date)
            {
                return BadRequest();
            }

            db.Entry(tbWasteBinSensor).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbWasteBinSensorExists(id))
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

        // POST: api/WasteBinSensor
        [ResponseType(typeof(tbWasteBinSensor))]
        public async Task<IHttpActionResult> PosttbWasteBinSensor(tbWasteBinSensor tbWasteBinSensor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbWasteBinSensor.Add(tbWasteBinSensor);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (tbWasteBinSensorExists(tbWasteBinSensor.Date))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tbWasteBinSensor.Date }, tbWasteBinSensor);
        }

        // DELETE: api/WasteBinSensor/5
        [ResponseType(typeof(tbWasteBinSensor))]
        public async Task<IHttpActionResult> DeletetbWasteBinSensor(DateTime id)
        {
            tbWasteBinSensor tbWasteBinSensor = await db.tbWasteBinSensor.FindAsync(id);
            if (tbWasteBinSensor == null)
            {
                return NotFound();
            }

            db.tbWasteBinSensor.Remove(tbWasteBinSensor);
            await db.SaveChangesAsync();

            return Ok(tbWasteBinSensor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbWasteBinSensorExists(DateTime id)
        {
            return db.tbWasteBinSensor.Count(e => e.Date == id) > 0;
        }
    }
}