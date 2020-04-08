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
    public class WaterQualitySensorController : ApiController
    {
        private dbSmartCityContext db = new dbSmartCityContext();

        // GET: api/WaterQualitySensor
        public IQueryable<tbWaterQualitySensor> GettbWaterQualitySensor()
        {
            return db.tbWaterQualitySensor;
        }

        // GET: api/WaterQualitySensor/5
        [ResponseType(typeof(tbWaterQualitySensor))]
        public async Task<IHttpActionResult> GettbWaterQualitySensor(DateTime id)
        {
            tbWaterQualitySensor tbWaterQualitySensor = await db.tbWaterQualitySensor.FindAsync(id);
            if (tbWaterQualitySensor == null)
            {
                return NotFound();
            }

            return Ok(tbWaterQualitySensor);
        }

        // PUT: api/WaterQualitySensor/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttbWaterQualitySensor(DateTime id, tbWaterQualitySensor tbWaterQualitySensor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbWaterQualitySensor.Date)
            {
                return BadRequest();
            }

            db.Entry(tbWaterQualitySensor).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbWaterQualitySensorExists(id))
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

        // POST: api/WaterQualitySensor
        [ResponseType(typeof(tbWaterQualitySensor))]
        public async Task<IHttpActionResult> PosttbWaterQualitySensor(tbWaterQualitySensor tbWaterQualitySensor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbWaterQualitySensor.Add(tbWaterQualitySensor);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (tbWaterQualitySensorExists(tbWaterQualitySensor.Date))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tbWaterQualitySensor.Date }, tbWaterQualitySensor);
        }

        // DELETE: api/WaterQualitySensor/5
        [ResponseType(typeof(tbWaterQualitySensor))]
        public async Task<IHttpActionResult> DeletetbWaterQualitySensor(DateTime id)
        {
            tbWaterQualitySensor tbWaterQualitySensor = await db.tbWaterQualitySensor.FindAsync(id);
            if (tbWaterQualitySensor == null)
            {
                return NotFound();
            }

            db.tbWaterQualitySensor.Remove(tbWaterQualitySensor);
            await db.SaveChangesAsync();

            return Ok(tbWaterQualitySensor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbWaterQualitySensorExists(DateTime id)
        {
            return db.tbWaterQualitySensor.Count(e => e.Date == id) > 0;
        }
    }
}