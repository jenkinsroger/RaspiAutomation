using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;
using Windows.Storage.Streams;

namespace HomeAutomation
{
    class SerialCommunicationClass
    {
        public static async void ListAvailablePorts()
        {
            StaticPropertiescs.listOfDevices = new ObservableCollection<DeviceInformation>();

            try
            {
                string aqs = SerialDevice.GetDeviceSelector();
                var dis = await DeviceInformation.FindAllAsync(aqs);

                for (int i = 0; i < dis.Count; i++)
                {
                    StaticPropertiescs.listOfDevices.Add(dis[i]);
                    StaticPropertiescs.serialPort = await SerialDevice.FromIdAsync(dis[i].Id);
                }

            }
            catch (Exception ex)
            {

            }
        }

        public static async void ConnectToSerialDevice()
        {
            ListAvailablePorts();

            DeviceInformation entry = StaticPropertiescs.listOfDevices[0];

            try
            {

                //StaticPropertiescs.serialPort = await SerialDevice.FromIdAsync(entry.Id);

                StaticPropertiescs.serialPort.WriteTimeout = TimeSpan.FromMilliseconds(1000);
                StaticPropertiescs.serialPort.ReadTimeout = TimeSpan.FromMilliseconds(1000);
                StaticPropertiescs.serialPort.BaudRate = 9600;
                StaticPropertiescs.serialPort.Parity = SerialParity.None;
                StaticPropertiescs.serialPort.StopBits = SerialStopBitCount.One;
                StaticPropertiescs.serialPort.DataBits = 8;
                StaticPropertiescs.serialPort.Handshake = SerialHandshake.None;

                CommunicationScreen.Message = "CONNECTED TO DEVICE....";
            }
            catch(Exception e)
            {
                CommunicationScreen.Message = "Error connecting \r\n" + e.Message;
            }
        }

        public static async void SendText(string txt)
        {
            try
            {
                if(StaticPropertiescs.serialPort != null)
                {
                    StaticPropertiescs.dataWriteObject = new DataWriter(StaticPropertiescs.serialPort.OutputStream);
                    await WriteAsync();
                }
            }
            catch
            {

            }
            finally
            {
                if (StaticPropertiescs.dataWriteObject != null)
                {
                    StaticPropertiescs.dataWriteObject.DetachStream();
                    StaticPropertiescs.dataWriteObject = null;
                }
            }
        }

        private static async Task WriteAsync()
        {
            Task<UInt32> storeAsyncTask;

            if (("this").Length != 0)
            {
                // Load the text from the sendText input text box to the dataWriter object
                StaticPropertiescs.dataWriteObject.WriteString("test");

                // Launch an async task to complete the write operation
                storeAsyncTask = StaticPropertiescs.dataWriteObject.StoreAsync().AsTask();

                UInt32 bytesWritten = await storeAsyncTask;
                if (bytesWritten > 0)
                {

                }
                
            }
            else
            {
               
            }
        }

        public static async void SerialListen()
        {
            try
            {
                if(StaticPropertiescs.serialPort != null)
                {
                    StaticPropertiescs.dataReaderObject = new DataReader(StaticPropertiescs.serialPort.InputStream);

                    while(true)
                    {
                        await ReadAsync(StaticPropertiescs.ReadCancellationTokenSource.Token);
                    }
                }
            }
            catch
            {

            }
            finally
            {
                if (StaticPropertiescs.dataReaderObject != null)
                {
                    StaticPropertiescs.dataReaderObject.DetachStream();
                    StaticPropertiescs.dataReaderObject = null;
                }
            }
        }

        public static async Task ReadAsync(CancellationToken cancellationToken)
        {
            Task<UInt32> loadAsyncTask;

            uint ReadBufferLength = 1024;

            // If task cancellation was requested, comply
            cancellationToken.ThrowIfCancellationRequested();

            // Set InputStreamOptions to complete the asynchronous read operation when one or more bytes is available
            StaticPropertiescs.dataReaderObject.InputStreamOptions = InputStreamOptions.Partial;

            // Create a task object to wait for data on the serialPort.InputStream
            loadAsyncTask = StaticPropertiescs.dataReaderObject.LoadAsync(ReadBufferLength).AsTask(cancellationToken);

            // Launch the task and wait
            UInt32 bytesRead = await loadAsyncTask;
            if (bytesRead > 0)
            {

            }
        }
    }
}
