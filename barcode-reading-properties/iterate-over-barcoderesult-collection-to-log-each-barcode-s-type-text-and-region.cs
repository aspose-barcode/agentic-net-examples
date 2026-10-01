// Title: Iterate over BarCodeResult collection and log barcode details
// Description: This example generates a Code128 barcode, reads it back using Aspose.BarCode, and iterates through the BarCodeResult collection to output each barcode's type, decoded text, and region coordinates.
// Category-Description: The sample belongs to the Aspose.BarCode reading and generation category, illustrating how to use BarcodeGenerator, BarCodeReader, and BarCodeResult classes. Developers commonly need to generate barcodes, decode them from images, and extract positional information for further processing such as inventory tracking or document analysis. This snippet serves as a quick reference for typical barcode detection workflows in C#.
// Prompt: Iterate over BarCodeResult collection to log each barcode's type, text, and region.
// Tags: barcode symbology, generation, reading, result iteration, console output, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation, detection, and detailed logging of each detected barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode image, reads it, and logs detection results.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a temporary barcode image to keep the example self‑contained.
        // --------------------------------------------------------------------
        string sampleText = "1234567890";
        string tempImagePath = Path.Combine(Path.GetTempPath(), "sample_barcode.png");

        // Generate a Code128 barcode and save it as a PNG file.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, sampleText))
        {
            generator.Save(tempImagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created before attempting to read it.
        if (!File.Exists(tempImagePath))
        {
            Console.WriteLine($"Barcode image not found at '{tempImagePath}'.");
            return;
        }

        // ---------------------------------------------------------------
        // Read all supported barcodes from the generated image file.
        // ---------------------------------------------------------------
        using (var reader = new BarCodeReader(tempImagePath, DecodeType.AllSupportedTypes))
        {
            BarCodeResult[] results = reader.ReadBarCodes();

            // Handle the case where no barcodes were detected.
            if (results == null || results.Length == 0)
            {
                Console.WriteLine("No barcodes were detected.");
                return;
            }

            // -----------------------------------------------------------
            // Iterate over each detection result and log its details.
            // -----------------------------------------------------------
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine("=== Barcode Detected ===");
                Console.WriteLine($"Type      : {result.CodeTypeName}");
                Console.WriteLine($"Text      : {result.CodeText}");

                // Region information includes the bounding rectangle and rotation angle.
                var rect = result.Region.Rectangle;
                Console.WriteLine($"Region    : X={rect.X}, Y={rect.Y}, Width={rect.Width}, Height={rect.Height}");
                Console.WriteLine($"Angle     : {result.Region.Angle}");
                Console.WriteLine();
            }
        }

        // ---------------------------------------------------------------
        // Clean up the temporary image file; ignore any errors.
        // ---------------------------------------------------------------
        try
        {
            File.Delete(tempImagePath);
        }
        catch
        {
            // Deletion failure is non‑critical for the demonstration.
        }
    }
}