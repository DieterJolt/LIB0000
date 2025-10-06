using System.Collections.ObjectModel;

namespace LIB0000
{
    public partial class MessageService : ObservableObject
    {
        #region Commands
        #endregion
        #region Constructor
        public MessageService()
        {
            //CreateDatabase();

            ActualMessage = new ActualMessage();
            HistoryMessage = new HistoryMessage();
            PossibleMessage = new PossibleMessage();
            StepMessage = new StepMessage();
        }
        #endregion
        #region Events
        #endregion
        #region Fields
        #endregion
        #region Methods

        public bool CreateDatabase(List<MessageModel> lMessages)
        {
            bool result = true;
            using (var context = new LocalDbContext())
            {
                foreach (var messages in lMessages)
                {
                    MessageModel mes = new MessageModel();
                    try
                    {
                        mes = context.MessagesDbSet.FirstOrDefault(e => (e.Nr == messages.Nr) && (e.Group == messages.Group));
                    }
                    catch
                    {
                        result = false;
                    }
                    if (mes == null)
                    {
                        context.MessagesDbSet.Add(messages);
                    }
                    else
                    {
                        mes.Nr = messages.Nr;
                        mes.Group = messages.Group;
                        mes.MessageText = messages.MessageText;
                        mes.Help = messages.Help;
                        mes.Type = messages.Type;
                    }
                }
                context.SaveChanges();
            }
            CreateListOfGroups(lMessages);
            PossibleMessage.GetList();
            return result;
        }


        public void CreateListOfGroups(List<MessageModel> lMessages)
        {
            foreach (var messages in lMessages)
            {
                if (!_selectionOfGroupOfMessages.Contains(messages.Group))
                {
                    _selectionOfGroupOfMessages.Add(messages.Group);
                }
            }
        }


        private List<MessageJoinModel> Filter(List<MessageJoinModel> list, int type)
        {
            List<MessageJoinModel> result = new List<MessageJoinModel>();
            // filteren op de types
            result = list;

            if (FilterErrorsVisible == false)
            {
                result = result.Where(e => e.Type != MessageType.Error).ToList();
            }
            if (FilterWarningsVisible == false)
            {
                result = result.Where(e => e.Type != MessageType.Warning).ToList();
            }
            if (FilterInfosVisible == false)
            {
                result = result.Where(e => e.Type != MessageType.Info).ToList();
            }
            if (!string.IsNullOrEmpty(FilterText))
            {
                result = result
                 .Where(e => (e.MessageText.Contains(FilterText, StringComparison.OrdinalIgnoreCase)) ||
                             (e.Group.Contains(FilterText, StringComparison.OrdinalIgnoreCase)) ||
                             (e.Help.Contains(FilterText, StringComparison.OrdinalIgnoreCase)))
                 .ToList();
            }



            // filteren op datum ( enkel bij historiek)
            if ((FilterStartDate != null) && (type == 1))
            {
                result = result
                    .Where(e => ((DateTime)e.TimeStamp >= FilterStartDate) &&
                    ((DateTime)e.TimeStamp <= FilterEndDate))
                    .ToList();
            }
            // indien lijst van meldingen, sorteren op id
            if (type == 2)
            {
                result = result.OrderBy(e => e.Id).ToList();
            }

            if (SelectionOfGroupIndex != 0)
            {
                result = result.Where(e => e.Group == SelectionOfGroupOfMessages[SelectionOfGroupIndex]).ToList();
            }
            return result;


        }

        public void Set(string group, string nr)
        {

            MessageJoinModel alreadyActive = ActualMessage.List.FirstOrDefault(e => (e.Nr == nr) && (e.Group == group));
            // kijken of de message al bestaat in de actieve list, indien ja, dan niet toevoegen
            if (alreadyActive == null)
            {
                setMessageInDatabase(group, nr);
            }
        }
        private void setMessageInDatabase(string group, string nr)
        {
            DateTime now = DateTime.Now;
            using (var context = new LocalDbContext())
            {
                MessageActiveModel errActToSet = context.MessagesActiveDbSet.FirstOrDefault(e => (e.Nr == nr) && (e.Group == group));
                // kijken of de message al bestaat in de actieve list IN database
                if (errActToSet == null)
                {

                    context.MessagesActiveDbSet.Add(new MessageActiveModel { Nr = nr, Group = group, LastUpdate = now });
                    context.MessagesHistoryDbSet.Add(new MessageHistoryLineModel { Nr = nr, Group = group, TimeStamp = now });
                }
                context.SaveChanges();
                UpdateList();
                ActualMessage.ListEmpty = false;

            }
        }
        public void Reset(string group, string nr)
        {
            MessageJoinModel activeExist = ActualMessage.List.FirstOrDefault(e => (e.Nr == nr) && (e.Group == group));
            // kijken of de message al bestaat in de actieve list, indien ja, dan verwijderen,anders niets doen
            if (activeExist != null)
            {
                resetMessageInDatabase(group, nr);
            }

        }
        private void resetMessageInDatabase(string group, string nr)
        {
            using (var context = new LocalDbContext())
            {
                MessageActiveModel errActToReset = context.MessagesActiveDbSet.FirstOrDefault(e => (e.Nr == nr) && (e.Group == group));
                if (errActToReset != null)
                {
                    context.MessagesActiveDbSet.Remove(errActToReset);
                }
                context.SaveChanges();
                UpdateList();
                MessageActiveModel err = context.MessagesActiveDbSet.FirstOrDefault();
                if (err == null)
                {
                    ActualMessage.ListEmpty = true;
                }
            }
        }
        public void ResetAll()
        {
            using (var context = new LocalDbContext())
            {
                context.MessagesActiveDbSet.RemoveRange(context.MessagesActiveDbSet);
                context.SaveChanges();
                UpdateList();
                MessageActiveModel err = context.MessagesActiveDbSet.FirstOrDefault();
                if (err == null)
                {
                    ActualMessage.ListEmpty = true;
                }
            }
        }

        public void UpdateList()
        {
            ActualMessage.List = ActualMessage.GetList();
            ActualMessage.ListFiltered = Filter(ActualMessage.List, 0);

            HistoryMessage.List = HistoryMessage.GetList();
            HistoryMessage.ListFiltered = Filter(HistoryMessage.List, 1);
        }

        public void UpdateListPossible()
        {
            PossibleMessage.ListFiltered = Filter(PossibleMessage.List, 2);
        }



        #endregion
        #region Properties

        [ObservableProperty]
        private string _databasePath;

        [ObservableProperty]
        private ActualMessage _actualMessage;

        [ObservableProperty]
        private HistoryMessage _historyMessage;

        [ObservableProperty]
        private PossibleMessage _possibleMessage;

        [ObservableProperty]
        private StepMessage _stepMessage;

        // observable property
        private DateTime _filterEndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
        public DateTime FilterEndDate
        {
            get { return _filterEndDate; }
            set
            {
                if (_filterEndDate != value)
                {
                    if (_filterEndDate > FilterStartDate)
                    {
                        _filterEndDate = value;
                    }
                    else
                    {
                        _filterEndDate = FilterStartDate.AddDays(1);
                    }
                    _filterEndDate = new DateTime(_filterEndDate.Year, _filterEndDate.Month, _filterEndDate.Day, 23, 59, 59);
                    OnPropertyChanged(nameof(FilterEndDate));

                }
            }
        }
        // observable property
        private DateTime _filterStartDate = DateTime.Today.AddDays(-30);
        public DateTime FilterStartDate
        {
            get { return _filterStartDate; }
            set
            {

                if (_filterStartDate != value)
                {
                    _filterStartDate = value;
                    _filterStartDate = new DateTime(_filterStartDate.Year, _filterStartDate.Month, _filterStartDate.Day, 0, 0, 0);
                    OnPropertyChanged(nameof(FilterStartDate));

                }

            }
        }
        // observable property
        private string _filterText;
        public string FilterText
        {
            get { return _filterText; }
            set
            {
                if (_filterText != value)
                {
                    _filterText = value;
                    OnPropertyChanged(nameof(FilterText));
                    UpdateListPossible();
                    UpdateList();
                }
            }
        }

        [ObservableProperty]
        private bool _filterErrorsVisible = true;
        [ObservableProperty]
        private bool _filterWarningsVisible = true;
        [ObservableProperty]
        private bool _filterInfosVisible = true;
        [ObservableProperty]
        private bool _datePickersVisible = false;




        // observable property
        private int _selectionOfGroupIndex = 0;
        public int SelectionOfGroupIndex
        {
            get { return _selectionOfGroupIndex; }
            set
            {
                if (_selectionOfGroupIndex != value)
                {
                    _selectionOfGroupIndex = value;
                    OnPropertyChanged(nameof(SelectionOfGroupIndex));
                    UpdateListPossible();
                    UpdateList();
                }
            }
        }
        [ObservableProperty]
        private IList<string> _selectionOfGroupOfMessages = new ObservableCollection<string>
        { "Alle groepen"};














        #endregion
    }
    public partial class ActualMessage : ObservableObject
    {
        #region Commands
        #endregion
        #region Constructor
        public ActualMessage()
        {
        }

        #endregion
        #region Events
        #endregion
        #region Fields
        #endregion
        #region Methods

        public List<MessageJoinModel> GetList()
        {
            using (var context = new LocalDbContext())
            {
                var result = from message in context.MessagesDbSet
                             join messageActive in context.MessagesActiveDbSet
                             on new { Nr = message.Nr, Group = message.Group } equals new { Nr = messageActive.Nr, Group = messageActive.Group }
                             orderby messageActive.LastUpdate descending
                             select new MessageJoinModel
                             {
                                 Id = message.Id,
                                 Group = message.Group,
                                 Nr = message.Nr,
                                 MessageText = message.MessageText,
                                 Help = message.Help,
                                 TimeStamp = messageActive.LastUpdate,
                                 Type = message.Type
                             };

                if (result.ToList().Count > 0)
                {
                    dynamic val = result.ToList()[0];
                    StatusHeader.Nr = val.Nr;
                    StatusHeader.Group = val.Group;
                    StatusHeader.MessageText = val.MessageText;
                    StatusHeader.Help = val.Help;
                    StatusHeader.Type = val.Type;
                }

                return result.ToList();

            }
        }

        public void Remove()
        {
            using (var context = new LocalDbContext())
            {
                context.MessagesActiveDbSet.RemoveRange(context.MessagesActiveDbSet);
                context.SaveChanges();
            }
        }

        #endregion
        #region Properties

        [ObservableProperty]
        private List<MessageJoinModel> _list = new List<MessageJoinModel>();
        [ObservableProperty]
        private List<MessageJoinModel> _listFiltered = new List<MessageJoinModel>();
        [ObservableProperty]
        private MessageJoinModel _selected = new MessageJoinModel();
        [ObservableProperty]
        private bool _listEmpty = true;
        private MessageStatusModel _statusHeader = new MessageStatusModel();
        public MessageStatusModel StatusHeader
        {
            get { return _statusHeader; }
            set
            {
                if (_statusHeader != value)
                {
                    _statusHeader = value;
                    OnPropertyChanged(nameof(StatusHeader));
                }
            }
        }


        #endregion
    }
    public partial class HistoryMessage : ObservableObject
    {

        #region Commands
        #endregion
        #region Constructor
        public HistoryMessage()
        {
        }
        #endregion
        #region Events
        #endregion
        #region Fields
        #endregion
        #region Methods

        public List<MessageJoinModel> GetList()
        {
            using (var context = new LocalDbContext())
            {
                var result = from message in context.MessagesDbSet
                             join messageHistoryLine in context.MessagesHistoryDbSet
                             on new { Nr = message.Nr, Group = message.Group } equals new { Nr = messageHistoryLine.Nr, Group = messageHistoryLine.Group }
                             orderby messageHistoryLine.TimeStamp descending
                             select new MessageJoinModel
                             {
                                 Group = message.Group,
                                 Nr = message.Nr,
                                 MessageText = message.MessageText,
                                 Help = message.Help,
                                 TimeStamp = messageHistoryLine.TimeStamp,
                                 Type = message.Type
                             };

                return result.ToList();
            }
        }
        public void Remove(int max)
        {
            using (var context = new LocalDbContext())
            {
                // Select the rows to keep (the newest messages)
                var rowsToKeep = context.MessagesHistoryDbSet
                                        .OrderByDescending(e => e.TimeStamp)
                                        .Take(max)
                                        .ToList();

                var allRows = context.MessagesHistoryDbSet.ToList();
                var rowsToRemove = allRows.Except(rowsToKeep).ToList();
                context.MessagesHistoryDbSet.RemoveRange(rowsToRemove);

                context.SaveChanges();
            }
        }

        #endregion
        #region Properties

        [ObservableProperty]
        private List<MessageJoinModel> _list = new List<MessageJoinModel>();
        [ObservableProperty]
        private List<MessageJoinModel> _listFiltered = new List<MessageJoinModel>();
        [ObservableProperty]
        private MessageJoinModel _selected = new MessageJoinModel();
        #endregion
    }
    public partial class PossibleMessage : ObservableObject
    {
        #region Commands
        #endregion
        #region Constructor
        public PossibleMessage()
        {
        }
        #endregion
        #region Events
        #endregion
        #region Fields
        #endregion
        #region Methods
        public void GetList()
        {
            using (var context = new LocalDbContext())
            {
                var result = from message in context.MessagesDbSet
                             orderby message.Id descending
                             select new MessageJoinModel
                             {
                                 Id = message.Id,
                                 Group = message.Group,
                                 Nr = message.Nr,
                                 Type = message.Type,
                                 MessageText = message.MessageText,
                                 Help = message.Help
                             };

                List = result.ToList();
            }
        }
        #endregion
        #region Properties

        [ObservableProperty]
        private List<MessageJoinModel> _list = new List<MessageJoinModel>();
        [ObservableProperty]
        private List<MessageJoinModel> _listFiltered = new List<MessageJoinModel>();
        [ObservableProperty]
        private MessageJoinModel _selected = new MessageJoinModel();
        #endregion
    }
    public partial class StepMessage : ObservableObject
    {
        #region Commands
        #endregion
        #region Constructor
        public StepMessage()
        {

        }
        #endregion
        #region Events
        #endregion
        #region Fields
        #endregion
        #region Methods

        public void Add(string group, string status, bool log)
        {
            ActiveStep.Group = group;
            ActiveStep.Status = status;

            if (log)
            {
                using (var context = new LocalDbContext())
                {
                    Remove(max: 1000);
                    MessageStepModel row = context.MessageStepsDbSet.FirstOrDefault();

                    row = new MessageStepModel();
                    row.TimeStamp = DateTime.Now;
                    row.Group = group;
                    row.Status = status;
                    context.MessageStepsDbSet.Add(row);
                    context.SaveChanges();
                }
                GetList();
            }
        }
        public void GetList()
        {
            using (var context = new LocalDbContext())
            {
                List<MessageStepModel> result = context.MessageStepsDbSet.OrderByDescending(x => x.Id).ToList();
                List = result;
            }
        }
        public void Remove(int max)
        {

            using (var context = new LocalDbContext())
            {
                int count = context.MessageStepsDbSet.Count();
                if (count >= max)
                {
                    context.MessageStepsDbSet.OrderBy(e => e.TimeStamp)
                                         .Take(count - max)
                                         .ToList()
                                         .ForEach(e => context.MessageStepsDbSet.Remove(e));
                    context.SaveChanges();
                }
            }
        }

        #endregion
        #region Properties
        [ObservableProperty]
        private List<MessageStepModel> _list = new List<MessageStepModel>();
        [ObservableProperty]
        private MessageStepModel _activeStep = new MessageStepModel();
        [ObservableProperty]
        private MessageType _stepStatus;

        #endregion
    }

}
