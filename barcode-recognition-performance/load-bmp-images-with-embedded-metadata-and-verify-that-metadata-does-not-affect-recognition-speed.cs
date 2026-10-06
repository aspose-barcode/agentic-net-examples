// Title: Load BMP barcode images with metadata and compare recognition speed
// Description: Demonstrates generating Aztec barcodes saved as BMP files with and without embedded metadata, then measures the time required to recognize each image.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the BarcodeGenerator class for creating barcodes and the BarCodeReader class for decoding them. Typical use cases include testing how image metadata influences decoding performance, a common concern for developers optimizing barcode scanning workflows.
// Prompt: Load BMP images with embedded metadata and verify that metadata does not affect recognition speed.
// Tags: aztec, bmp, metadata, performance, barcode-generation, barcode-recognition, aspose.barcode

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that creates Aztec barcode images with and without embedded metadata,
/// then measures and compares the recognition time for each image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcode images, measures decoding speed, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the generated images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeMetaTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the images with and without metadata
        string metaPath = Path.Combine(tempFolder, "aztec_meta.bmp");
        string noMetaPath = Path.Combine(tempFolder, "aztec_nometa.bmp");

        // ------------------------------------------------------------
        // Generate Aztec barcode with embedded metadata
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Aztec, "SampleMeta"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.Aztec.SymbolMode = AztecSymbolMode.FullRange;
            generator.Parameters.Barcode.Aztec.IsReaderInitialization = true;
            generator.Parameters.Barcode.Aztec.StructuredAppendBarcodeId = 2;
            generator.Parameters.Barcode.Aztec.StructuredAppendBarcodesCount = 4;
            generator.Parameters.Barcode.Aztec.StructuredAppendFileId = "File01";
            generator.Save(metaPath, BarCodeImageFormat.Bmp);
        }

        // ------------------------------------------------------------
        // Generate Aztec barcode without any additional metadata
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Aztec, "SampleMeta"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Save(noMetaPath, BarCodeImageFormat.Bmp);
        }

        // Verify that both image files were created successfully
        if (!File.Exists(metaPath) || !File.Exists(noMetaPath))
        {
            Console.WriteLine("Failed to create barcode images.");
            return;
        }

        var stopwatch = new Stopwatch();

        // ------------------------------------------------------------
        // Measure recognition time for the image that contains metadata
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(metaPath, DecodeType.Aztec))
        {
            stopwatch.Start();
            BarCodeResult[] results = reader.ReadBarCodes();
            stopwatch.Stop();
            Console.WriteLine($"Metadata image read time: {stopwatch.ElapsedMilliseconds} ms, detected: {results.Length}");
        }

        // Reset the stopwatch before the next measurement
        stopwatch.Reset();

        // ------------------------------------------------------------
        // Measure recognition time for the image without metadata
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(noMetaPath, DecodeType.Aztec))
        {
            stopwatch.Start();
            BarCodeResult[] results = reader.ReadBarCodes();
            stopwatch.Stop();
            Console.WriteLine($"No-metadata image read time: {stopwatch.ElapsedMilliseconds} ms, detected: {results.Length}");
        }

        // ------------------------------------------------------------
        // Cleanup temporary files and folder
        // ------------------------------------------------------------
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore any errors that occur during cleanup
        }
    }
}