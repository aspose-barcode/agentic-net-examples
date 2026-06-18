using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates reading PDF417 barcodes from an image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Reads a local image file, detects PDF417 barcodes, and prints their details.
    /// </summary>
    static void Main()
    {
        // NOTE: In a real environment you would download the image from AWS S3 using the AWS SDK.
        // Example (requires AWSSDK.S3 package):
        // var s3Client = new AmazonS3Client();
        // using var response = s3Client.GetObjectAsync("my-bucket", "path/to/image.png").Result;
        // using var s3Stream = response.ResponseStream;
        // var bitmap = new Bitmap(s3Stream);
        // For this runnable example we fall back to a local file.

        // Path to the local image containing the PDF417 barcode.
        string localImagePath = "sample.pdf417.png";

        // Verify that the image file exists before attempting to read it.
        if (!File.Exists(localImagePath))
        {
            Console.WriteLine($"Image file not found: {localImagePath}");
            return;
        }

        // Load the image into a Bitmap and create a BarCodeReader instance.
        using (var bitmap = new Bitmap(localImagePath))
        using (var reader = new BarCodeReader())
        {
            // Instruct the reader to decode only PDF417 barcodes.
            reader.SetBarCodeReadType(DecodeType.Pdf417);
            // Assign the bitmap image to the reader.
            reader.SetBarCodeImage(bitmap);

            // Iterate through all detected barcodes.
            foreach (var result in reader.ReadBarCodes())
            {
                // Output basic barcode information.
                Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");

                // Retrieve and display Macro PDF417 (linked state) metadata.
                var pdfExt = result.Extended.Pdf417;
                Console.WriteLine($"MacroPdf417 Segment ID: {pdfExt.MacroPdf417SegmentID}");
                Console.WriteLine($"MacroPdf417 Segments Count: {pdfExt.MacroPdf417SegmentsCount}");
                Console.WriteLine($"MacroPdf417 File ID: {pdfExt.MacroPdf417FileID}");

                // Output the orientation angle of the detected barcode region.
                Console.WriteLine($"Orientation Angle: {result.Region.Angle}");
                Console.WriteLine();
            }
        }
    }
}