using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a GS1 DataMatrix barcode and saving it as a JPEG image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a GS1 DataMatrix barcode with a sample GTIN and writes it to a JPEG file.
    /// </summary>
    static void Main()
    {
        // Sample GS1 DataMatrix code text (GTIN with Application Identifier 01)
        const string codeText = "(01)01234567890128";

        // Destination file path for the generated JPEG image
        const string outputPath = "gs1datamatrix.jpg";

        // Initialize the barcode generator for GS1 DataMatrix with the specified payload
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Set the ECI (Extended Channel Interpretation) to UTF‑8 for proper encoding of the payload
            generator.Parameters.Barcode.DataMatrix.ECIEncoding = ECIEncodings.UTF8;

            // Save the generated barcode as a JPEG image.
            // Aspose.BarCode does not provide a direct JPEG quality parameter,
            // so the library's default quality setting is used.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user that the barcode image has been saved
        Console.WriteLine($"GS1 DataMatrix barcode saved to '{outputPath}'.");
        // Note about JPEG quality limitation
        Console.WriteLine("Note: JPEG quality cannot be set explicitly via Aspose.BarCode API.");
    }
}