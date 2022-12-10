using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingAppAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatingAppAPI.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
            {

            }
        public DbSet<AppUser>Users{get;set;}
            
    }
}
