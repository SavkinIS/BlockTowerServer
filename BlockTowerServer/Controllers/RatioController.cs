using BlockTowerServer.DataFolder;
using BlockTowerServer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlockTowerServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatioController : ControllerBase
    {
        Data db;

        public RatioController()
        {
            this.db = new Data(); ;
        }

        [HttpGet("{id}")]
        public ActionResult<RatioOfPositions> Get(int id)
        {
            return db.GetBestResult(id);
        }
    }
}
