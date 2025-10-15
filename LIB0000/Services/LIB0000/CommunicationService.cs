using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace LIB0000
{
    public partial class CommunicationService : ObservableObject
    {

        #region Commands
        #endregion

        #region Constructor

        public CommunicationService()
        {
            Task.Run(() => cyclic());
        }

        #endregion

        #region Events
        #endregion

        #region Fields
        static TcpListener tcpListener;
        static TcpClient tcpClient;
        static NetworkStream networkStream;

        private DateTime dtStart;
        private DateTime dtStop;
        private DateTime timeRead;
        static string messageSend;
        static byte[] bufferSend = new byte[2000];
        static byte[] bufferReceive = new byte[2000];
        private bool ipAdressExists = false;
        private DateTime watchdogTime = DateTime.Now;
        #endregion

        #region Methods

        private bool doesIpAddressExist(string ipAddress)
        {
            IPAddress addressToCheck = IPAddress.Parse(ipAddress);

            var interfaces = NetworkInterface.GetAllNetworkInterfaces()
                .Where(i => i.OperationalStatus == OperationalStatus.Up);

            foreach (var ni in interfaces)
            {
                var ipProps = ni.GetIPProperties();

                foreach (var ip in ipProps.UnicastAddresses)
                {
                    if (ip.Address.Equals(addressToCheck))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        private async Task<bool> convertReceivedData()
        {

            // bool values 
            for (int i = 0; i < 800; i++)
            {
                int byteIndex = i / 8;
                int bitIndex = i % 8;
                bool bitValue = (bufferReceive[byteIndex] & (1 << bitIndex)) != 0;
                BoolToHmi[i] = bitValue;

            }

            for (int i = 0; i < 800; i++)
            {
                int byteIndex = (i + 800) / 8;
                int bitIndex = (i + 800) % 8;
                bool bitValue = (bufferReceive[byteIndex] & (1 << bitIndex)) != 0;
                BoolToHmi02[i] = bitValue;
            }

            // Vul de ParArray met float waarden uit de laatste 1000 bytes van de byte array
            // Een float bestaat uit 4 bytes, dus we hebben 4 * 250 = 1000 bytes nodig
            for (int i = 0; i < 450; i++)
            {
                int byteIndex = 200 + i * 4;
                if (byteIndex + 3 < bufferReceive.Length)
                {
                    SingleToHmi[i] = BitConverter.ToSingle(bufferReceive, byteIndex);
                }
            }

            return true;
        }
        private async Task<bool> convertSendData()
        {
            // Zet de ToPlcCmdBoolArray bools terug naar de eerste 1000 bits van bufferSend
            for (int i = 0; i < 800; i++)
            {
                int byteIndex = i / 8;
                int bitIndex = i % 8;
                if (BoolToPlc[i])
                {
                    bufferSend[byteIndex] |= (byte)(1 << bitIndex);
                }
                else
                {
                    bufferSend[byteIndex] &= (byte)~(1 << bitIndex);
                }
            }

            // Zet de ToPlcStatBoolArray bools terug naar de volgende 1000 bits van bufferSend
            for (int i = 0; i < 800; i++)
            {
                int byteIndex = (i + 800) / 8;
                int bitIndex = (i + 800) % 8;
                if (BoolToPlc02[i])
                {
                    bufferSend[byteIndex] |= (byte)(1 << bitIndex);
                }
                else
                {
                    bufferSend[byteIndex] &= (byte)~(1 << bitIndex);
                }
            }


            // Zet de toPlcParSingleArray floats terug naar de laatste 1000 bytes van bufferSend
            for (int i = 0; i < 450; i++)
            {
                int byteIndex = 200 + i * 4;
                if (byteIndex + 3 < bufferSend.Length)
                {
                    byte[] floatBytes = BitConverter.GetBytes(SingleToPlc[i]);
                    Array.Copy(floatBytes, 0, bufferSend, byteIndex, 4);
                }
            }

            return true;
        }
        private async Task cyclic()
        {
            bool success = false;

            while (!ClosingApplication)
            {
                switch (Step)
                {

                    case stepEnum.WaitConnect:


                        success = await connect(IpAddress, Port);


                        if (success)
                        {
                            Step = stepEnum.WaitData;
                            Debug.WriteLine("Connect ok");
                            Connected = true;
                        }
                        else
                        {
                            Step = stepEnum.WaitConnect;
                            Debug.WriteLine("Connect failed");
                            Connected = false;


                        }

                        break;

                    case stepEnum.WaitData:

                        if (!networkStream.Socket.Connected)
                        {

                            Step = stepEnum.WaitConnect;
                            Debug.WriteLine("Disconnected " + DateTime.Now + " try to reconnect");
                        }



                        WriteIsPossible = (networkStream.CanWrite) && networkStream.Socket.Connected && networkStream.DataAvailable == false && networkStream.Socket.Poll(0, SelectMode.SelectWrite);
                        ReadIsPossible = (networkStream.CanRead) && networkStream.Socket.Connected && (networkStream.Socket.Available > 1999) && networkStream.DataAvailable;


                        if ((WriteIsPossible) && (!ReadIsPossible))
                        {
                            //Step = stepEnum.Send;
                            // watchdog ophogen
                            WatchDogLag = WatchDog - SingleToHmi[400];
                            WatchDog++;
                            if (WatchDog > 1000)
                            {
                                WatchDog = 0;
                                dtStop = DateTime.Now;
                                CycleTime = dtStop - dtStart;
                                dtStart = DateTime.Now;
                            }
                            SingleToPlc[61] = 61;
                            SingleToPlc[400] = WatchDog;
                            // data omzetten naar byte array
                            await convertSendData();
                            // data versturen
                            success = await send();

                            if (success)
                            {
                                WriteCounter++;
                                //  Debug.WriteLine("Send ok " + WriteCounter);
                            }
                            else
                            {

                                //{ Step = stepEnum.WaitConnect; }
                                //  Debug.WriteLine("Send nok " + WriteCounter);
                            }


                        }

                        if (ReadIsPossible)
                        {
                            success = await receive();

                            if (success)
                            {
                                timeRead = DateTime.Now;
                                await convertReceivedData();
                                ReadCounter++;
                                // Debug.WriteLine("Receive ok " + ReadCounter);
                            }
                            else
                            {

                                // Debug.WriteLine("Receive nok " + ReadCounter);
                            }
                        }


                        break;


                }
                Thread.Sleep(4);

            }
        }
        private async Task<bool> connect(string serverIP, int port)
        {
            bool result = false;

            try
            {
                if (networkStream != null)
                {
                    networkStream.Close();
                }
                if (tcpClient != null)
                {
                    tcpClient.Close();
                }
                if (tcpListener != null)
                {
                    tcpListener.Stop();
                }

                ipAdressExists = doesIpAddressExist(IpAddress);
                if (ipAdressExists) // 
                {


                    //client = new TcpClient();
                    //client.ReceiveTimeout = 10000;
                    //client.SendTimeout = 10000;
                    tcpListener = new TcpListener(System.Net.IPAddress.Parse(IpAddress), Port);
                    tcpListener.Start();
                    tcpClient = await tcpListener.AcceptTcpClientAsync();
                    tcpClient.ReceiveBufferSize = 2000;
                    tcpClient.SendBufferSize = 2000;

                    networkStream.ConfigureAwait(true);
                    networkStream = tcpClient.GetStream();
                    networkStream.WriteTimeout = 10000;
                    networkStream.ReadTimeout = 10000;

                    //await client.ConnectAsync(serverIP, port);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
        private async Task<bool> send()
        {
            bool result = false;
            try
            {
                if ((networkStream.CanWrite) && networkStream.DataAvailable == false && networkStream.Socket.Poll(0, SelectMode.SelectWrite))
                {
                    await networkStream.WriteAsync(bufferSend, 0, 2000);
                    result = true;
                }
                else
                {
                    result = false;
                }

            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }
        private async Task<bool> receive()
        {
            bool result = false;



            if ((networkStream.Socket.Available > 1999) && networkStream.Socket.Poll(0, SelectMode.SelectRead))
            {
                await networkStream.ReadAsync(bufferReceive);
                await networkStream.FlushAsync();

                return true;
            }
            return false;
        }



        #endregion

        #region Properties

        [ObservableProperty]
        private int _readCounter;
        [ObservableProperty]
        private int _writeCounter;
        [ObservableProperty]
        private TimeSpan _cycleTime;

        [ObservableProperty]
        private bool _writeIsPossible;
        [ObservableProperty]
        private bool _readIsPossible;

        [ObservableProperty]
        private Single _bufferSizeReceive;

        [ObservableProperty]
        private int _numberOfPackages;

        [ObservableProperty]
        private Single _watchDogLag;

        [ObservableProperty]
        private bool _timeOutCommunication;

        [ObservableProperty]
        private string _ipAddress = "10.5.6.110";
        [ObservableProperty]
        private int _hardwareId;
        [ObservableProperty]
        private Single _watchDog;
        [ObservableProperty]
        private int _port = 6000;
        [ObservableProperty]
        private bool _connected = false;
        [ObservableProperty]
        private long _messagesReceivedCount = 0;
        [ObservableProperty]
        private long _messagesSendCount = 0;
        [ObservableProperty]
        private long _messagesFailedCount;
        [ObservableProperty]
        private string _messageStringReceive;
        [ObservableProperty]
        private Single[] _inputArray;
        [ObservableProperty]
        private Single[] _outputArray;
        [ObservableProperty]
        private ObservableCollection<bool> _boolToHmi = new ObservableCollection<bool>(Enumerable.Repeat(false, 800));
        [ObservableProperty]
        private ObservableCollection<bool> _boolToHmi02 = new ObservableCollection<bool>(Enumerable.Repeat(false, 800));
        [ObservableProperty]
        private ObservableCollection<Single> _singleToHmi = new ObservableCollection<Single>(Enumerable.Repeat(0.0f, 450));
        [ObservableProperty]
        private ObservableCollection<bool> _boolToPlc = new ObservableCollection<bool>(Enumerable.Repeat(false, 800));
        [ObservableProperty]
        private ObservableCollection<bool> _boolToPlc02 = new ObservableCollection<bool>(Enumerable.Repeat(false, 800));
        [ObservableProperty]
        private ObservableCollection<Single> _singleToPlc = new ObservableCollection<Single>(Enumerable.Repeat(0.0f, 450));


        [ObservableProperty]
        private stepEnum _step;

        [ObservableProperty]
        private bool _closingApplication;

        private int myVar;

        public int MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }

        public enum stepEnum
        {
            WaitConnect,
            WaitData,
            Send,
            Receive,

        }

        #endregion

    }

}
