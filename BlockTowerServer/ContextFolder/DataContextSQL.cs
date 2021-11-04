using BlockTowerServer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockTowerServer.ContextFolder
{
    public class DataContextSQL : DbContext
    {
        public DbSet<User> Users { get; set; }



        public DataContextSQL(){}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ///Изменить строку подключения
            optionsBuilder.UseSqlServer(
                @"Server = wpl42.hosting.reg.ru;Database=u1449750_mathRoad;User ID=u1449750_savkin89;Password=Stooky1989!"
                );
        }

       
    }
}
