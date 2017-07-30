using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PassAway.Models.Shared;

namespace PassAway.Migrations.Application
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20170730141614_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PassAway.Models.Cart+CartItem", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("OrderID");

                    b.Property<int?>("ProductID");

                    b.Property<int>("Quantity");

                    b.HasKey("ID");

                    b.HasIndex("OrderID");

                    b.HasIndex("ProductID");

                    b.ToTable("CartItem");
                });

            modelBuilder.Entity("PassAway.Models.Order", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<string>("Country")
                        .IsRequired();

                    b.Property<bool>("GiftWrap");

                    b.Property<string>("Line1")
                        .IsRequired();

                    b.Property<string>("Line2");

                    b.Property<string>("Line3");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<bool>("Shipped");

                    b.Property<string>("State")
                        .IsRequired();

                    b.Property<string>("Zip");

                    b.HasKey("ID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("PassAway.Models.Rating", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Total");

                    b.Property<double>("Voters");

                    b.HasKey("ID");

                    b.ToTable("Rating");
                });

            modelBuilder.Entity("PassAway.Models.Shared.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Launched");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<decimal>("Price");

                    b.Property<int?>("RatingID");

                    b.Property<int>("Stock");

                    b.Property<string>("URL");

                    b.HasKey("ID");

                    b.HasIndex("RatingID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("PassAway.Models.Cart+CartItem", b =>
                {
                    b.HasOne("PassAway.Models.Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderID");

                    b.HasOne("PassAway.Models.Shared.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID");
                });

            modelBuilder.Entity("PassAway.Models.Shared.Product", b =>
                {
                    b.HasOne("PassAway.Models.Rating", "Rating")
                        .WithMany()
                        .HasForeignKey("RatingID");
                });
        }
    }
}
