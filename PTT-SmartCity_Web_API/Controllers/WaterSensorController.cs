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
    public class WaterSensorController : ApiController
    {
        private dbEnvironmentLoRaContext db = new dbEnvironmentLoRaContext();

        // GET: api/WaterSensor
        public IQueryable<tbWaterSensor> GettbWaterSensor()
        {
            return db.tbWaterSensor;
        }

        // GET: api/WaterSensor/5
        [ResponseType(typeof(tbWaterSensor))]
        public async Task<IHttpActionResult> GettbWaterSensor(DateTime id)
        {
            tbWaterSensor tbWaterSensor = await db.tbWaterSensor.FindAsync(id);
            if (tbWaterSensor == null)
            {
                return NotFound();
            }

            return Ok(tbWaterSensor);
        }

        // PUT: api/WaterSensor/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttbWaterSensor(DateTime id, tbWaterSensor tbWaterSensor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbWaterSensor.Date)
            {
                return BadRequest();
            }

            db.Entry(tbWaterSensor).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbWaterSensorExists(id))
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

        // POST: api/WaterSensor
        [ResponseType(typeof(tbWaterSensor))]
        public async Task<IHttpActionResult> PosttbWaterSensor(tbWaterSensor tbWaterSensor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbWaterSensor.Add(tbWaterSensor);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (tbWaterSensorExists(tbWaterSensor.Date))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tbWaterSensor.Date }, tbWaterSensor);
        }

        // DELETE: api/WaterSensor/5
        [ResponseType(typeof(tbWaterSensor))]
        public async Task<IHttpActionResult> DeletetbWaterSensor(DateTime id)
        {
            tbWaterSensor tbWaterSensor = await db.tbWaterSensor.FindAsync(id);
            if (tbWaterSensor == null)
            {
                return NotFound();
            }

            db.tbWaterSensor.Remove(tbWaterSensor);
            await db.SaveChangesAsync();

            return Ok(tbWaterSensor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbWaterSensorExists(DateTime id)
        {
            return db.tbWaterSensor.Count(e => e.Date == id) > 0;
        }
    }
}