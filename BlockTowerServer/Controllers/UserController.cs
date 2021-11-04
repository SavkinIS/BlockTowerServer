using BlockTowerServer.ContextFolder;
using BlockTowerServer.DataBase;
using BlockTowerServer.DataFolder;
using BlockTowerServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockTowerServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        Data db;

        public UserController()
        {
            this.db = new Data();
        }


        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return db.GetUsers();
        }


        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            return db.GetUser(id);
        }





        /// <summary>
        /// Добавление предмета в БД
        /// </summary>
        /// <param name="sn"></param>
        /// <returns></returns>
        [HttpPost("{bestScore}")]
        public ActionResult<User> Post(int bestScore)
        {
            User user = new User { BestScore = bestScore };
            if (ModelState.IsValid)
            {
                db.AddUser(user);
                return Ok(user);
            }
            return BadRequest(ModelState);
        }


        /// <summary>
        /// Изменеие предмета
        /// </summary>
        /// <param name="sn"></param>
        /// <returns></returns>
        // POST: Home/Put
        [Route("{id:int}/{bestScore}")]
        [HttpPut]
        public ActionResult Put(int id, int bestScore)
        {

            User user = new User {  Id = id, BestScore = bestScore, };
            if (ModelState.IsValid)
            {
                db.PutUser(user, user.Id);
                return Ok(user);
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult<User> Delete(User user)
        {
           return db.DelClient(user.Id);
        }

    }
}
