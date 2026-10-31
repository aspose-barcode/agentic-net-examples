// Title: Read rotated barcode from webcam frame and log orientation angle
// Description: Demonstrates generating a QR code, rotating the image to simulate a webcam capture, and reading the barcode while retrieving its orientation angle.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, Bitmap for basic image manipulation, and BarCodeReader for decoding. Typical scenarios include processing images captured from cameras, handling rotated or skewed barcodes, and extracting orientation data—common tasks for developers integrating barcode scanning into imaging workflows.
// Prompt: Read barcodes from a video frame captured by a webcam and log orientation angles.
// Tags: qr, barcode, orientation, angle, generation, recognition, aspose.barcode, image-processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR barcode, rotates it to mimic a webcam frame,
/// reads the barcode, and logs its orientation angle.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder to store generated images.
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the original and rotated barcode images.
        string originalPath = Path.Combine(tempFolder, "barcode.png");
        string rotatedPath = Path.Combine(tempFolder, "barcode_rotated.png");

        // Generate a QR barcode and save it as a PNG file.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample123"))
        {
            generator.Save(originalPath, BarCodeImageFormat.Png);
        }

        // Load the generated image, rotate it 90 degrees, and save the rotated version.
        using (Bitmap bitmap = new Bitmap(originalPath))
        {
            bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
            bitmap.Save(rotatedPath, ImageFormat.Png);
        }

        // Read the rotated barcode and output its type, text, and orientation angle.
        using (var reader = new BarCodeReader(rotatedPath, DecodeType.AllSupportedTypes))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"Angle: {result.Region.Angle}");
            }
        }

        // Attempt to clean up temporary files and directory (optional).
        try
        {
            File.Delete(originalPath);
            File.Delete(rotatedPath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome.
        }
    }
}