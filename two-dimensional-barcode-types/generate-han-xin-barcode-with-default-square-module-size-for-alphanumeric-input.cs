// Title: Generate Han Xin Barcode with Default Square Modules
// Description: Demonstrates creating a Han Xin barcode for alphanumeric data using Aspose.BarCode with the default square module size and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It showcases the use of the BarcodeGenerator class together with EncodeTypes to produce 2‑D barcodes. Typical use cases include encoding product identifiers, serial numbers, or any alphanumeric information into a Han Xin symbol for printing or digital display. Developers often need to generate barcodes quickly with minimal configuration, relying on default settings for module shape and size.
// Prompt: Generate a Han Xin barcode with default square module size for alphanumeric input.
// Tags: hanxin, barcode, generation, png, aspose.barcode, encode, alphanumeric

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a Han Xin barcode using default settings and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the barcode.
        string codeText = "ABC123";

        // Build the full path for the output image file in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "HanXin.png");

        // Create a BarcodeGenerator for the Han Xin symbology with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
        {
            // No additional configuration is required because the default settings produce square modules.
            // Generate the barcode image as a Bitmap.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the bitmap to a PNG file at the designated location.
                bitmap.Save(outputPath, ImageFormat.Png);
            }
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Han Xin barcode saved to: {outputPath}");
    }
}