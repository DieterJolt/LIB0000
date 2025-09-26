namespace LIB0000
{
    public partial class RecipeTyp : ObservableObject
    {
        #region Commands
        #endregion

        #region Constructor
        public RecipeTyp()
        {
            MachinePar = new MachineParTyp();
            InvoerPar = new InvoerParTyp();
            UitvoerPar = new UitvoerParTyp();
        }
        #endregion

        #region Events
        #endregion

        #region Fields
        #endregion

        #region Methods
        #endregion

        #region Properties
        [ObservableProperty]
        MachineParTyp _machinePar;
        [ObservableProperty]
        InvoerParTyp _invoerPar;
        [ObservableProperty]
        UitvoerParTyp _uitvoerPar;
        #endregion

    }
}
