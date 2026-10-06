// Title: Generate Dutch KIX barcode and transmit via network stream
// Description: Demonstrates creating a Dutch KIX barcode with Aspose.BarCode, saving it as PNG, and sending the image over a TCP network stream.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure barcode parameters, render the image to a memory stream, and transmit it using .NET networking classes such as TcpListener and TcpClient. Developers working with barcode creation for printing, data exchange, or real‑time distribution can use these patterns to integrate barcode images into networked applications.
// Prompt: Use a barcode generator to produce a Dutch KIX barcode and write the image to a network stream.
// Tags: dutchkix, barcode generation, png, aspose.barcode, network stream, tcp

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Dutch KIX barcode, saves it as a PNG image,
/// and sends the image over a TCP network stream using a loopback connection.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and transmits it.
    /// </summary>
    static void Main()
    {
        // Sample data for Dutch KIX barcode
        string codeText = "123456ASPOSE";

        // Create a barcode generator for the Dutch KIX symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.DutchKIX, codeText))
        {
            // Optional appearance settings: set module size and bar height
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Parameters.Barcode.BarHeight.Pixels = 50;

            // Render the barcode into a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // Set up a TCP listener on an OS‑assigned free port (loopback address)
                var listener = new TcpListener(IPAddress.Loopback, 0);
                listener.Start();
                int port = ((IPEndPoint)listener.LocalEndpoint).Port;

                // Connect a client to the listener (simulating a remote consumer)
                using (var client = new TcpClient())
                {
                    client.Connect(IPAddress.Loopback, port);

                    // Accept the incoming connection on the server side
                    using (var serverClient = listener.AcceptTcpClient())
                    using (NetworkStream networkStream = serverClient.GetStream())
                    {
                        // Transfer the barcode image bytes to the network stream
                        ms.CopyTo(networkStream);
                        networkStream.Flush();
                    }
                }

                // Clean up the listener
                listener.Stop();
                Console.WriteLine("Barcode image sent over network stream.");
            }
        }
    }
}