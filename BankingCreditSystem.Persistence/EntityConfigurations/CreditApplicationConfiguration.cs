using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BankingCreditSystem.Domain.Entities;

namespace BankingCreditSystem.Persistence.EntityConfigurations
{
    public class CreditApplicationConfiguration : IEntityTypeConfiguration<CreditApplication>
    {
        public void Configure(EntityTypeBuilder<CreditApplication> builder)
        {
            builder.ToTable("CreditApplications");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.RequestedAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(c => c.RequestedTerm)
                .IsRequired();

            builder.Property(c => c.ApprovedAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(c => c.ApprovedTerm)
                .IsRequired();

            builder.Property(c => c.InterestRate)
                .HasColumnType("decimal(5,2)")
                .IsRequired();

            builder.Property(c => c.MonthlyPayment)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(c => c.TotalPayment)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(c => c.Status)
                .IsRequired();

            builder.Property(c => c.RejectionReason)
                .HasMaxLength(500);

            // Relationships
            builder.HasOne(c => c.CreditType)
                .WithMany(c => c.CreditApplications)
                .HasForeignKey(c => c.CreditTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Customer)
                .WithMany()
                .HasForeignKey(c => c.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}