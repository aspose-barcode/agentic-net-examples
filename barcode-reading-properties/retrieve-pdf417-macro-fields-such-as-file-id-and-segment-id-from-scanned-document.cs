using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a PDF417 barcode with macro fields and then reading those fields.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a sample PDF417 barcode with macro fields if the image does not exist,
    /// then reads the barcode and outputs the macro field values.
    /// </summary>
    static void Main()
    {
        const string imagePath = "pdf417_macro.png";

        // ------------------------------------------------------------
        // Generate a sample PDF417 barcode with macro fields if needed
        // ------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            // Create a barcode generator for PDF417 with sample text
            using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Sample macro PDF417"))
            {
                // Configure macro fields for the PDF417 barcode
                generator.Parameters.Barcode.Pdf417.MacroPdf417FileID = 12345;      // Unique file identifier
                generator.Parameters.Barcode.Pdf417.MacroPdf417SegmentID = 1;      // Current segment number
                generator.Parameters.Barcode.Pdf417.MacroPdf417SegmentsCount = 3; // Total number of segments

                // Save the generated barcode image to disk
                generator.Save(imagePath);
                Console.WriteLine($"Generated barcode image: {imagePath}");
            }
        }

        // ------------------------------------------------------------
        // Read the barcode and extract macro fields
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.Pdf417))
        {
            // Iterate through all detected barcodes in the image
            foreach (var result in reader.ReadBarCodes())
            {
                // Access PDF417-specific extended information
                var pdf417Ext = result.Extended.Pdf417;

                // Output the macro field values to the console
                Console.WriteLine("Detected PDF417 Macro Fields:");
                Console.WriteLine($"  File ID: {pdf417Ext.MacroPdf417FileID}");
                Console.WriteLine($"  Segment ID: {pdf417Ext.MacroPdf417SegmentID}");
                Console.WriteLine($"  Segments Count: {pdf417Ext.MacroPdf417SegmentsCount}");
            }
        }
    }
}