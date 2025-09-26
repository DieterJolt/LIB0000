using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIB0000
{
    internal class InstructionTypesList
    {
        public static List<InstructionTypesModel> GetTypes()
        {
            List<InstructionTypesModel> lTypes = new List<InstructionTypesModel>();
            lTypes.Add(new InstructionTypesModel
            {
                Id = 1,
                InstructionType = InstructionType.Click,
                Icon = "pack://application:,,,/Assets/Images/InstructionTypeClick.png",
                Name = "Click",
            });
            lTypes.Add(new InstructionTypesModel
            {
                Id = 2,
                InstructionType = InstructionType.Hotspot,
                Icon = "pack://application:,,,/Assets/Images/InstructionTypeHotspot.png",
                Name = "Hotspot",
            });
            lTypes.Add(new InstructionTypesModel
            {
                Id = 3,
                InstructionType = InstructionType.Snapshot,
                Icon = "pack://application:,,,/Assets/Images/InstructionTypeSnapshot.png",
                Name = "Snapshot",
            });
            lTypes.Add(new InstructionTypesModel
            {
                Id = 4,
                InstructionType = InstructionType.RemoteInput,
                Icon = "pack://application:,,,/Assets/Images/InstructionTypeRemoteInput.png",
                Name = "Remote input",
            });
            lTypes.Add(new InstructionTypesModel
            {
                Id = 5,
                InstructionType = InstructionType.Scan,
                Icon = "pack://application:,,,/Assets/Images/InstructionTypeCheckBarcode.png",
                Name = "Scan",
            });
            lTypes.Add(new InstructionTypesModel
            {
                Id = 6,
                InstructionType = InstructionType.Checklist,
                Icon = "pack://application:,,,/Assets/Images/InstructionTypeChecklist.png",
                Name = "Checklist",
            });
            lTypes.Add(new InstructionTypesModel
            {
                Id = 7,
                InstructionType = InstructionType.ImageChecklist,
                Icon = "pack://application:,,,/Assets/Images/InstructionTypeChecklist.png",
                Name = "ImageChecklist",
            });
            lTypes.Add(new InstructionTypesModel
            {
                Id = 8,
                InstructionType = InstructionType.Selectionlist,
                Icon = "pack://application:,,,/Assets/Images/InstructionTypeSelectionlist.png",
                Name = "Selectionlist",
            });
            lTypes.Add(new InstructionTypesModel
            {
                Id = 9,
                InstructionType = InstructionType.ImageSelectionlist,
                Icon = "pack://application:,,,/Assets/Images/InstructionTypeSelectionlist.png",
                Name = "ImageSelectionlist",
            });
            lTypes.Add(new InstructionTypesModel
            {
                Id = 10,
                InstructionType = InstructionType.Login,
                Icon = "pack://application:,,,/Assets/Images/User.png",
                Name = "Login",
            });
            lTypes.Add(new InstructionTypesModel
            {
                Id = 11,
                InstructionType = InstructionType.YesNo,
                Icon = "pack://application:,,,/Assets/Images/InstructionTypeYesNo.png",
                Name = "YesNo",
            });
            lTypes.Add(new InstructionTypesModel
            {
                Id = 12,
                InstructionType = InstructionType.Pdf,
                Icon = "pack://application:,,,/Assets/Images/Pdf.png",
                Name = "Pdf",
            });

            return lTypes;
        }
    }

    public enum InstructionType
    {
        None = 0,
        Click = 1,
        Hotspot = 2,
        Snapshot = 3,
        RemoteInput = 4,
        Scan = 5,
        Checklist = 6,
        ImageChecklist = 7,
        Selectionlist = 8,
        ImageSelectionlist = 9,
        Login = 10,
        YesNo = 11,
        Pdf = 12
    }
}
