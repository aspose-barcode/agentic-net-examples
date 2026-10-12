// Title: DotCode barcode with rectangular layout of 20 columns
// Description: Generates a DotCode barcode using a rectangular layout with 20 columns, demonstrating increased data capacity.
// Category-Description: This example belongs to the Aspose.BarCode 2D barcode generation category. It showcases how to use the BarcodeGenerator class together with EncodeTypes and BarCodeImageFormat to create custom DotCode symbols. Developers often need to adjust layout parameters such as column count to meet specific data density requirements in inventory, tracking, or authentication scenarios.
// Prompt: Configure DotCode to use rectangular layout with 20 columns for increased data capacity.
// Tags: dotcode, rectangular-layout, columns, barcode, generation, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates how to generate a DotCode barcode with a rectangular layout of 20 columns.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary folder, generates the barcode, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Build a unique temporary directory to store the generated image.
        string tempDir = Path.Combine(Path.GetTempPath(), "DotCodeExample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define the full path for the output PNG file.
        string outputPath = Path.Combine(tempDir, "DotCode20Columns.png");

        // Initialize the barcode generator for DotCode with sample data.
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.DotCode, "SampleData"))
        {
            // Set the X-dimension (module size) to 10 pixels for better visibility.
            gen.Parameters.Barcode.XDimension.Pixels = 10;

            // Configure the DotCode layout to use 20 columns (rectangular layout).
            gen.Parameters.Barcode.DotCode.Columns = 20;

            // Save the generated barcode image to the specified path in PNG format.
            gen.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine("Barcode saved to: " + outputPath);
    }
}