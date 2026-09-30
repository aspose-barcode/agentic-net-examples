// Title: Uniform Padding and Rotation Verification for Code128 Barcode
// Description: Demonstrates how to apply a 20‑pixel uniform padding around a Code128 barcode, rotate the image, and verify that the rotation does not cause clipping.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, padding configuration, and rotation handling. Typical use cases include preparing barcodes for print or display where consistent margins and orientation are required. Developers often need to ensure that added padding prevents content loss after transformations such as rotation.
// Prompt: Set uniform Padding of 20 pixels around a Code128 barcode and verify no clipping after rotation.
// Tags: code128, padding, rotation, verification, aspose.barcode, image, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates setting uniform padding on a Code128 barcode, rotating it, and verifying that no clipping occurs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, applies padding, rotates it, saves images, and checks dimensions.
    /// </summary>
    static void Main()
    {
        // Sample data to encode
        string codeText = "1234567890";

        // Create a temporary folder for output files
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Define file paths for the normal and rotated images
        string rotatedPath = Path.Combine(outputFolder, "barcode_rotated.png");
        string normalPath = Path.Combine(outputFolder, "barcode_normal.png");

        // Generate the barcode with uniform padding of 20 pixels on all sides
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set padding (20 pixels) for left, top, right, and bottom
            generator.Parameters.Barcode.Padding.Left.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Top.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Right.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = 20f;

            // Save the non‑rotated image (used as a size reference)
            generator.Save(normalPath, BarCodeImageFormat.Png);

            // Apply a 90‑degree rotation (supported rotation angle)
            generator.Parameters.RotationAngle = 90f;

            // Save the rotated image
            generator.Save(rotatedPath, BarCodeImageFormat.Png);
        }

        // Load both images to retrieve their dimensions
        int normalWidth, normalHeight, rotatedWidth, rotatedHeight;
        using (var normalImg = new Bitmap(normalPath))
        {
            normalWidth = normalImg.Width;
            normalHeight = normalImg.Height;
        }
        using (var rotatedImg = new Bitmap(rotatedPath))
        {
            rotatedWidth = rotatedImg.Width;
            rotatedHeight = rotatedImg.Height;
        }

        // Expected dimensions after a 90° rotation: width and height are swapped
        int expectedRotatedWidth = normalHeight;
        int expectedRotatedHeight = normalWidth;

        // Verify that the rotated image is at least as large as expected (no clipping)
        bool noClipping = rotatedWidth >= expectedRotatedWidth && rotatedHeight >= expectedRotatedHeight;

        // Output verification results
        Console.WriteLine($"Normal image size   : {normalWidth}x{normalHeight}");
        Console.WriteLine($"Rotated image size  : {rotatedWidth}x{rotatedHeight}");
        Console.WriteLine($"Expected size after rotation: {expectedRotatedWidth}x{expectedRotatedHeight}");
        Console.WriteLine(noClipping
            ? "Verification passed: No clipping detected after rotation."
            : "Verification failed: Rotated image is smaller than expected, possible clipping.");

        // Cleanup temporary files and folder (optional, best‑effort)
        try
        {
            File.Delete(normalPath);
            File.Delete(rotatedPath);
            Directory.Delete(outputFolder);
        }
        catch
        {
            // Ignored – cleanup is best‑effort
        }
    }
}