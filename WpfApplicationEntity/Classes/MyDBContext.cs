using System.Data.Entity;

namespace WFAEntity.API
{
    public class MyDBContext : DbContext
    {
        public MyDBContext()
            : base("DbConnectStringLocalDB")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employees>()
                .HasMany(c => c.Other_services)
                .WithRequired(o => o.Employees)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Employees>()
                .HasMany(c => c.MK_schedule)
                .WithRequired(o => o.Employees)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Employees>()
                .HasMany(c => c.Skates_hire)
                .WithRequired(o => o.Employees)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Other_services>()
                .HasMany(c => c.Ticket)
                .WithRequired(o => o.Other_services)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Client>()
               .HasMany(c => c.Ticket)
               .WithRequired(o => o.Client)
               .WillCascadeOnDelete(false);
               
            
        }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<MK_schedule> MK_schedule { get; set; }
        public DbSet<Other_services> Other_services { get; set; }
        public DbSet<Skates_hire> Skates_hire { get; set; }
        public static MyDBContext DBContext = new WFAEntity.API.MyDBContext();
    }
}
