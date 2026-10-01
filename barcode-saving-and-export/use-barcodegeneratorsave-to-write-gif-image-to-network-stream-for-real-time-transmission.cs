// Title: Generate Code128 Barcode as GIF and Save to Stream
// Description: Demonstrates creating a Code128 barcode, rendering it as a GIF image, and saving it to a memory stream for potential network transmission.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator with EncodeTypes and BarCodeImageFormat to produce barcode images. Typical use cases include generating barcodes on‑the‑fly for web services, printing, or streaming to clients. Developers often need to configure barcode parameters, render to a stream, and then transmit the image via network protocols.
// Prompt: Use BarcodeGenerator.Save to write a GIF image to a network stream for real‑time transmission.
// Tags: code128, barcode generation, gif, stream, network transmission, aspose.barcode, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode, saves it as a GIF into a memory stream,
/// and demonstrates how the stream could be transmitted over a network.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes the GIF to a temporary file.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the barcode.
        string codeText = "1234567890";

        // Initialize the barcode generator with Code128 symbology and the sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Configure the barcode to display human‑readable text below the bars (optional).
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Create a memory stream to hold the generated GIF image.
            using (var memoryStream = new MemoryStream())
            {
                // Render the barcode into the memory stream in GIF format.
                generator.Save(memoryStream, BarCodeImageFormat.Gif);
                // Reset the stream position to the beginning for subsequent reads.
                memoryStream.Position = 0;

                // In a real application you would write the stream to a NetworkStream,
                // e.g., obtained from a TcpClient, for real‑time transmission.
                // Example (commented out because no server is available in this runner):
                // using (var client = new TcpClient("server.example.com", 9000))
                // using (var networkStream = client.GetStream())
                // {
                //     memoryStream.CopyTo(networkStream);
                // }

                // For demonstration purposes, write the GIF to a temporary file on disk.
                string tempPath = Path.Combine(Path.GetTempPath(), "barcode.gif");
                using (var fileStream = new FileStream(tempPath, FileMode.Create, FileAccess.Write))
                {
                    memoryStream.CopyTo(fileStream);
                }

                // Inform the user where the GIF file was saved.
                Console.WriteLine($"Barcode GIF generated and saved to: {tempPath}");
            }
        }
    }
}