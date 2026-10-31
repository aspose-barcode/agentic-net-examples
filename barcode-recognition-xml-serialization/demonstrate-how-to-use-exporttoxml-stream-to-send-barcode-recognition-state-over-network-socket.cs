// Title: Export barcode recognition state to XML and transmit via TCP socket
// Description: Demonstrates exporting the recognition state of a barcode to an XML stream and sending it over a network socket using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, showcasing how to use BarCodeReader to capture recognition state, serialize it with ExportToXml, and transmit it via standard .NET networking classes. Developers often need to share barcode processing results between services or store them for later analysis; this pattern illustrates the key API classes (BarCodeReader, ExportToXml) combined with TcpListener/TcpClient for real‑time data exchange.
// Prompt: Demonstrate how to use ExportToXml(Stream) to send barcode recognition state over a network socket.
// Tags: code128, export, xml, barcodegenerator, barcodereader, tcplistener, tcpclient

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates exporting barcode recognition state to XML and transmitting it over a TCP socket.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates a barcode, exports its recognition state to XML,
    /// and sends the XML data to a client via a TCP connection.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Generate a temporary barcode image (required for a full recognition state)
        // --------------------------------------------------------------------
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "sample.png");

        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // 2. Configure BarCodeReader and export its state to a memory stream
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader())
        {
            // Example settings – adjust as needed for your scenario
            reader.BarcodeSettings.StripFNC = true;
            reader.QualitySettings.XDimension = XDimensionMode.Small;

            // Load the image so the reader can build a complete state object
            reader.SetBarCodeImage(barcodePath);

            using (var stateStream = new MemoryStream())
            {
                // Serialize the recognition state to XML
                reader.ExportToXml(stateStream);
                stateStream.Position = 0; // Reset for reading

                // ----------------------------------------------------------------
                // 3. Set up a TCP listener to send the XML state to a client
                // ----------------------------------------------------------------
                int port = 5000;
                using (var listener = new TcpListener(IPAddress.Loopback, port))
                {
                    listener.Start();

                    // ------------------------------------------------------------
                    // 3a. Start a client thread that connects and reads the XML data
                    // ------------------------------------------------------------
                    Thread clientThread = new Thread(() =>
                    {
                        try
                        {
                            using (var client = new TcpClient())
                            {
                                client.Connect(IPAddress.Loopback, port);
                                using (var netStream = client.GetStream())
                                using (var received = new MemoryStream())
                                {
                                    netStream.CopyTo(received);
                                    Console.WriteLine($"Received XML state size: {received.Length} bytes");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Client error: " + ex.Message);
                        }
                    });
                    clientThread.Start();

                    // ------------------------------------------------------------
                    // 3b. Accept the client connection and transmit the XML state
                    // ------------------------------------------------------------
                    using (var serverClient = listener.AcceptTcpClient())
                    using (var serverStream = serverClient.GetStream())
                    {
                        stateStream.CopyTo(serverStream);
                        serverStream.Flush();
                    }

                    // Wait for the client to finish processing
                    clientThread.Join();
                    listener.Stop();
                }
            }
        }

        // --------------------------------------------------------------------
        // 4. Clean up temporary files and directories
        // --------------------------------------------------------------------
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored – cleanup failures should not affect the demo outcome
        }

        Console.WriteLine("Demo completed.");
    }
}