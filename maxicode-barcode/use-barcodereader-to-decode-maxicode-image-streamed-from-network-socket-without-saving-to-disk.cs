// Title: Decode MaxiCode barcode from a network stream using Aspose.BarCode
// Description: Demonstrates how to generate a MaxiCode barcode, stream it over a TCP socket, and decode it directly from the received stream without writing to disk.
// Category-Description: This example belongs to the Aspose.BarCode barcode reading and generation category. It showcases the use of BarcodeGenerator for creating barcodes, TcpListener/TcpClient for network streaming, and BarCodeReader for decoding. Developers working with real‑time barcode transmission, such as point‑of‑sale or logistics systems, can use this pattern to process barcodes on the fly without intermediate files.
// Prompt: Use the BarcodeReader to decode a MaxiCode image streamed from a network socket without saving to disk.
// Tags: maxicode, barcode, decoding, network, stream, aspose.barcode, generation, reading

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
/// Example program that generates a MaxiCode barcode, streams it over a TCP socket,
/// and decodes it directly from the received stream using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, sends it via a TCP listener, receives it,
    /// and decodes the barcode without persisting the image to disk.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Generate a MaxiCode barcode image and store it in a byte array.
        // ------------------------------------------------------------
        byte[] imageBytes;
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "1234567890"))
        {
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                imageBytes = ms.ToArray();
            }
        }

        // ------------------------------------------------------------
        // 2. Set up a TCP listener on a dynamic port to act as the server.
        // ------------------------------------------------------------
        int port;
        using (var listener = new TcpListener(IPAddress.Loopback, 0))
        {
            listener.Start();
            port = ((IPEndPoint)listener.LocalEndpoint).Port;

            // --------------------------------------------------------
            // 3. Server thread: accept a single client connection and
            //    write the generated image bytes to the network stream.
            // --------------------------------------------------------
            Thread serverThread = new Thread(() =>
            {
                using (var client = listener.AcceptTcpClient())
                using (var networkStream = client.GetStream())
                {
                    networkStream.Write(imageBytes, 0, imageBytes.Length);
                }
                listener.Stop();
            });
            serverThread.Start();

            // --------------------------------------------------------
            // 4. Client side: connect to the server and read the image
            //    bytes into a MemoryStream.
            // --------------------------------------------------------
            using (var client = new TcpClient())
            {
                client.Connect(IPAddress.Loopback, port);
                using (var networkStream = client.GetStream())
                using (var receivedStream = new MemoryStream())
                {
                    networkStream.CopyTo(receivedStream);
                    receivedStream.Position = 0; // Reset stream position for reading.

                    // ----------------------------------------------------
                    // 5. Decode the MaxiCode barcode directly from the stream.
                    // ----------------------------------------------------
                    using (var reader = new BarCodeReader(receivedStream, DecodeType.MaxiCode))
                    {
                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"CodeText: {result.CodeText}");
                            Console.WriteLine($"CodeTypeName: {result.CodeTypeName}");
                        }
                    }
                }
            }

            // ------------------------------------------------------------
            // 6. Wait for the server thread to finish before exiting.
            // ------------------------------------------------------------
            serverThread.Join();
        }
    }
}