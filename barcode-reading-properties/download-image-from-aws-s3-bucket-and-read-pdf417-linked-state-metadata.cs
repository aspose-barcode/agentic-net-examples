// Title: Read PDF417 Linked State Metadata from Image
// Description: Downloads an image (simulated) and extracts PDF417 barcode data along with its linked state metadata using Aspose.BarCode.
// Prompt: Download image from AWS S3 bucket and read PDF417 linked state metadata.
// Tags: pdf417, barcode, metadata, aspose, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how to read PDF417 barcode data and its linked state metadata from an image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Downloads (simulated) an image and processes PDF417 barcodes.
    /// </summary>
    static void Main()
    {
        // NOTE: In a real environment you would download the image from AWS S3 using the AWS SDK.
        // The SDK is not available in the snippet runner, so we fall back to a local file.
        // Example of real download (commented out):
        // var s3Client = new AmazonS3Client(accessKey, secretKey, RegionEndpoint.USEast1);
        // using var response = s3Client.GetObjectAsync(bucketName, objectKey).Result;
        // using var s3Stream = response.ResponseStream;
        // using var fileStream = File.Create(localPath);
        // s3Stream.CopyTo(fileStream);

        // Path to the local image file that would have been downloaded from S3.
        string localImagePath = "sample_pdf417.png";

        // Verify that the image file exists before attempting to read it.
        if (!File.Exists(localImagePath))
        {
            Console.WriteLine($"Image file not found: {localImagePath}");
            return;
        }

        // Create a BarCodeReader configured for PDF417 symbology.
        using (var reader = new BarCodeReader(localImagePath, DecodeType.Pdf417))
        {
            // Iterate through all detected barcodes in the image.
            foreach (var result in reader.ReadBarCodes())
            {
                // Output basic barcode information.
                Console.WriteLine($"Detected Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");

                // Access PDF417 extended (linked state) metadata, if present.
                var pdf417Ext = result.Extended.Pdf417;
                if (pdf417Ext != null)
                {
                    Console.WriteLine("PDF417 Linked State Metadata:");
                    Console.WriteLine($"  MacroPdf417SegmentID: {pdf417Ext.MacroPdf417SegmentID}");
                    Console.WriteLine($"  MacroPdf417SegmentsCount: {pdf417Ext.MacroPdf417SegmentsCount}");
                    Console.WriteLine($"  MacroPdf417FileID: {pdf417Ext.MacroPdf417FileID}");
                    Console.WriteLine($"  MacroPdf417Addressee: {pdf417Ext.MacroPdf417Addressee}");
                    Console.WriteLine($"  MacroPdf417Sender: {pdf417Ext.MacroPdf417Sender}");
                    Console.WriteLine($"  MacroPdf417TimeStamp: {pdf417Ext.MacroPdf417TimeStamp}");
                }
                else
                {
                    // No extended metadata was found for this barcode.
                    Console.WriteLine("No PDF417 extended metadata available.");
                }

                Console.WriteLine(); // Blank line between results for readability.
            }
        }
    }
}