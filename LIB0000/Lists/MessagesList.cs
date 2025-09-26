
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
                Nr = "002",
                Group = "Machine",
                MessageText = "Timeout Periodieke controle Workstation",
                Help = "",
                Type = MessageType.Error
            });
            lMessages.Add(new MessageModel
            {
                Nr = "003",
                Group = "Machine",
                MessageText = "Timeout Periodieke controle Productgroup",
                Help = "",
                Type = MessageType.Error
            });
            lMessages.Add(new MessageModel
            {
                Nr = "004",
                Group = "Machine",
                MessageText = "Timeout Periodieke controle Product",
                Help = "",
                Type = MessageType.Error
            });
            lMessages.Add(new MessageModel
            {
                Nr = "005",
                Group = "Machine",
                MessageText = "Camera foutdetectie",
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

            return lMessages;
        }
    }

}