namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;

    using Microsoft.EntityFrameworkCore;

    public class AddFriendCommand
    {
        // AddFriend <username1> <username2>
        public static string Execute(string[] data)
        {
            string inviterUsername = data[1];
            string invitedUsername = data[2];

            using (var context = new PhotoShareContext())
            {
                var inviter = context.Users.Include(u => u.FriendsAdded).ThenInclude(fa => fa.Friend).FirstOrDefault(u => u.Username == inviterUsername);

                if (inviter == null)
                {
                    throw new ArgumentException($"{inviterUsername} not found!");
                }

                var invited = context.Users.Include(u => u.FriendsAdded).ThenInclude(fa => fa.Friend).FirstOrDefault(u => u.Username == invitedUsername);

                if (invited == null)
                {
                    throw new ArgumentException($"{invitedUsername} not found!");
                }

                var isAlreadyAdded = inviter.FriendsAdded.Any(u => u.Friend == invited);

                if (isAlreadyAdded)
                {
                    throw new InvalidOperationException($"{invitedUsername} is already a friend to {inviterUsername}");
                }

                inviter.FriendsAdded.Add(new Friendship()
                {
                    User = inviter,
                    Friend = invited
                });

                context.SaveChanges();

                return $"Friend {invitedUsername} added to {inviterUsername}";
            }
        }
    }
}
