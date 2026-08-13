// Title: Create DataBar Expanded Stacked barcode and save as BMP
// Description: Demonstrates generating a GS1 DataBar Expanded Stacked barcode with three columns and an aspect ratio of eight, then saving it as a BMP image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.DatabarExpandedStacked. Developers often need to create high‑density DataBar barcodes for retail and inventory applications, adjusting parameters such as column count and aspect ratio to meet scanning requirements. The snippet illustrates typical steps: instantiate the generator, set code text, configure DataBar‑specific settings, and export the result to an image format.
// Prompt: Create DataBar Expanded Stacked barcode with three columns, aspect ratio eight, save BMP image.
// Tags: databar, expanded stacked, barcode, generation, bmp, aspose.barcode, encode types, image output

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a GS1 DataBar Expanded Stacked barcode and saves it as a BMP file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, configures its properties, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Initialize the barcode generator for the GS1 DataBar Expanded Stacked symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.DatabarExpandedStacked))
        {
            // Assign the data to be encoded (sample GS1 numeric string)
            generator.CodeText = "123456789012";

            // Configure DataBar‑specific parameters:
            //   - Columns: three columns for expanded stacked layout
            //   - AspectRatio: eight, defining the height‑to‑width proportion
            generator.Parameters.Barcode.DataBar.Columns = 3;          // three columns
            generator.Parameters.Barcode.DataBar.AspectRatio = 8f;    // aspect ratio eight

            // Save the generated barcode as a BMP image file
            generator.Save("databar_expanded_stacked.bmp");
        }

        // Output a simple confirmation message to the console
        Console.WriteLine("DataBar Expanded Stacked barcode saved as databar_expanded_stacked.bmp");
    }
}