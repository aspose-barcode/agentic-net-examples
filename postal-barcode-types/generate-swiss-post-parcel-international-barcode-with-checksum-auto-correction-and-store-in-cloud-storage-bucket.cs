using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Sample code text for Swiss Post Parcel International barcode.
        // The library will auto‑correct the checksum if needed.
        const string codeText = "1234567890123";

        // Path to save the generated barcode image locally.
        const string localPath = "SwissPostParcel.png";

        // Generate the barcode.
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, codeText))
        {
            // Enable checksum generation (auto‑correction is the default behavior).
            generator.Parameters.Barcode.IsChecksumEnabled = Aspose.BarCode.Generation.EnableChecksum.Yes;

            // Optional: set visual appearance.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the image.
            generator.Save(localPath);
        }

        // -----------------------------------------------------------------
        // In a real environment you would upload the file to a cloud bucket
        // (e.g., Azure Blob Storage, AWS S3, Google Cloud Storage). The
        // required SDKs are not available in the snippet runner, so the
        // upload code is provided as a comment for reference.
        // -----------------------------------------------------------------
        /*
        // Example: Azure Blob Storage upload (requires Azure.Storage.Blobs package)
        string connectionString = "<your-connection-string>";
        string containerName = "<your-container>";
        string blobName = Path.GetFileName(localPath);

        var blobClient = new BlobClient(connectionString, containerName, blobName);
        using (FileStream uploadFileStream = File.OpenRead(localPath))
        {
            blobClient.Upload(uploadFileStream, overwrite: true);
        }
        */

        // Verify that the barcode can be read back (optional).
        using (var reader = new BarCodeReader(localPath, DecodeType.SwissPostParcel))
        {
            // Enable checksum validation during recognition.
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");
                // The library automatically validates the checksum; if invalid,
                // result.CodeText may be empty or incorrect.
            }
        }

        // Clean up the local file if desired.
        // File.Delete(localPath);
    }
}