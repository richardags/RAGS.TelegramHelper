using System;
using System.Collections.Generic;

namespace TelegramHelper
{
    internal class TelegramManager
    {
        private readonly List<TelegramMember> clients = new();

        internal void AddMember(TelegramMember member)
        {
            clients.Add(member);
        }
        internal bool? TryRemoveMember(long telegramId)
        {
            TelegramMember member = clients.Find(e => e.user.from.id == telegramId);

            if (member != null)
            {
                return clients.Remove(member);
            }
            else
            {
                Console.WriteLine("TelegramManager.cs TryRemoveMember() - client not found.");
                return null;
            }
        }
        internal TelegramMember FindMember(long telegramId)
        {
            return clients.Find(e => e.user.from.id == telegramId);
        }
        internal bool CheckIfExists(long telegramId)
        {
            return clients.Exists(e => e.user.from.id == telegramId);
        }
    }
}