using System;
using DAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EFLibCore
{
    //using this context class we can connect to db
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext() {}
        protected readonly IConfiguration configuration;
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server = localhost,1433\\Catalog=BookStoreDB; Database = BookStoreDB; User=sa; Password=1qaz2wsxSena.; TrustServerCertificate=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //transform models to the tables
            modelBuilder.Entity<Book>().ToTable("Book");
            modelBuilder.Entity<Author>().ToTable("Author");
            modelBuilder.Entity<Log>().ToTable("Log");
        }

        public DbSet<Book> Book { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Log> Log { get; set; }
    }
}