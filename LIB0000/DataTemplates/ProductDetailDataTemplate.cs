using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LIB0000
{
    internal class ProductDetailDataTemplate : DataTemplateSelector
    {
        #region Commands
        #endregion

        #region Constructor
        #endregion

        #region Events
        #endregion

        #region Fields
        #endregion

        #region Methods
        public override DataTemplate? SelectTemplate(object item, DependencyObject container)
        {
            if (item is HardwareFunction hardwareFunction)
            {
                return hardwareFunction switch
                {
                    HardwareFunction.Fhv7ShapeSearch => Fhv7ShapeSearchTemplate,
                    HardwareFunction.Fhv7Barcode => Fhv7BarcodeTemplate,
                    _ => EmptyTemplate
                };
            }
            return null;
        }
        #endregion

        #region Properties
        public DataTemplate? EmptyTemplate { get; set; }
        public DataTemplate? Fhv7ShapeSearchTemplate { get; set; }
        public DataTemplate? Fhv7BarcodeTemplate { get; set; }
        #endregion





    }
}
