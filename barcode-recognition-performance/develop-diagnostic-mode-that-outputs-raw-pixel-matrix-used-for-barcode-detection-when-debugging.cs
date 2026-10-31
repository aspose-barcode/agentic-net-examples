// Title: Generate barcode image, display raw pixel matrix, and decode with diagnostic mode
// Description: This example creates a Code128 barcode, prints its raw pixel matrix for debugging, saves the image, and then reads the barcode using Aspose.BarCode with diagnostic settings enabled.
// Category-Description: Demonstrates core Aspose.BarCode operations such as barcode generation, image handling, raw pixel extraction, and barcode recognition with quality settings. It showcases the BarcodeGenerator, BarCodeReader, and related classes, useful for developers needing to debug detection algorithms or implement custom image analysis.
// Prompt: Develop a diagnostic mode that outputs the raw pixel matrix used for barcode detection when debugging.
// Tags: barcode, code128, generation, detection, raw pixel matrix, diagnostic mode, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode, outputs its raw pixel matrix,
/// saves the image, and reads the barcode using diagnostic mode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, matrix output, image saving,
    /// and barcode recognition with diagnostic settings.
    /// </summary>
    static void Main()
    {
        // Define barcode content and symbology
        string codeText = "123456";
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Create a generator for the specified barcode type
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Generate the barcode image with default size settings
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Output the raw pixel matrix (0 = light, 1 = dark) to the console
                Console.WriteLine("Raw pixel matrix (0 = light, 1 = dark):");
                for (int y = 0; y < bitmap.Height; y++)
                {
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        Color pixel = bitmap.GetPixel(x, y);
                        // Simple threshold on the red channel (assuming grayscale image)
                        int value = pixel.R < 128 ? 1 : 0;
                        Console.Write(value);
                    }
                    Console.WriteLine();
                }

                // Save the generated barcode image to a temporary file (optional)
                string imagePath = Path.Combine(Path.GetTempPath(), "sample_barcode.png");
                using (var fileStream = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                {
                    bitmap.Save(fileStream, ImageFormat.Png);
                }
                Console.WriteLine($"Barcode image saved to: {imagePath}");

                // Prepare to read the barcode from the bitmap
                BaseDecodeType decodeType = DecodeType.AllSupportedTypes;
                using (var reader = new BarCodeReader(bitmap, decodeType))
                {
                    // Enable diagnostic mode: allow processing of potentially incorrect barcodes
                    reader.QualitySettings.AllowIncorrectBarcodes = true;

                    // Perform barcode detection
                    BarCodeResult[] results = reader.ReadBarCodes();
                    Console.WriteLine($"Barcodes detected: {results.Length}");
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
                    }
                }
            }
        }
    }
}