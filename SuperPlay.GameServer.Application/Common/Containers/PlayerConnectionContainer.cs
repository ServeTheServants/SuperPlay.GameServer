using System;
using SuperPlay.GameServer.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace SuperPlay.GameServer.Application.Common.Containers
{
	public class PlayerConnectionContainer : IPlayerConnectionContainer
	{
        private readonly LinkedList<IPlayerConnection> _playerConnections = new();

        private readonly ReaderWriterLockSlim _readerWriterLockSlim = new();

        public IPlayerConnection Get(int playerId)
        {
            _readerWriterLockSlim.EnterReadLock();

            try
            {
                return _playerConnections.FirstOrDefault(playerConnection => playerConnection.PlayerId == playerId);
            }
            finally
            {
                _readerWriterLockSlim.ExitReadLock();
            }
        }

        public IPlayerConnection Get(Guid connectionId)
        {
            _readerWriterLockSlim.EnterReadLock();

            try
            {
                return _playerConnections.FirstOrDefault(playerConnection => playerConnection.ConnectionId == connectionId);
            }
            finally
            {
                _readerWriterLockSlim.ExitReadLock();
            }
        }

        public void Add(IPlayerConnection playerConnection)
        {
            _readerWriterLockSlim.EnterWriteLock();

            try
            {
                _playerConnections.AddFirst(playerConnection);
            }
            finally
            {
                _readerWriterLockSlim.ExitWriteLock();
            }
        }

        public bool Remove(IPlayerConnection playerConnection)
        {
            _readerWriterLockSlim.EnterWriteLock();

            try
            {
                return _playerConnections.Remove(playerConnection);
            }
            finally
            {
                _readerWriterLockSlim.ExitWriteLock();
            }
        }
    }
}