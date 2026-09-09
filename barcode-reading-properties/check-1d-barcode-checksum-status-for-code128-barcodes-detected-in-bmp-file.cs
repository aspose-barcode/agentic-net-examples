// Title: Check Code128 barcode checksum status in BMP image
// Description: Demonstrates generating a Code128 barcode, saving it as BMP, and reading it to display the checksum status.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create a 1D barcode, BarCodeReader with DecodeType to detect Code128 symbols, and how to enable checksum validation via BarcodeSettings.ChecksumValidation. Developers often need to verify checksum information when processing scanned barcodes for data integrity in inventory, shipping, or point‑of‑sale systems.
// Prompt: Check 1D barcode checksum status for Code128 barcodes detected in a BMP file.
// Tags: code128, checksum, barcode, generation, recognition, bmp, 1d, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode, saves it as a BMP file,
/// reads the barcode back, and outputs its checksum status.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, detection, and cleanup.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to store the generated BMP file
        string tempFolder = Path.Combine(Path.GetTempPath(), "Code128ChecksumDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "code128.bmp");

        // Generate a Code128 barcode image and save it as BMP
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Aspose1234"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2; // Set barcode module size
            generator.Save(imagePath, BarCodeImageFormat.Bmp);
        }

        // Verify that the image file was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Barcode image not found at '{imagePath}'.");
            return;
        }

        // Initialize a reader for Code128 barcodes in the BMP image
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Turn on checksum validation for symbologies that require it
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Iterate through all detected barcodes and display details
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                // Extended.OneD contains checksum information for 1D barcodes
                Console.WriteLine($"Checksum: {result.Extended.OneD.CheckSum}");
            }
        }

        // Attempt to delete temporary files and folder; ignore any errors
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Cleanup failures are non‑critical; they do not affect program logic
        }
    }
}