using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Media.Media3D;


namespace LIB0000
{
    public partial class FhService : ObservableObject
    {
        #region Commands        

        #endregion

        #region Constructor

        public FhService()
        {
            watchdogThread = new Thread(watchdog);
            watchdogThread.Start();
            checkDataThread = new Thread(checkData);
            checkDataThread.Start();
            checkImageThread = new Thread(checkLastImageAndDate);
            checkImageThread.Start();
            deleteImagesThread = new Thread(deleteImages);
            deleteImagesThread.Start();
        }

        #endregion

        #region Events
        #endregion

        #region Fields
        private TcpClient client;
        private Thread watchdogThread;
        private Thread checkDataThread;
        private Thread checkImageThread;
        private Thread deleteImagesThread;
        static AutoResetEvent responseReceived = new AutoResetEvent(false);
        private string dataString = "";
        private bool newImage = true; // Initeel op true zetten zodat bij opstart van programma de laatste image wordt ingeladen        
        private DateTime time = DateTime.Now;
        private DateTime timeStop = DateTime.Now;
        private TimeSpan timeSpan = new TimeSpan();

        #endregion

        #region Methods

        public async Task CmdAutoFocus(int unitNr)
        {
            string response = "";
            response = await SendData("UnitData " + unitNr.ToString() + " autoFocus0 0");
            await Task.Run(() => Measure());
        }

        public void CheckConnectionServer(string serverIP, int port)
        {
            try
            {
                client = new TcpClient();
                client.Connect(serverIP, port);
                IsConnected = true;
            }
            catch (Exception ex)
            {
                IsConnected = false;
            }

        }
        public async Task CmdDataSave()
        {
            string response = "";
            response = await SendData("DATASAVE");
        }

        public async Task CmdResetCounters()
        {
            string response = "";
            response = await SetSceneVarAndFhStatVar("STAT.TELLER_OK&&", "0", 7);
            response = await SetSceneVarAndFhStatVar("STAT.TELLER_NOK&&", "0", 8);
            response = await SetSceneVarAndFhStatVar("STAT.TELLER_TOTAAL&&", "0", 9);
            response = await SetSceneVarAndFhStatVar("STAT.TELLER_TOTAAL_STOPMACHINE&", "0", 10);
            CounterGood = 0;
            CounterBad = 0;
            CounterTotal = 0;
            await CmdDataSave();

        }

        public async Task CmdResetFault()
        {
            string response = "";
            response = await SendData("CMD_SET_JUDGE_1");
        }

        public async Task CmdSetBarcode(int unitNr, string barcode)
        {
            // Werkt voor 1D en 2D barcode                            
            string response = "";
            response = await SendData("UnitData " + unitNr.ToString() + " judgeCompString " + @"""" + barcode + @"""");
        }

        public async Task CmdSetDetectionPoint(int unitNr, int X, int Y)
        {
            string response = "";
            response = await SendData("UnitData " + unitNr.ToString() + " detectionPosX " + X.ToString());
            response = await SendData("UnitData " + unitNr.ToString() + " detectionPosY " + Y.ToString());
        }

        public async Task CmdSetRegionShapeSearch(int unitNr, double pointAreaUpperLeftX, double pointAreaUpperLeftY, double pointAreaLowerRightX, double pointAreaLowerRightY, double actualImageWidth, double actualImageHeight)
        {
            string response = "";

            response = await SendData("UnitData " + unitNr.ToString() + " 91014 " + (pointAreaUpperLeftX * ResolutionX) / actualImageWidth);
            response = await SendData("UnitData " + unitNr.ToString() + " 91015 " + (pointAreaUpperLeftY * ResolutionY) / actualImageHeight);
            response = await SendData("UnitData " + unitNr.ToString() + " 91016 " + (pointAreaLowerRightX * ResolutionX) / actualImageWidth);
            response = await SendData("UnitData " + unitNr.ToString() + " 91017 " + (pointAreaLowerRightY * ResolutionY) / actualImageHeight);
            response = await SendData("UnitData " + unitNr.ToString() + " 91099 1");
        }

        public async Task CmdSetRegionBarcode(int unitNr, double pointAreaUpperLeftX, double pointAreaUpperLeftY, double pointAreaLowerRightX, double pointAreaLowerRightY, double actualImageWidth, double actualImageHeight)
        {
            // Werkt voor 1D en 2D barcode
            string response = "";
            response = await SendData("UnitData " + unitNr.ToString() + " 90014 " + (pointAreaUpperLeftX * ResolutionX) / actualImageWidth);
            response = await SendData("UnitData " + unitNr.ToString() + " 90015 " + (pointAreaUpperLeftY * ResolutionY) / actualImageHeight);
            response = await SendData("UnitData " + unitNr.ToString() + " 90016 " + (pointAreaLowerRightX * ResolutionX) / actualImageWidth);
            response = await SendData("UnitData " + unitNr.ToString() + " 90017 " + (pointAreaLowerRightY * ResolutionY) / actualImageHeight);
            response = await SendData("UnitData " + unitNr.ToString() + " 90099 1");
        }

        public async Task CmdSetModel(int unitNr, double pointAreaUpperLeftX, double pointAreaUpperLeftY, double pointAreaLowerRightX, double pointAreaLowerRightY, double actualImageWidth, double actualImageHeight)
        {
            string response = "";
            response = await SendData("SCNDATA PAR.MODEL_UL_X&& " + (pointAreaUpperLeftX * ResolutionX) / actualImageWidth);
            response = await SendData("SCNDATA PAR.MODEL_UL_Y&& " + (pointAreaUpperLeftY * ResolutionY) / actualImageHeight);
            response = await SendData("SCNDATA PAR.MODEL_LR_X&& " + (pointAreaLowerRightX * ResolutionX) / actualImageWidth);
            response = await SendData("SCNDATA PAR.MODEL_LR_Y&& " + (pointAreaLowerRightY * ResolutionY) / actualImageHeight);
            response = await SendData("SCNDATA PAR.FLOWNR_SS&& " + unitNr.ToString());
            response = await SendData("CMD_SET_MODEL_SS");
        }

        public async Task CmdSetCorShapeSearch(int unitNr, double lowerCorrelation)
        {
            string response = "";
            response = await SendData("UnitData " + unitNr.ToString() + " lowerCorrelation " + lowerCorrelation.ToString());

        }

        public async Task CmdSetAngleShapeSearch(int unitNr, double angle)
        {
            string response = "";
            response = await SendData("UnitData " + unitNr.ToString() + " upperAngle " + angle.ToString());
            response = await SendData("UnitData " + unitNr.ToString() + " lowerAngle " + (-1 * angle).ToString());
        }

        public async Task CmdSetLight(int unitNr, int part1, int part2, int part3, int part4)
        {
            string hex1 = Math.Clamp(part1, 0, 255).ToString("x2");
            string hex2 = Math.Clamp(part2, 0, 255).ToString("x2");
            string hex3 = Math.Clamp(part3, 0, 255).ToString("x2");
            string hex4 = Math.Clamp(part4, 0, 255).ToString("x2");

            string hexResult = hex1 + hex2 + hex3 + hex4;
            string response = "";
            response = await SendData("UnitData " + unitNr.ToString() + " internalLightGain0 " + hexResult);
        }

        public async Task CmdSetGain(int unitNr, int gain)
        {
            string response = "";
            response = await SendData("UnitData " + unitNr.ToString() + " gain " + gain.ToString());
        }

        public async Task CmdSetShutter(int unitNr, int shutter)
        {
            string response = "";
            response = await SendData("UnitData " + unitNr.ToString() + " exposureTime " + shutter.ToString());
        }
        public async Task CmdRegisterImageAndSetModel(int unitNr, double pointAreaUpperLeftX, double pointAreaUpperLeftY, double pointAreaLowerRightX, double pointAreaLowerRightY, double actualImageWidth, double actualImageHeight, string imagePath)
        {
            string response = "";
            imagePath = imagePath.Replace(@"\\", @"\");
            imagePath = imagePath.Replace(@"C:\FH", @"Z:");

            response = await SendData("RID 000 2 " + @"""" + imagePath + @"""");
            response = await SendData("RID 000");
            await Task.Run(() => CmdSetModel(unitNr, pointAreaUpperLeftX, pointAreaUpperLeftY, pointAreaLowerRightX, pointAreaLowerRightY, actualImageWidth, actualImageHeight));
        }

        private async void watchdog()
        {
            DateTime nextRunTime = DateTime.Now.AddSeconds(1);
            bool connectToNetworkDrive = true;
            bool dateTimeSent = false;
            while (!ClosingApplication)
            {
                if (nextRunTime < DateTime.Now)
                {
                    nextRunTime = DateTime.Now.AddSeconds(1);

                    if (!IsConnected)
                    {
                        Status = "Connectie verbroken, terug connecteren " + DateTime.Now.ToLongTimeString(); ;
                        CheckConnectionServer(IpAddress, Port); // Replace with your server IP and port
                        connectToNetworkDrive = true;
                    }
                    else
                    {
                        try
                        {
                            // Check if the connection is still alive by sending a small message
                            client.Client.Send(new byte[0]);
                            Status = "Connectie is ok " + DateTime.Now.ToLongTimeString();
                            if (connectToNetworkDrive == true)
                            {
                                await SendData("CMD_CONNECT_NETWORKDRIVE");
                                connectToNetworkDrive = false;
                            }
                            if (dateTimeSent == false)
                            {
                                string result = await SendData("DATE " + DateTime.Now.ToString("yyyyMMddHHmmss"));
                                dateTimeSent = true;
                            }
                        }
                        catch
                        {
                            // If sending failed, the connection is lost
                            Status = "Connection verbroken";
                            IsConnected = false;
                        }
                    }
                }
            }
        }

        public async Task<string> SendData(string data)
        {
            if (data is string)
            {
                Debug.WriteLine(data);
                responseReceived.Reset();
                time = DateTime.Now;
                if (IsConnected)
                {
                    byte[] dataBytes = Encoding.ASCII.GetBytes(data);
                    client.Client.Send(dataBytes);

                    if (responseReceived.WaitOne(TimeSpan.FromSeconds(2)))
                    {
                        return "OK";
                    }
                    else
                    {
                        return "FAIL";
                    }
                }
                return "No connection";
            }
            else
            {
                return "Data is null";
            }
        }

        public async Task<string> SetSceneVarAndFhStatVar(string SceneVar, string value, int FhStatVarNr)
        {
            string response = await SendData("SCNDATA " + SceneVar + " " + value.ToString());
            if (TcpIpOutputData != null && TcpIpOutputData.Count() > FhStatVarNr)
            {
                TcpIpOutputData[FhStatVarNr] = value;
            }
            return response;
        }

        public async Task<string> SetSceneVar(string SceneVar, string value)
        {
            string response = await SendData("SCNDATA " + SceneVar + " " + value.ToString());
            return response;
        }

        private void checkData()
        {
            while (!ClosingApplication)
            {
                if (IsConnected)
                {
                    if (client.Available > 0)
                    {
                        byte[] data = new byte[client.Available];
                        client.Client.Receive(data);
                        dataString = Encoding.ASCII.GetString(data) + dataString;
                        Debug.WriteLine(dataString);

                    }
                    if (dataString.Contains("\r") == true || dataString.Contains("%") == true)
                    {

                        if (dataString.Contains("%") == true)
                        {
                            CultureInfo currentCulture = CultureInfo.CurrentCulture;
                            NumberFormatInfo numberFormatInfo = currentCulture.NumberFormat;
                            string decimalSeparator = numberFormatInfo.NumberDecimalSeparator;

                            dataString = dataString.Replace('.', decimalSeparator[0]);
                            dataString = dataString.Replace("%", "");
                            TcpIpOutputDataString = dataString;
                            tcpIpDataSplit(dataString);
                            dataString = "";
                        }
                        else
                        {
                            if (dataString.Contains("OK"))
                            {
                                responseReceived.Set();
                            }
                            else if (dataString.Contains("ER"))
                            {
                                //
                            }

                            dataString = "";

                            timeStop = DateTime.Now;
                            timeSpan = timeStop - time;
                            string ms = timeSpan.Milliseconds.ToString();
                            // 70 ms is de tijd nodig om een parameter te schrijven
                        }
                    }
                }
                Thread.Sleep(1);
            }
        }
        private void tcpIpDataSplit(string data)
        {
            // in FH 
            // Character code > 0 / Delimiter string # / Termination string %
            // Digits of integer 10 ( punt is ook character ) geen decimaal
            // Fill with 0 aanduiden

            newImage = true;
            string[] separatedData = Regex.Split(data, "#");

            switch (separatedData[0])
            {
                case "DATA":
                    TcpIpOutputData = Regex.Split(data, "#");
                    NewData = true;
                    AlreadyDataReceivedSinceStartup = true;
                    //separatedData[2] = separatedData[2].Replace('.', ',');

                    if (TcpIpOutputData[2] == "OK")
                    {
                        //Shape search
                        LastScore = Convert.ToDouble((TcpIpOutputData[3])).ToString("F2");
                        LastAngle = Convert.ToDouble((TcpIpOutputData[4])).ToString("F2");

                        //Qrcode search
                        LastReadedQrCode = TcpIpOutputData[5];

                        //Barcode search
                        LastReadedBarCode = TcpIpOutputData[6];

                        CounterGood = Convert.ToInt32(TcpIpOutputData[7]);
                        CounterBad = Convert.ToInt32(TcpIpOutputData[8]);
                        CounterTotal = Convert.ToInt32(TcpIpOutputData[9]);
                        CounterMachineStop = Convert.ToInt32(TcpIpOutputData[10]);

                    }
                    else
                    {
                        //Shape search
                        LastScore = Convert.ToDouble((TcpIpOutputData[3])).ToString("F2");
                        LastAngle = Convert.ToDouble((TcpIpOutputData[4])).ToString("F2");
                        LastScoreError = Convert.ToDouble((TcpIpOutputData[3])).ToString("F2");
                        LastAngleError = Convert.ToDouble((TcpIpOutputData[4])).ToString("F2");

                        //Qrcode search
                        LastReadedQrCode = TcpIpOutputData[5];
                        LastReadedQrCodeError = TcpIpOutputData[5];

                        if (LastReadedQrCode == "")
                        {
                            LastReadedQrCode = "No read";
                            LastReadedQrCodeError = "No read";
                        }

                        //Barcode search
                        LastReadedBarCode = TcpIpOutputData[6];
                        LastReadedBarCodeError = TcpIpOutputData[6];

                        if (LastReadedBarCode == "")
                        {
                            LastReadedBarCode = "No read";
                            LastReadedBarCodeError = "No read";
                        }

                        CounterGood = Convert.ToInt32(TcpIpOutputData[7]);
                        CounterBad = Convert.ToInt32(TcpIpOutputData[8]);
                        CounterTotal = Convert.ToInt32(TcpIpOutputData[9]);
                        CounterMachineStop = Convert.ToInt32(TcpIpOutputData[10]);
                    }
                    break;
                case "PAR":
                    TcpIpOutputPar = Regex.Split(data, "#");
                    break;


            }
        }

        private void checkLastImageAndDate()
        {
            string? lastOkImage = null;
            string? lastNgImage = null;
            string? lastImage = null;

            while (!ClosingApplication)
            {
                if (Directory.Exists(FilesLocation))
                {
                    newImage = false;

                    lastNgImage = Directory.EnumerateFiles(FilesLocation, "*.bmp", System.IO.SearchOption.TopDirectoryOnly)
                                        .Where(f => Path.GetFileName(f).StartsWith("NG", StringComparison.OrdinalIgnoreCase))
                                        .OrderByDescending(f => new FileInfo(f).LastWriteTime)
                                        .FirstOrDefault();

                    lastImage = Directory.EnumerateFiles(FilesLocation, "*.bmp", System.IO.SearchOption.TopDirectoryOnly)
                                        .Where(f => Path.GetFileName(f).StartsWith("LAST", StringComparison.OrdinalIgnoreCase))
                                        .OrderByDescending(f => new FileInfo(f).LastWriteTime)
                                        .FirstOrDefault();


                    if (lastNgImage != null)
                    {
                        //string pattern = @"NG_(\d{4}-\d{2}-\d{2}_\d{2}-\d{2}-\d{2})";
                        DateTime lastImageDate = new FileInfo(lastImage).LastWriteTime;
                        DateLastNg = lastImageDate;
                        // Search for the pattern in the string
                        //Match match = Regex.Match(lastNgImage, pattern);

                        //if (match.Success)
                        //{
                        // Extract the datetime string
                        //    string datetimeStr = match.Groups[1].Value;

                        // Replace underscores with spaces and hyphens in the time part with colons
                        //    datetimeStr = datetimeStr.Replace("_", " ").Replace("-", ":").Replace("-", ":");

                        // Parse the datetime string to a DateTime object
                        //    DateLastNg = lastImageDate; //DateTime.ParseExact(datetimeStr, "yyyy:MM:dd HH:mm:ss", null);
                        //}
                    }


                    if ((lastImage != null) && (LastImage != lastImage))
                    {
                        bool fileBusy = true;
                        try
                        {
                            // Try to open the file exclusively
                            using (FileStream fs = new FileStream(lastImage, FileMode.Open, FileAccess.Read, FileShare.None))
                            {
                                // If we can open the file, it's not being written to                                
                            }
                            fileBusy = false;
                        }
                        catch { fileBusy = true; }

                        if (!fileBusy)
                        {
                            LastImage = lastImage;
                        }
                    }


                    if ((lastNgImage != null) && (LastNgImage != lastNgImage))
                    {
                        bool fileBusy = true;
                        try
                        {
                            // Try to open the file exclusively
                            using (FileStream fs = new FileStream(lastNgImage, FileMode.Open, FileAccess.Read, FileShare.None))
                            {
                                // If we can open the file, it's not being written to                                
                            }
                            fileBusy = false;
                        }
                        catch { fileBusy = true; }

                        if (!fileBusy)
                        {
                            LastNgImage = lastNgImage;
                        }
                    }
                    Thread.Sleep(10);
                }
            }

        }
        private void deleteImages()
        {
            DateTime nextRunTime = DateTime.Now.AddSeconds(10);

            while (!ClosingApplication)
            {
                if (nextRunTime < DateTime.Now)
                {
                    nextRunTime = DateTime.Now.AddSeconds(10);

                    if (Directory.Exists(@"C:\FH\IMAGES\MODELS\"))
                    {
                        // Get all bmp files in the folder
                        string[] bmpFiles = Directory.GetFiles(@"C:\FH\IMAGES\MODELS\", "*.bmp", System.IO.SearchOption.AllDirectories);

                        // Sort the files by last write time ( new ones first )
                        var sortedBmpFiles = bmpFiles.Select(f => new FileInfo(f))
                                                      .OrderByDescending(fi => fi.LastWriteTime)
                                                      .ToList();


                        var LastFiles = sortedBmpFiles.Where(f => f.Name.Contains("MODEL")).Take(10).ToList();

                        // delete the rest
                        //var filesToDelete = sortedBmpFiles.Except(LastFiles).Except(ngFiles).Except(modelFiles).ToList();
                        var filesToDelete = sortedBmpFiles.Except(LastFiles).ToList();
                        foreach (var file in filesToDelete)
                        {
                            try
                            {
                                File.Delete(file.FullName);
                            }
                            catch
                            { }
                        }
                    }
                }
            }
        }

        public async Task Measure()
        {
            string response = "";
            response = await SendData("Measure");
            NewData = false;
        }

        public async Task UpdateBasicSettings(List<SettingModel> listSettings)
        {
            listSettings = listSettings.Where(s => s.HardwareFunction == HardwareFunction.None).ToList();

            int shutter = Convert.ToInt32(listSettings.Where(s => s.Nr == "002").FirstOrDefault().Value);
            int gain = Convert.ToInt32(listSettings.Where(s => s.Nr == "003").FirstOrDefault().Value);
            int lightUp = Convert.ToInt32(listSettings.Where(s => s.Nr == "004").FirstOrDefault().Value);
            int lightLeft = Convert.ToInt32(listSettings.Where(s => s.Nr == "005").FirstOrDefault().Value);
            int lightDown = Convert.ToInt32(listSettings.Where(s => s.Nr == "006").FirstOrDefault().Value);
            int lightRight = Convert.ToInt32(listSettings.Where(s => s.Nr == "007").FirstOrDefault().Value);

            await Task.Run(() => CmdSetShutter(0, shutter));
            await Task.Run(() => CmdSetGain(0, gain));
            await Task.Run(() => CmdSetLight(0, lightUp, lightLeft, lightDown, lightRight));
        }

        public async Task UpdateFunctionSettings(List<ProductDetailJoinModel> listProductDetailJoin, HardwareFunction hardwareFunction)
        {
            listProductDetailJoin = listProductDetailJoin.Where(s => s.HardwareFunction == hardwareFunction).ToList();

            switch (hardwareFunction)
            {
                case HardwareFunction.Fhv7ShapeSearch:

                    string destinationPath = Path.Combine(@"C:\FH\IMAGES\MODELS\MODEL_" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + @".bmp");
                    try
                    {
                        File.Copy(LastImage, destinationPath, overwrite: true);
                    }
                    catch { }
                    await Task.Run(() => SetSceneVar("PAR.TYPE_CONTROLE&&", "1")); // Type1 = ShapeSearch

                    double imageWidth = Convert.ToDouble(listProductDetailJoin.Where(s => s.Nr == "011").FirstOrDefault().Value);
                    double imageHeight = Convert.ToDouble(listProductDetailJoin.Where(s => s.Nr == "012").FirstOrDefault().Value);

                    if (true) // updateModel?
                    {
                        double modelUpperLeftX = Convert.ToDouble(listProductDetailJoin.Where(s => s.Nr == "007").FirstOrDefault().Value);
                        double modelUpperLeftY = Convert.ToDouble(listProductDetailJoin.Where(s => s.Nr == "008").FirstOrDefault().Value);
                        double modelLowerRightX = Convert.ToDouble(listProductDetailJoin.Where(s => s.Nr == "009").FirstOrDefault().Value);
                        double modelLowerRightY = Convert.ToDouble(listProductDetailJoin.Where(s => s.Nr == "010").FirstOrDefault().Value);

                        CmdSetModel(3, modelUpperLeftX, modelUpperLeftY, modelLowerRightX, modelLowerRightY, imageWidth, imageHeight);
                    }

                    double regionUpperLeftX = Convert.ToDouble(listProductDetailJoin.Where(s => s.Nr == "003").FirstOrDefault().Value);
                    double regionUpperLeftY = Convert.ToDouble(listProductDetailJoin.Where(s => s.Nr == "004").FirstOrDefault().Value);
                    double regionLowerRightX = Convert.ToDouble(listProductDetailJoin.Where(s => s.Nr == "005").FirstOrDefault().Value);
                    double regionLowerRightY = Convert.ToDouble(listProductDetailJoin.Where(s => s.Nr == "006").FirstOrDefault().Value);


                    await CmdSetRegionShapeSearch(3, regionUpperLeftX, regionUpperLeftY, regionLowerRightX, regionLowerRightY, imageWidth, imageHeight);
                    double correlation = Convert.ToDouble(listProductDetailJoin.Where(s => s.Nr == "001").FirstOrDefault().Value);
                    await CmdSetCorShapeSearch(3, correlation);
                    double angle = Convert.ToDouble(listProductDetailJoin.Where(s => s.Nr == "002").FirstOrDefault().Value);
                    await CmdSetAngleShapeSearch(3, angle);
                    break;

                case HardwareFunction.Fhv7Barcode:
                    await Task.Run(() => SetSceneVar("PAR.TYPE_CONTROLE&&", "3")); // Type3 = Barcode
                    regionUpperLeftX = Convert.ToDouble(listProductDetailJoin.Where(s => s.Nr == "002").FirstOrDefault().Value);
                    regionUpperLeftY = Convert.ToDouble(listProductDetailJoin.Where(s => s.Nr == "003").FirstOrDefault().Value);
                    regionLowerRightX = Convert.ToDouble(listProductDetailJoin.Where(s => s.Nr == "004").FirstOrDefault().Value);
                    regionLowerRightY = Convert.ToDouble(listProductDetailJoin.Where(s => s.Nr == "005").FirstOrDefault().Value);
                    imageWidth = Convert.ToDouble(listProductDetailJoin.Where(s => s.Nr == "006").FirstOrDefault().Value);
                    imageHeight = Convert.ToDouble(listProductDetailJoin.Where(s => s.Nr == "007").FirstOrDefault().Value);
                    await Task.Run(() => CmdSetRegionBarcode(7, regionUpperLeftX, regionUpperLeftY, regionLowerRightX, regionLowerRightY, imageWidth, imageHeight));
                    string barcode = listProductDetailJoin.Where(s => s.Nr == "001").FirstOrDefault().Value;
                    await Task.Run(() => CmdSetBarcode(7, barcode));
                    break;

                    //case HardwareFunction.QrCode:
                    //await Task.Run(() => SetSceneVar("PAR.TYPE_CONTROLE&&", "2")); // Type3 = QrCode
                    //regionUpperLeftX = Convert.ToDouble(listSettings.Where(s => s.Nr == "002").FirstOrDefault().Value);
                    //regionUpperLeftY = Convert.ToDouble(listSettings.Where(s => s.Nr == "003").FirstOrDefault().Value);
                    //regionLowerRightX = Convert.ToDouble(listSettings.Where(s => s.Nr == "004").FirstOrDefault().Value);
                    //regionLowerRightY = Convert.ToDouble(listSettings.Where(s => s.Nr == "005").FirstOrDefault().Value);
                    //await Task.Run(() => CmdSetRegionBarcode(7, regionUpperLeftX, regionUpperLeftY, regionLowerRightX, regionLowerRightY, ActualImageWidth, ActualImageHeight));
                    //barcode = listSettings.Where(s => s.Nr == "002").FirstOrDefault().Value;
                    //await Task.Run(() => CmdSetBarcode(7, barcode));
                    //break;
            }

            // Max fouten tot stop machine
            await SetSceneVar("PAR.TELLER_STOPMACHINE_MAX&&", "1");
        }
        #endregion

        #region Properties

        [ObservableProperty]
        private bool _isConnected;

        [ObservableProperty]
        private string _status;

        [ObservableProperty]
        private string _ipAddress;

        [ObservableProperty]
        private int _hardwareId;

        [ObservableProperty]
        private int _port = 9876;

        [ObservableProperty]
        private bool _alreadyDataReceivedSinceStartup = false;

        [ObservableProperty]
        private string[] _tcpIpOutputData;

        [ObservableProperty]
        private string[] _tcpIpOutputPar;

        [ObservableProperty]
        private string _tcpIpOutputDataString = "";

        [ObservableProperty]
        private bool _newData = false;

        [ObservableProperty]
        private string _lastImage;

        [ObservableProperty]
        private string _lastNgImage;

        [ObservableProperty]
        private string _filesLocation = "C:/FH/IMAGES/";

        [ObservableProperty]
        private DateTime _dateLastOk;

        [ObservableProperty]
        private DateTime _dateLastNg;

        [ObservableProperty]
        string _lastScore;

        [ObservableProperty]
        string _lastScoreError;

        [ObservableProperty]
        string _lastAngle;

        [ObservableProperty]
        string _LastAngleError;

        [ObservableProperty]
        string _lastReadedQrCode;

        [ObservableProperty]
        string _lastReadedQrCodeError;

        [ObservableProperty]
        string _lastReadedBarCode;

        [ObservableProperty]
        string _LastReadedBarCodeError;

        [ObservableProperty]
        int _counterGood;

        [ObservableProperty]
        int _counterBad;

        [ObservableProperty]
        int _counterTotal;

        [ObservableProperty]
        int _counterMachineStop;

        [ObservableProperty]
        private int _resolutionY = 1536;

        [ObservableProperty]
        private int _resolutionX = 2048;

        [ObservableProperty]
        private bool _closingApplication;

        #endregion

    }
}
