using Domashna1.Domain.DomainModels;
using Domashna1.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domashna1.Repository
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<TicketCart> TicketCarts { get; set; }
        public virtual DbSet<TicketInOrder> TicketsInOrders { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<TicketInTicketCart> TicketsInTicketCarts { get; set; }
        public virtual DbSet<EmailMessage> EmailMessages { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<Ticket>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<TicketCart>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

           

            builder.Entity<TicketInTicketCart>()
                .HasOne(z => z.Ticket)
                .WithMany(z => z.TicketsInTicketCart)
                .HasForeignKey(z => z.CartId);

            builder.Entity<TicketInTicketCart>()
                .HasOne(z => z.TicketCart)
                .WithMany(z => z.TicketsInTicketCart)
                .HasForeignKey(z => z.TicketId);


            builder.Entity<TicketCart>()
                .HasOne<ApplicationUser>(z => z.AppUser)
                .WithOne(z => z.TicketCart)
                .HasForeignKey<TicketCart>(z => z.ApplicationUserId);
            

            builder.Entity<TicketInOrder>()
                .HasOne(z => z.Ticket)
                .WithMany(z => z.TicketInOrders)
                .HasForeignKey(z => z.OrderId);

            builder.Entity<TicketInOrder>()
                .HasOne(z => z.Order)
                .WithMany(z => z.TicketsInOrder)
                .HasForeignKey(z => z.TicketId);
        }
    }
}
