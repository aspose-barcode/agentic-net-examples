// Title: Generate a Han Xin barcode with automatic version selection
// Description: Demonstrates creating a Han Xin barcode using Aspose.BarCode, letting the library automatically choose the appropriate square version for a larger payload.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on Han Xin symbology. It showcases the use of BarcodeGenerator, EncodeTypes, and HanXin parameters to produce a barcode image. Developers commonly need to generate Han Xin barcodes for Chinese characters or large data sets, and this snippet illustrates how to configure version selection and optional error correction.
// Prompt: Configure Han Xin to use rectangular shape with 15 rows and 40 columns for larger payload.
// Tags: hanxin, barcode, generation, image, aspose.barcode, encode-types, version-auto

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Han Xin barcode with automatic version selection using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates and saves a Han Xin barcode image.
    /// </summary>
    static void Main()
    {
        // Define a sample payload that requires a larger Han Xin symbol.
        string payload = "This is a sample text that requires a larger Han Xin symbol.";

        // Han Xin supports only square symbols. Rectangular shapes or custom row/column counts
        // (e.g., 15 rows x 40 columns) are not available. The version is chosen automatically
        // based on the payload size.
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, payload))
        {
            // Let the library select the appropriate square version automatically.
            generator.Parameters.Barcode.HanXin.Version = HanXinVersion.Auto;

            // Optional: set other parameters if needed, e.g., error correction level.
            // generator.Parameters.Barcode.HanXin.ErrorLevel = ErrorLevel.L4;

            // Save the generated barcode image to a file.
            string outputPath = "HanXinBarcode.png";
            generator.Save(outputPath);
            Console.WriteLine($"Han Xin barcode saved to: {outputPath}");
        }
    }
}