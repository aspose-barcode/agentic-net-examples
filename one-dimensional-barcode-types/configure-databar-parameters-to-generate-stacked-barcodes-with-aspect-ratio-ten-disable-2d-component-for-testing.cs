// Title: Generate GS1 DataBar Stacked barcode with custom aspect ratio
// Description: Demonstrates configuring DataBar parameters to create a stacked barcode with an aspect ratio of ten and disabling the 2D composite component.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on DataBar symbologies. It showcases the use of BarcodeGenerator, EncodeTypes, and DataBar parameters to customize appearance, a common need for developers generating retail or logistics barcodes where specific sizing and component settings are required.
// Prompt: Configure DataBar parameters to generate stacked barcodes with aspect ratio ten, disable 2D component for testing.
// Tags: databar, stacked, aspectratio, disable2d, png, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates configuring DataBar parameters to generate a stacked barcode with a custom aspect ratio and without a 2D composite component.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates and saves a GS1 DataBar Stacked barcode image.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for the GS1 DataBar Stacked symbology with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.DatabarStacked, "(01)12345678901231"))
        {
            // Set the DataBar aspect ratio to 10 (height divided by width)
            generator.Parameters.Barcode.DataBar.AspectRatio = 10f;

            // Disable the optional 2D composite component for testing purposes
            generator.Parameters.Barcode.DataBar.Is2DCompositeComponent = false;

            // Save the generated barcode as a PNG image file
            generator.Save("databar_stacked.png");
        }

        // Output a simple confirmation message
        Console.WriteLine("DataBar stacked barcode generated successfully.");
    }
}