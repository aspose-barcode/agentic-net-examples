using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Swiss Post Parcel barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a barcode image and saves it to a temporary file.
    /// </summary>
    static void Main()
    {
        // Sample codetext for Swiss Post Parcel international barcode.
        // In a real scenario, this should follow the Swiss Post specification.
        string codeText = "1234567890123";

        // Build a full path in the system's temporary folder for the output PNG file.
        string outputPath = Path.Combine(Path.GetTempPath(), "SwissPostParcel.png");

        // Initialize the barcode generator with the SwissPostParcel symbology and the provided codetext.
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, codeText))
        {
            // Allow the generator to automatically correct an invalid codetext instead of throwing.
            generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

            // Enable checksum calculation (required for many postal barcodes).
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Set a higher resolution (dots per inch) for better image quality.
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode image to the specified file (PNG format by default).
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");

        // -------------------------------------------------------------------------
        // Cloud storage upload (placeholder):
        // The following comment shows where you would integrate with a cloud SDK
        // (e.g., AWS S3, Azure Blob Storage, Google Cloud Storage) to upload the
        // generated file to a bucket. The actual implementation depends on the
        // specific SDK and credentials, which are not available in this runner.
        //
        // Example (pseudo‑code):
        // var client = new CloudStorageClient(...);
        // client.UploadFile(bucketName, "SwissPostParcel.png", File.ReadAllBytes(outputPath));
        // -------------------------------------------------------------------------
    }
}