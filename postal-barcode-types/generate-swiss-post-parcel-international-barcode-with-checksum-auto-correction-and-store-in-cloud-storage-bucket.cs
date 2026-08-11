// Title: Generate Swiss Post Parcel Barcode with Auto‑Checksum and Save to Cloud
// Description: Demonstrates how to create a Swiss Post Parcel (international) barcode, enable automatic checksum correction, and save the image as PNG.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It shows how to use the BarcodeGenerator class with EncodeTypes.SwissPostParcel, configure checksum settings, and export the result to an image file. Developers working with postal symbologies often need to generate barcodes that comply with specific standards and then store them in cloud storage for downstream processing.
// Prompt: Generate a Swiss Post Parcel international barcode with checksum auto‑correction and store in a cloud storage bucket.
// Tags: barcode, swisspostparcel, checksum, image, png, cloud, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Swiss Post Parcel barcode,
/// enables automatic checksum correction, saves it as a PNG file,
/// and provides a placeholder for uploading the file to cloud storage.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the raw data for the Swiss Post Parcel (international) barcode.
        // In a real scenario, this should follow the Swiss Post specification.
        string codeText = "1234567890123";

        // Initialize the barcode generator for the Swiss Post Parcel symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, codeText))
        {
            // Allow the generator to automatically correct the checksum
            // instead of throwing an exception for incorrect code text.
            generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

            // Enable checksum generation (required for most postal barcodes).
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Build the full path for the output PNG file.
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "SwissPostParcel.png");

            // Save the generated barcode image to the specified path in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);

            Console.WriteLine($"Barcode image saved to: {outputPath}");

            // -----------------------------------------------------------------
            // Cloud storage upload placeholder.
            // The actual upload would require a cloud SDK (e.g., Google Cloud,
            // AWS S3, Azure Blob). Since such packages are not available in the
            // snippet runner, the implementation is shown as a comment.
            //
            // Example (Google Cloud Storage):
            // using Google.Cloud.Storage.V1;
            // var storage = StorageClient.Create();
            // string bucketName = "my-bucket";
            // string objectName = "SwissPostParcel.png";
            // using var fileStream = File.OpenRead(outputPath);
            // storage.UploadObject(bucketName, objectName, "image/png", fileStream);
            // -----------------------------------------------------------------
        }
    }
}