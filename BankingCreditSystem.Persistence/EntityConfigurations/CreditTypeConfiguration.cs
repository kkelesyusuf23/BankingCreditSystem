using BankingCreditSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingCreditSystem.Persistence.EntityConfigurations
{
    public class CreditTypeConfiguration : IEntityTypeConfiguration<CreditType>
    {
        public void Configure(EntityTypeBuilder<CreditType> builder)
        {
            builder.ToTable("CreditTypes");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Description)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(c => c.CustomerType)
                .IsRequired();

            builder.Property(c => c.MinAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(c => c.MaxAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(c => c.MinTerm)
                .IsRequired();

            builder.Property(c => c.MaxTerm)
                .IsRequired();

            builder.Property(c => c.BaseInterestRate)
                .HasColumnType("decimal(5,2)")
                .IsRequired();

            // Self-referencing relationship for hierarchical structure
            builder.HasOne(c => c.ParentCreditType)
                .WithMany(c => c.SubCreditTypes)
                .HasForeignKey(c => c.ParentCreditTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}