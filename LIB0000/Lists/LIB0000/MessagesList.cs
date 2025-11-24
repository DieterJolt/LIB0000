
namespace LIB0000
{
    public static class MessagesList
    {
        public static List<MessageModel> GetMessages()
        {
            List<MessageModel> lMessages = new List<MessageModel>();
            lMessages.Add(new MessageModel
            {
                Nr = "001",
                Group = "Database",
                MessageText = "Geen verbinding met database",
                Help = "",
                Type = MessageType.Error
            });
            lMessages.Add(new MessageModel
            {
                Nr = "001",
                Group = "Machine",
                MessageText = "Geen machinenummer ingevuld, of machinenummer niet gevonden in database",
                Help = "",
                Type = MessageType.Error
            });
            lMessages.Add(new MessageModel
            {
                Nr = "001",
                Group = "Login",
                MessageText = "No connection with domain",
                Help = "Check connection",
                Type = MessageType.Error
            });
            lMessages.Add(new MessageModel
            {
                Nr = "002",
                Group = "Login",
                MessageText = "Username or password are wrong",
                Help = "Check connection",
                Type = MessageType.Error
            });
            lMessages.Add(new MessageModel
            {
                Nr = "003",
                Group = "Login",
                MessageText = "Securitygroup not found",
                Help = "Check connection",
                Type = MessageType.Error
            });

            lMessages.Add(new MessageModel
            {
                Nr = "001",
                Group = "PLC",
                MessageText = "PLC Error 001",
                Help = "",
                Type = MessageType.Error
            });
            lMessages.Add(new MessageModel
            {
                Nr = "002",
                Group = "PLC",
                MessageText = "PLC Error 002",
                Help = "",
                Type = MessageType.Error
            });
            lMessages.Add(new MessageModel
            {
                Nr = "003",
                Group = "PLC",
                MessageText = "PLC Error 003",
                Help = "",
                Type = MessageType.Error
            });
            lMessages.Add(new MessageModel
            {
                Nr = "004",
                Group = "PLC",
                MessageText = "PLC Error 004",
                Help = "",
                Type = MessageType.Error
            });
            lMessages.Add(new MessageModel
            {
                Nr = "005",
                Group = "PLC",
                MessageText = "PLC Error 005",
                Help = "",
                Type = MessageType.Error
            });
            lMessages.Add(new MessageModel
            {
                Nr = "001",
                Group = "Turck",
                MessageText = "Lost Connection with Turck module",
                Help = "Check connection",
                Type = MessageType.Error
            });


            return lMessages;
        }
    }

}