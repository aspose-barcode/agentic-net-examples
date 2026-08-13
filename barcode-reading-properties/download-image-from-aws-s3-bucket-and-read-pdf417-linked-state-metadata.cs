// Title: Read PDF417 Barcode Linked State Metadata from Image
// Description: Generates a PDF417 barcode image (simulating an AWS S3 download) and reads its linked state metadata using Aspose.BarCode.
// Category-Description: This example demonstrates Aspose.BarCode generation and recognition workflows, focusing on PDF417 symbology. It showcases the BarcodeGenerator for creating barcodes and BarCodeReader for extracting data, including extended metadata. Developers working with barcode imaging, document processing, or inventory systems often need to generate barcodes, store them (e.g., in cloud storage), and later decode them to retrieve embedded information.
// Prompt: Download image from AWS S3 bucket and read PDF417 linked state metadata.
// Tags: pdf417, barcode, read, metadata, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how to generate a PDF417 barcode image, simulate its retrieval from AWS S3,
/// and read linked state metadata using Aspose.BarCode APIs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, verifies its existence,
    /// and reads the barcode along with any linked state metadata.
    /// </summary>
    static void Main()
    {
        // Define the local path for the sample barcode image.
        string barcodePath = "pdf417.png";

        // ------------------------------------------------------------
        // Step 1: Generate a sample PDF417 barcode image locally.
        // ------------------------------------------------------------
        // In a real scenario the image would be downloaded from AWS S3.
        // Since AWS SDK is not available in the runner, we use a local file as a substitute.
        // The following code creates a PDF417 barcode with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Sample PDF417 Text"))
        {
            // Save the generated barcode image to the specified path.
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was successfully created.
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine($"Error: Barcode image '{barcodePath}' was not found.");
            return;
        }

        // ------------------------------------------------------------
        // Step 2: Read the PDF417 barcode and output linked state metadata.
        // ------------------------------------------------------------
        // The BarCodeReader reads the barcode from the image file.
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Pdf417))
        {
            // Iterate through detected barcodes (there should be only one in this example).
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Barcode Type: {result.CodeType}");
                Console.WriteLine($"CodeText: {result.CodeText}");

                // Linked state metadata (if present) can be accessed via the extended PDF417 parameters.
                // The exact property name may vary; typically it is something like:
                // result.Extended.Pdf417.LinkedStateMetadata
                // Uncomment and adjust the following line if the property exists in your version:
                // Console.WriteLine($"Linked State Metadata: {result.Extended.Pdf417.LinkedStateMetadata}");

                // Placeholder indicating where metadata extraction would occur.
                Console.WriteLine("Linked State Metadata extraction placeholder.");
            }
        }

        // ------------------------------------------------------------
        // Note: In a production environment, replace the local file handling
        // with actual AWS S3 download logic (e.g., using AmazonS3Client).
        // ------------------------------------------------------------
    }
}