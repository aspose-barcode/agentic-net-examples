// Title: Determine barcode orientation angle from a BMP image
// Description: Generates a QR barcode rotated by 45°, saves it as a BMP file, then reads the image to detect barcodes and outputs each barcode's orientation angle.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It demonstrates how to use BarcodeGenerator to create rotated barcodes, BarCodeReader to detect them, and how to access the Region.Angle property for orientation. Developers working with image preprocessing, barcode scanning, or quality inspection often need to determine barcode rotation to correct or validate scans.
// Prompt: Determine barcode orientation angle for each detected barcode in a BMP image.
// Tags: qr, barcode orientation, bmp, aspose.barcode, generation, recognition, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to generate a rotated QR barcode, save it as BMP,
/// read the image, and output the orientation angle of each detected barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary BMP with a rotated QR code,
    /// reads it back, prints barcode details including orientation, and cleans up.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeOrientation_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated BMP image
        string bmpPath = Path.Combine(tempFolder, "rotated_qr.bmp");

        // Generate a QR barcode rotated by 45 degrees and save it as BMP
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText"))
        {
            generator.Parameters.RotationAngle = 45;
            generator.Save(bmpPath, BarCodeImageFormat.Bmp);
        }

        // Verify that the BMP file was created successfully before attempting to read it
        if (!File.Exists(bmpPath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Read all barcodes from the BMP image and output their orientation angles
        using (var reader = new BarCodeReader(bmpPath, DecodeType.AllSupportedTypes))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"Orientation Angle: {result.Region.Angle} degrees");
                Console.WriteLine();
            }
        }

        // Attempt to clean up temporary files; ignore any errors during cleanup
        try
        {
            File.Delete(bmpPath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Cleanup failures are non‑critical; they do not affect program outcome
        }
    }
}