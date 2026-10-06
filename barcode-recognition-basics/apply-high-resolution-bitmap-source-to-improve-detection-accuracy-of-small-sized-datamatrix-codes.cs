// Title: High‑Resolution DataMatrix Barcode Generation and Recognition
// Description: Generates a small DataMatrix barcode at 600 DPI and demonstrates how to recognize it using high‑quality settings to improve detection of tiny codes.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Developers often need to produce high‑resolution images for small‑sized symbols and configure quality settings to ensure reliable detection in scanning applications.
// Prompt: Apply a high‑resolution bitmap source to improve detection accuracy of small‑sized DataMatrix codes.
// Tags: datamatrix, barcode, generation, recognition, high resolution, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a high‑resolution small DataMatrix barcode and recognizing it with
/// quality settings optimized for tiny symbols.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary folder, generates a high‑DPI DataMatrix barcode,
    /// reads it back using enhanced quality settings, and cleans up resources.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a unique temporary folder for the barcode image
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "DataMatrixDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "datamatrix.png");

        // --------------------------------------------------------------------
        // Generate a small DataMatrix barcode with high resolution (600 DPI)
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "ABC123"))
        {
            // Set DPI to produce a high‑resolution bitmap
            generator.Parameters.Resolution = 600f;
            // Reduce module size to keep the physical barcode small
            generator.Parameters.Barcode.XDimension.Point = 0.5f;
            // Save the barcode as a PNG image
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Verify that the barcode image was successfully created
        // --------------------------------------------------------------------
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // --------------------------------------------------------------------
        // Read the barcode using high‑resolution quality settings
        // --------------------------------------------------------------------
        BaseDecodeType decodeType = DecodeType.DataMatrix;
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            // Configure reader for small barcodes
            reader.QualitySettings.XDimension = XDimensionMode.Small;
            reader.QualitySettings.MinimalXDimension = 1f;
            reader.QualitySettings.BarcodeQuality = BarcodeQualityMode.High;

            // Perform recognition and output results
            bool found = false;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected DataMatrix code: {result.CodeText}");
                found = true;
            }

            if (!found)
            {
                Console.WriteLine("No DataMatrix barcode detected.");
            }
        }

        // --------------------------------------------------------------------
        // Clean up temporary files and folder
        // --------------------------------------------------------------------
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failure should not affect program outcome
        }
    }
}