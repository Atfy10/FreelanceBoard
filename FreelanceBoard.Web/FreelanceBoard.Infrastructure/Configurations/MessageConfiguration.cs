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

			builder.HasKey(m => m.Id);

			builder.Property(m => m.Body)
				   .IsRequired()
				   .HasMaxLength(1000);

			builder.Property(m => m.Timestamp)
				   .IsRequired();


			builder.HasOne(m => m.Sender)
				   .WithMany(u => u.SentMessages)
				   .HasForeignKey(m => m.SenderId)
				   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.Receiver)
				   .WithMany(u => u.ReceivedMessages)
				   .HasForeignKey(m => m.ReceiverId)
				   .OnDelete(DeleteBehavior.Restrict);
        }

	}
}
