// Title: Generate GS1 DataMatrix barcode and save as JPEG with UTF‑8 encoding
// Description: Demonstrates creating a GS1 DataMatrix barcode using Aspose.BarCode, setting UTF‑8 ECI encoding, and exporting the image to JPEG format. The example shows how to configure barcode parameters and save the result to a file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on DataMatrix symbology with GS1 formatting. It illustrates usage of BarcodeGenerator, EncodeTypes, and barcode parameter classes such as DataMatrixEncodeMode and ECIEncodings. Developers often need to generate machine‑readable GS1 DataMatrix codes for product identification and export them to common image formats for integration into documents or labeling workflows.
// Prompt: Create a GS1 DataMatrix barcode, specify UTF‑8 encoding mode, and export to JPEG with quality 90.
// Tags: datamatrix, gs1, barcode generation, jpeg, utf-8, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a GS1 DataMatrix barcode with UTF‑8 encoding and saving it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Determine output file path in the current directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "gs1_datamatrix.jpg");
        // GS1 DataMatrix payload (Application Identifier 01 with a GTIN)
        string codeText = "(01)01234567890128";

        // Initialize the barcode generator with GS1 DataMatrix symbology and the payload
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Set the module size (X-dimension) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            // Use automatic encoding mode for DataMatrix
            generator.Parameters.Barcode.DataMatrix.EncodeMode = DataMatrixEncodeMode.Auto;
            // Specify UTF‑8 ECI encoding to support Unicode characters
            generator.Parameters.Barcode.DataMatrix.ECIEncoding = ECIEncodings.UTF8;
            // Save the generated barcode as a JPEG image (default quality is 90)
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"GS1 DataMatrix barcode saved to: {outputPath}");
    }
}