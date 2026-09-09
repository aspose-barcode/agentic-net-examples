// Title: Generate Dutch KIX Barcode and Write to a Network Stream
// Description: Demonstrates how to generate a Dutch KIX barcode using Aspose.BarCode and output the image to a stream that can be sent over a network.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator with EncodeTypes.DutchKIX. It shows typical steps such as setting barcode parameters, rendering to an image format, and writing the result to a stream. Developers working with barcode creation for POS or logistics often need to produce KIX barcodes and transmit them via network sockets or web services.
// Prompt: Use a barcode generator to produce a Dutch KIX barcode and write the image to a network stream.
// Tags: barcode, dutch kix, generation, image, png, stream, network, aspose.barcode, generatortypes

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Dutch KIX barcode and saving it to a stream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, configures appearance, and writes the PNG image to a memory stream.
    /// </summary>
    static void Main()
    {
        // Sample code text for Dutch KIX barcode
        string codeText = "123456ASPOSE";

        // Create the barcode generator for Dutch KIX
        using (var generator = new BarcodeGenerator(EncodeTypes.DutchKIX, codeText))
        {
            // Optional appearance settings
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;

            // Write the barcode image to a memory stream (simulating a network stream)
            using (var memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                memoryStream.Position = 0;
                Console.WriteLine($"Generated Dutch KIX barcode image size: {memoryStream.Length} bytes");

                // In a real network scenario, replace the MemoryStream with a NetworkStream, e.g.:
                // using (var client = new System.Net.Sockets.TcpClient("host", port))
                // using (var networkStream = client.GetStream())
                // {
                //     generator.Save(networkStream, BarCodeImageFormat.Png);
                // }
            }
        }
    }
}