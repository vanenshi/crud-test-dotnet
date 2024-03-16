using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(t => t.LastName).IsRequired().HasMaxLength(100);

        // Email max sized {64}@{255} = 320
        builder.Property(t => t.Email).IsRequired().HasMaxLength(321);
        builder.Property(t => t.BankAccountNumber);
        builder.Property(t => t.DateOfBirth).IsRequired();

        builder.Property(t => t.PhoneNumber).IsRequired().HasColumnType("bigint");
    }
}
