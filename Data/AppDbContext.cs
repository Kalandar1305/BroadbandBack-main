using System.Security.Cryptography;
using BroadBandBillingPaymentSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BroadBandBillingPaymentSystem.Data
{
    public class AppDbContext : DbContext
    {
        private static string customerViewQuery = @"create view CustomerView as 
                                                    select c.customer_id, c.f_name, c.l_name, c.phone, c.address,c.email, a.tarrif_plan_id,a.status, p.amount, p.description 
                                                    from (Customer as  c join Account as a on a.account_id = c.account_id) left join TarrifPlan as p on p.tarrif_plan_id=a.tarrif_plan_id";
        private static string billTriggerQuery = @"create trigger PayBillTrigger on bill after update as update bill set payment_date = GETDATE(), payment_mode=inserted.payment_mode, customer_id = inserted.customer_id from inserted where Bill.bill_id=inserted.bill_id";

        private static string choosePlanSP = @"create procedure chooseplan @plan_id nvarchar(40), @account_id nvarchar(40)
as begin update account set tarrif_plan_id=@plan_id where account_id = @account_id end";

        private static string mostUsedPlanSP = @"create procedure 
mostusedplan as 
begin
select count(*), t.tarrif_plan_id, t.amount, t.DESCRIPTION, t.admin_id
 from TarrifPlan t, Account a
where t.tarrif_plan_id = a.tarrif_plan_id AND
a.tarrif_plan_id is not NULL
group by t.tarrif_plan_id, t.amount, t.DESCRIPTION, t.admin_id

having count(a.account_id) >= all(
    select count(a1.account_id)
 from TarrifPlan t1, Account a1
where t1.tarrif_plan_id = a1.tarrif_plan_id AND
a1.tarrif_plan_id is not NULL
group by t1.tarrif_plan_id
)
end";

private static string accountStatus = @"create trigger accountStatus on Account instead of update as update Account set Account.status = 'active', Account.tarrif_plan_id=inserted.tarrif_plan_id from inserted where Account.account_id = inserted.account_id and inserted.tarrif_plan_id is not null";

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Bill> Bill { get; set; }
        public DbSet<Feedback> Feedback { get; set; }
        public DbSet<TarrifPlan> TarrifPlan { get; set; }
        public DbSet<CustomerView> CustomerView { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasOne(a => a.Customer)
                .WithOne(b => b.Account)
                .HasForeignKey<Customer>(a => a.account_id);

            modelBuilder.Entity<TarrifPlan>()
                .HasMany(a => a.Account)
                .WithOne(b => b.TarrifPlan)
                .HasForeignKey(b => b.tarrif_plan_id);

            modelBuilder.Entity<Customer>()
               .HasMany(a => a.Feedback)
               .WithOne(b => b.Customer)
               .HasForeignKey(b => b.customer_id);

            modelBuilder.Entity<Customer>()
               .HasMany(a => a.Bill)
               .WithOne(b => b.Customer)
               .HasForeignKey(b => b.customer_id);

            modelBuilder.Entity<Account>()
               .HasMany(a => a.Bill)
               .WithOne(b => b.Account)
               .HasForeignKey(b => b.account_id);

            modelBuilder.Entity<Admin>()
               .HasMany(a => a.TarrifPlan)
               .WithOne(b => b.Admin)
               .HasForeignKey(b => b.admin_id);

            modelBuilder.Entity<Admin>()
                .HasMany(a => a.Feedback)
                .WithOne(b => b.Admin)
                .HasForeignKey(b => b.admin_id);

            modelBuilder.Entity<CustomerView>().ToView("CustomerView").HasKey(n => n.customer_id);
        }

        public static void SeedAdmin(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>()!;
                context.Database.EnsureCreated();
                if (!context.Admin.Any())
                {
                    context.Admin.Add(new Admin()
                    {
                        email = "admin@broadband.com",
                        password = BCrypt.Net.BCrypt.HashPassword("Admin@123", BCrypt.Net.BCrypt.GenerateSalt(12))
                    });
                    context.SaveChanges();
                }
            }
        }
        public static void CreateCustomElements(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(customerViewQuery);
            migrationBuilder.Sql(billTriggerQuery);
            migrationBuilder.Sql(choosePlanSP);
            migrationBuilder.Sql(mostUsedPlanSP);
            migrationBuilder.Sql(accountStatus);
        }

        public static void DropCustomElements(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("drop view CustomerView");
            migrationBuilder.Sql("drop trigger PayBillTrigger");
            migrationBuilder.Sql("drop procedure chooseplan");
            migrationBuilder.Sql("drop procedure mostusedplan");
            migrationBuilder.Sql("drop procedure accountStatus");
        }
    }
}
