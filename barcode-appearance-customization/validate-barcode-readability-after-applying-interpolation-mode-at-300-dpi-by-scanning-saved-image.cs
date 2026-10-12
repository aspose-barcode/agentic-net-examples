// Title: Validate barcode readability after applying interpolation mode at 300 dpi
// Description: Generates a DataMatrix barcode using Aspose.BarCode with interpolation auto‑size mode at 300 dpi, saves it as PNG, then reads it back to verify readability.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It demonstrates how to use BarcodeGenerator to create a barcode with specific rendering settings (AutoSizeMode, Resolution, dimensions) and BarCodeReader to decode and validate the barcode. Typical use cases include automated testing of barcode quality, preparing high‑resolution images for printing, and ensuring scan reliability in production workflows. Developers often need to adjust rendering parameters and confirm that the resulting image can be successfully read by scanners or software libraries.
// Prompt: Validate barcode readability after applying Interpolation mode at 300 dpi by scanning the saved image.
// Tags: datamatrix, generation, recognition, interpolation, 300dpi, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a DataMatrix barcode with interpolation mode at 300 dpi,
/// saving it, and verifying its readability using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates, saves, reads, and validates a barcode,
    /// then cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the barcode image.
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "barcode.png");

        // Generate a DataMatrix barcode with interpolation auto‑size mode at 300 dpi.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "ASPOSE"))
        {
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation; // Use interpolation for scaling.
            generator.Parameters.Resolution = 300f;                         // Set image resolution to 300 dpi.
            generator.Parameters.ImageWidth.Pixels = 300f;                  // Define image width.
            generator.Parameters.ImageHeight.Pixels = 300f;                 // Define image height.
            generator.Parameters.Barcode.XDimension.Pixels = 3f;            // Set module size.
            generator.Save(barcodePath, BarCodeImageFormat.Png);            // Save as PNG.
        }

        // Verify that the barcode image file was created successfully.
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            Cleanup(tempFolder);
            return;
        }

        // Read and validate the barcode from the saved image.
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            bool anyFound = false;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                anyFound = true;
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
            }

            if (!anyFound)
            {
                Console.WriteLine("No barcode detected in the image.");
            }
            else
            {
                Console.WriteLine("Barcode read successfully.");
            }
        }

        // Clean up temporary files and folder.
        Cleanup(tempFolder);
    }

    /// <summary>
    /// Deletes the specified folder and its contents, handling any exceptions.
    /// </summary>
    /// <param name="folderPath">The path of the folder to delete.</param>
    static void Cleanup(string folderPath)
    {
        try
        {
            if (Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath, true);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup failed: {ex.Message}");
        }
    }
}