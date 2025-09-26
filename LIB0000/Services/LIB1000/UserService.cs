using System.Collections.ObjectModel;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;

namespace LIB0000
{
    public partial class UserService : ObservableObject
    {

        #region Commands
        #endregion

        #region Constructor

        public UserService(string databasePath)
        {
            DatabasePath = databasePath;

            Login = new Login(databasePath);
            User = new User(databasePath);
            History = new History(databasePath);
            Pages = new Pages(databasePath);



        }

        #endregion

        #region Events
        #endregion

        #region Fields
        #endregion

        #region Methods

        public void LoginWindowsServerAndRegister()
        {
            UserModel UserFound = Login.AuthenticateAndGetRole(Login.InputUser.User, Login.InputUser.Password, User.List);
            if (UserFound != null)
            {
                Login.LoginUser(UserFound);
            }
            History.AddLogin(Login.ActualUser);
        }

        public void LoginAndRegister()
        {
            Login.LoginUser();
            History.AddLogin(Login.ActualUser);
        }
        public void LogoutAndRegister()
        {
            Login.LogoutUser();
            History.AddLogout(Login.ActualUser);
        }

        #endregion

        #region Properties

        [ObservableProperty]
        private Login _login;

        [ObservableProperty]
        private User _user;

        [ObservableProperty]
        private History _history;

        [ObservableProperty]
        private Pages _pages;

        [ObservableProperty]
        private List<int> _levels = new List<int> { 1, 2, 3 };

        [ObservableProperty]
        private string _databasePath;

        #endregion

    }

    public partial class Login : ObservableObject
    {

        #region Commands
        #endregion

        #region Constructor

        public Login(string databasePath)
        {
            DatabasePath = databasePath;
        }

        #endregion

        #region Events
        #endregion

        #region Fields

        #endregion

        #region Methods

        public void AutoLoginForDefaultOperator()
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                UserModel operatorUser = context.UserDbSet
                .FirstOrDefault(e => e.User == "Operator");

                if (operatorUser.User == "Operator" && operatorUser.Password == "Operator")
                {
                    ActualUser.Id = 1;
                    ActualUser.User = "Operator";
                    ActualUser.Password = "Operator";
                    ActualUser.Level = 1;
                    IsLoggedIn = true;
                }
            }
        }



        public void LoginUser(UserModel userLogin = null)
        {

            UserModel user = new UserModel();
            using (var context = new ServerDbContext(DatabasePath))
            {

                if (InputUser.User == "User" && InputUser.Password == "ORG0001")
                {
                    ActualUser.Id = 4;
                    ActualUser.User = "User";
                    ActualUser.Password = "ORG0001";
                    ActualUser.Level = 4;
                    IsLoggedIn = true;
                    DateTimeLoggedIn = DateTime.Now;
                }
                else
                {



                    // Haal user op
                    if (userLogin == null)
                    {
                        user = context.UserDbSet.FirstOrDefault(e => e.User == InputUser.User);

                        if (user.PasswordRequired == true)
                        {
                            // Controleer gebruikersnaam en wachtwoord
                            user = context.UserDbSet.FirstOrDefault(e => e.User == InputUser.User && e.Password == InputUser.Password);
                        }
                        else
                        {
                            // Controleer gebruikersnaam
                            user = user;
                        }
                    }
                    else
                    {
                        user = userLogin;
                    }

                    if (user != null)
                    {
                        ActualUser.Id = user.Id;
                        ActualUser.User = InputUser.User;
                        ActualUser.Password = InputUser.Password;
                        ActualUser.Level = user.Level;
                        ActualUser.LoginName = InputUser.User;
                        IsLoggedIn = true;
                        DateTimeLoggedIn = DateTime.Now;

                    }
                    InputUser.Password = "";
                }
            }

        }
        public void LogoutUser()
        {
            ActualUser.User = "";
            ActualUser.Password = "";
            ActualUser.Level = 0;
            InputUser.User = "";
            InputUser.Password = "";
            IsLoggedIn = false;

        }

        public UserModel AuthenticateAndGetRole(string username, string password, List<UserModel> ListUsers)
        {
            string domainName;
            try
            {
                domainName = Domain.GetComputerDomain().Name;
            }
            catch
            {
                //Geen domain gevonden of gen verbinding met domain          
                LoginError = 1;
                return null;
            }

            using (var context = new PrincipalContext(ContextType.Domain, domainName))
            {

                bool isValid = context.ValidateCredentials(username, password, ContextOptions.Negotiate);
                if (!isValid)
                {
                    //Foute inlog gegevens => wachtwoord of gebruikersnaam
                    LoginError = 2;
                    return null;
                }

                var userPrincipal = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, username);
                if (userPrincipal == null)
                {
                    //Als de gebruiker bestaat qua credentials, maar niet gevonden wordt in de AD
                    return null;
                }



                // Controleer of de gebruiker in een specifieke groep zit
                foreach (UserModel user in ListUsers)
                {
                    if (IsUserInGroup(userPrincipal, user.Group))
                    {
                        // Succes

                        LoginError = 0;
                        return user;
                    }

                }
                // Als de gebruiker nergens in de groepen zit
                LoginError = 3;
                return null;
            }

        }

        private bool IsUserInGroup(UserPrincipal user, string groupName)
        {
            PrincipalSearchResult<Principal> groups = user.GetAuthorizationGroups();

            foreach (var principal in groups)
            {
                if (principal.Name == groupName) { return true; }
            }

            return false;


        }

        #endregion

        #region Properties

        [ObservableProperty]
        private string _databasePath;

        // ActualUser = user die effectief ingelogd is
        [ObservableProperty]
        private UserModel _actualUser = new UserModel();

        // InputUser = user die wil inloggen
        [ObservableProperty]
        private UserModel _inputUser = new UserModel();

        // controle om te kijken of er een user ingelogt is of niet
        // zorgt ook voor zichtbaarheid van loggin logoutbutton en inlogbalk voor naam en paswoord
        [ObservableProperty]
        private bool _isLoggedIn;

        [ObservableProperty]
        private DateTime _dateTimeLoggedIn;

        [ObservableProperty]
        private int _loginError;

        #endregion
    }
    public partial class User : ObservableObject
    {

        #region Commands
        #endregion
        #region Constructor
        public User(string databasePath)
        {

            DatabasePath = databasePath;
            // first load the list of users
            GetList();
            // if empty fill up with standard
            if ((List == null) || (List.Count() == 0))
            {
                AddUsersToList();
            }
            // get the list again
            GetList();
        }
        #endregion
        #region Events
        #endregion
        #region Fields
        #endregion
        #region Methods

        public void GetList()
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                var result = from users in context.UserDbSet
                             orderby users.Level
                             select users;

                List = result.ToList();
                ListOnlyUsers = new List<string>(List.Select(e => e.User));
                ListUsersPlusAlles = new List<string>(List.Select(e => e.User));
                ListUsersPlusAlles.Insert(0, "Alles");
            }
        }
        public void AddRow()
        {
            using (var context = new ServerDbContext(DatabasePath))
            {

                UserModel row = context.UserDbSet.FirstOrDefault(e => e.User == Edit.User);

                if ((row == null) && (!string.IsNullOrEmpty(Edit.User)))
                {
                    row = new UserModel();
                    row.User = Edit.User;
                    row.Password = Edit.Password;
                    row.Level = Edit.Level;
                    row.PasswordRequired = Edit.PasswordRequired;
                    row.Group = Edit.Group;
                    context.UserDbSet.Add(row);
                    context.SaveChanges();
                    Edit.User = "";
                    Edit.Password = "";
                }
                else if (row != null)
                {
                    UpdateRow();
                }

            }
            GetList();
            Selected = List.OrderByDescending(e => e.Id).FirstOrDefault();

        }
        public void DeleteRow()
        {

            using (var context = new ServerDbContext(DatabasePath))
            {
                UserModel row = context.UserDbSet.FirstOrDefault(e => e.User == Selected.User);

                if (row != null)
                {
                    // Controleer hoeveel gebruikers Level 3 hebben
                    int level3Count = context.UserDbSet.Count(e => e.Level == 3);

                    // Als deze gebruiker de enige is met Level 3, annuleer de verwijdering
                    if (level3Count == 1 && row.Level == 3)
                    {
                        CustomMessageBox.Show("Er moet minstens 1 gebruiker zijn met Level 3.", "Warning", MessageBoxImage.Warning);
                        return;
                    }

                    context.UserDbSet.Remove(row);
                    Edit.User = "";
                    Edit.Password = "";

                }
                context.SaveChanges();
            }
            GetList();
        }

        public void UpdateRow()
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                if (string.IsNullOrEmpty(Edit.User)) { return; }

                UserModel row = context.UserDbSet.FirstOrDefault(e => e.Id == Edit.Id);

                if (row != null)
                {
                    // Controleer hoeveel gebruikers Level 3 hebben
                    int level3Count = context.UserDbSet.Count(e => e.Level == 3);

                    // Als er maar 1 gebruiker is met Level 3 en die probeert zijn level te verlagen, stop de update
                    if (level3Count == 1 && row.Level == 3 && Edit.Level != 3)
                    {
                        CustomMessageBox.Show("Er moet minstens 1 gebruiker zijn met Level 3.", "Warning", MessageBoxImage.Warning);
                        return;
                    }

                    row.User = Edit.User;
                    row.Password = Edit.Password;
                    row.Level = Edit.Level;
                    row.PasswordRequired = Edit.PasswordRequired;
                    row.Group = Edit.Group;
                    context.UserDbSet.Update(row);
                    context.SaveChanges();
                    Edit.User = "";
                    Edit.Password = "";
                    GetList();
                }
            }
        }
        public void UpdateSelected()
        {
            if (Selected is UserModel)
            {
                Edit.User = Selected.User;
                Edit.Password = Selected.Password;
                Edit.Level = Selected.Level;
                Edit.PasswordRequired = Selected.PasswordRequired;
                Edit.Group = Selected.Group;
                Edit.Id = Selected.Id;
            }
        }
        public void AddUsersToList()
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                context.UserDbSet.AddRange(UsersList.GetUsers());
                context.SaveChanges();
            }
        }

        public void FilterList(string filter)
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                var result = from Users in context.UserDbSet
                             where EF.Functions.Like(Users.User, $"%{filter}%")
                             orderby Users.Id
                             select Users;

                List = new List<UserModel>(result.ToList());
            }
        }

        #endregion
        #region Properties

        [ObservableProperty]
        private int _testCounter;

        [ObservableProperty]
        private string _databasePath;

        [ObservableProperty]
        private List<int> _levels = new List<int> { 1, 2, 3 };

        [ObservableProperty]
        private List<UserModel> _list = new List<UserModel>();

        [ObservableProperty]
        private UserModel _edit = new UserModel();

        [ObservableProperty]
        private UserModel _selected = new UserModel();

        // listOnlyUsers => lijst met enkel userNames
        [ObservableProperty]
        private List<string> _listOnlyUsers = new List<string>();

        ///////
        [ObservableProperty]
        private UserModel _selectedUser = new UserModel();

        private int _selectedIndexOfListUsers;
        public int SelectedIndexOfListUsers
        {
            get { return _selectedIndexOfListUsers; }
            set
            {
                _selectedIndexOfListUsers = value;

                UpdateSelected();
            }
        }

        [ObservableProperty]
        private List<string> _listUsersPlusAlles = new List<string>();

        //// editUser => in gebruikerslijst voor aanmaken en aanpassen gebruiker
        //[ObservableProperty]
        //private UserModel _editUser = new UserModel();

        #endregion


    }
    public partial class History : ObservableObject
    {
        #region Commands
        #endregion
        #region Constructor

        public History(string databasePath)
        {
            DatabasePath = databasePath;
            GetList();
            SetDropBoxLists();
        }

        #endregion
        #region Events
        #endregion
        #region Fields
        #endregion
        #region Methods

        public void GetList()
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                var result = from UserHistory in context.UserHistoryDbSet
                             join user in context.UserDbSet on UserHistory.UserId equals user.Id
                             orderby UserHistory.Id
                             select new UserHistoryJoinModel
                             {
                                 Id = UserHistory.Id,
                                 UserId = user.Id,
                                 User = user.User,
                                 Level = user.Level,
                                 Action = UserHistory.Action,
                                 DateTime = UserHistory.DateTime,
                             };

                List = result.ToList();
                List = FilterUserHistoryList(List);

            }
        }
        public void AddLogin(UserModel us)
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                var oldestRow = context.UserHistoryDbSet.OrderBy(e => e.Id).FirstOrDefault();

                if (context.UserHistoryDbSet.Count() >= 1000)
                {
                    if (oldestRow != null)
                    {
                        context.UserHistoryDbSet.Remove(oldestRow);
                        context.SaveChanges();
                    }
                }

                context.UserHistoryDbSet.Add(new UserHistoryModel { UserId = us.Id, Action = "Ingelogd", DateTime = DateTime.Now });
                context.SaveChanges();
            }
            GetList();
        }
        public void AddLogout(UserModel us)
        {
            using (var context = new ServerDbContext(DatabasePath))
            {

                var oldestRow = context.UserHistoryDbSet.OrderBy(e => e.Id).FirstOrDefault();

                if (context.UserHistoryDbSet.Count() >= 1000)
                {
                    if (oldestRow != null)
                    {
                        context.UserHistoryDbSet.Remove(oldestRow);
                        context.SaveChanges();
                    }
                }

                context.UserHistoryDbSet.Add(new UserHistoryModel { UserId = us.Id, Action = "Uitgelogd", DateTime = DateTime.Now });
                context.SaveChanges();
            }
            GetList();
        }
        private List<UserHistoryJoinModel> FilterUserHistoryList(List<UserHistoryJoinModel> list)
        {
            List<UserHistoryJoinModel> result = new List<UserHistoryJoinModel>();

            result = list;

            if (!string.IsNullOrEmpty(SelectedUserFilter) && (SelectedUserFilter != "Alles"))
            {
                result = new List<UserHistoryJoinModel>(result
                    .Where(e => e.User.Contains(SelectedUserFilter, StringComparison.OrdinalIgnoreCase)));
            }
            if (!string.IsNullOrEmpty(SelectedLevelFilter) && (SelectedLevelFilter != "Alles"))
            {
                int selectedLevel = int.Parse(SelectedLevelFilter);

                result = new List<UserHistoryJoinModel>(result
                    .Where(e => e.Level == selectedLevel));
            }
            if (!string.IsNullOrEmpty(SelectedActionFilter) && (SelectedActionFilter != "Alles"))
            {
                result = new List<UserHistoryJoinModel>(result
                    .Where(e => e.Action.Contains(SelectedActionFilter, StringComparison.OrdinalIgnoreCase)));
            }
            if (SelectedStartDateFilter != null && SelectedEndDateFilter != null)
            {
                result = new List<UserHistoryJoinModel>(result
                    .Where(e => e.DateTime >= SelectedStartDateFilter && e.DateTime <= SelectedEndDateFilter));
            }

            return result;
        }
        public void SetDropBoxLists()
        {
            ListActionPlusAlles.Clear();
            ListActionPlusAlles.Add("Alles");
            ListActionPlusAlles.Add("Ingelogd");
            ListActionPlusAlles.Add("Uitgelogd");

            ListLevelsPlusAlles.Clear();
            ListLevelsPlusAlles.Add("Alles");
            ListLevelsPlusAlles.Add("1");
            ListLevelsPlusAlles.Add("2");
            ListLevelsPlusAlles.Add("3");

        }

        #endregion
        #region Properties

        [ObservableProperty]
        private string _databasePath;

        [ObservableProperty]
        private List<UserHistoryJoinModel> _list = new List<UserHistoryJoinModel>();
        [ObservableProperty]
        private UserHistoryJoinModel _edit = new UserHistoryJoinModel();
        [ObservableProperty]
        private UserHistoryJoinModel _selected = new UserHistoryJoinModel();


        [ObservableProperty]
        private List<string> _listLevelsPlusAlles = new List<string>();

        [ObservableProperty]
        private List<string> _listActionPlusAlles = new List<string>();


        private string _selectedUserFilter;
        public string SelectedUserFilter
        {
            get { return _selectedUserFilter; }
            set
            {
                _selectedUserFilter = value;

                // methode
                GetList();
            }
        }

        private string _selectedLevelFilter;
        public string SelectedLevelFilter
        {
            get { return _selectedLevelFilter; }
            set
            {
                _selectedLevelFilter = value;


                GetList();
            }
        }

        private string _selectedActionFilter;
        public string SelectedActionFilter
        {
            get { return _selectedActionFilter; }
            set
            {
                _selectedActionFilter = value;


                GetList();
            }
        }

        private DateTime _selectedEndDateFilter = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
        public DateTime SelectedEndDateFilter
        {
            get { return _selectedEndDateFilter; }
            set
            {
                if (_selectedEndDateFilter != value)
                {
                    if (_selectedEndDateFilter > _selectedStartDateFilter)
                    {
                        _selectedEndDateFilter = value;
                    }
                    else
                    {
                        _selectedEndDateFilter = _selectedStartDateFilter.AddDays(1);
                    }
                    _selectedEndDateFilter = new DateTime(_selectedEndDateFilter.Year, _selectedEndDateFilter.Month, _selectedEndDateFilter.Day, 23, 59, 59);
                    OnPropertyChanged(nameof(_selectedEndDateFilter));
                    GetList();
                }
            }
        }

        private DateTime _selectedStartDateFilter = DateTime.Today.AddDays(-30);
        public DateTime SelectedStartDateFilter
        {
            get { return _selectedStartDateFilter; }
            set
            {

                if (_selectedStartDateFilter != value)
                {
                    _selectedStartDateFilter = value;
                    _selectedStartDateFilter = new DateTime(_selectedStartDateFilter.Year, _selectedStartDateFilter.Month, _selectedStartDateFilter.Day, 0, 0, 0);
                    OnPropertyChanged(nameof(_selectedStartDateFilter));
                    GetList();
                }

            }
        }

        #endregion
    }
    public partial class Pages : ObservableObject
    {

        #region Commands
        #endregion

        #region Constructor
        public Pages(string databasePath)
        {
            DatabasePath = databasePath;
            GetList();
        }
        #endregion

        #region Events
        #endregion

        #region Fields
        #endregion

        #region Methods

        public void GetList()
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                var result = from UserPages in context.UserPagesDbSet
                             orderby UserPages.Level
                             select UserPages;

                List = result.ToList();
            }
        }
        public void UpdateSelected()
        {
            if (Selected is UserPagesModel)
            {
                Edit.Id = Selected.Id;
                Edit.Level = Selected.Level;
            }
        }
        public void UpdateRow()
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                if (Edit.Level <= 0) { return; }

                UserPagesModel row = context.UserPagesDbSet.FirstOrDefault(e => e.Id == Edit.Id);

                if (row != null)
                {
                    row.Level = Edit.Level;
                    context.UserPagesDbSet.Update(row);
                    context.SaveChanges();
                    GetList();
                }
            }
        }
        public void SetList()
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                context.AddRange(List);
                context.SaveChanges();
                GetList();
            }

        }

        #endregion

        #region Properties

        [ObservableProperty]
        private string _databasePath;

        [ObservableProperty]
        private List<UserPagesModel> _list = new List<UserPagesModel>();

        [ObservableProperty]
        private UserPagesModel _edit = new UserPagesModel();

        [ObservableProperty]
        private UserPagesModel _selected = new UserPagesModel();

        #endregion


    }


}
