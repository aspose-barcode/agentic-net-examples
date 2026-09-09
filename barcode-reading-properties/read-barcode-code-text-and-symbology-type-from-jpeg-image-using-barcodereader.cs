// Title: Read QR code from JPEG using Aspose.BarCodeReader
// Description: Demonstrates generating a QR code barcode, saving it as a JPEG, and then reading the barcode text and symbology type from the image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes and BarCodeReader to decode them from image files. Developers often need to embed barcodes in documents or images and later extract the encoded information, making these APIs essential for inventory, ticketing, and authentication scenarios.
// Prompt: Read barcode code text and symbology type from a JPEG image using BarCodeReader.
// Tags: barcode, qr code, jpeg, read, decode, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that generates a QR code, saves it as a JPEG, and reads it back using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a temporary folder, generates a QR code image, reads the barcode, and cleans up.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the sample JPEG image
        string imagePath = Path.Combine(tempFolder, "sample.jpg");

        // Generate a QR code barcode and save it as a JPEG image
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            // Set the module size (pixel dimension) for the QR code
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Save(imagePath, BarCodeImageFormat.Jpeg);
        }

        // Verify that the image file was successfully created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read all supported barcodes from the JPEG image
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output the results or indicate that no barcodes were found
            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes detected.");
            }
            else
            {
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"CodeText: {result.CodeText}");
                    Console.WriteLine($"CodeTypeName: {result.CodeTypeName}");
                }
            }
        }

        // Clean up temporary files (optional)
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}