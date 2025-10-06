namespace LIB0000
{


    public static class UsersList
    {

        public static List<UserModel> GetUsers()
        {
            List<UserModel> lUsers = new List<UserModel>();
            lUsers.Add(new UserModel
            {
                User = "Operator",
                Password = "Operator",
                Level = 1,
                Group = "GroepOperator",

            });
            lUsers.Add(new UserModel
            {
                User = "Steller",
                Password = "Steller",
                Level = 2,
                Group = "GroepSteller",

            });
            lUsers.Add(new UserModel
            {
                User = "Technieker",
                Password = "Technieker",
                Level = 3,
                Group = "GroepTechnieker",

            });
            lUsers.Add(new UserModel
            {
                User = "Administrator",
                Password = "Administrator",
                Level = 4,
                Group = "GroepAdmin",

            });

            return lUsers;
        }

    }


}
