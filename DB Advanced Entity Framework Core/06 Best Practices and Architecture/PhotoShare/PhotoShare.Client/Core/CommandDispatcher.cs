namespace PhotoShare.Client.Core
{
    using System;
    using Commands;

    public class CommandDispatcher
    {
        public string DispatchCommand(string[] commandParameters)
        {
            string commandToExecute = commandParameters[0].ToLower();
            string result = null;

            switch (commandToExecute)
            {
                case "registeruser":
                    result = RegisterUserCommand.Execute(commandParameters);
                    break;
                case "addtown":
                    result = AddTownCommand.Execute(commandParameters);
                    break;
                case "modifyuser":
                    result = ModifyUserCommand.Execute(commandParameters);
                    break;
                case "deleteuser":
                    result = DeleteUser.Execute(commandParameters);
                    break;
                case "addtag":
                    result = AddTagCommand.Execute(commandParameters);
                    break;
                case "addfriend":
                    result = AddFriendCommand.Execute(commandParameters);
                    break;
                case "createalbum":
                    result = CreateAlbumCommand.Execute(commandParameters);
                    break;
                case "exit":
                    break;
                default:
                    throw new InvalidOperationException($"Command {commandToExecute} not valid!");
            }

            return result;
        }
    }
}
