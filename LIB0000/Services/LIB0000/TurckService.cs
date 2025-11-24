using Microsoft.Extensions.Hosting;
using Sres.Net.EEIP;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Protection.PlayReady;

namespace LIB0000
{
    public partial class TurckService : ObservableObject
    {

        #region Commands
        #endregion

        #region Constructor

        public TurckService(string ipAddress, int hardwareId)
        {
            // TBEN-S1-4DXP IP-adres
            //eeip.IPAddress = "10.5.6.150";
            IpAddress = ipAddress;
            HardwareId = hardwareId;

            eeip.IPAddress = ipAddress;


            //if (IpAddress == null)
            //{
            //    return;
            //}
            //eeip.IPAddress = IpAddress;


            // INPUT (Target → Originator)
            eeip.T_O_InstanceID = INPUT_ASSEMBLY;
            eeip.T_O_RealTimeFormat = RealTimeFormat.Modeless;
            eeip.T_O_ConnectionType = ConnectionType.Point_to_Point;
            eeip.T_O_Priority = Priority.Scheduled;

            eeip.RequestedPacketRate_T_O = 10000;  // 10 ms

            // OUTPUT (Originator → Target)
            eeip.O_T_InstanceID = OUTPUT_ASSEMBLY;
            eeip.O_T_RealTimeFormat = RealTimeFormat.Modeless;
            eeip.O_T_ConnectionType = ConnectionType.Point_to_Point;
            eeip.O_T_Priority = Priority.Scheduled;
            eeip.RequestedPacketRate_O_T = 10000;  // 10 ms                       

            MonitorEthernetThread = new Thread(MonitorEthernet);
            MonitorEthernetThread.Start();;

        }

        #endregion

        #region Events
        #endregion

        #region Fields

        private Thread MonitorEthernetThread;

        private readonly EEIPClient eeip = new EEIPClient();
        private Thread cyclicThread;
        //test
        private CancellationTokenSource cyclicCts;

        // TBEN-S1-4DXP Assembly numbers (confirmed)
        private const int INPUT_ASSEMBLY = 103;   // T->O
        private const int OUTPUT_ASSEMBLY = 104;  // O->T

        public byte[] Inputs { get; private set; } = new byte[0];
        public byte[] Outputs { get; private set; } = new byte[0];

        #endregion

        #region Methods

        public void Start()
        {
            //if (IsConnected) return;

            //Connect();

            //IsConnected = true;
            //cyclicThread = new Thread(CyclicTask);
            //cyclicThread.IsBackground = true;
            //cyclicThread.Start();

            //Debug.WriteLine("TBEN-S1-4DXP thread started.");

            if (IsConnected) return;

            Connect();
            IsConnected = true;

            cyclicCts = new CancellationTokenSource();
            var token = cyclicCts.Token;

            cyclicThread = new Thread(() => CyclicTask(token));
            cyclicThread.IsBackground = true;
            cyclicThread.Start();

            //Debug.WriteLine("TBEN-S1-4DXP thread started.");
            Debug.WriteLine($"{IpAddress}: thread started.");

        }

        public void Stop()
        {
            //IsConnected = false;
            //Thread.Sleep(50);

            //try { eeip.ForwardClose(); } catch { }
            //try { eeip.UnRegisterSession(); } catch { }

            //Debug.WriteLine("TBEN-S1-4DXP connection closed.");

            if (!IsConnected) return;

            IsConnected = false;

            // Vraag thread netjes te stoppen
            cyclicCts?.Cancel();

            // Wacht tot thread klaar is
            cyclicThread?.Join();

            try { eeip.ForwardClose(); } catch { }
            try { eeip.UnRegisterSession(); } catch { }

            //Debug.WriteLine("TBEN-S1-4DXP connection closed.");
            Debug.WriteLine($"{IpAddress}: connection closed.");
        }


        /// <summary>
        /// Zet een digitale uitgang (0..3) HIGH
        /// </summary>
        public void SetOutput(int channel)
        {
            lock (eeip)
            {
                Outputs[0] |= (byte)(1 << 0); //wel  
                Outputs[6] |= (byte)(1 << channel); //wel              
            }
        }

        /// <summary>
        /// Zet een digitale uitgang (0..3) LOW
        /// </summary>
        public void ClearOutput(int channel)
        {
            lock (eeip)
            {
                Outputs[2] &= (byte)~(1 << channel);
            }
        }

        /// <summary>
        /// Leest de status van digitale ingang (0..3)
        /// </summary>
        public bool GetInput(int channel)
        {
            lock (eeip)
            {
                return (Inputs[2] & (1 << channel)) != 0;
            }
        }

        // ------------------------------------------------------------------------------------------
        // INTERNAL IMPLEMENTATION
        // ------------------------------------------------------------------------------------------

        private void Connect()
        {
            eeip.RegisterSession();

            // Detect output length
            eeip.O_T_Length = eeip.Detect_O_T_Length();
            Outputs = new byte[eeip.O_T_Length];

            // Detect input length
            eeip.T_O_Length = eeip.Detect_T_O_Length();
            Inputs = new byte[eeip.T_O_Length];

            eeip.ForwardOpen();

            Debug.WriteLine($"{IpAddress}:Connected. InputLen={Inputs.Length}, OutputLen={Outputs.Length}");
        }

        //private void CyclicTask()
        //{
        //    while (IsConnected)
        //    {
        //        lock (eeip)
        //        {
        //            // Read inputs
        //            Array.Copy(eeip.T_O_IOData, Inputs, Inputs.Length);
        //            Debug.WriteLine($"Input byte 2: {Inputs[2]}");


        //            // Write outputs                    
        //            Array.Copy(Outputs, eeip.O_T_IOData, Outputs.Length);
        //            Debug.WriteLine($"Output byte 2: {Outputs[2]}");
        //        }

        //        Thread.Sleep(10); // 10 ms cyclic
        //    }
        //}

        private void CyclicTask(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                lock (eeip)
                {
                    Array.Copy(eeip.T_O_IOData, Inputs, Inputs.Length);
                    Debug.WriteLine($"{IpAddress}:Input byte 2: {Inputs[2]}");

                    Array.Copy(Outputs, eeip.O_T_IOData, Outputs.Length);
                    Debug.WriteLine($"{IpAddress}:Output byte 2: {Outputs[2]}");
                }
                Thread.Sleep(10);
            }
        }

        static NetworkInterface GetEthernet()
        {
            foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
                if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                    return nic;

            return null;
        }

        private void MonitorEthernet()
        {
            while (true)
            {
                var nic = GetEthernet();

                if (nic != null && nic.OperationalStatus != lastStatus)
                {
                    lastStatus = nic.OperationalStatus;

                    if (lastStatus == OperationalStatus.Up)
                    {
                        //Debug.WriteLine("Ethernet is VERBONDEN");
                        Debug.WriteLine($"{IpAddress}: Ethernet is VERBONDEN");
                        Start();
                    }
                    else
                    {
                        //Debug.WriteLine("Ethernet is VERBROKEN");
                        Debug.WriteLine($"{IpAddress}: Ethernet is VERBROKEN");
                        Stop();
                    }
                }

                Thread.Sleep(1000);
            }
        }





        #endregion

        #region Properties

        [ObservableProperty]
        private string _ipAddress;

        [ObservableProperty]
        private int _hardwareId;

        [ObservableProperty]
        private bool _isConnected;

        static OperationalStatus lastStatus = OperationalStatus.Unknown;

        #endregion


    }
}
