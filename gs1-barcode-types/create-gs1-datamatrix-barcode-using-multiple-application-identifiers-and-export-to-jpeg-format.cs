// Title: Generate GS1 DataMatrix Barcode and Save as JPEG
// Description: Demonstrates creating a GS1 DataMatrix barcode with multiple Application Identifiers and exporting it to a JPEG image file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.GS1DataMatrix. Typical use cases include encoding product information for GS1 compliance, such as GTIN, serial numbers, and batch numbers, and exporting the result to common image formats for printing or digital display. Developers often need to combine several Application Identifiers into a single barcode and save the output for downstream processing or visual verification.
/// Prompt: Create a GS1 DataMatrix barcode using multiple Application Identifiers and export to a JPEG format.
/// Tags: gs1, datamatrix, barcode, generation, jpeg, export, aspose.barcode, aspnet

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a GS1 DataMatrix barcode containing multiple Application Identifiers
/// and saves the result as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define a temporary folder to store the generated image.
        string outputFolder = Path.Combine(Path.GetTempPath(), "GS1DataMatrixDemo");
        Directory.CreateDirectory(outputFolder);

        // Full path for the output JPEG file.
        string outputPath = Path.Combine(outputFolder, "gs1_datamatrix.jpg");

        // Barcode content with multiple GS1 Application Identifiers:
        // (01) – GTIN, (21) – Serial Number, (30) – Quantity.
        string codeText = "(01)12345678901231(21)ASPOSE(30)9876";

        // Create a BarcodeGenerator for GS1 DataMatrix using the specified text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Set the module size (X-dimension) to 4 pixels for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the generated barcode as a JPEG image.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine("GS1 DataMatrix barcode saved to: " + outputPath);
    }
}