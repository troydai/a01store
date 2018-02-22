using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pisces.Store.Models;

namespace Pisces.Store.Controllers
{
    public class MainController : Controller
    {
        [HttpGet, Route("api/healthy")]
        public PiscesStatus Healthy() => new PiscesStatus();

        [HttpGet, Route("api/runs")]
        public IEnumerable<PiscesRun> GetRuns()
        {
            return new PiscesRun[] { new PiscesRun() };
        }

        [HttpGet, Route("api/run/{id}")]
        public PiscesRun GetRun(int id)
        {
            return new PiscesRun();
        }

        [HttpPost, Route("api/run")]
        public void CreateRun([FromBody]PiscesRun value)
        {
        }

        [HttpDelete, Route("api/run/{id}")]
        public void DeleteRun(int id)
        {
        }

        [HttpGet, Route("api/task/{id}")]
        public PiscesTask GetTask(int id)
        {
            return new PiscesTask();
        }

        [HttpGet, Route("api/run/{runId}/tasks")]
        public IEnumerable<PiscesTask> GetTasks(int runId)
        {
            return new PiscesTask[] { new PiscesTask() };
        }

        [HttpPost, Route("api/run/{runId}/tasks")]
        public void CreateTask(int runId, [FromBody]IEnumerable<PiscesTask> tasks)
        {

        }

        [HttpPatch, Route("api/task/{id}")]
        public void PatchTask([FromBody]PiscesTask value)
        {
            
        }

        [HttpPost, Route("api/run/{runId}/checkout")]
        public PiscesTask CheckoutTask(int runId)
        {
            return new PiscesTask();
        }
    }
}
