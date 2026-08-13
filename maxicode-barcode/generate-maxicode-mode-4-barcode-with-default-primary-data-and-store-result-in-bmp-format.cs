// Title: Generate MaxiCode Mode 4 barcode and save as BMP
// Description: Demonstrates creating a MaxiCode Mode 4 barcode with default primary data using Aspose.BarCode and saving the image in BMP format.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode symbologies such as MaxiCode. It showcases the use of ComplexBarcodeGenerator and MaxiCodeStandardCodetext classes to configure mode and data, a common task for developers needing high‑density 2‑D barcodes for logistics and tracking applications. Typical use cases include generating shipping labels and parcel identifiers where MaxiCode is required.
// Prompt: Generate a MaxiCode Mode 4 barcode with default primary data and store the result in BMP format.
// Tags: maxicode, barcode generation, bmp, complexbarcode, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that generates a MaxiCode Mode 4 barcode and saves it as a BMP image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "maxicode_mode4.bmp";

        // Initialize standard codetext for MaxiCode Mode 4.
        var maxiCodeData = new MaxiCodeStandardCodetext
        {
            // Set the barcode mode to Mode 4.
            Mode = MaxiCodeMode.Mode4,
            // Provide default primary data (a simple message).
            Message = "Test message"
        };

        // Create a ComplexBarcodeGenerator with the configured codetext.
        using (var generator = new ComplexBarcodeGenerator(maxiCodeData))
        {
            // Save the generated barcode image in BMP format to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Output the full path of the saved barcode image.
        Console.WriteLine($"MaxiCode Mode 4 barcode saved to {Path.GetFullPath(outputPath)}");
    }
}