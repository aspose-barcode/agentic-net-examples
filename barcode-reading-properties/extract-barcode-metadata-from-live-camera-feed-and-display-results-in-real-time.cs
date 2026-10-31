// Title: Extract PDF417 macro metadata from generated barcodes
// Description: Demonstrates generating PDF417 barcodes with macro metadata, reading them, and displaying extracted information. Useful for verifying macro data in batch barcode processing.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create PDF417 barcodes with macro information and BarCodeReader to decode and retrieve extended PDF417 metadata such as file ID, segment ID, and segment count. Developers working with bulk PDF417 documents, inventory tracking, or secure data transmission often need to embed and read macro data, making this pattern a common requirement.
// Prompt: Extract barcode metadata from live camera feed and display results in real time.
// Tags: pdf417, macro, metadata, barcode generation, barcode recognition, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Sample program that generates PDF417 barcodes with macro metadata,
/// reads them back, and prints the extracted information to the console.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a series of barcodes, decodes each one, and outputs metadata.
    /// </summary>
    static void Main()
    {
        // Number of barcode samples to generate and process
        const int sampleCount = 3;

        // Iterate through each sample
        for (int i = 1; i <= sampleCount; i++)
        {
            // ------------------------------------------------------------
            // Generate a PDF417 barcode with macro metadata and store it in memory
            // ------------------------------------------------------------
            using (var barcodeStream = new MemoryStream())
            {
                using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, $"SampleData{i}"))
                {
                    // Set macro-specific properties
                    generator.Parameters.Barcode.Pdf417.MacroPdf417FileID = 1000 + i;
                    generator.Parameters.Barcode.Pdf417.MacroPdf417SegmentID = i;
                    generator.Parameters.Barcode.Pdf417.MacroPdf417SegmentsCount = sampleCount;

                    // Define visual appearance
                    generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                    generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                    // Save the generated barcode as PNG into the memory stream
                    generator.Save(barcodeStream, BarCodeImageFormat.Png);
                }

                // Reset stream position for reading
                barcodeStream.Position = 0;

                // ------------------------------------------------------------
                // Read the barcode from the memory stream and extract metadata
                // ------------------------------------------------------------
                using (var reader = new BarCodeReader(barcodeStream, DecodeType.AllSupportedTypes))
                {
                    // Use high-quality settings for more reliable detection
                    reader.QualitySettings = QualitySettings.HighQuality;

                    BarCodeResult[] results;
                    try
                    {
                        // Perform the recognition
                        results = reader.ReadBarCodes();
                    }
                    catch (RecognitionAbortedException ex)
                    {
                        Console.WriteLine($"Recognition aborted: {ex.Message}");
                        continue;
                    }

                    // Handle case where no barcode was detected
                    if (results.Length == 0)
                    {
                        Console.WriteLine($"No barcode detected in sample {i}.");
                        continue;
                    }

                    // Output information for each detected barcode
                    foreach (var result in results)
                    {
                        Console.WriteLine($"Sample {i}:");
                        Console.WriteLine($"  Code Text : {result.CodeText}");
                        Console.WriteLine($"  Code Type : {result.CodeTypeName}");

                        // Region and orientation details
                        var rect = result.Region.Rectangle;
                        Console.WriteLine($"  Region    : X={rect.X}, Y={rect.Y}, Width={rect.Width}, Height={rect.Height}");
                        Console.WriteLine($"  Angle     : {result.Region.Angle} degrees");

                        // PDF417 specific macro metadata
                        var pdf417Ext = result.Extended.Pdf417;
                        if (pdf417Ext != null)
                        {
                            Console.WriteLine($"  PDF417 Macro File ID   : {pdf417Ext.MacroPdf417FileID}");
                            Console.WriteLine($"  PDF417 Macro Segment ID: {pdf417Ext.MacroPdf417SegmentID}");
                            Console.WriteLine($"  PDF417 Macro Segments Count: {pdf417Ext.MacroPdf417SegmentsCount}");
                        }

                        Console.WriteLine();
                    }
                }
            }
        }
    }
}