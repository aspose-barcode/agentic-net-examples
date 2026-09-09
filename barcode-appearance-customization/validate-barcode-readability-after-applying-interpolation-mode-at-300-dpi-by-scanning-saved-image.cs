// Title: Validate barcode readability after applying interpolation mode at 300 dpi
// Description: Generates a DataMatrix barcode using interpolation scaling at 300 dpi, saves it as PNG, and reads it back to confirm the barcode can be decoded.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition APIs. The example uses BarcodeGenerator to create a barcode with AutoSizeMode.Interpolation, sets image resolution, and then employs BarCodeReader to decode the saved image. This pattern is common for developers who need to ensure barcode quality after image processing or scaling operations, especially when preparing assets for high‑resolution printing or scanning.
// Prompt: Validate barcode readability after applying Interpolation mode at 300 dpi by scanning the saved image.
// Tags: datamatrix, barcode generation, barcode recognition, interpolation, 300dpi, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates creating a DataMatrix barcode with interpolation scaling at 300 dpi,
/// saving it to a temporary PNG file, and then verifying its readability using Aspose.BarCode's
/// recognition engine.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates the barcode, saves it, reads it back,
    /// outputs the decoding result, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a unique temporary folder and file path for the barcode image
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "barcode.png");

        // --------------------------------------------------------------
        // Generate a DataMatrix barcode using interpolation mode at 300 dpi
        // --------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "ASPOSE"))
        {
            // Use interpolation scaling to fit the requested dimensions
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set the target image size (pixels) – 300 × 300
            generator.Parameters.ImageWidth.Pixels = 300f;
            generator.Parameters.ImageHeight.Pixels = 300f;

            // Define module size (X dimension) for the DataMatrix
            generator.Parameters.Barcode.XDimension.Pixels = 3f;

            // Set the image resolution to 300 dpi
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode as a PNG file
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // -------------------------------------------------
        // Verify that the barcode image file was created
        // -------------------------------------------------
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // -------------------------------------------------
        // Read and decode the barcode from the saved image
        // -------------------------------------------------
        BaseDecodeType decodeType = DecodeType.AllSupportedTypes;
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            var results = reader.ReadBarCodes();

            // Determine if decoding succeeded
            bool success = results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText);
            Console.WriteLine($"Barcode read {(success ? "successful" : "failed")}.");

            // Output details for each decoded barcode
            if (success)
            {
                foreach (var result in results)
                {
                    Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}, Quality: {result.ReadingQuality}");
                }
            }
        }

        // ---------------------------------
        // Clean up temporary files and folder
        // ---------------------------------
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup is not critical for the demo
        }
    }
}