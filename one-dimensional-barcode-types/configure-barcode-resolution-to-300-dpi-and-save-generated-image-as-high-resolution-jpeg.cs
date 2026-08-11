// Title: Generate high‑resolution Code128 barcode and save as JPEG
// Description: Demonstrates configuring the barcode generator resolution to 300 DPI and exporting the result as a high‑quality JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to adjust rendering parameters such as resolution. It uses the BarcodeGenerator class together with EncodeTypes and BarCodeImageFormat to create barcodes for common use cases like product labeling, inventory tracking, and document embedding. Developers often need to control DPI to meet print‑ready specifications or to ensure clarity on high‑resolution displays.
// Prompt: Configure barcode resolution to 300 DPI and save the generated image as a high‑resolution JPEG.
// Tags: code128, resolution, jpeg, barcode, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a Code128 barcode, sets its resolution to 300 DPI,
/// and saves it as a high‑resolution JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated JPEG image.
        string outputPath = "high_res_barcode.jpg";

        // Ensure the target directory exists; create it if necessary.
        string directory = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Initialize a barcode generator for the Code128 symbology with sample data.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            // Configure the rendering resolution to 300 DPI (float literal required).
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode as a high‑resolution JPEG image.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Output the full path of the saved barcode image.
        Console.WriteLine($"Barcode saved to: {Path.GetFullPath(outputPath)}");
    }
}