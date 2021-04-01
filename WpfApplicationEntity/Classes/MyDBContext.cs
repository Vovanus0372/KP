using System.Data.Entity;

namespace WFAEntity.API
{
    class MyDBContext : DbContext
    {
        public MyDBContext()
            : base("DbConnectStringLocalDB")
        {
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
