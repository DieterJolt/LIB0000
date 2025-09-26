
using System.Windows.Controls;

namespace LIB0000
{
    public class SettingDataTemplate : DataTemplateSelector
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
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is SettingModel settingsObject)
            {
                switch (settingsObject.ControlType)
                {
                    case ControlType.TextBoxWithFilePicker:
                        return TemplateTextBoxWithFilePicker;
                    case ControlType.TextBox:
                        return TemplateTextBox;
                    case ControlType.TextBoxWithFolderPicker:
                        return TemplateTextBoxWithFolderPicker;
                    case ControlType.ToggleSwitch:
                        return TemplateToggleSwitch;
                    case ControlType.ComboBox:
                        return TemplateComboBox;
                    case ControlType.Slider:
                        return TemplateSlider;
                    default:
                        return TemplateTextBox;

                }
            }
            else if (item is ProductDetailJoinModel productDetailObject)
            {
                switch (productDetailObject.ControlType)
                {
                    case ControlType.TextBoxWithFilePicker:
                        return TemplateTextBoxWithFilePicker;
                    case ControlType.TextBox:
                        return TemplateTextBox;
                    case ControlType.TextBoxWithFolderPicker:
                        return TemplateTextBoxWithFolderPicker;
                    case ControlType.ToggleSwitch:
                        return TemplateToggleSwitch;
                    case ControlType.ComboBox:
                        return TemplateComboBox;
                    default:
                        return TemplateTextBox;

                }
            }
            return TemplateTextBox;
        }
        #endregion

        #region Properties
        public DataTemplate? TemplateTextBox { get; set; }
        public DataTemplate? TemplateTextBoxWithFilePicker { get; set; }
        public DataTemplate? TemplateTextBoxWithFolderPicker { get; set; }
        public DataTemplate? TemplateToggleSwitch { get; set; }
        public DataTemplate? TemplateComboBox { get; set; }
        public DataTemplate? TemplateSlider { get; set; }

        #endregion






    }
}