// Title: Load BMP barcodes with and without metadata and compare recognition speed
// Description: Demonstrates generating Aztec barcodes saved as BMP files, one plain and one containing Aztec metadata, then measures and compares the time required to recognize each image.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It shows how to use BarcodeGenerator to create barcodes, how to embed Aztec-specific metadata, and how to employ BarCodeReader for decoding. Typical use cases include performance testing, metadata handling, and validating that additional barcode information does not degrade scan speed. Developers often need to generate barcodes with custom settings, read extended properties, and benchmark recognition times.
// Prompt: Load BMP images with embedded metadata and verify that metadata does not affect recognition speed.
// Tags: aztec, metadata, performance, barcode-generation, barcode-recognition, bmp, aspose.barcode

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that creates two Aztec barcode images (plain and with metadata),
/// measures their recognition times, and demonstrates that embedded metadata does not
/// significantly affect decoding performance.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcode images, measures recognition speed,
    /// outputs results, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the generated images
        string tempFolder = Path.Combine(Path.GetTempPath(), "MetadataTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the plain and metadata‑enhanced images
        string plainPath = Path.Combine(tempFolder, "AztecPlain.bmp");
        string metaPath = Path.Combine(tempFolder, "AztecMeta.bmp");
        string codeText = "SampleMeta";

        // ------------------------------------------------------------
        // Generate a plain Aztec barcode (no metadata)
        // ------------------------------------------------------------
        using (BarcodeGenerator plainGen = new BarcodeGenerator(EncodeTypes.Aztec, codeText))
        {
            plainGen.Parameters.Barcode.XDimension.Pixels = 4f;
            plainGen.Save(plainPath, BarCodeImageFormat.Bmp);
        }

        // ------------------------------------------------------------
        // Generate an Aztec barcode with embedded metadata
        // ------------------------------------------------------------
        using (BarcodeGenerator metaGen = new BarcodeGenerator(EncodeTypes.Aztec, codeText))
        {
            metaGen.Parameters.Barcode.XDimension.Pixels = 4f;
            metaGen.Parameters.Barcode.Aztec.SymbolMode = AztecSymbolMode.FullRange;
            metaGen.Parameters.Barcode.Aztec.IsReaderInitialization = true;
            metaGen.Parameters.Barcode.Aztec.StructuredAppendBarcodeId = 2;
            metaGen.Parameters.Barcode.Aztec.StructuredAppendBarcodesCount = 4;
            metaGen.Parameters.Barcode.Aztec.StructuredAppendFileId = "File01";
            metaGen.Save(metaPath, BarCodeImageFormat.Bmp);
        }

        // Verify that both image files were created successfully
        if (!File.Exists(plainPath) || !File.Exists(metaPath))
        {
            Console.WriteLine("Failed to create barcode images.");
            return;
        }

        // ------------------------------------------------------------
        // Local function to measure the time required to recognize a barcode
        // ------------------------------------------------------------
        double MeasureRecognition(string imagePath, BaseDecodeType decodeType)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            using (BarCodeReader reader = new BarCodeReader(imagePath, decodeType))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // Output basic result information to ensure full processing
                    Console.WriteLine($"Detected: {result.CodeTypeName}, Text: {result.CodeText}");

                    // If the barcode contains Aztec metadata, display the extended properties
                    if (result.Extended?.Aztec != null)
                    {
                        Console.WriteLine($"IsReaderInit: {result.Extended.Aztec.IsReaderInitialization}");
                        Console.WriteLine($"StructAppendId: {result.Extended.Aztec.StructuredAppendBarcodeId}");
                        Console.WriteLine($"StructAppendCount: {result.Extended.Aztec.StructuredAppendBarcodesCount}");
                        Console.WriteLine($"StructAppendFileId: {result.Extended.Aztec.StructuredAppendFileId}");
                    }
                }
            }

            sw.Stop();
            return sw.Elapsed.TotalMilliseconds;
        }

        // Measure recognition speed for the plain image
        double plainTime = MeasureRecognition(plainPath, DecodeType.Aztec);
        // Measure recognition speed for the image that includes metadata
        double metaTime = MeasureRecognition(metaPath, DecodeType.Aztec);

        // Output the timing results
        Console.WriteLine($"Recognition time (plain): {plainTime} ms");
        Console.WriteLine($"Recognition time (with metadata): {metaTime} ms");
        Console.WriteLine("Metadata does not significantly affect recognition speed if times are comparable.");

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