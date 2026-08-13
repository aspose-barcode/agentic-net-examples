// Title: Generate OneCode barcode with custom 300 DPI PNG output
// Description: Demonstrates creating a OneCode barcode, setting a custom resolution of 300 DPI, and saving it as a high‑resolution PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.OneCode. Developers often need to customize barcode resolution for print quality, export to high‑resolution image formats, and integrate barcode creation into automated workflows. Typical use cases include product labeling, inventory tracking, and document automation where high‑resolution output is required.
// Prompt: Generate a OneCode barcode with custom DPI of 300 and save as high‑resolution PNG.
// Tags: onecode, barcode, generation, resolution, dpi, png, aspose.barcode, csharp

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a OneCode barcode with a custom DPI and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates the barcode, configures resolution, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the numeric text to encode (20 digits, valid length for OneCode)
        const string codeText = "12345678901234567890";

        // Initialize the BarcodeGenerator for OneCode symbology with the specified text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.OneCode, codeText))
        {
            // Set the image resolution to 300 DPI for high‑quality output
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode as a high‑resolution PNG file
            generator.Save("OneCode.png");
        }

        // Inform the user that the barcode has been successfully created
        Console.WriteLine("OneCode barcode generated and saved as OneCode.png");
    }
}