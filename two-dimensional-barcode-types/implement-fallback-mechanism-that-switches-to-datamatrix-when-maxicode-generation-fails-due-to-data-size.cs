// Title: MaxiCode generation with DataMatrix fallback on data size overflow
// Description: Demonstrates generating a MaxiCode barcode and automatically falling back to a DataMatrix barcode when the data exceeds MaxiCode capacity.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use ComplexBarcodeGenerator for MaxiCode (Mode 2) and BarcodeGenerator for DataMatrix. It shows typical error handling for size constraints, a common scenario when developers need to ensure barcode creation succeeds despite data limits.
// Prompt: Implement fallback mechanism that switches to DataMatrix when MaxiCode generation fails due to data size.
// Tags: maxicode, datamatrix, fallback, barcode generation, complexbarcodegenerator, barcodelibrary, aspnet, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a MaxiCode barcode and falling back to DataMatrix when the data is too large.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates output folder, defines large data, and triggers barcode generation with fallback.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for output files
        string outputFolder = Path.Combine(Path.GetTempPath(), "MaxiCodeFallback_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Sample data that is intentionally large to trigger a generation failure for MaxiCode
        string largeData = new string('A', 2000);

        // Paths for the generated images
        string maxiPath = Path.Combine(outputFolder, "maxicode.png");
        string dmPath = Path.Combine(outputFolder, "datamatrix.png");

        // Attempt to generate MaxiCode, fallback to DataMatrix on failure
        GenerateMaxiCodeOrFallback(largeData, maxiPath, dmPath);

        Console.WriteLine("Generation completed.");
        Console.WriteLine("MaxiCode image (if generated): " + maxiPath);
        Console.WriteLine("DataMatrix fallback image: " + dmPath);
    }

    /// <summary>
    /// Tries to generate a MaxiCode (Mode 2). If generation fails (e.g., due to data size), generates a DataMatrix barcode instead.
    /// </summary>
    /// <param name="data">The text data to encode.</param>
    /// <param name="maxiOutputPath">File path for the MaxiCode image.</param>
    /// <param name="dmOutputPath">File path for the DataMatrix fallback image.</param>
    static void GenerateMaxiCodeOrFallback(string data, string maxiOutputPath, string dmOutputPath)
    {
        // Try to generate a MaxiCode (Mode 2) using ComplexBarcodeGenerator
        try
        {
            var maxiCodeData = new MaxiCodeCodetextMode2
            {
                PostalCode = "524032140",   // 9‑digit postal code required for Mode 2
                CountryCode = 56,
                ServiceCategory = 999,
                // Use the large data as the second message (standard message)
                SecondMessage = new MaxiCodeStandardSecondMessage { Message = data }
            };

            // Generate the barcode image and save it as PNG
            using (var complexGenerator = new ComplexBarcodeGenerator(maxiCodeData))
            {
                using (Bitmap bitmap = complexGenerator.GenerateBarCodeImage())
                {
                    bitmap.Save(maxiOutputPath, ImageFormat.Png);
                }
            }

            Console.WriteLine("MaxiCode generated successfully: " + maxiOutputPath);
        }
        catch (Exception ex)
        {
            // Log the failure and proceed with DataMatrix fallback
            Console.WriteLine("MaxiCode generation failed: " + ex.Message);
            Console.WriteLine("Falling back to DataMatrix...");

            // Generate a DataMatrix barcode with the same data
            using (var dmGenerator = new BarcodeGenerator(EncodeTypes.DataMatrix))
            {
                dmGenerator.CodeText = data;
                // Use automatic version selection (default)
                dmGenerator.Save(dmOutputPath, BarCodeImageFormat.Png);
            }

            Console.WriteLine("DataMatrix generated: " + dmOutputPath);
        }
    }
}