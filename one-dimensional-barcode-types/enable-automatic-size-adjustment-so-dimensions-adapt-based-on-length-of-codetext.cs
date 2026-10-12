// Title: Automatic barcode size adjustment based on CodeText length
// Description: Demonstrates how Aspose.BarCode automatically sizes the generated barcode image according to the length of the provided CodeText.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator with EncodeTypes.Code128. It illustrates default auto‑sizing behavior where developers do not need to set explicit dimensions; the library calculates optimal width and height based on the encoded data. Typical use cases include dynamic barcode creation for varying product codes, inventory IDs, or any variable‑length data where image size must adapt automatically.
// Prompt: Enable automatic size adjustment so dimensions adapt based on the length of CodeText.
// Tags: barcode, autosize, codetext length, code128, generation, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates Code128 barcodes with automatic size adjustment based on the length of the supplied CodeText.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates temporary output folder, generates barcodes for a set of sample texts,
    /// writes image dimensions to console, and saves each barcode as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define a temporary directory to store generated barcode images.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeAutoSizeDemo");
        Directory.CreateDirectory(outputDir);

        // Sample CodeText values of varying lengths to demonstrate auto‑sizing.
        List<string> codeTexts = new List<string>
        {
            "A",
            "ABC123",
            "LongerCodeTextExample12345"
        };

        // Iterate over each CodeText, generate a barcode, and save it.
        foreach (string codeText in codeTexts)
        {
            // Build the full file path for the PNG output.
            string filePath = Path.Combine(outputDir, $"barcode_{codeText}.png");

            // Initialize the generator with Code128 symbology and the current CodeText.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // No manual size settings; the generator auto‑sizes based on CodeText length.
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    // Output the generated image dimensions to the console.
                    Console.WriteLine($"{codeText} => {bitmap.Width}x{bitmap.Height}");

                    // Save the bitmap to a memory stream in PNG format, then write to disk.
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bitmap.Save(ms, ImageFormat.Png);
                        File.WriteAllBytes(filePath, ms.ToArray());
                    }
                }
            }

            // Confirm that the file has been saved.
            Console.WriteLine($"Saved: {filePath}");
        }

        // Indicate that all barcode generation tasks are complete.
        Console.WriteLine("Barcode generation completed.");
    }
}