// Title: Read PDF417 linked state from a generated barcode image
// Description: Demonstrates generating a PDF417 barcode with linked state enabled, saving it locally, and reading the linked state metadata using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating PDF417 barcodes and BarCodeReader for extracting extended PDF417 metadata such as the linked state. Developers working with PDF417 symbology often need to encode linked data and later verify it, making this pattern useful for inventory, shipping, or document tracking solutions.
// Prompt: Download image from AWS S3 bucket and read PDF417 linked state metadata.
// Tags: pdf417, linked state, barcode generation, barcode recognition, aspose.barcode, image processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a PDF417 barcode with linked state and reading its metadata.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, optionally downloads it from S3, and reads linked state metadata.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary working directory for the barcode image
        string workDir = Path.Combine(Path.GetTempPath(), "Pdf417Linked_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // Define the full path where the generated barcode image will be saved
        string barcodePath = Path.Combine(workDir, "linkedPdf417.png");

        // Generate a PDF417 barcode with the linked state enabled
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Pdf417, "SampleLinkedState"))
        {
            // Set barcode visual parameters
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            // Enable the linked state flag for PDF417
            generator.Parameters.Barcode.Pdf417.IsLinked = true;
            // Save the barcode as a PNG image
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // In a real environment you could download the image from AWS S3 like this:
        // (Amazon S3 SDK is not available in the snippet runner, so this code is commented out)
        /*
        using (var s3Client = new AmazonS3Client("accessKey", "secretKey", RegionEndpoint.USEast1))
        {
            var request = new GetObjectRequest
            {
                BucketName = "your-bucket-name",
                Key = "path/to/linkedPdf417.png"
            };
            using (var response = s3Client.GetObjectAsync(request).Result)
            using (var responseStream = response.ResponseStream)
            using (var fileStream = new FileStream(barcodePath, FileMode.Create, FileAccess.Write))
            {
                responseStream.CopyTo(fileStream);
            }
        }
        */

        // Verify that the barcode image was created before attempting to read it
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Barcode image not found: " + barcodePath);
            return;
        }

        // Read the barcode image and output linked state metadata
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Pdf417))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"IsLinked: {result.Extended.Pdf417.IsLinked}");
            }
        }

        // Clean up temporary files (optional)
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(workDir);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}