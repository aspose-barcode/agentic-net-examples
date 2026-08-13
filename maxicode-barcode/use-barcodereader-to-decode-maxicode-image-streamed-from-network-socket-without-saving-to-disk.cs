// Title: Decode MaxiCode barcode from a network stream using BarCodeReader
// Description: Demonstrates how to generate a MaxiCode barcode, transmit it over a TCP socket, and decode it directly from the received stream without writing to disk.
// Category-Description: This example belongs to the Aspose.BarCode reading category, showcasing in‑memory barcode processing. It uses BarcodeGenerator to create a barcode, TcpListener/TcpClient for network transmission, and BarCodeReader with DecodeType.MaxiCode to extract data. Developers working with real‑time barcode scanning, networked devices, or streaming scenarios can adapt this pattern for efficient, disk‑free decoding.
// Prompt: Use the BarcodeReader to decode a MaxiCode image streamed from a network socket without saving to disk.
// Tags: maxicode, barcode reading, streaming, network socket, aspose.barcode, in‑memory processing

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a MaxiCode barcode, sends it over a TCP socket,
/// and decodes it directly from the received stream using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Performs in‑memory barcode generation, network transmission,
    /// and decoding without persisting any files to disk.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Generate a sample MaxiCode barcode image into a memory stream.
        // ------------------------------------------------------------
        byte[] imageBytes;
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Test MaxiCode"))
        {
            // Set visual appearance of the barcode.
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the generated image to a temporary memory stream.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                imageBytes = ms.ToArray(); // Capture the raw PNG bytes.
            }
        }

        // ------------------------------------------------------------
        // 2. Set up a TCP listener on a dynamic (ephemeral) port.
        // ------------------------------------------------------------
        int port;
        using (var listener = new TcpListener(IPAddress.Loopback, 0))
        {
            listener.Start();
            port = ((IPEndPoint)listener.LocalEndpoint).Port;

            // --------------------------------------------------------
            // 3. Start a client thread that connects to the listener and
            //    streams the generated image bytes.
            // --------------------------------------------------------
            var clientThread = new Thread(() =>
            {
                using (var client = new TcpClient())
                {
                    client.Connect(IPAddress.Loopback, port);
                    using (var netStream = client.GetStream())
                    {
                        netStream.Write(imageBytes, 0, imageBytes.Length);
                    }
                }
            });
            clientThread.Start();

            // --------------------------------------------------------
            // 4. Accept the incoming connection and read the image data
            //    into a memory stream for decoding.
            // --------------------------------------------------------
            using (var serverClient = listener.AcceptTcpClient())
            using (var netStream = serverClient.GetStream())
            using (var receivedMs = new MemoryStream())
            {
                netStream.CopyTo(receivedMs);
                receivedMs.Position = 0; // Reset position for reading.

                // ----------------------------------------------------
                // 5. Decode the received image using BarCodeReader for
                //    MaxiCode without writing to disk.
                // ----------------------------------------------------
                using (var reader = new BarCodeReader(receivedMs, DecodeType.MaxiCode))
                {
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"Decoded Text: {result.CodeText}");
                        var bounds = result.Region.Rectangle;
                        Console.WriteLine($"Region - X:{bounds.X}, Y:{bounds.Y}, Width:{bounds.Width}, Height:{bounds.Height}");
                    }
                }
            }

            // ------------------------------------------------------------
            // 6. Clean up: ensure the client thread finishes and stop the listener.
            // ------------------------------------------------------------
            clientThread.Join();
            listener.Stop();
        }
    }
}