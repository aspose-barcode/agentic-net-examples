// Title: Retrieve PDF417 macro fields from a generated barcode image
// Description: Demonstrates how to generate a Macro PDF417 barcode, save it as an image, and then read back macro fields such as file ID and segment ID.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating Macro PDF417 barcodes and BarCodeReader for extracting extended macro information. Developers working with PDF417 macro symbology can learn how to set macro parameters, save barcode images, and retrieve metadata like file ID, segment ID, and segment count, which are essential for multi-part document encoding.
// Prompt: Retrieve PDF417 macro fields such as file ID and segment ID from a scanned document.
// Tags: pdf417, macro, barcode generation, barcode recognition, aspnet, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Macro PDF417 barcode, saving it to an image, and reading macro fields.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, reads macro fields, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "Pdf417MacroDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "macro.png");

        // Generate a Macro PDF417 barcode with metadata
        using (var generator = new BarcodeGenerator(EncodeTypes.MacroPdf417, "SampleData"))
        {
            // Set barcode visual parameters
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.Pdf417.Columns = 4;

            // Set Macro PDF417 specific fields
            generator.Parameters.Barcode.Pdf417.MacroPdf417FileID = 12345678;
            generator.Parameters.Barcode.Pdf417.MacroPdf417SegmentID = 1;
            generator.Parameters.Barcode.Pdf417.MacroPdf417SegmentsCount = 2;

            // Save the barcode image to file
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify the image was created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode and extract macro fields
        using (var reader = new BarCodeReader(imagePath, DecodeType.MacroPdf417))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("---Macro PDF417 Detected---");
                Console.WriteLine("CodeText: " + result.CodeText);
                Console.WriteLine("Pdf417MacroFileID: " + result.Extended.Pdf417.MacroPdf417FileID);
                Console.WriteLine("Pdf417MacroSegmentID: " + result.Extended.Pdf417.MacroPdf417SegmentID);
                Console.WriteLine("Pdf417MacroSegmentsCount: " + result.Extended.Pdf417.MacroPdf417SegmentsCount);
            }
        }

        // Clean up temporary files (optional)
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}