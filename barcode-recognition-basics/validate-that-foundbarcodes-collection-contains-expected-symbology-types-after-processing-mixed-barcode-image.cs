// Title: Validate detection of multiple barcode symbologies in a combined image
// Description: Demonstrates generating several barcode types, merging them into one image, and verifying that the Aspose.BarCode reader correctly identifies each symbology.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, Bitmap handling with Aspose.Drawing, and BarCodeReader for decoding. Developers often need to batch‑process mixed barcode images, validate detection accuracy, or build composite scans; this snippet illustrates those common tasks and the key API classes involved.
// Prompt: Validate that FoundBarCodes collection contains expected symbology types after processing a mixed barcode image.
// Tags: barcode, symbology, generation, recognition, mixed image, aspose.barcode, csharp, aspnet

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a set of different barcode types, combines them into a single image,
/// and validates that each expected symbology is detected by the BarCodeReader.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, image composition,
    /// recognition, and validation of detected symbology types.
    /// </summary>
    static void Main()
    {
        // Define sample barcodes to generate (type and associated text)
        var samples = new List<(BaseEncodeType type, string text)>
        {
            (EncodeTypes.Code128, "CODE128-123"),
            (EncodeTypes.QR, "https://example.com"),
            (EncodeTypes.DataMatrix, "DM-456"),
            (EncodeTypes.Pdf417, "PDF417-789"),
            (EncodeTypes.Aztec, "AZTEC-ABC")
        };

        // Containers for generated bitmap images and their dimensions
        var barcodeImages = new List<Bitmap>();
        var widths = new List<int>();
        var heights = new List<int>();

        // Generate individual barcode images using default settings
        foreach (var (type, text) in samples)
        {
            using (var generator = new BarcodeGenerator(type, text))
            {
                Bitmap bmp = generator.GenerateBarCodeImage();
                barcodeImages.Add(bmp);
                widths.Add(bmp.Width);
                heights.Add(bmp.Height);
            }
        }

        // Calculate combined image size for a horizontal layout
        int totalWidth = 0;
        int maxHeight = 0;
        foreach (int w in widths) totalWidth += w;
        foreach (int h in heights) if (h > maxHeight) maxHeight = h;

        // Create a new bitmap that will hold all barcodes side by side
        using (var combined = new Bitmap(totalWidth, maxHeight))
        {
            using (var graphics = Graphics.FromImage(combined))
            {
                // Fill background with white
                graphics.Clear(Aspose.Drawing.Color.White);
                int offsetX = 0;

                // Draw each barcode image onto the combined bitmap
                for (int i = 0; i < barcodeImages.Count; i++)
                {
                    Bitmap src = barcodeImages[i];
                    graphics.DrawImage(src, offsetX, 0, src.Width, src.Height);
                    offsetX += src.Width;
                    src.Dispose(); // Dispose individual bitmap after it has been drawn
                }
            }

            // Save the combined image to a temporary file for recognition
            string tempPath = Path.Combine(Path.GetTempPath(), "mixed_barcodes.png");
            combined.Save(tempPath, ImageFormat.Png);

            // Build a set of expected symbology type names from the generated samples
            var expectedTypes = new HashSet<string>();
            foreach (var (type, _) in samples)
            {
                expectedTypes.Add(type.TypeName);
            }

            // Initialize the reader to decode all supported barcode types
            using (var reader = new BarCodeReader(tempPath, DecodeType.AllSupportedTypes))
            {
                // Perform recognition on the combined image
                reader.ReadBarCodes();

                // Collect the symbology types that were actually found
                var foundTypes = new HashSet<string>();
                foreach (var result in reader.FoundBarCodes)
                {
                    if (result?.CodeType != null)
                    {
                        foundTypes.Add(result.CodeType.TypeName);
                    }
                }

                // Verify that each expected type is present in the found collection
                bool allFound = true;
                foreach (string expected in expectedTypes)
                {
                    if (!foundTypes.Contains(expected))
                    {
                        Console.WriteLine($"Missing expected symbology: {expected}");
                        allFound = false;
                    }
                }

                // Output validation result
                if (allFound)
                {
                    Console.WriteLine("All expected symbology types were successfully detected.");
                }
                else
                {
                    Console.WriteLine("Some expected symbology types were not detected.");
                }
            }

            // Attempt to delete the temporary file; ignore any errors
            try
            {
                if (File.Exists(tempPath))
                {
                    File.Delete(tempPath);
                }
            }
            catch
            {
                // Suppress cleanup exceptions
            }
        }
    }
}