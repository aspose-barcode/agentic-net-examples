using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation, recognition, and transmission of recognition data over a TCP socket.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Code128 barcode, reads it, exports the recognition result to XML,
    /// and simulates sending that XML over a network socket.
    /// </summary>
    static void Main()
    {
        // Generate a simple Code128 barcode and keep it in a memory stream.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            using (var imageStream = new MemoryStream())
            {
                // Save the generated barcode image as PNG into the memory stream.
                generator.Save(imageStream, BarCodeImageFormat.Png);
                // Reset stream position to the beginning for reading.
                imageStream.Position = 0;

                // Recognize the barcode from the image stream.
                using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
                {
                    // Iterate through all detected barcodes and output their type and text.
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"Detected: {result.CodeTypeName} - {result.CodeText}");
                    }

                    // Export the recognition state to an XML stream.
                    using (var xmlStream = new MemoryStream())
                    {
                        reader.ExportToXml(xmlStream);
                        // Reset stream position to the beginning for transmission.
                        xmlStream.Position = 0;

                        // ----- Simulate sending the XML over a network socket -----
                        const int port = 5000;
                        // Set up a TCP listener on the loopback address.
                        var listener = new TcpListener(IPAddress.Loopback, port);
                        listener.Start();

                        // Begin accepting an incoming client connection asynchronously.
                        var acceptTask = listener.AcceptTcpClientAsync();

                        // Client side: connect to the listener and send the XML data.
                        using (var client = new TcpClient())
                        {
                            client.Connect(IPAddress.Loopback, port);
                            using (var netStream = client.GetStream())
                            {
                                // Copy the XML bytes to the network stream.
                                xmlStream.CopyTo(netStream);
                            }
                        }

                        // Server side: receive the XML data from the client.
                        using (var serverClient = acceptTask.Result)
                        using (var serverStream = serverClient.GetStream())
                        using (var receivedMs = new MemoryStream())
                        {
                            // Copy received bytes into a memory stream.
                            serverStream.CopyTo(receivedMs);
                            // Convert the received bytes to a UTF-8 string.
                            string receivedXml = System.Text.Encoding.UTF8.GetString(receivedMs.ToArray());
                            Console.WriteLine("Received XML:");
                            Console.WriteLine(receivedXml);
                        }

                        // Stop listening for further connections.
                        listener.Stop();
                        // ---------------------------------------------------------
                    }
                }
            }
        }
    }
}