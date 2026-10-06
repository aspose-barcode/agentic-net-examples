// Title: Retrieve Macro PDF417 barcode fields from a generated image
// Description: Demonstrates generating a Macro PDF417 barcode, saving it as PNG, and extracting macro fields such as file ID, segment ID, and segment count.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating Macro PDF417 barcodes and BarCodeReader for decoding them. Developers working with document automation, inventory tracking, or secure data encoding often need to embed and later retrieve macro information from PDF417 symbols. The key API classes used are BarcodeGenerator, BarCodeReader, and related parameter objects for macro settings.
/// Prompt: Retrieve PDF417 macro fields such as file ID and segment ID from a scanned document.
/// Tags: pdf417, macro, barcode, generation, recognition, c#, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a Macro PDF417 barcode, saves it to a temporary file,
/// reads the barcode back, and prints macro fields (FileID, SegmentID, SegmentsCount).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode generation, reading, and cleanup.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to store the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "Pdf417MacroDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Full path for the generated PNG barcode image
        string barcodePath = Path.Combine(tempFolder, "macroPdf417.png");

        // Generate a Macro PDF417 barcode with specific macro metadata
        using (var generator = new BarcodeGenerator(EncodeTypes.MacroPdf417, "SampleData"))
        {
            // Set visual and macro-specific parameters
            generator.Parameters.Barcode.XDimension.Pixels = 2f;                     // Module width in pixels
            generator.Parameters.Barcode.Pdf417.Columns = 4;                        // Number of columns
            generator.Parameters.Barcode.Pdf417.MacroPdf417FileID = 12345678;        // Unique file identifier
            generator.Parameters.Barcode.Pdf417.MacroPdf417SegmentID = 12;          // Segment identifier within the file
            generator.Parameters.Barcode.Pdf417.MacroPdf417SegmentsCount = 3;       // Total number of segments

            // Save the barcode image as PNG
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was successfully created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode image and extract macro fields using the appropriate decode type
        using (var reader = new BarCodeReader(barcodePath, DecodeType.MacroPdf417))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("---Macro PDF417 Detected---");
                Console.WriteLine("CodeText: " + result.CodeText);
                Console.WriteLine("MacroPdf417FileID: " + result.Extended.Pdf417.MacroPdf417FileID);
                Console.WriteLine("MacroPdf417SegmentID: " + result.Extended.Pdf417.MacroPdf417SegmentID);
                Console.WriteLine("MacroPdf417SegmentsCount: " + result.Extended.Pdf417.MacroPdf417SegmentsCount);
            }
        }

        // Attempt to clean up temporary files and folder; ignore any errors during cleanup
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Cleanup failures are non‑critical; program can exit gracefully
        }
    }
}