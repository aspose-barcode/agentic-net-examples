// Title: Generate barcode with auto‑sized height using Aspose.BarCode
// Description: Demonstrates how to create a Code128 barcode where the bar height is automatically determined from the content by setting BarCodeHeight to zero (auto‑size mode). The barcode is saved as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on auto‑sizing and layout configuration. It showcases the use of BarcodeGenerator, EncodeTypes, and AutoSizeMode to let the library calculate optimal dimensions based on the encoded data. Developers often need to generate barcodes that adapt to varying content lengths without manually specifying size parameters, especially for dynamic reporting or label printing scenarios.
// Prompt: Generate barcode with BarCodeHeight zero to enable auto‑size based on content, using default units.
// Tags: code128, autosize, barcodeheight, png, aspnet, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode with automatic height sizing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a barcode, enables auto‑size mode, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with the sample text "Sample123".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set the auto‑size mode to Interpolation.
            // In this mode the BarCodeHeight property is ignored, allowing the library to determine the optimal height.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Define the output file path.
            const string outputPath = "barcode.png";

            // Save the generated barcode image to the specified path.
            generator.Save(outputPath);

            // Inform the user that the barcode has been saved.
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}