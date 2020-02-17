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
    public class WeatheSensorController : ApiController
    {
        private dbEnvironmentLoRaContext db = new dbEnvironmentLoRaContext();

        // GET: api/WeatheSensor
        public IQueryable<tbWeatherSensor> GettbWeatherSensor()
        {
            return db.tbWeatherSensor;
        }

        // GET: api/WeatheSensor/5
        [ResponseType(typeof(tbWeatherSensor))]
        public async Task<IHttpActionResult> GettbWeatherSensor(DateTime id)
        {
            tbWeatherSensor tbWeatherSensor = await db.tbWeatherSensor.FindAsync(id);
            if (tbWeatherSensor == null)
            {
                return NotFound();
            }

            return Ok(tbWeatherSensor);
        }

        // PUT: api/WeatheSensor/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttbWeatherSensor(DateTime id, tbWeatherSensor tbWeatherSensor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbWeatherSensor.Date)
            {
                return BadRequest();
            }

            db.Entry(tbWeatherSensor).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbWeatherSensorExists(id))
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

        // POST: api/WeatheSensor
        [ResponseType(typeof(tbWeatherSensor))]
        public async Task<IHttpActionResult> PosttbWeatherSensor(tbWeatherSensor tbWeatherSensor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbWeatherSensor.Add(tbWeatherSensor);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (tbWeatherSensorExists(tbWeatherSensor.Date))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tbWeatherSensor.Date }, tbWeatherSensor);
        }

        // DELETE: api/WeatheSensor/5
        [ResponseType(typeof(tbWeatherSensor))]
        public async Task<IHttpActionResult> DeletetbWeatherSensor(DateTime id)
        {
            tbWeatherSensor tbWeatherSensor = await db.tbWeatherSensor.FindAsync(id);
            if (tbWeatherSensor == null)
            {
                return NotFound();
            }

            db.tbWeatherSensor.Remove(tbWeatherSensor);
            await db.SaveChangesAsync();

            return Ok(tbWeatherSensor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbWeatherSensorExists(DateTime id)
        {
            return db.tbWeatherSensor.Count(e => e.Date == id) > 0;
        }
    }
}