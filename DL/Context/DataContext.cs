using DL.Models;
using Microsoft.EntityFrameworkCore;

namespace DL.Context
{
    public class DataContext : DbContext
    {
        public DataContext() { }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<QuizTeamTussentabel>(q => { q.HasNoKey(); });
            //modelBuilder.Entity<QuizRondeTussentabel>(q => { q.HasNoKey(); });
            //modelBuilder.Entity<RondeVraagTussentabel>(q => { q.HasNoKey(); });


            base.OnModelCreating(modelBuilder);
        }
        public DbSet<IngevoerdAntwoord> IngevoerdAntwoorden { get; set; }
        public DbSet<Quiz> Quizen { get; set; }
        public DbSet<QuizTeamTussentabel> QuizTeamTussentabellen { get; set; }
        public DbSet<QuizRondeTussentabel> QuizROndeTussentabellen { get; set; }
        public DbSet<Ronde> Ronden { get; set; }
        public DbSet<RondeVraagTussentabel> RondeVraagTussentabellen { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Vraag> Vragen { get; set; }
        public DbSet<TypeVraag> TypeVragen { get; set; }
        public DbSet<StripeAccount> StripeAccounts { get; set; }

    }
}
