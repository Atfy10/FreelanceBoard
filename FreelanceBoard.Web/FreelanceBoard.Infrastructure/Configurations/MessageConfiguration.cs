using FreelanceBoard.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace FreelanceBoard.Infrastructure.Configurations
{
	internal class MessageConfiguration : IEntityTypeConfiguration<Message>
	{
		public void Configure(EntityTypeBuilder<Message> builder)
		{
			builder.ToTable("Messages");

			builder.Property(m => m.Body)
				   .IsRequired()
				   .HasMaxLength(1000);

			builder.Property(m => m.Timestamp)
				   .IsRequired();

			builder.Property(m => m.IsRead)
				   .HasDefaultValue(false);
		}

	}
}
