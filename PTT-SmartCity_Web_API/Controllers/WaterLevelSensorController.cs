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
    public class WaterLevelSensorController : ApiController
    {
        private dbSmartCityContext db = new dbSmartCityContext();

        // GET: api/WaterLevelSensor
        public IQueryable<tbWaterLevelSensor> GettbWaterLevelSensor()
        {
            return db.tbWaterLevelSensor;
        }

        // GET: api/WaterLevelSensor/5
        [ResponseType(typeof(tbWaterLevelSensor))]
        public async Task<IHttpActionResult> GettbWaterLevelSensor(DateTime id)
        {
            tbWaterLevelSensor tbWaterLevelSensor = await db.tbWaterLevelSensor.FindAsync(id);
            if (tbWaterLevelSensor == null)
            {
                return NotFound();
            }

            return Ok(tbWaterLevelSensor);
        }

        // PUT: api/WaterLevelSensor/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttbWaterLevelSensor(DateTime id, tbWaterLevelSensor tbWaterLevelSensor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbWaterLevelSensor.Date)
            {
                return BadRequest();
            }

            db.Entry(tbWaterLevelSensor).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbWaterLevelSensorExists(id))
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

        // POST: api/WaterLevelSensor
        [ResponseType(typeof(tbWaterLevelSensor))]
        public async Task<IHttpActionResult> PosttbWaterLevelSensor(tbWaterLevelSensor tbWaterLevelSensor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbWaterLevelSensor.Add(tbWaterLevelSensor);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (tbWaterLevelSensorExists(tbWaterLevelSensor.Date))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tbWaterLevelSensor.Date }, tbWaterLevelSensor);
        }

        // DELETE: api/WaterLevelSensor/5
        [ResponseType(typeof(tbWaterLevelSensor))]
        public async Task<IHttpActionResult> DeletetbWaterLevelSensor(DateTime id)
        {
            tbWaterLevelSensor tbWaterLevelSensor = await db.tbWaterLevelSensor.FindAsync(id);
            if (tbWaterLevelSensor == null)
            {
                return NotFound();
            }

            db.tbWaterLevelSensor.Remove(tbWaterLevelSensor);
            await db.SaveChangesAsync();

            return Ok(tbWaterLevelSensor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbWaterLevelSensorExists(DateTime id)
        {
            return db.tbWaterLevelSensor.Count(e => e.Date == id) > 0;
        }
    }
}