// Title: Generate DotCode barcode with rectangular layout of 20 columns
// Description: Demonstrates how to configure a DotCode barcode to use a rectangular layout with 20 columns, increasing data capacity, and save it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator with EncodeTypes.DotCode. It illustrates setting barcode parameters such as XDimension and DotCode layout options, a common task for developers needing high‑capacity 2‑D barcodes. Typical use cases include product labeling, inventory tracking, and data‑dense encoding where rectangular DotCode layouts are preferred.
// Prompt: Configure DotCode to use rectangular layout with 20 columns for increased data capacity.
// Tags: dotcode, barcode, generation, rectangular-layout, columns, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a DotCode barcode with a rectangular layout of 20 columns
/// and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the output file
        string outputDir = Path.Combine(Path.GetTempPath(), "DotCodeExample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the full path for the generated PNG image
        string outputPath = Path.Combine(outputDir, "DotCode20Columns.png");

        // Initialize the barcode generator for DotCode with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, "SampleData"))
        {
            // Set the X dimension (module size) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 10;

            // Configure the DotCode to use a rectangular layout with 20 columns
            generator.Parameters.Barcode.DotCode.Columns = 20;

            // Save the generated barcode image to the specified path in PNG format
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine("Barcode saved to: " + outputPath);
    }
}