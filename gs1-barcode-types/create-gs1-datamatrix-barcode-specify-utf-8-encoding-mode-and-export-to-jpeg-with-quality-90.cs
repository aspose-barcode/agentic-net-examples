// Title: Generate GS1 DataMatrix Barcode with UTF‑8 Encoding and Save as JPEG
// Description: Demonstrates creating a GS1 DataMatrix barcode, configuring UTF‑8 encoding, and exporting the image to a JPEG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.GS1DataMatrix, set DataMatrix encoding mode to ECI, specify UTF‑8 ECI encoding, and save the result in a common image format. Developers often need to generate GS1‑compliant DataMatrix symbols for product identification and export them for printing or digital distribution.
// Prompt: Create a GS1 DataMatrix barcode, specify UTF‑8 encoding mode, and export to JPEG with quality 90.
// Tags: gs1, datamatrix, barcode, generation, utf-8, jpeg, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a GS1 DataMatrix barcode with UTF‑8 encoding
/// and saves it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output directory and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated JPEG file
        string outputPath = Path.Combine(outputDir, "GS1DataMatrix.jpg");

        // GS1 DataMatrix payload (example GTIN)
        string gs1CodeText = "(01)12345678901231";

        // Initialize the barcode generator with GS1 DataMatrix symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, gs1CodeText))
        {
            // Set the module size (X‑dimension) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 8f;

            // Configure DataMatrix to use ECI (Extended Channel Interpretation) encoding mode
            generator.Parameters.Barcode.DataMatrix.EncodeMode = DataMatrixEncodeMode.ECI;

            // Specify UTF‑8 as the ECI encoding
            generator.Parameters.Barcode.DataMatrix.ECIEncoding = ECIEncodings.UTF8;

            // Save the barcode image as JPEG (default quality is 90)
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}