using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating multiple barcode types, combining them into a single image,
/// and then reading back the barcodes to verify detection.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates barcodes, composes them on a canvas, and validates detection.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Define the set of barcode symbologies and the corresponding text to encode.
        // --------------------------------------------------------------------
        var symbologies = new (BaseEncodeType type, string text)[]
        {
            (EncodeTypes.Code128, "ABC123456"),
            (EncodeTypes.QR, "https://example.com"),
            (EncodeTypes.DataMatrix, "DM12345"),
            (EncodeTypes.Aztec, "AZTEC")
        };

        // --------------------------------------------------------------------
        // Generate individual barcode images and keep track of expected symbology names.
        // --------------------------------------------------------------------
        var barcodeImages = new List<Bitmap>();
        var expectedNames = new List<string>();

        foreach (var (type, text) in symbologies)
        {
            using (var generator = new BarcodeGenerator(type, text))
            {
                // Store the human‑readable name of the generated symbology for later validation.
                expectedNames.Add(generator.BarcodeType.TypeName);

                // Save the barcode to a memory stream in PNG format.
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    ms.Position = 0; // Reset stream position before reading.

                    // Load the PNG data into an Aspose.Drawing.Bitmap.
                    var bmp = new Bitmap(ms);
                    barcodeImages.Add(bmp);
                }
            }
        }

        // --------------------------------------------------------------------
        // Combine the individual barcode images into a single canvas (2 columns).
        // --------------------------------------------------------------------
        const int cols = 2;
        int rows = (int)Math.Ceiling(barcodeImages.Count / (float)cols);
        const int cellWidth = 300;
        const int cellHeight = 300;
        int canvasWidth = cols * cellWidth;
        int canvasHeight = rows * cellHeight;

        using (var canvas = new Bitmap(canvasWidth, canvasHeight))
        {
            using (var graphics = Graphics.FromImage(canvas))
            {
                // Fill the background with white.
                graphics.Clear(Color.White);

                // Draw each barcode image into its grid cell.
                for (int i = 0; i < barcodeImages.Count; i++)
                {
                    int col = i % cols;
                    int row = i / cols;
                    int x = col * cellWidth;
                    int y = row * cellHeight;
                    graphics.DrawImage(barcodeImages[i], x, y, cellWidth, cellHeight);
                }
            }

            // ----------------------------------------------------------------
            // Save the combined image to a temporary file for reading.
            // ----------------------------------------------------------------
            string tempPath = Path.Combine(Path.GetTempPath(), "combined_barcode.png");
            canvas.Save(tempPath, ImageFormat.Png);

            // Verify that the file was created successfully.
            if (!File.Exists(tempPath))
            {
                Console.WriteLine("Failed to create the combined barcode image.");
                return;
            }

            // ----------------------------------------------------------------
            // Read barcodes from the combined image and collect detected symbology names.
            // ----------------------------------------------------------------
            using (var combinedBmp = new Bitmap(tempPath))
            using (var reader = new BarCodeReader(combinedBmp, DecodeType.AllSupportedTypes))
            {
                var foundNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (var result in reader.ReadBarCodes())
                {
                    // Add each detected symbology name to the set.
                    foundNames.Add(result.CodeTypeName);
                }

                // ----------------------------------------------------------------
                // Validate that each expected symbology was detected.
                // ----------------------------------------------------------------
                bool allFound = true;
                foreach (var expected in expectedNames)
                {
                    if (foundNames.Contains(expected))
                    {
                        Console.WriteLine($"PASS: Expected symbology '{expected}' was detected.");
                    }
                    else
                    {
                        Console.WriteLine($"FAIL: Expected symbology '{expected}' was NOT detected.");
                        allFound = false;
                    }
                }

                // Summarize the overall result.
                if (allFound)
                {
                    Console.WriteLine("All expected symbologies were successfully detected.");
                }
                else
                {
                    Console.WriteLine("Some expected symbologies were missing.");
                }
            }

            // ----------------------------------------------------------------
            // Clean up the temporary file.
            // ----------------------------------------------------------------
            try
            {
                File.Delete(Path.Combine(Path.GetTempPath(), "combined_barcode.png"));
            }
            catch
            {
                // Ignored – best effort cleanup.
            }
        }

        // --------------------------------------------------------------------
        // Dispose of the individual barcode bitmaps to release resources.
        // --------------------------------------------------------------------
        foreach (var bmp in barcodeImages)
        {
            bmp.Dispose();
        }
    }
}