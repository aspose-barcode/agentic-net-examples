// Title: Save Barcode as GIF to Network Stream for Real‑Time Transmission
// Description: Demonstrates generating a Code128 barcode, saving it as a GIF image, and sending it over a TCP network stream in real time.
// Category-Description: This example belongs to the Aspose.BarCode generation and image output category. It shows how to use BarcodeGenerator together with BarCodeImageFormat to create barcode images, and how to transmit them via sockets. Developers working with barcode generation for web services, IoT devices, or real‑time applications often need to stream barcode images directly to clients without writing to disk.
// Prompt: Use BarcodeGenerator.Save to write a GIF image to a network stream for real‑time transmission.
// Tags: barcode generation, code128, gif, network stream, tcp, aspose.barcode, save, real-time transmission

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode, saves it as a GIF,
/// and transmits it over a TCP network stream in real time.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Sets up a TCP listener, sends a generated GIF barcode,
    /// and receives it on the server side for verification.
    /// </summary>
    static void Main()
    {
        // Initialize a TCP listener on the loopback interface with an OS‑assigned port.
        using (TcpListener listener = new TcpListener(IPAddress.Loopback, 0))
        {
            listener.Start();
            int port = ((IPEndPoint)listener.LocalEndpoint).Port;

            // Run the server side in a background task to accept the incoming connection.
            Task serverTask = Task.Run(() =>
            {
                // Accept the client connection and obtain its network stream.
                using (TcpClient serverClient = listener.AcceptTcpClient())
                using (NetworkStream serverStream = serverClient.GetStream())
                using (MemoryStream received = new MemoryStream())
                {
                    // Copy the incoming data (the GIF barcode) into a memory buffer.
                    serverStream.CopyTo(received);
                    Console.WriteLine($"Received {received.Length} bytes of GIF barcode.");
                }
            });

            // Create a client, connect to the server, and send the generated barcode.
            using (TcpClient client = new TcpClient())
            {
                client.Connect(IPAddress.Loopback, port);
                using (NetworkStream clientStream = client.GetStream())
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
                {
                    // Save the barcode as a GIF into a temporary memory stream.
                    using (MemoryStream tempStream = new MemoryStream())
                    {
                        generator.Save(tempStream, BarCodeImageFormat.Gif);
                        // Reset the stream position before copying.
                        tempStream.Position = 0;
                        // Transmit the GIF data to the server over the network stream.
                        tempStream.CopyTo(clientStream);
                    }
                }
            }

            // Wait for the server task to complete processing.
            serverTask.Wait();
        }
    }
}