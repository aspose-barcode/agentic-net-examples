// Title: Detect and count barcodes in an image using Aspose.BarCode
// Description: Demonstrates how to generate a barcode image if missing, read it, and use the FoundCount property to report the total number of detected barcodes.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing the BarCodeReader class for detecting multiple barcode symbologies in an image. Typical use cases include inventory scanning, document processing, and quality control where developers need to verify the presence and count of barcodes. The snippet illustrates generating a sample barcode, reading all supported types, and accessing the FoundCount property.
// Prompt: Read the FoundCount property to verify the total number of barcodes detected in the source image.
// Tags: barcode detection, foundcount, barcodereader, code128, png, aspnet.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation, detection, and counting using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a sample barcode if needed, reads the image, and prints the total count of detected barcodes.
    /// </summary>
    static void Main()
    {
        // Path for the sample barcode image
        const string imagePath = "sample.png";

        // Generate a sample barcode image if it does not exist
        if (!File.Exists(imagePath))
        {
            // Create a generator for Code128 symbology with sample text "123456"
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
            {
                // Save the generated barcode as a PNG file
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }
        }

        // Initialize a reader that will detect all supported barcode types in the image
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Perform the recognition and retrieve all detected barcode results
            var results = reader.ReadBarCodes();

            // Output the total number of detected barcodes using the FoundCount property
            Console.WriteLine($"Total barcodes detected: {reader.FoundCount}");

            // List each detected barcode's type and decoded text
            foreach (var result in results)
            {
                Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }
    }
}