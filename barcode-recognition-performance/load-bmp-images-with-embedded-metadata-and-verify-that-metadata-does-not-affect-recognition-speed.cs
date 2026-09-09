// Title: Compare barcode recognition speed for BMP images with and without embedded metadata
// Description: Demonstrates loading BMP images that contain barcode metadata and measuring whether the metadata impacts recognition performance.
// Category-Description: This example belongs to the Aspose.BarCode image processing category, illustrating how to generate barcodes, embed optional metadata, and use the BarCodeReader for fast decoding. Developers often need to assess the effect of image metadata on scanning speed, especially in high‑throughput scenarios. The key API classes used are BarcodeGenerator, BarCodeReader, and related parameter objects.
// Prompt: Load BMP images with embedded metadata and verify that metadata does not affect recognition speed.
// Tags: aztec, barcode, metadata, performance, bmp, generation, recognition, aspose.barcode

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating Aztec barcodes with and without embedded metadata,
/// measuring recognition time, and confirming that metadata does not significantly affect performance.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary BMP files, measures decoding times,
    /// outputs results, and cleans up temporary resources.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for test images
        string tempDir = Path.Combine(Path.GetTempPath(), "MetaTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define file paths for images with and without metadata
        string pathWithMeta = Path.Combine(tempDir, "meta.bmp");
        string pathWithoutMeta = Path.Combine(tempDir, "plain.bmp");

        // Generate an Aztec barcode and embed metadata (structured append, file ID, etc.)
        using (var gen = new BarcodeGenerator(EncodeTypes.Aztec, "SampleMeta"))
        {
            gen.Parameters.Barcode.XDimension.Pixels = 4;
            gen.Parameters.Barcode.Aztec.SymbolMode = AztecSymbolMode.FullRange;
            gen.Parameters.Barcode.Aztec.IsReaderInitialization = true;
            gen.Parameters.Barcode.Aztec.StructuredAppendBarcodeId = 2;
            gen.Parameters.Barcode.Aztec.StructuredAppendBarcodesCount = 4;
            gen.Parameters.Barcode.Aztec.StructuredAppendFileId = "File01";
            gen.Save(pathWithMeta, BarCodeImageFormat.Bmp);
        }

        // Generate a plain Aztec barcode without any additional metadata
        using (var gen = new BarcodeGenerator(EncodeTypes.Aztec, "SamplePlain"))
        {
            gen.Parameters.Barcode.XDimension.Pixels = 4;
            gen.Save(pathWithoutMeta, BarCodeImageFormat.Bmp);
        }

        // Verify that both image files were created successfully
        if (!File.Exists(pathWithMeta) || !File.Exists(pathWithoutMeta))
        {
            Console.WriteLine("Failed to create test images.");
            return;
        }

        // Measure recognition time for the image that contains metadata
        long timeWithMeta = MeasureReadTime(pathWithMeta);
        // Measure recognition time for the image without metadata
        long timeWithoutMeta = MeasureReadTime(pathWithoutMeta);

        // Output the measured times
        Console.WriteLine($"Recognition time (metadata): {timeWithMeta} ms");
        Console.WriteLine($"Recognition time (no metadata): {timeWithoutMeta} ms");

        // Compare the two timings and report whether the difference is within an acceptable range
        long diff = Math.Abs(timeWithMeta - timeWithoutMeta);
        Console.WriteLine($"Difference: {diff} ms");
        if (diff <= 20) // arbitrary small threshold
        {
            Console.WriteLine("Metadata does not significantly affect recognition speed.");
        }
        else
        {
            Console.WriteLine("Metadata may affect recognition speed.");
        }

        // Cleanup temporary files and directory
        try
        {
            File.Delete(pathWithMeta);
            File.Delete(pathWithoutMeta);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }

    /// <summary>
    /// Measures the time required to read all barcodes from the specified image.
    /// </summary>
    /// <param name="imagePath">Full path to the BMP image containing the barcode.</param>
    /// <returns>Elapsed time in milliseconds.</returns>
    static long MeasureReadTime(string imagePath)
    {
        var stopwatch = Stopwatch.StartNew();
        using (var reader = new BarCodeReader(imagePath, DecodeType.Aztec))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                // Access the result to ensure the barcode is fully processed
                var _ = result.CodeText;
            }
        }
        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }
}