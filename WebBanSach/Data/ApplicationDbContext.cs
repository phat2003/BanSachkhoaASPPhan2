﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebBanSach.Models;

namespace WebBanSach.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
    }
}
