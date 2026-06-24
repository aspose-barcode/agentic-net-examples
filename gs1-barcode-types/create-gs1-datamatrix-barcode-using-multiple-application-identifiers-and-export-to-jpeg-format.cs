using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a GS1 DataMatrix barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a GS1 DataMatrix barcode with sample data and saves it as a JPEG file.
    /// </summary>
    static void Main()
    {
        // Define the GS1 DataMatrix payload containing multiple Application Identifiers (AIs):
        // (01) – GTIN, (10) – Batch/Lot number, (17) – Expiration date (YYMMDD)
        string codeText = "(01)12345678901231(10)BATCH123(17)250731";

        // Destination file path for the generated barcode image (JPEG format)
        string outputPath = "gs1_datamatrix.jpg";

        // Initialize the barcode generator for GS1 DataMatrix symbology with the specified payload
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Persist the generated barcode to the file system as a JPEG image
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"GS1 DataMatrix barcode saved to: {outputPath}");
    }
}