// Title: Generate Swiss Post Parcel International Barcode with Auto‑Corrected Checksum
// Description: Demonstrates creating a Swiss Post Parcel International barcode, letting Aspose.BarCode auto‑correct an invalid checksum, and saving the image locally.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator with EncodeTypes.SwissPostParcel and BarCodeReader with DecodeType.SwissPostParcel. Developers commonly need to generate postal barcodes, validate them, and optionally store the results in cloud storage.
// Prompt: Generate a Swiss Post Parcel international barcode with checksum auto‑correction and store in a cloud storage bucket.
// Tags: barcode generation, swiss post parcel, checksum correction, png output, cloud upload, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Swiss Post Parcel International barcode,
/// automatically corrects an invalid checksum, saves the image locally,
/// reads back the barcode to display the corrected data, and outlines how
/// to upload the file to a cloud storage bucket.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it, reads it back,
    /// and provides placeholder code for cloud storage upload.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // -----------------------------------------------------------------
        // Define the local output path for the generated barcode image.
        // -----------------------------------------------------------------
        string outputPath = Path.Combine(Path.GetTempPath(), "SwissPostInternational.png");

        // -----------------------------------------------------------------
        // Prepare barcode data with an intentionally incorrect checksum.
        // Aspose.BarCode will automatically correct the checksum during generation.
        // -----------------------------------------------------------------
        string codeText = "RM999605017CH";

        // -----------------------------------------------------------------
        // Generate the Swiss Post Parcel International barcode.
        // -----------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, codeText))
        {
            // Set barcode visual parameters.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.BarHeight.Pixels = 40f;

            // Save the generated barcode image to the local file system.
            generator.Save(outputPath, BarCodeImageFormat.Png);
            Console.WriteLine($"Barcode image saved to: {outputPath}");

            // -----------------------------------------------------------------
            // Read the barcode back to demonstrate that the checksum was corrected.
            // -----------------------------------------------------------------
            using (var reader = new BarCodeReader(generator.GenerateBarCodeImage(), DecodeType.SwissPostParcel))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected type: {result.CodeTypeName}, data: {result.CodeText}");
                }
            }
        }

        // -----------------------------------------------------------------
        // Cloud storage upload (placeholder – actual SDK not available here)
        // -----------------------------------------------------------------
        // The following commented code demonstrates how one might upload the
        // generated file to a cloud bucket (e.g., Azure Blob Storage, AWS S3,
        // Google Cloud Storage) when the appropriate SDKs are referenced.
        //
        // // Example for Azure Blob Storage:
        // // var blobClient = new BlobClient(connectionString, containerName, blobName);
        // // using (FileStream fs = new FileStream(outputPath, FileMode.Open, FileAccess.Read))
        // // {
        // //     blobClient.Upload(fs);
        // // }
        //
        // // Example for AWS S3:
        // // var s3Client = new AmazonS3Client(accessKey, secretKey, RegionEndpoint.USEast1);
        // // var putRequest = new PutObjectRequest
        // // {
        // //     BucketName = bucketName,
        // //     Key = "SwissPostInternational.png",
        // //     FilePath = outputPath
        // // };
        // // s3Client.PutObjectAsync(putRequest).Wait();
        //
        // // Example for Google Cloud Storage:
        // // var storage = StorageClient.Create();
        // // using (var fileStream = File.OpenRead(outputPath))
        // // {
        // //     storage.UploadObject(bucketName, "SwissPostInternational.png", null, fileStream);
        // // }
        //
        // Note: The above code is commented out because the required cloud SDK
        // packages are not available in the snippet runner environment.
    }
}