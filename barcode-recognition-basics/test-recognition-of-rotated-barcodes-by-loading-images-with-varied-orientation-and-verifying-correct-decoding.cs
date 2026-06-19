using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating and reading rotated Code128 barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates barcode images at various rotation angles, then reads and validates them.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory to store generated barcode images.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeRotationTest");
        Directory.CreateDirectory(outputDir);

        // The text to encode in the barcode.
        const string codeText = "Test123";

        // Rotation angles (in degrees) to apply to each generated barcode.
        float[] angles = new float[] { 0f, 90f, 180f, 270f };

        // --------------------------------------------------------------------
        // Generate barcode images with the specified rotations.
        // --------------------------------------------------------------------
        foreach (float angle in angles)
        {
            // Build the file name for the current angle.
            string filePath = Path.Combine(outputDir, $"barcode_{angle}.png");

            // Create a barcode generator for Code128 with the sample text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Apply the rotation angle to the barcode.
                generator.Parameters.RotationAngle = angle;

                // Save the generated barcode image to disk.
                generator.Save(filePath);
            }
        }

        // --------------------------------------------------------------------
        // Read each generated image and verify the decoded content and orientation.
        // --------------------------------------------------------------------
        foreach (float angle in angles)
        {
            // Determine the path of the image for the current angle.
            string filePath = Path.Combine(outputDir, $"barcode_{angle}.png");

            // Ensure the file exists before attempting to read it.
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Initialize a barcode reader for Code128.
            using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
            {
                // Attempt to read all barcodes present in the image.
                BarCodeResult[] results = reader.ReadBarCodes();

                // If no barcodes were detected, report and move to the next file.
                if (results.Length == 0)
                {
                    Console.WriteLine($"No barcode detected in {Path.GetFileName(filePath)}");
                    continue;
                }

                // Output details for each detected barcode.
                foreach (var result in results)
                {
                    Console.WriteLine($"File: {Path.GetFileName(filePath)}");
                    Console.WriteLine($"Decoded Text: {result.CodeText}");
                    Console.WriteLine($"Detected Orientation (degrees): {result.Region.Angle}");
                    Console.WriteLine($"Match Expected Text: {result.CodeText == codeText}");
                    Console.WriteLine();
                }
            }
        }

        // Optional cleanup: delete the temporary directory and its contents.
        // Uncomment the line below if you want the files removed automatically.
        // Directory.Delete(outputDir, true);
    }
}