using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configuration
{
    public class ToDoConfiguration : IEntityTypeConfiguration<ToDo>
    {
        public void Configure(EntityTypeBuilder<ToDo> builder)
        {
            builder.ToTable("todos");

            builder.HasKey(todo => todo.Id);

            builder
                .Property(todo => todo.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(todo => todo.Description);

            builder
                .Property(todo => todo.CreatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            builder.Property(todo => todo.FinishedAt);

            builder
                .Property(todo => todo.Status)
                .HasConversion<short>()
                .IsRequired();
        }
    }
}
