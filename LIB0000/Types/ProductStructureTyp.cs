

using System.Windows.Media.Media3D;

namespace LIB0000
{
    public partial class ProductStructureTyp : ObservableObject
    {
        #region Commands
        #endregion

        #region Constructor
        public ProductStructureTyp()
        {
         

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
