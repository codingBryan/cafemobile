using CafeMobile_api.Models.Entities;

namespace CafeMobile_api.Context
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> opts):base(opts){ }
        public DbSet<Student> students { get; set; }
        public DbSet<Parent> parents { get; set; }
        public DbSet<Coupon> coupons { get; set; }
        public DbSet<Meal> meals { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Coupon_meal> coupon_meals { get; set; }
        public DbSet<Redemption> redemptions { get; set; }
        public DbSet<Transaction> transactions { get; set; }
        public DbSet<Sale> sales { get; set; }
        public DbSet<Notification> notifications { get; set; }
        public DbSet<Redemption_meal> redemption_meals { get; set; }
        public DbSet<Parents_Students> parents_students { get; set; }
        public DbSet<StudentCoupon> studentCoupons { get; set; }
        public DbSet<Staff> staff { get; set; }
        public DbSet<ItemSales> item_sales { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            builder.Entity<StudentCoupon>().HasOne(c => c.Student).WithMany(s => s.coupons);
            builder.Entity<StudentCoupon>().HasOne(s => s.Coupon).WithMany(c => c.purchasedCoupons);

            builder.Entity<Transaction>().HasOne(t => t.Student).WithMany(s => s.transactions);
            builder.Entity<Transaction>().HasOne(t => t.Parent).WithMany(p => p.transactions);
            builder.Entity<Transaction>().HasOne(t => t.Student).WithMany(s => s.transactions);

            builder.Entity<ItemSales>().HasKey(k => new { k.ItemName, k.sold_on });
            builder.Entity<Sale>().HasOne(s=>s.Student).WithMany(s => s.sales);
            builder.Entity<Redemption>().HasOne(r => r.Student).WithMany(s => s.redemptions);
            builder.Entity<Sale>().HasOne(s=>s.Redemption).WithMany(r => r.sales);

            builder.Entity<Parents_Students>().HasKey(k => new { k.parentId, k.studentId });
            builder.Entity<Parents_Students>().HasOne(ps=>ps.Student).WithMany(s => s.parents);
            builder.Entity<Parents_Students>().HasOne(ps => ps.Parent).WithMany(p => p.students);

            builder.Entity<Coupon_meal>().HasKey(k => new { k.CouponId, k.MealId });
            builder.Entity<Coupon_meal>().HasOne(cm => cm.Coupon).WithMany(c => c.couponMeals).HasForeignKey(cm=>cm.CouponId);
            builder.Entity<Coupon_meal>().HasOne(cm => cm.Meal).WithMany(m => m.coupons).HasForeignKey(cm => cm.MealId);

            builder.Entity<Redemption_meal>().HasKey(k => new { k.RedemptionId, k.MealId });
            builder.Entity<Redemption_meal>().HasOne(rm => rm.Redemption).WithMany(r => r.meals).HasForeignKey(cm => cm.RedemptionId);
            builder.Entity<Redemption_meal>().HasOne(rm => rm.Meal).WithMany(m => m.redemptions).HasForeignKey(cm => cm.MealId);



        }
    }
}
