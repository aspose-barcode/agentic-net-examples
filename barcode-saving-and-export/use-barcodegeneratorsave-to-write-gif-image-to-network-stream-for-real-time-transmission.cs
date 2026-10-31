// Title: Transmit a Code128 barcode as GIF over a TCP network stream
// Description: Demonstrates generating a Code128 barcode, encoding it as a GIF image, and sending it through a TCP socket in real time.
// Category-Description: This example belongs to the Aspose.BarCode image generation and network transmission category. It showcases the use of BarcodeGenerator, BarCodeImageFormat, and standard .NET networking classes (TcpListener, TcpClient) to create a barcode image and stream it to a client. Developers often need to deliver barcodes instantly over networks for POS systems, mobile apps, or web services, and this pattern illustrates a simple real‑time transmission scenario.
// Prompt: Use BarcodeGenerator.Save to write a GIF image to a network stream for real‑time transmission.
// Tags: barcode, code128, gif, network, tcp, streaming, aspose.barcode, generation, save

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode as a GIF image and transmitting it over a TCP network stream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, starts a TCP listener, connects a client, and streams the image.
    /// </summary>
    static void Main()
    {
        // Generate a Code128 barcode and store the GIF bytes in memory
        byte[] barcodeBytes;
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            using (var ms = new MemoryStream())
            {
                // Save the barcode image directly to the memory stream in GIF format
                generator.Save(ms, BarCodeImageFormat.Gif);
                barcodeBytes = ms.ToArray();
            }
        }

        // Configure a TCP listener on the loopback address (localhost) and a fixed port
        const int port = 5000;
        var localEndPoint = new IPEndPoint(IPAddress.Loopback, port);
        using (var listener = new TcpListener(localEndPoint))
        {
            listener.Start();

            // Create a client that connects to the listener (simulating a remote consumer)
            using (var client = new TcpClient())
            {
                client.Connect(IPAddress.Loopback, port);

                // Accept the incoming client connection on the server side
                using (var serverClient = listener.AcceptTcpClient())
                {
                    // Obtain the network stream for the accepted connection
                    using (var networkStream = serverClient.GetStream())
                    {
                        // Transmit the GIF image bytes over the network stream
                        networkStream.Write(barcodeBytes, 0, barcodeBytes.Length);
                        networkStream.Flush();
                    }
                }
            }

            // Stop listening for additional connections
            listener.Stop();
        }

        Console.WriteLine("Barcode GIF image transmitted over network stream.");
    }
}