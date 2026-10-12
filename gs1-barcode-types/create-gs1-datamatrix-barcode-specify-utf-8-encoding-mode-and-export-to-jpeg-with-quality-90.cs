// Title: Generate GS1 DataMatrix barcode and save as JPEG with UTF‑8 encoding
// Description: Demonstrates creating a GS1 DataMatrix barcode, configuring UTF‑8 ECI encoding, and exporting the image to JPEG format.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It shows how to use the BarcodeGenerator class with EncodeTypes.GS1DataMatrix, set barcode parameters such as X‑dimension and ECI encoding, and save the result in a common image format. Developers working with product identification, inventory, or logistics often need to generate GS1 DataMatrix symbols with specific character encodings and output them as JPEG files for integration into documents or web services.
// Prompt: Create a GS1 DataMatrix barcode, specify UTF‑8 encoding mode, and export to JPEG with quality 90.
// Tags: gs1, datamatrix, barcode, generation, utf-8, eci, jpeg, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a GS1 DataMatrix barcode, applies UTF‑8 ECI encoding,
/// and saves the barcode image as a JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the system temporary folder.
        string outputPath = Path.Combine(Path.GetTempPath(), "gs1_datamatrix.jpg");

        // GS1 DataMatrix code text. The (01) Application Identifier requires a 14‑digit GTIN.
        string codeText = "(01)12345678901231";

        // Initialize the barcode generator for GS1 DataMatrix with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Set the module (pixel) size of the barcode.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Configure UTF‑8 encoding using ECI (Extended Channel Interpretation) mode.
            generator.Parameters.Barcode.DataMatrix.EncodeMode = DataMatrixEncodeMode.ECI;
            generator.Parameters.Barcode.DataMatrix.ECIEncoding = ECIEncodings.UTF8;

            // Save the generated barcode as a JPEG image.
            // Note: Aspose.BarCode uses the default JPEG quality; explicit quality control is not exposed.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"GS1 DataMatrix barcode saved to: {outputPath}");
    }
}