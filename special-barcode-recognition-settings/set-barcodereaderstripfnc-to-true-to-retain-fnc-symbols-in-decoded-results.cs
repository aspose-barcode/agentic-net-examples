// Title: Retain FNC symbols in decoded results using BarCodeReader.StripFNC
// Description: Demonstrates generating a Code128 barcode, then reading it with StripFNC enabled so that any FNC characters are preserved in the decoded text.
// Category-Description: This example belongs to the Aspose.BarCode reading and generation category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader with its BarcodeSettings.StripFNC property for controlling the handling of Function (FNC) symbols during decoding. Developers working with barcode data that includes FNC characters—such as inventory systems or custom encoding schemes—can use this pattern to ensure those symbols are not stripped away.
// Prompt: Set BarCodeReader.StripFNC to true to retain FNC symbols in decoded results.
// Tags: barcode symbology, code128, stripfnc, reading, generation, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode, reads it with StripFNC enabled,
/// and outputs the decoded information while preserving any FNC symbols.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode image, reads it with StripFNC set to true,
    /// displays the results, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to store the generated barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "StripFNCExample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "code128.png");

        // Generate a simple Code128 barcode and save it as a PNG file
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Aspose"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2; // Set barcode module size
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was successfully created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode image with StripFNC set to true (retain FNC symbols in the result)
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            reader.BarcodeSettings.StripFNC = true;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
            }
        }

        // Attempt to delete the temporary files and folder; ignore any errors
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Cleanup failures are non‑critical for this example
        }
    }
}