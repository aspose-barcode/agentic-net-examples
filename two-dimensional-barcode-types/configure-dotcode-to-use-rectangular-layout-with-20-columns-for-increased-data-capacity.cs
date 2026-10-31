// Title: Configure DotCode barcode with rectangular layout and 20 columns
// Description: Demonstrates how to generate a DotCode barcode using a rectangular layout with 20 columns to increase data capacity.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of the BarcodeGenerator class with EncodeTypes.DotCode. It shows how to adjust DotCode-specific parameters such as column count to customize the barcode layout, a common requirement when developers need higher data density for DotCode symbology. The snippet is useful for generating PNG images of DotCode barcodes in .NET applications.
// Prompt: Configure DotCode to use rectangular layout with 20 columns for increased data capacity.
// Tags: dotcode, barcode, generation, rectangular layout, columns, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a DotCode barcode with a rectangular layout of 20 columns.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode image and saves it to a temporary folder.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the output image
        string outputDir = Path.Combine(Path.GetTempPath(), "DotCodeExample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the full path for the PNG file to be saved
        string outputPath = Path.Combine(outputDir, "DotCode20Columns.png");

        // Initialize the barcode generator for DotCode with sample data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DotCode, "SampleData for DotCode"))
        {
            // Set the X-dimension (module size) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 5f;

            // Configure DotCode to use a rectangular layout with 20 columns
            generator.Parameters.Barcode.DotCode.Columns = 20;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine("DotCode barcode saved to: " + outputPath);
    }
}