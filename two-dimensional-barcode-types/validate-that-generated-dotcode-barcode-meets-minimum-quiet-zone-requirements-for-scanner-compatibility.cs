// Title: Validate DotCode barcode quiet zone using Aspose.BarCode
// Description: Generates a DotCode barcode, saves it as PNG, and verifies that the quiet zone meets scanner requirements by decoding the image.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It demonstrates how to use the BarcodeGenerator class to create a DotCode barcode and the BarCodeReader class to decode it, checking reading quality as an indicator of sufficient quiet zone. Developers working with barcode scanning often need to ensure quiet zones are adequate for reliable detection, especially for dot matrix symbologies.
// Prompt: Validate that generated DotCode barcode meets minimum quiet zone requirements for scanner compatibility.
// Tags: dotcode, quiet zone, barcode generation, barcode recognition, aspose.barcode, png, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a DotCode barcode and validating its quiet zone for scanner compatibility.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a DotCode barcode, checks quiet zone via decoding, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the generated barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "DotCodeQuietZone_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the output file path and the text to encode
        string barcodePath = Path.Combine(tempFolder, "dotcode.png");
        string codeText = "Aspose";

        // Generate DotCode barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, codeText))
        {
            // Set the module size (X dimension) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 10f;
            // Quiet zone is handled automatically by the generator
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Flag indicating whether the quiet zone is sufficient
        bool isQuietZoneSufficient = false;

        // Verify that the barcode file exists before attempting to read it
        if (File.Exists(barcodePath))
        {
            // Specify the decode type for DotCode
            BaseDecodeType decodeType = DecodeType.DotCode;

            // Attempt to read the barcode using BarCodeReader
            using (var reader = new BarCodeReader(barcodePath, decodeType))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    if (result != null && result.CodeTypeName == "DotCode")
                    {
                        // Consider the quiet zone sufficient if reading quality is greater than zero
                        if (result.ReadingQuality > 0)
                        {
                            isQuietZoneSufficient = true;
                            Console.WriteLine($"Barcode recognized. CodeText: {result.CodeText}, Quality: {result.ReadingQuality}");
                        }
                        else
                        {
                            Console.WriteLine($"Barcode recognized but low quality: {result.ReadingQuality}");
                        }
                        break; // Exit after processing the first DotCode result
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("Generated barcode file not found.");
        }

        // Output the quiet zone validation result
        Console.WriteLine(isQuietZoneSufficient
            ? "Quiet zone meets minimum scanner requirements."
            : "Quiet zone may be insufficient for reliable scanning.");

        // Clean up temporary files and directory
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Suppress any cleanup exceptions
        }
    }
}