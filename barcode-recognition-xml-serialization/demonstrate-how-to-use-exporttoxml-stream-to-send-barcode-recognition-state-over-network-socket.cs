using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a barcode generator and generate a bitmap image
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
            {
                // Initialize a barcode reader with the generated image and decode type
                using (var reader = new BarCodeReader(barcodeImage, DecodeType.Code128))
                {
                    // Perform recognition (required before exporting state)
                    reader.ReadBarCodes();

                    // Export the recognition state to a memory stream as XML
                    using (var xmlStream = new MemoryStream())
                    {
                        bool exported = reader.ExportToXml(xmlStream);
                        if (!exported)
                        {
                            Console.WriteLine("Failed to export recognition state to XML.");
                            return;
                        }

                        // Prepare a TCP listener on a free port
                        using (var listener = new TcpListener(IPAddress.Loopback, 0))
                        {
                            listener.Start();
                            int port = ((IPEndPoint)listener.LocalEndpoint).Port;

                            // Start a client that connects to the listener
                            Task clientTask = Task.Run(() =>
                            {
                                using (var client = new TcpClient())
                                {
                                    client.Connect(IPAddress.Loopback, port);
                                    // Read the incoming XML (optional, just to consume the data)
                                    using (var ns = client.GetStream())
                                    using (var ms = new MemoryStream())
                                    {
                                        ns.CopyTo(ms);
                                    }
                                }
                            });

                            // Accept the incoming client connection on the server side
                            using (var serverClient = listener.AcceptTcpClient())
                            using (var networkStream = serverClient.GetStream())
                            {
                                // Reset the XML stream position and send its contents over the network
                                xmlStream.Position = 0;
                                xmlStream.CopyTo(networkStream);
                            }

                            // Ensure the client task completes
                            clientTask.Wait();
                        }
                    }
                }
            }
        }
    }
}