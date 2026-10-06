// Title: Write barcode GIF to network stream using BarcodeGenerator.Save
// Description: Demonstrates generating a Code128 barcode, saving it as a GIF to a memory stream, and transmitting it over a TCP connection in real time.
// Category-Description: This example belongs to the Aspose.BarCode generation and output category, showing how to use BarcodeGenerator with BarCodeImageFormat to create image files and send them through network streams. It highlights key classes such as BarcodeGenerator, EncodeTypes, BarCodeImageFormat, and .NET networking types (TcpListener, TcpClient, NetworkStream). Developers often need to generate barcodes on the fly and deliver them to remote clients or services without writing to disk.
// Prompt: Use BarcodeGenerator.Save to write a GIF image to a network stream for real‑time transmission.
// Tags: barcode generation, code128, gif, network stream, tcp, aspose.barcode, barcodelibrary

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode, saves it as a GIF,
/// and streams the image to a connected TCP client in real time.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Sets up a TCP listener, creates a barcode,
    /// and sends the resulting GIF image to the first client that connects.
    /// </summary>
    static void Main()
    {
        const string host = "127.0.0.1";
        const int port = 5000;
        const string codeText = "12345678";

        // Initialize a TCP listener to accept a single client connection.
        using (TcpListener listener = new TcpListener(IPAddress.Parse(host), port))
        {
            listener.Start();
            Console.WriteLine($"Listening on {host}:{port}...");

            // Block until a client connects.
            using (TcpClient client = listener.AcceptTcpClient())
            {
                Console.WriteLine("Client connected.");

                // Obtain the network stream for sending data to the client.
                using (NetworkStream networkStream = client.GetStream())
                {
                    // Create a barcode generator for Code128 with the specified text.
                    using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
                    {
                        // Use a memory stream as an intermediate buffer (must be seekable).
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            // Save the generated barcode as a GIF image into the memory stream.
                            generator.Save(memoryStream, BarCodeImageFormat.Gif);
                            memoryStream.Position = 0; // Reset position for reading.

                            // Copy the GIF data from the memory stream to the network stream.
                            memoryStream.CopyTo(networkStream);
                            networkStream.Flush(); // Ensure all data is sent.

                            Console.WriteLine("Barcode GIF sent to client.");
                        }
                    }
                }
            }

            // Stop listening for additional connections.
            listener.Stop();
        }
    }
}