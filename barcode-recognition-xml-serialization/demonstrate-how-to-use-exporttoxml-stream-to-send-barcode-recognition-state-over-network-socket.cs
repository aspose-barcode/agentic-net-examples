// Title: Export barcode recognition state to XML and transmit via TCP socket
// Description: Demonstrates generating a barcode, exporting the BarCodeReader state to XML, sending it over a network socket, and importing it back to read the barcode.
// Category-Description: This example belongs to the Aspose.BarCode recognition and serialization category. It shows how to use BarCodeReader, ExportToXml, and ImportFromXml to persist and transfer recognition settings and state. Typical use cases include distributed barcode processing, remote diagnostics, and client‑server barcode validation where developers need to serialize reader configuration and results for network transmission.
// Prompt: Demonstrate how to use ExportToXml(Stream) to send barcode recognition state over a network socket.
// Tags: barcode, code128, exporttoxml, importfromxml, tcp, networking, aspose.barcode, recognition, serialization

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a barcode, exports the recognition state to XML,
/// transmits it over a TCP socket, and then imports the state to read the barcode again.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the end‑to‑end workflow without requiring user interaction.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Prepare a temporary folder for the generated barcode image.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "barcode.png");

        // --------------------------------------------------------------------
        // 2. Generate a sample Code128 barcode and save it as PNG.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // 3. Initialize BarCodeReader, configure settings, and load the image.
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader())
        {
            reader.BarcodeSettings.StripFNC = true;
            reader.QualitySettings.XDimension = XDimensionMode.Small;
            reader.SetBarCodeImage(barcodePath);

            // ----------------------------------------------------------------
            // 4. Export the reader's state (including settings) to a MemoryStream as XML.
            // ----------------------------------------------------------------
            using (var exportStream = new MemoryStream())
            {
                reader.ExportToXml(exportStream);
                exportStream.Position = 0;
                byte[] xmlData = exportStream.ToArray();

                // ----------------------------------------------------------------
                // 5. Start a TCP listener that will send the XML data to a client.
                // ----------------------------------------------------------------
                int port;
                using (var listener = new TcpListener(IPAddress.Loopback, 0))
                {
                    listener.Start();
                    port = ((IPEndPoint)listener.LocalEndpoint).Port;

                    Thread serverThread = new Thread(() =>
                    {
                        using (var client = listener.AcceptTcpClient())
                        using (var networkStream = client.GetStream())
                        {
                            networkStream.Write(xmlData, 0, xmlData.Length);
                        }
                    });
                    serverThread.Start();

                    // ----------------------------------------------------------------
                    // 6. Client connects to the listener and receives the XML data.
                    // ----------------------------------------------------------------
                    using (var client = new TcpClient())
                    {
                        client.Connect(IPAddress.Loopback, port);
                        using (var networkStream = client.GetStream())
                        using (var receivedStream = new MemoryStream())
                        {
                            byte[] buffer = new byte[4096];
                            int bytesRead;
                            while ((bytesRead = networkStream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                receivedStream.Write(buffer, 0, bytesRead);
                            }
                            receivedStream.Position = 0;

                            // ----------------------------------------------------------------
                            // 7. Import the reader state from the received XML and read the barcode.
                            // ----------------------------------------------------------------
                            using (var importedReader = BarCodeReader.ImportFromXml(receivedStream))
                            {
                                importedReader.SetBarCodeImage(barcodePath);
                                var results = importedReader.ReadBarCodes();
                                Console.WriteLine($"Barcodes read after import: {results.Length}");
                                foreach (var result in results)
                                {
                                    Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                                }
                            }
                        }
                    }

                    // Wait for the server thread to finish and clean up the listener.
                    serverThread.Join();
                    listener.Stop();
                }
            }
        }

        // --------------------------------------------------------------------
        // 8. Clean up temporary files and directory.
        // --------------------------------------------------------------------
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}