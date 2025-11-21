using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIB0000.Lists
{
    internal class HardwareTypesList
    {

        public static List<HardwareTypesModel> GetTypes()
        {
            List<HardwareTypesModel> lTypes = new List<HardwareTypesModel>();
            lTypes.Add(new HardwareTypesModel
            {
                Id = 1,
                Name = "None",
                Description = "None",
                Image = "",
                HardwareType = HardwareType.None

            });
            lTypes.Add(new HardwareTypesModel
            {
                Id = 2,
                Name = "FHV7",
                Description = "FHV7 series",
                Image = "pack://application:,,,/Assets/Devices/FHV7.png",
                HardwareType = HardwareType.FHV7

            });
            lTypes.Add(new HardwareTypesModel
            {
                Id = 3,
                Name = "V430",
                Description = "V430 series",
                Image = "pack://application:,,,/Assets/Devices/V430.png",
                HardwareType = HardwareType.V430

            });
            lTypes.Add(new HardwareTypesModel
            {
                Id = 4,
                Name = "PLC",
                Description = "PLC series",
                Image = "pack://application:,,,/Assets/Devices/PLC.png",
                HardwareType = HardwareType.PLC
            });
            lTypes.Add(new HardwareTypesModel
            {
                Id = 5,
                Name = "Gige",
                Description = "Gige Camera",
                Image = "pack://application:,,,/Assets/Devices/V430.png",
                HardwareType = HardwareType.GigeCam
            });
            lTypes.Add(new HardwareTypesModel
            {
                Id = 6,
                Name = "Turck 4DXP",
                Description = "TBEN-S1-4DXP",
                Image = "pack://application:,,,/Assets/Devices/Turck1.png",
                HardwareType = HardwareType.Turck_4DXP
            });
            lTypes.Add(new HardwareTypesModel
            {
                Id = 7,
                Name = "Turck 8DXP",
                Description = "TBEN-S1-8DXP",
                Image = "pack://application:,,,/Assets/Devices/Turck2.png",
                HardwareType = HardwareType.Turck_8DXP
            });

            return lTypes;
        }

    }
}
