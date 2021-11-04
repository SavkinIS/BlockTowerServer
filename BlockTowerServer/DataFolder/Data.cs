using BlockTowerServer.ContextFolder;
using BlockTowerServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockTowerServer.DataFolder
{
    public class Data
    {

        private readonly DataContextSQL context;

        public Data()
        {
            var context = new DataContextSQL();
            this.context = context;
        }

        #region User
        public ActionResult<User> AddUser(User user)
        {
            int id =  context.Users.Add(user).Entity.Id;
            context.SaveChanges();
            return user;
        }

        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return context.Users.ToList();
        }

        public ActionResult<User> GetUser(int id)
        {
            return context.Users.Find(id);
        }

        public string PutUser(User changedUser, int id)
        {
            try
            {
                context.Users.Update(changedUser);
                context.SaveChanges();
                return "Ok";
            }
            
            catch (DbUpdateConcurrencyException)
            {
                if (!context.Users.Any(e => e.Id == id))
                {
                    return "NotFound";
                }
                else
                {
                    throw;
                }
            }
            catch
            {
                return "Cant Change Clients ID";
            }
        

            /*
            //User user = context.Users.Find(id);
            //try 
            //{
            //    user.BestScore = changedUser.BestScore;
            //    context.SaveChanges();
            //    return "Ok";
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!context.Users.Any(e => e.Id == id))
            //    {
            //        return "NotFound";
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}
            //catch
            //{
            //    return "Cant Change Clients ID";
            //}
            */

        }


        public User DelClient(int id)
        {
            var client = context.Users.Find(id);
            if (client == null)
            {
                return client;
            }
            try
            {
                context.Users.Remove(client);
                context.SaveChanges();
                return client;
            }
            catch (Exception e)
            {
                //добавить LOG
                return client;
            }

        }


        public ActionResult<RatioOfPositions> GetUserBestResult(int id)
        {
            RatioOfPositions ratio = new RatioOfPositions();
            var sortUsers =  context.Users.OrderByDescending(u => u.BestScore).ToList();
            var client = context.Users.Find(id);
            float onePerc = (float)sortUsers.First().BestScore /100f;

            float percen = client.BestScore / onePerc;
            ratio.BestResult = sortUsers.Max(u => u.BestScore);
            ratio.PositionByPercent = percen;
            ratio.CurrentPossittion = sortUsers.FindIndex(cl => cl.Id == client.Id)+1;
            return ratio;

        }


        public ActionResult<RatioOfPositions> GetBestResult(int bestScore)
        {
            RatioOfPositions ratio = new RatioOfPositions();
            //var sortUsers = context.Users.OrderByDescending(u => u.BestScore).ToList();
            //var client = context.Users.Find(id);
            //List <int> users = context.Users.Select(x =>x.BestScore).Distinct().ToList();
            float best = context.Users.Max(u => u.BestScore);
            float onePerc = best / 100;

            float percen = bestScore / onePerc;
            ratio.BestResult = best;
            ratio.PositionByPercent = percen/100f;
            // ratio.CurrentPossittion = sortUsers.FindIndex(cl => cl.Id == client.Id) + 1;
            ratio.CurrentPossittion = 0;
            return ratio;

        }

        #endregion
    }
}
