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
using PTT_SmartCity_Web_API.Interfaces;
using PTT_SmartCity_Web_API.Models;
using PTT_SmartCity_Web_API.Services;

namespace PTT_SmartCity_Web_API.Controllers
{
    public class SensorHubController : ApiController
    {

        private readonly ISensorHubService sensorHubService;

        public SensorHubController()
        {
            this.sensorHubService = new SensorHubService();
        }

        // GET: api/sensorhub
        [Route("api/sensorhub")]
        public GetSensorHubDataModel GettbSensorHub()
        {
            return this.sensorHubService.getSensorHubItems();
        }

        // GET: api/sensorhub/all
        //[Route("api/sensorhub/all")]
        //public GetSensorHubDataModel GettbSensorHubAll()
        //{
        //    return this.sensorHubService.getSensorHubItemsAll();
        //}

        // GET: api/sensorhub
        [Route("api/sensorhub/filter")]
        public GetSensorHubDataModel GettbSensorHubFilter([FromUri]SensorHubDataFilterOptions filters)
        {
            if (ModelState.IsValid)
            {
                return this.sensorHubService.getSensorHubItemsFilter(filters);
            }
            throw new HttpResponseException(Request.CreateResponse(
                HttpStatusCode.BadRequest,
                new { Message = ModelState.GetErrorModelState() }
            ));
        }

        //public IEnumerable<tbSensorHub> GettbSensorHub()
        //{
        //    return this.sensorHubService.sensorHubItems;
        //}

        //// GET: api/SensorHub/5
        //[ResponseType(typeof(tbSensorHub))]
        //public IHttpActionResult GettbSensorHub(int id)
        //{
        //    var tbSensorHub = this.db.tbSensorHub.OrderByDescending(x => x.Date).ThenByDescending(x => x.Time).Take(id);
        //    if (tbSensorHub == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(tbSensorHub);
        //}

        //[ResponseType(typeof(tbSensorHub))]
        //public IHttpActionResult GetSensorHub(int id)
        //{
        //    var tbSensorHub = this.db.tbSensorHub.OrderByDescending(x => x.Date).ThenByDescending(x => x.Time).Take(id);
        //    if (tbSensorHub == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(tbSensorHub);
        //}

        //// PUT: api/SensorHub/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PuttbSensorHub(DateTime id, tbSensorHub tbSensorHub)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != tbSensorHub.Date)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(tbSensorHub).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!tbSensorHubExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/SensorHub
        //[ResponseType(typeof(tbSensorHub))]
        //public async Task<IHttpActionResult> PosttbSensorHub(tbSensorHub tbSensorHub)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.tbSensorHub.Add(tbSensorHub);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (tbSensorHubExists(tbSensorHub.Date))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtRoute("DefaultApi", new { id = tbSensorHub.Date }, tbSensorHub);
        //}

        //// DELETE: api/SensorHub/5
        //[ResponseType(typeof(tbSensorHub))]
        //public async Task<IHttpActionResult> DeletetbSensorHub(DateTime id)
        //{
        //    tbSensorHub tbSensorHub = await db.tbSensorHub.FindAsync(id);
        //    if (tbSensorHub == null)
        //    {
        //        return NotFound();
        //    }

        //    db.tbSensorHub.Remove(tbSensorHub);
        //    await db.SaveChangesAsync();

        //    return Ok(tbSensorHub);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool tbSensorHubExists(DateTime id)
        //{
        //    return db.tbSensorHub.Count(e => e.Date == id) > 0;
        //}
    }
}