// Title: Generate Swiss Post Parcel barcode with auto‑checksum correction and cloud storage upload
// Description: Demonstrates creating a Swiss Post Parcel international barcode, enabling automatic checksum correction, and saving the image for later upload to a cloud storage bucket.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on symbology‑specific settings and image output. It showcases the use of BarcodeGenerator, EncodeTypes, and image format classes to produce barcodes, a common task for developers integrating barcode creation into applications that require storage in cloud services such as Azure Blob or AWS S3.
// Prompt: Generate a Swiss Post Parcel international barcode with checksum auto‑correction and store in a cloud storage bucket.
// Tags: swisspostparcel, barcode, generation, checksum, auto-correction, png, cloud-storage, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Swiss Post Parcel barcode,
/// enables automatic checksum correction, saves the image locally,
/// and outlines how to upload it to a cloud storage bucket.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define a sample Swiss Post Parcel code.
        // Replace with a valid code as required by your use case.
        string codeText = "123456789012";

        // Initialize the barcode generator for the Swiss Post Parcel symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, codeText))
        {
            // Allow the generator to auto‑correct the checksum instead of throwing an exception.
            generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

            // Optional visual customizations.
            generator.Parameters.Barcode.BarColor = Color.Black;   // Set barcode bars to black.
            generator.Parameters.BackColor = Color.White;          // Set background to white.
            generator.Parameters.Barcode.XDimension.Point = 2f;    // Define module (X) size.

            // Determine the output file path in the current working directory.
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "SwissPostParcel.png");

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);

            Console.WriteLine($"Barcode image saved to: {outputPath}");

            // -----------------------------------------------------------------
            // Cloud storage upload (e.g., Azure Blob Storage, AWS S3) would be performed here.
            // The required SDKs are not included in this snippet; see documentation for integration.
            // -----------------------------------------------------------------
            // var connectionString = "<your-connection-string>";
            // var containerName = "<your-container>";
            // var blobName = "SwissPostParcel.png";
            // var blobClient = new BlobClient(connectionString, containerName, blobName);
            // using (var fileStream = File.OpenRead(outputPath))
            // {
            //     blobClient.Upload(fileStream);
            // }
        }
    }
}