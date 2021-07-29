using Microsoft.EntityFrameworkCore;
using rest_api.Models;

public class DataContext : DbContext {
    public DataContext(DbContextOptions<DataContext> options) : base(options){

    }

    public DbSet<User> Users {get; set;}
}

