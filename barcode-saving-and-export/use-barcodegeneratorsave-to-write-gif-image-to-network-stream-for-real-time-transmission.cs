// Title: Write barcode GIF to network stream using BarcodeGenerator.Save
// Description: Demonstrates generating a Code128 barcode and sending it as a GIF image over a TCP connection in real‑time.
// Category-Description: This example belongs to the Aspose.BarCode image generation and network transmission category. It showcases the use of BarcodeGenerator, its Parameters, and the Save method with BarCodeImageFormat to produce barcode images directly to streams. Typical scenarios include real‑time barcode delivery to remote services, printers, or web clients where immediate transmission is required. Developers often need to generate barcodes on‑the‑fly and stream them without intermediate files.
// Prompt: Use BarcodeGenerator.Save to write a GIF image to a network stream for real‑time transmission.
// Tags: barcode, code128, gif, network, stream, save, aspnet, aspose.barcode, generation

using System;
using System.IO;
using System.Net.Sockets;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode and transmitting it as a GIF image over a TCP connection.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Connects to a TCP server and streams the generated barcode.
    /// </summary>
    static void Main()
    {
        // Server details for the network transmission
        string server = "127.0.0.1";
        int port = 5000;

        // Create a barcode generator for Code128 with sample text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Optional: set a higher resolution for better image quality
            generator.Parameters.Resolution = 300;

            try
            {
                // Establish a TCP connection to the server
                using (TcpClient client = new TcpClient())
                {
                    client.Connect(server, port);

                    // Obtain the network stream for writing data
                    using (NetworkStream networkStream = client.GetStream())
                    {
                        // Save the barcode directly to the network stream as a GIF image
                        generator.Save(networkStream, BarCodeImageFormat.Gif);
                        networkStream.Flush(); // Ensure all data is sent
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during transmission
                Console.WriteLine("Error transmitting barcode: " + ex.Message);
            }
        }
    }
}