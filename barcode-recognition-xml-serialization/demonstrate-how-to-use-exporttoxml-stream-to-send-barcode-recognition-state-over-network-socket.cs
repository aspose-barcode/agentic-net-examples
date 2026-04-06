using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Generate a barcode image in memory
        using (var barcodeStream = new MemoryStream())
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
            }

            // Prepare the image stream for reading
            barcodeStream.Position = 0;

            // Recognize the barcode and export the reader state to XML
            using (var reader = new BarCodeReader(barcodeStream, DecodeType.Code128))
            {
                // Perform recognition (result not used further here)
                reader.ReadBarCodes();

                // Export reader state to XML in a memory stream
                using (var xmlStateStream = new MemoryStream())
                {
                    bool exported = reader.ExportToXml(xmlStateStream);
                    if (!exported)
                    {
                        Console.WriteLine("Failed to export reader state to XML.");
                        return;
                    }

                    // Prepare XML data for sending
                    xmlStateStream.Position = 0;
                    byte[] xmlData = xmlStateStream.ToArray();

                    // Set up a TCP listener (server) on localhost
                    using (var listener = new TcpListener(IPAddress.Loopback, 5000))
                    {
                        listener.Start();

                        // Connect a client to the listener and send the XML data
                        using (var client = new TcpClient())
                        {
                            client.Connect(IPAddress.Loopback, 5000);
                            using (NetworkStream clientStream = client.GetStream())
                            {
                                clientStream.Write(xmlData, 0, xmlData.Length);
                            }
                        }

                        // Accept the incoming connection and read the XML data
                        using (TcpClient serverClient = listener.AcceptTcpClient())
                        using (NetworkStream serverStream = serverClient.GetStream())
                        using (var receivedStream = new MemoryStream())
                        {
                            serverStream.CopyTo(receivedStream);
                            string receivedXml = System.Text.Encoding.UTF8.GetString(receivedStream.ToArray());
                            Console.WriteLine("Received XML state:");
                            Console.WriteLine(receivedXml);
                        }

                        listener.Stop();
                    }
                }
            }
        }
    }
}