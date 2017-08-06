using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PassAway.Models.Shared;

namespace PassAway.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<double>("Amount");

                    b.Property<string>("Email");

                    b.Property<int>("ProductID");

                    b.HasKey("ID");

                    b.HasIndex("ProductID");

                    b.ToTable("Rating");
                });

            modelBuilder.Entity("PassAway.Models.Shared.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<decimal>("Price");

                    b.Property<DateTime>("Produced");

                    b.Property<bool>("Promotion");

                    b.Property<string>("Region");

                    b.Property<int>("Stock");

                    b.Property<string>("Type");

                    b.Property<string>("URL");

                    b.HasKey("ID");

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

            modelBuilder.Entity("PassAway.Models.Rating", b =>
                {
                    b.HasOne("PassAway.Models.Shared.Product")
                        .WithMany("Ratings")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
