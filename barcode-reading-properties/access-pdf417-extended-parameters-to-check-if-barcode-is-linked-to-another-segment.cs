// Title: PDF417 Macro Barcode Linked Segment Detection
// Description: Demonstrates how to generate a PDF417 barcode with macro parameters and read its extended PDF417 properties to determine if it is linked to other segments.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on PDF417 macro (linked) barcodes. It showcases the use of BarcodeGenerator, BarCodeReader, and the Extended.Pdf417 properties to access macro information such as file ID, segment ID, and segment count. Developers working with multi-segment PDF417 barcodes can use this pattern to validate linking and manage segmented data.
// Prompt: Access PDF417 extended parameters to check if the barcode is linked to another segment.
// Tags: pdf417, macro, linked segment, barcode generation, barcode recognition, extended parameters, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a PDF417 barcode with macro (linked) parameters,
/// reads it back, and inspects the extended PDF417 properties to determine linkage.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a macro PDF417 barcode, reads it, and prints macro details.
    /// </summary>
    static void Main()
    {
        // Create a PDF417 barcode generator with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "SampleData"))
        {
            // Configure macro parameters to simulate a linked segment
            generator.Parameters.Barcode.Pdf417.MacroPdf417FileID = 12345;      // Identifier of the macro file
            generator.Parameters.Barcode.Pdf417.MacroPdf417SegmentID = 1;      // Current segment index
            generator.Parameters.Barcode.Pdf417.MacroPdf417SegmentsCount = 3; // Total number of segments

            // Save the generated barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for subsequent reading

                // Set up a reader for PDF417 barcodes using the memory stream
                BaseDecodeType decodeType = DecodeType.Pdf417;
                using (var reader = new BarCodeReader(ms, decodeType))
                {
                    // Read all barcodes found in the stream
                    var results = reader.ReadBarCodes();
                    foreach (var result in results)
                    {
                        // Output basic barcode information
                        Console.WriteLine($"Code Text: {result.CodeText}");
                        Console.WriteLine($"Symbology: {result.CodeTypeName}");

                        // Access PDF417 extended parameters (macro information)
                        var pdf417Ext = result.Extended.Pdf417;
                        Console.WriteLine($"MacroPdf417FileID: {pdf417Ext.MacroPdf417FileID}");
                        Console.WriteLine($"MacroPdf417SegmentID: {pdf417Ext.MacroPdf417SegmentID}");
                        Console.WriteLine($"MacroPdf417SegmentsCount: {pdf417Ext.MacroPdf417SegmentsCount}");

                        // Determine if this barcode is linked to another segment
                        bool isLinked = pdf417Ext.MacroPdf417SegmentsCount > 1;
                        Console.WriteLine($"Is linked to another segment: {isLinked}");
                    }
                }
            }
        }
    }
}