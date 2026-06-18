using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to read multi‑segment QR codes from an image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Loads an image, scans for QR codes, and prints their data and extended QR parameters.
    /// </summary>
    static void Main()
    {
        // Path to the image containing multi‑segment QR codes.
        string imagePath = "qr_multi.png";

        // Verify that the image file exists before attempting to load it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the image into a Bitmap object (wrapped in a using statement for proper disposal).
        using (Bitmap bitmap = (Bitmap)Image.FromFile(imagePath))
        {
            // Initialize a BarCodeReader that is configured to decode QR codes only.
            using (BarCodeReader reader = new BarCodeReader(bitmap, DecodeType.QR))
            {
                bool anyFound = false; // Tracks whether any QR codes were detected.

                // Iterate through all detected QR code results.
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    anyFound = true;
                    Console.WriteLine($"CodeText: {result.CodeText}");

                    // Retrieve QR-specific extended parameters, if available.
                    var qrExt = result.Extended?.QR;
                    if (qrExt != null)
                    {
                        // Output structured append information for multi‑segment QR codes.
                        Console.WriteLine($"  Structured Append Total Segments: {qrExt.StructuredAppendModeBarCodesQuantity}");
                        Console.WriteLine($"  Segment Index: {qrExt.StructuredAppendModeBarCodeIndex}");
                        Console.WriteLine($"  Parity Data: {qrExt.StructuredAppendModeParityData}");
                    }
                    else
                    {
                        // Indicate that no extended QR parameters were present for this result.
                        Console.WriteLine("  No QR extended parameters available.");
                    }
                }

                // If no QR codes were found, inform the user.
                if (!anyFound)
                {
                    Console.WriteLine("No QR codes were detected in the image.");
                }
            }
        }
    }
}