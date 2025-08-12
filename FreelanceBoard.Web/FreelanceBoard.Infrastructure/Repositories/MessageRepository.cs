using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Interfaces;
using FreelanceBoard.Infrastructure.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Infrastructure.Repositories
{
    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        public MessageRepository(AppDbContext dbContext) : base(dbContext)
        { }
        public async Task DeleteMessageId(string messageId)
        {
            var message = await _dbContext.Messages.FindAsync(messageId) ??
                throw new KeyNotFoundException($"Message with ID {messageId} not found.");

            if (message.SenderId is null && message.ReceiverId is null)
            {
                _dbContext.Messages.Remove(message);
                await _dbContext.SaveChangesAsync();
            }
        }

    }
}
