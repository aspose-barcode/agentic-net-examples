// Title: Generate OneCode barcode with custom DPI and save as high‑resolution PNG
// Description: Demonstrates creating a OneCode barcode using Aspose.BarCode, setting a 300 DPI resolution, and exporting it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with custom encoding types and image resolution. Developers often need to produce high‑resolution barcodes for printing or scanning applications, and this snippet shows typical steps: selecting a symbology via reflection, configuring parameters, and saving the result in a desired format.
// Prompt: Generate a OneCode barcode with custom DPI of 300 and save as high‑resolution PNG.
// Tags: onecode, barcode, generation, dpi, png, aspose.barcode, resolution, imageformat

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a OneCode barcode with a custom DPI and saving it as a high‑resolution PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output directory, configures the barcode generator, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define the output directory and ensure it exists
        string outputDir = Path.Combine(Environment.CurrentDirectory, "output");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated PNG file
        string outputPath = Path.Combine(outputDir, "OneCode.png");

        // Text to encode in the barcode
        string codeText = "12345678901234567890";

        // Retrieve the OneCode symbology via reflection (avoids hard‑coding enum value)
        FieldInfo field = typeof(EncodeTypes).GetField("OneCode");
        if (field == null)
        {
            Console.WriteLine("OneCode symbology not found.");
            return;
        }

        // Cast the reflected value to the base encode type used by the generator
        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Initialize the generator with the selected symbology and data
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Set the image resolution to 300 DPI for high‑quality output
            generator.Parameters.Resolution = 300f;

            // Save the barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"OneCode barcode saved to: {outputPath}");
    }
}