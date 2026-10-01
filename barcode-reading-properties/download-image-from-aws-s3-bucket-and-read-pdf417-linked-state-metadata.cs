// Title: Read Macro PDF417 Metadata from an Image Downloaded from AWS S3
// Description: Demonstrates downloading a barcode image from an AWS S3 bucket (or generating a sample) and extracting PDF417 linked state (Macro PDF417) metadata using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on PDF417 symbology with Macro (linked state) support. It showcases the use of BarcodeGenerator to create a PDF417 barcode with macro properties and BarCodeReader to decode the barcode and retrieve extended metadata. Developers commonly use these APIs for multi-part barcode handling, document scanning, and data integrity verification.
// Prompt: Download image from AWS S3 bucket and read PDF417 linked state metadata.
// Tags: pdf417, macro, barcode, generation, recognition, image, aws, s3, metadata

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Program that downloads (or generates) a PDF417 barcode image and reads its Macro PDF417 metadata.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a sample barcode if not present, reads the image, and outputs Macro PDF417 metadata.
    /// </summary>
    static void Main()
    {
        // Define S3 bucket and object key (placeholder values)
        string bucketName = "my-s3-bucket";
        string objectKey = "sample-barcode.png";

        // Local path where the image will be stored
        string localImagePath = Path.Combine(Path.GetTempPath(), "sample-barcode.png");

        // -----------------------------------------------------------------
        // Attempt to download the image from AWS S3.
        // The actual AWS SDK code is commented out because the required
        // assemblies are not available in the snippet runner environment.
        // -----------------------------------------------------------------
        /*
        // Uncomment and add the necessary NuGet package (AWSSDK.S3) in a real project.
        using (var s3Client = new AmazonS3Client(Amazon.RegionEndpoint.USEast1))
        {
            var request = new GetObjectRequest
            {
                BucketName = bucketName,
                Key = objectKey
            };
            using (var response = s3Client.GetObjectAsync(request).Result)
            using (var responseStream = response.ResponseStream)
            using (var fileStream = new FileStream(localImagePath, FileMode.Create, FileAccess.Write))
            {
                responseStream.CopyTo(fileStream);
            }
        }
        */

        // -----------------------------------------------------------------
        // If the image does not exist locally, generate a sample PDF417 barcode
        // with Macro PDF417 (linked state) metadata.
        // -----------------------------------------------------------------
        if (!File.Exists(localImagePath))
        {
            // Sample code text; actual content can be anything.
            string sampleCodeText = "Sample PDF417 with Macro";

            // Create a PDF417 barcode generator.
            using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, sampleCodeText))
            {
                // Configure Macro PDF417 (linked state) properties.
                generator.Parameters.Barcode.Pdf417.MacroPdf417FileID = 12345;      // Identifier for the whole file.
                generator.Parameters.Barcode.Pdf417.MacroPdf417SegmentID = 1;      // Current segment number.
                generator.Parameters.Barcode.Pdf417.MacroPdf417SegmentsCount = 3; // Total number of segments.

                // Save the barcode image to the local path.
                generator.Save(localImagePath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Sample barcode generated at: {localImagePath}");
        }
        else
        {
            Console.WriteLine($"Using existing image at: {localImagePath}");
        }

        // -----------------------------------------------------------------
        // Read the barcode and extract linked state (Macro PDF417) metadata.
        // -----------------------------------------------------------------
        if (!File.Exists(localImagePath))
        {
            Console.WriteLine("Error: Barcode image not found.");
            return;
        }

        // Create a reader for PDF417 symbology.
        using (var reader = new BarCodeReader(localImagePath, DecodeType.Pdf417))
        {
            // Read all barcodes found in the image.
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes detected.");
                return;
            }

            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"CodeType: {result.CodeTypeName}");

                // Access extended PDF417 parameters if present.
                var pdf417Ext = result.Extended?.Pdf417;
                if (pdf417Ext != null)
                {
                    Console.WriteLine($"MacroPdf417FileID: {pdf417Ext.MacroPdf417FileID}");
                    Console.WriteLine($"MacroPdf417SegmentID: {pdf417Ext.MacroPdf417SegmentID}");
                    Console.WriteLine($"MacroPdf417SegmentsCount: {pdf417Ext.MacroPdf417SegmentsCount}");
                }
                else
                {
                    Console.WriteLine("No PDF417 extended metadata available.");
                }
            }
        }
    }
}