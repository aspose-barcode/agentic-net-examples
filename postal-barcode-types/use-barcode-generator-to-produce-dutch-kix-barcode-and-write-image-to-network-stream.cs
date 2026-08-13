// Title: Generate Dutch KIX Barcode and Send via Network Stream
// Description: Demonstrates how to generate a Dutch KIX barcode using Aspose.BarCode and transmit the PNG image over a TCP network stream.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to create barcodes. Typical scenarios include producing barcode images on-the-fly and delivering them to remote services or devices via network streams. Developers working with real‑time barcode distribution often need to serialize images directly to sockets, making this pattern a common building block.
// Prompt: Use a barcode generator to produce a Dutch KIX barcode and write the image to a network stream.
// Tags: dutch kix, barcode generation, network stream, tcp, png, aspose.barcode, csharp

using System;
using System.Net.Sockets;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a Dutch KIX barcode and sends the PNG image over a TCP connection.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode and writes it to a network stream.
    /// </summary>
    static void Main()
    {
        // The data to encode in the Dutch KIX barcode.
        const string codeText = "1234567890";

        // Network endpoint configuration (example uses localhost on port 5000).
        const string host = "127.0.0.1";
        const int port = 5000;

        try
        {
            // Initialize the barcode generator for Dutch KIX symbology.
            using (var generator = new BarcodeGenerator(EncodeTypes.DutchKIX, codeText))
            {
                // Establish a TCP connection to the specified host and port.
                using (var client = new TcpClient())
                {
                    client.Connect(host, port);

                    // Obtain the network stream associated with the TCP client.
                    using (NetworkStream networkStream = client.GetStream())
                    {
                        // Serialize the barcode image directly to the network stream in PNG format.
                        generator.Save(networkStream, BarCodeImageFormat.Png);
                        Console.WriteLine("Barcode image sent to {0}:{1}", host, port);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Output any errors that occur during generation or transmission.
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}