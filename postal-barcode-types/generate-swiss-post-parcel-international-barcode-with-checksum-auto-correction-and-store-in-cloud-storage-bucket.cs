// Title: Generate Swiss Post Parcel International Barcode with Auto‑Checksum Correction
// Description: This example creates a Swiss Post Parcel International barcode, automatically corrects an invalid checksum, and saves the image to a temporary folder.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition for Swiss Post Parcel barcodes. It uses BarcodeGenerator to create barcodes, BarCodeReader to decode them, and showcases typical scenarios such as checksum handling, image output, and verification. Developers working with postal barcode standards often need these APIs for packaging, logistics, and tracking solutions.
// Prompt: Generate a Swiss Post Parcel international barcode with checksum auto‑correction and store in a cloud storage bucket.
// Tags: barcode, swisspost, parcel, international, checksum, generation, recognition, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Swiss Post Parcel International barcode with automatic checksum correction,
/// saving it as an image, and reading it back for verification.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output folder for the generated barcode image.
        string outputFolder = Path.Combine(Path.GetTempPath(), "SwissPostBarcodes");
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Define the full file path for the barcode image.
        string barcodePath = Path.Combine(outputFolder, "SwissPostInternational.png");

        // Generate a Swiss Post Parcel International barcode.
        // The provided data contains an incorrect checksum; the library auto‑corrects it.
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, "RM999605017CH"))
        {
            // Set visual parameters: module size and bar height.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.BarHeight.Pixels = 40f;

            // Save the barcode as a PNG image.
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode image saved to: {barcodePath}");

        // Read the saved barcode image to verify that the checksum was corrected.
        using (var reader = new BarCodeReader(barcodePath, DecodeType.SwissPostParcel))
        {
            BarCodeResult[] results = reader.ReadBarCodes();
            foreach (var result in results)
            {
                Console.WriteLine($"Detected type: {result.CodeTypeName}");
                Console.WriteLine($"Decoded data: {result.CodeText}");
            }
        }

        // NOTE: In a real environment, the generated file would be uploaded to a cloud storage bucket
        // using the appropriate SDK (e.g., Azure Blob Storage, AWS S3, Google Cloud Storage).
        // The upload code is omitted here because the required SDKs are not available in the snippet runner.
    }
}