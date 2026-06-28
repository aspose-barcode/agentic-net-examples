using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generating MaxiCode barcodes for a list of product identifiers
/// using Aspose.BarCode library.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates MaxiCode PNG files for each product ID.
    /// </summary>
    static void Main()
    {
        // Define a sample list of product identifiers.
        string[] productIds = new string[]
        {
            "PROD001",
            "PROD002",
            "PROD003",
            "PROD004",
            "PROD005"
        };

        // Specify the output directory where barcode images will be saved.
        string outputDir = "MaxiCodeBarcodes";

        // Ensure the output directory exists; create it if it does not.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Iterate over each product identifier to generate a corresponding barcode.
        for (int i = 0; i < productIds.Length; i++)
        {
            // Current product identifier.
            string productId = productIds[i];

            // Create a standard MaxiCode codetext using Mode4 (data only) and set the message.
            var maxiCodeCodetext = new MaxiCodeStandardCodetext
            {
                Mode = MaxiCodeMode.Mode4,
                Message = productId
            };

            // Build the full file path for the output PNG file.
            string outputPath = Path.Combine(outputDir, $"maxicode_{i + 1}.png");

            // Generate the MaxiCode barcode and save it as a PNG image.
            using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
            {
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            // Inform the user that the barcode has been generated.
            Console.WriteLine($"Generated MaxiCode for '{productId}' at '{outputPath}'.");
        }
    }
}