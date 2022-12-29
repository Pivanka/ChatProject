using DAL.Context;
using DAL.Repositories;
using DAL.Repositories.Contracts;
using DAL.UoW.Contract;

namespace DAL.UoW
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ChatDbContext _context;

        private IMessageRepository _messageRepository;
        private IGroupRepository _groupRepository;
        private IFriendshipRepository _friendshipRepository;

        public UnitOfWork(ChatDbContext context)
        {
            _context = context;
            _messageRepository = new MessageRepository(context);
            _groupRepository = new GroupRepository(context);
            _friendshipRepository = new FriendshipRepository(context);
        }

        public IMessageRepository MessageRepository
        {
            get => _messageRepository;
        }

        public IGroupRepository GroupRepository
        {
            get => _groupRepository;
        }

        public IFriendshipRepository FriendshipRepository
        {
            get => _friendshipRepository;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
