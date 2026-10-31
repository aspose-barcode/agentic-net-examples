// Title: Generate Dutch KIX barcode and transmit via network stream
// Description: Demonstrates creating a Dutch KIX barcode image using Aspose.BarCode and sending it over a TCP network stream.
// Category-Description: This example belongs to the Aspose.BarCode generation and output category, illustrating how to use the BarcodeGenerator class to produce barcodes, save them to a memory stream, and transmit the image data over network sockets. Typical use cases include real‑time barcode delivery to client applications, printing services, or remote devices. Developers often need to combine barcode generation with networking APIs to integrate barcode imaging into distributed systems.
// Prompt: Use a barcode generator to produce a Dutch KIX barcode and write the image to a network stream.
// Tags: dutch kix, barcode generation, network stream, tcp, aspose.barcode, png, memorystream

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Dutch KIX barcode and sending it over a TCP network stream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, writes it to a memory stream, and transmits it via a network stream.
    /// </summary>
    static void Main()
    {
        // Define the barcode text to encode
        const string codeText = "123456ASPOSE";

        // Create a seekable memory stream to hold the generated barcode image
        using (var memoryStream = new MemoryStream())
        {
            // Generate the Dutch KIX barcode and save it as PNG into the memory stream
            using (var generator = new BarcodeGenerator(EncodeTypes.DutchKIX, codeText))
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);
            }

            // Reset stream position to the beginning for reading
            memoryStream.Position = 0;

            // Set up a TCP listener on localhost at the specified port
            const int port = 5000;
            using (var listener = new TcpListener(IPAddress.Loopback, port))
            {
                listener.Start();

                // Begin accepting a client connection asynchronously
                Task<TcpClient> acceptTask = listener.AcceptTcpClientAsync();

                // Connect a client to the listener (acts as the sender)
                using (var client = new TcpClient())
                {
                    client.Connect(IPAddress.Loopback, port);

                    // Retrieve the server-side client once the connection is accepted
                    using (TcpClient serverClient = acceptTask.GetAwaiter().GetResult())
                    {
                        // Obtain the network stream from the server-side client
                        using (NetworkStream networkStream = serverClient.GetStream())
                        {
                            // Transfer the barcode image bytes from the memory stream to the network stream
                            memoryStream.CopyTo(networkStream);
                            networkStream.Flush();
                        }
                    }
                }

                // Stop listening for further connections
                listener.Stop();
            }
        }

        Console.WriteLine("Dutch KIX barcode generated and sent over network stream.");
    }
}