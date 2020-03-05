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
    public class EnvironmentSensorController : ApiController
    {
        private dbSmartCityContext db = new dbSmartCityContext();

        // GET: api/EnvironmentSensor
        public IQueryable<tbEnvironmentSensor> GettbEnvironmentSensor()
        {
            return db.tbEnvironmentSensor;
        }

        // GET: api/EnvironmentSensor/5
        [ResponseType(typeof(tbEnvironmentSensor))]
        public async Task<IHttpActionResult> GettbEnvironmentSensor(DateTime id)
        {
            tbEnvironmentSensor tbEnvironmentSensor = await db.tbEnvironmentSensor.FindAsync(id);
            if (tbEnvironmentSensor == null)
            {
                return NotFound();
            }

            return Ok(tbEnvironmentSensor);
        }

        // PUT: api/EnvironmentSensor/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttbEnvironmentSensor(DateTime id, tbEnvironmentSensor tbEnvironmentSensor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbEnvironmentSensor.Date)
            {
                return BadRequest();
            }

            db.Entry(tbEnvironmentSensor).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbEnvironmentSensorExists(id))
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

        // POST: api/EnvironmentSensor
        [ResponseType(typeof(tbEnvironmentSensor))]
        public async Task<IHttpActionResult> PosttbEnvironmentSensor(tbEnvironmentSensor tbEnvironmentSensor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbEnvironmentSensor.Add(tbEnvironmentSensor);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (tbEnvironmentSensorExists(tbEnvironmentSensor.Date))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tbEnvironmentSensor.Date }, tbEnvironmentSensor);
        }

        // DELETE: api/EnvironmentSensor/5
        [ResponseType(typeof(tbEnvironmentSensor))]
        public async Task<IHttpActionResult> DeletetbEnvironmentSensor(DateTime id)
        {
            tbEnvironmentSensor tbEnvironmentSensor = await db.tbEnvironmentSensor.FindAsync(id);
            if (tbEnvironmentSensor == null)
            {
                return NotFound();
            }

            db.tbEnvironmentSensor.Remove(tbEnvironmentSensor);
            await db.SaveChangesAsync();

            return Ok(tbEnvironmentSensor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbEnvironmentSensorExists(DateTime id)
        {
            return db.tbEnvironmentSensor.Count(e => e.Date == id) > 0;
        }
    }
}