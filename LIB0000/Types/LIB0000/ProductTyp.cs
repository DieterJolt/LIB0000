

using System.Windows.Media.Media3D;

namespace LIB0000
{
    public partial class ProductTyp : ObservableObject
    {
        #region Commands
        #endregion

        #region Constructor
        public ProductTyp()
        {
         

        }
        #endregion

        #region Events

        partial void OnWeightChanged(Single value)
        {
            BerekenTotalWeight();
        }

        partial void OnAmountChanged(int value)
        {
            BerekenTotalWeight();
        }

        #endregion

        #region Fields
        #endregion

        #region Methods

        private void BerekenTotalWeight()
        {
            TotalWeight = Weight * Amount;
        }

        #endregion

        #region Properties

        [ObservableProperty]
        double _speed;
        [ObservableProperty]
        Single _length;
        [ObservableProperty]
        Single _width;
        [ObservableProperty]
        ProductShape _shape;
        [ObservableProperty]
        Single _diameter;
        [ObservableProperty]
        Single _height;
        [ObservableProperty]
        int amount;
        [ObservableProperty]
        int _grijper;
        [ObservableProperty]
        int _blister;
        [ObservableProperty]
        Single _weight;
        [ObservableProperty]
        Single _totalWeight;

        #endregion

    }
    public enum ProductShape
    {
        ShapeExample
    }
}
