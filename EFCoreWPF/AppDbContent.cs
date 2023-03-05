using EFCoreWPF.Entitys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EFCoreWPF
{
    public class AppDbContext : DbContext
    {
        private const string ConnectionString = "Data Source=hello.db";

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(ConnectionString);
        }

        public DbSet<Student> Students => Set<Student>();
        public DbSet<Visit> Visits=> Set<Visit>();
        public DbSet<Group> Groups=> Set<Group>();
    }
}



//C:\Users\kuzne\source\repos\EFCoreWPF\EFCoreWPF