using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a rotated Code128 barcode, saving it to a temporary file,
/// and then reading it back to verify detection and orientation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a rotated barcode image, verifies its creation, reads the barcode,
    /// and outputs detection details to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define a temporary file path for the barcode image.
        string tempDir = Path.GetTempPath();
        string barcodePath = Path.Combine(tempDir, "rotated_barcode.png");

        // Generate a Code128 barcode and rotate it by 90 degrees.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            // RotationAngle is a root Parameters property.
            generator.Parameters.RotationAngle = 90f;
            // Save the rotated barcode image to the specified path.
            generator.Save(barcodePath);
        }

        // Verify that the image was successfully created.
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Read the rotated barcode; Aspose.BarCode automatically corrects orientation.
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            // Iterate through all detected barcodes in the image.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");
                // Orientation angle is provided via result.Region.Angle.
                Console.WriteLine($"Detected Orientation Angle: {result.Region.Angle}");
            }
        }

        // Optional cleanup: delete the temporary barcode image file.
        // File.Delete(barcodePath);
    }
}