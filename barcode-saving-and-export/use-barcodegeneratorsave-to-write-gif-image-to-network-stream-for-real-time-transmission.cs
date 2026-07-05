// Title: Generate Code128 Barcode GIF and Send via TCP
// Description: Creates a Code128 barcode, saves it as a GIF directly to a network stream, and transmits it to a TCP server in real time.
// Prompt: Use BarcodeGenerator.Save to write a GIF image to a network stream for real‑time transmission.
// Tags: barcode, code128, gif, network, tcp, aspnet, aspose.barcode, generation

using System;
using System.Net.Sockets;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode as a GIF and sending it over a TCP connection.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and streams it to a TCP server.
    /// </summary>
    static void Main()
    {
        // Barcode data to encode
        const string codeText = "1234567890";

        // Destination server details (modify as required)
        const string host = "localhost";
        const int port = 5000;

        try
        {
            // Establish a TCP connection to the target server
            using (TcpClient client = new TcpClient())
            {
                client.Connect(host, port);

                // Obtain the network stream for sending data
                using (NetworkStream networkStream = client.GetStream())
                {
                    // Initialize the barcode generator for Code128 symbology
                    using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
                    {
                        // Directly write the generated barcode as a GIF to the network stream
                        generator.Save(networkStream, BarCodeImageFormat.Gif);
                    }
                }
            }

            Console.WriteLine("Barcode GIF sent successfully.");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during the process
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}