// Title: Generate Swiss Post Parcel International Barcode with Auto‑Checksum Correction
// Description: This example creates a Swiss Post Parcel International barcode, lets Aspose.BarCode automatically correct an invalid checksum, saves the image, and demonstrates reading the corrected value.
// Category-Description: Shows how to work with Aspose.BarCode's Swiss Post symbology, covering barcode generation, automatic checksum correction, image saving, and basic recognition. Developers dealing with postal barcode standards use EncodeTypes.SwissPostParcel, BarcodeGenerator, and BarCodeReader to produce and validate barcodes for shipping and logistics applications.
// Prompt: Generate a Swiss Post Parcel international barcode with checksum auto‑correction and store in a cloud storage bucket.
// Tags: swisspost, parcel, barcode, generation, recognition, checksum, image, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Swiss Post Parcel International barcode with automatic checksum correction,
/// saving it to a file, and reading it back using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it, reads it back, and (optionally) shows how to upload to cloud storage.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output directory for the generated barcode image.
        string outputDir = Path.Combine(Path.GetTempPath(), "SwissPostBarcodes");
        Directory.CreateDirectory(outputDir);

        // Define the full file path for the PNG image.
        string barcodePath = Path.Combine(outputDir, "SwissPostInternational.png");

        // ----------------------------------------------------------------------
        // Generate a Swiss Post Parcel International barcode.
        // The input data contains an incorrect checksum; Aspose.BarCode will
        // automatically correct it during generation.
        // ----------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, "RM999605017CH"))
        {
            // Set barcode visual parameters.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.BarHeight.Pixels = 40f;

            // Save the generated barcode as a PNG image.
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to: {barcodePath}");

        // ----------------------------------------------------------------------
        // Read back the generated barcode to verify that the checksum was corrected.
        // ----------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, "RM999605017CH"))
        {
            using (var reader = new BarCodeReader(generator.GenerateBarCodeImage(), DecodeType.SwissPostParcel))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}, Data: {result.CodeText}");
                }
            }
        }

        // ----------------------------------------------------------------------
        // Cloud storage upload (example placeholder)
        // The following commented code illustrates how one might upload the file
        // to a cloud storage bucket (e.g., Azure Blob Storage, AWS S3, Google Cloud
        // Storage). SDK references are omitted for brevity.
        // ----------------------------------------------------------------------
        // // Example for Azure Blob Storage:
        // // var blobServiceClient = new Azure.Storage.Blobs.BlobServiceClient(connectionString);
        // // var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        // // var blobClient = containerClient.GetBlobClient("SwissPostInternational.png");
        // // using (FileStream fs = File.OpenRead(barcodePath))
        // // {
        // //     blobClient.Upload(fs, overwrite: true);
        // // }
    }
}