// Title: Export barcode recognition state to XML over a network socket
// Description: Generates a barcode, recognizes it, exports the recognition state as XML, and transmits it via a TCP socket.
// Prompt: Demonstrate how to use ExportToXml(Stream) to send barcode recognition state over a network socket.
// Tags: barcode, recognition, export, xml, network, socket, aspose

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a barcode, recognizing it, exporting the recognition state to XML,
/// and sending that XML over a TCP socket.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the barcode generation, recognition, export, and network transmission.
    /// </summary>
    static void Main()
    {
        // Generate a simple Code128 barcode image in memory.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
            {
                // Create a reader for the generated image and perform recognition of all supported types.
                using (var reader = new BarCodeReader(barcodeImage, DecodeType.AllSupportedTypes))
                {
                    // Output each detected barcode to the console.
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"Detected: {result.CodeTypeName} - {result.CodeText}");
                    }

                    // Export the recognition state to a memory stream in XML format.
                    using (var stateStream = new MemoryStream())
                    {
                        bool exported = reader.ExportToXml(stateStream);
                        Console.WriteLine($"Exported to XML: {exported}");

                        // Set up a TCP listener that will act as the server receiving the XML data.
                        using (var listener = new TcpListener(IPAddress.Loopback, 5000))
                        {
                            listener.Start();

                            // Accept the incoming connection on a background task.
                            Task acceptTask = Task.Run(() =>
                            {
                                using (TcpClient serverClient = listener.AcceptTcpClient())
                                using (NetworkStream serverStream = serverClient.GetStream())
                                using (var receivedStream = new MemoryStream())
                                {
                                    // Copy the incoming XML data into a memory stream.
                                    serverStream.CopyTo(receivedStream);
                                    Console.WriteLine($"Server received {receivedStream.Length} bytes of XML data.");
                                }
                            });

                            // Connect as a client and send the XML data over the socket.
                            using (var client = new TcpClient())
                            {
                                client.Connect(IPAddress.Loopback, 5000);
                                using (NetworkStream clientStream = client.GetStream())
                                {
                                    // Reset the position of the state stream before sending.
                                    stateStream.Position = 0;
                                    stateStream.CopyTo(clientStream);
                                    Console.WriteLine("Client sent XML data over the socket.");
                                }
                            }

                            // Wait for the server side to finish processing the received data.
                            acceptTask.Wait();
                            listener.Stop();
                        }
                    }
                }
            }
        }
    }
}