using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTask.Core.Domain.Calculations.Models;

namespace TestTask.Persistence.CalculationsDb.EntityTypeConfigurations;

public class CalculationsEntityTypeConfiguration : IEntityTypeConfiguration<Calculation>
{
    public void Configure(EntityTypeBuilder<Calculation> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Ignore(x => x.DomainEvents);

        builder
            .Property(x => x.AnnualTaxPaid)
            .IsRequired();

        builder
            .Property(x => x.GrossAnnualSalary)
            .IsRequired();

        builder
            .Property(x => x.NetAnnualSalary)
            .IsRequired();

        builder
            .Property(x => x.GrossMonthlySalary)
            .IsRequired();

        builder
            .Property(x => x.MonthlyTaxPaid)
            .IsRequired();

        builder
            .Property(x => x.NetMonthlySalary)
            .IsRequired();
    }
}
