using DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UoW.Contract
{
    public interface IUnitOfWork
    {
        public IMessageRepository MessageRepository { get; }

        public IGroupRepository GroupRepository { get; }

        public IFriendshipRepository FriendshipRepository { get; }
    }
}
