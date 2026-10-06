// Title: Create DataBar Expanded Stacked barcode and save as BMP
// Description: Demonstrates generating a DataBar Expanded Stacked barcode with three columns and an aspect ratio of eight, then saving it as a BMP image file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure DataBar symbologies using the BarcodeGenerator class. It shows setting X‑dimension, column count, and aspect ratio for DataBar Expanded Stacked barcodes, a common requirement for retail and logistics applications where high‑density, stacked linear barcodes are needed. Developers can use similar code to create other DataBar variants and export them to various image formats.
// Prompt: Create DataBar Expanded Stacked barcode with three columns, aspect ratio eight, save BMP image.
// Tags: databar, expandedstacked, barcode, generation, bmp, aspnet, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates creating a DataBar Expanded Stacked barcode and saving it as a BMP image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates the barcode and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output BMP file
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "DatabarExpandedStacked.bmp");

        try
        {
            // Initialize the barcode generator with the DataBar Expanded Stacked symbology and sample data
            using (var generator = new BarcodeGenerator(EncodeTypes.DatabarExpandedStacked, "(01)12345678901231"))
            {
                // Set the X-dimension (module width) in pixels
                generator.Parameters.Barcode.XDimension.Pixels = 2;

                // Configure DataBar-specific parameters: three columns and an aspect ratio of eight
                generator.Parameters.Barcode.DataBar.Columns = 3;
                generator.Parameters.Barcode.DataBar.AspectRatio = 8;

                // Save the generated barcode as a BMP image to the specified path
                generator.Save(outputPath, BarCodeImageFormat.Bmp);
            }

            // Inform the user that the barcode was saved successfully
            Console.WriteLine($"Barcode saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occurred during barcode generation
            Console.WriteLine($"Error generating barcode: {ex.Message}");
        }
    }
}