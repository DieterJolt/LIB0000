using System.Windows.Controls;

namespace LIB0000
{
    internal class InstructionTypeDataTemplate : DataTemplateSelector
    {
        public DataTemplate? EmptyTemplate { get; set; }
        public DataTemplate? ClickTemplate { get; set; }
        public DataTemplate? HotspotTemplate { get; set; }
        public DataTemplate? SnapshotTemplate { get; set; }
        public DataTemplate? RemoteInputTemplate { get; set; }
        public DataTemplate? ScanTemplate { get; set; }
        public DataTemplate? ChecklistTemplate { get; set; }
        public DataTemplate? ImageChecklistTemplate { get; set; }
        public DataTemplate? SelectionListTemplate { get; set; }
        public DataTemplate? ImageSelectionListTemplate { get; set; }
        public DataTemplate? YesNoTemplate { get; set; }
        public DataTemplate? PdfTemplate { get; set; }

        public override DataTemplate? SelectTemplate(object item, DependencyObject container)
        {
            if (item is InstructionModel instructionModel)
            {
                return instructionModel.InstructionType switch
                {
                    InstructionType.Click => ClickTemplate,
                    InstructionType.Hotspot => HotspotTemplate,
                    InstructionType.Snapshot => SnapshotTemplate,
                    InstructionType.RemoteInput => RemoteInputTemplate,
                    InstructionType.Scan => ScanTemplate,
                    InstructionType.Checklist => ChecklistTemplate,
                    InstructionType.ImageChecklist => ImageChecklistTemplate,
                    InstructionType.Selectionlist => SelectionListTemplate,
                    InstructionType.ImageSelectionlist => ImageSelectionListTemplate,
                    InstructionType.YesNo => YesNoTemplate,
                    InstructionType.Pdf => PdfTemplate,
                    _ => EmptyTemplate
                };
            }
            return null;
        }
    }
}
