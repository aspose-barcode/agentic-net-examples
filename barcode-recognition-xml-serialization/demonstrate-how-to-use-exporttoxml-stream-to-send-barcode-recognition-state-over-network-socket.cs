// Title: Export barcode recognition state to XML and transmit via TCP
// Description: Demonstrates generating a Code128 barcode, recognizing it, exporting the recognition state to XML, and sending that XML over a TCP socket.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to use BarcodeGenerator, BarCodeReader, and the ExportToXml(Stream) method. Typical use cases include transmitting barcode scan results between services or perserving recognition state. Developers often need to serialize recognition data for network communication or later analysis.
// Prompt: Demonstrate how to use ExportToXml(Stream) to send barcode recognition state over a network socket.
// Tags: code128, exporttoxml, xml, network, aspose.barcode, generation, recognition

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a barcode, recognizing it, exporting the recognition state to XML,
/// and transmitting that XML over a TCP socket using Aspose.BarCode APIs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the barcode generation, recognition, XML export,
    /// and network transmission steps.
    /// </summary>
    static void Main()
    {
        // Step 1: Generate a simple Code128 barcode image in memory.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            using (var barcodeStream = new MemoryStream())
            {
                // Save the barcode image to a memory stream (PNG format).
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0; // Reset stream position for reading.

                // Step 2: Create a BarCodeReader to recognize the barcode from the stream.
                using (var reader = new BarCodeReader(barcodeStream, DecodeType.AllSupportedTypes))
                {
                    // Perform recognition to populate internal state (optional).
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"Detected: {result.CodeTypeName} - {result.CodeText}");
                    }

                    // Step 3: Export the recognition state to an XML memory stream.
                    using (var xmlStateStream = new MemoryStream())
                    {
                        reader.ExportToXml(xmlStateStream);
                        xmlStateStream.Position = 0; // Prepare stream for sending.

                        // Step 4: Set up a TCP listener (server) on localhost.
                        const int port = 5000;
                        var listener = new TcpListener(IPAddress.Loopback, port);
                        listener.Start();

                        // Step 5: Start a client task that connects to the server.
                        var clientTask = Task.Run(() =>
                        {
                            using (var client = new TcpClient())
                            {
                                client.Connect(IPAddress.Loopback, port);
                                // Keep the connection open; no data is read in this demo.
                                using (var ns = client.GetStream())
                                {
                                    // Placeholder for potential client-side read logic.
                                }
                            }
                        });

                        // Step 6: Accept the client connection on the server side.
                        using (var serverClient = listener.AcceptTcpClient())
                        using (var networkStream = serverClient.GetStream())
                        {
                            // Send the XML state over the network stream.
                            xmlStateStream.CopyTo(networkStream);
                            networkStream.Flush();
                            Console.WriteLine("Barcode recognition state sent over network.");
                        }

                        // Clean up the listener.
                        listener.Stop();

                        // Ensure the client task completes before exiting.
                        clientTask.Wait();
                    }
                }
            }
        }
    }
}