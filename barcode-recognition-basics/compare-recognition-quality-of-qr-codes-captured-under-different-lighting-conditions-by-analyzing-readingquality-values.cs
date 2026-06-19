using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR code, applying lighting variations,
/// and evaluating the reading quality using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code, creates lighting scenarios, reads the code,
    /// and prints the reading quality for each scenario.
    /// </summary>
    static void Main()
    {
        // QR code text to encode
        const string qrText = "https://example.com";

        // Generate the original QR code bitmap and keep it in memory
        Bitmap originalBitmap;
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
        {
            using (var ms = new MemoryStream())
            {
                // Save the generated QR code as PNG into the memory stream
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading
                originalBitmap = new Bitmap(ms); // Load bitmap from stream
            }
        }

        // Define lighting scenarios: normal, dark overlay, bright overlay
        var scenarios = new (string Name, Action<Bitmap> ApplyEffect)[]
        {
            ("Normal", bmp => { /* No change for normal lighting */ }),
            ("Dark",   bmp => ApplyOverlay(bmp, Color.Black, 100)),
            ("Bright", bmp => ApplyOverlay(bmp, Color.White, 100))
        };

        // Process each lighting scenario
        foreach (var scenario in scenarios)
        {
            // Clone the original bitmap so each scenario works on a fresh copy
            using (var bmp = new Bitmap(originalBitmap))
            {
                // Apply the lighting effect specific to the scenario
                scenario.ApplyEffect(bmp);

                // Initialize a barcode reader for QR codes on the modified bitmap
                using (var reader = new BarCodeReader(bmp, DecodeType.QR))
                {
                    // Attempt to read barcodes
                    var results = reader.ReadBarCodes();

                    if (results.Length > 0)
                    {
                        // If a barcode is found, retrieve its reading quality
                        var result = results[0];
                        double quality = result.ReadingQuality;
                        Console.WriteLine($"{scenario.Name} lighting - ReadingQuality: {quality}");
                    }
                    else
                    {
                        // No barcode detected for this lighting condition
                        Console.WriteLine($"{scenario.Name} lighting - No barcode detected.");
                    }
                }
            }
        }

        // Release resources held by the original bitmap
        originalBitmap.Dispose();
    }

    /// <summary>
    /// Applies a semi‑transparent color overlay to simulate lighting changes.
    /// </summary>
    /// <param name="bitmap">The bitmap to modify.</param>
    /// <param name="overlayColor">The color of the overlay.</param>
    /// <param name="alpha">The opacity of the overlay (0‑255).</param>
    private static void ApplyOverlay(Bitmap bitmap, Color overlayColor, int alpha)
    {
        // Create graphics object from the bitmap for drawing
        using (var graphics = Graphics.FromImage(bitmap))
        {
            // Create a brush with the specified color and opacity
            using (var brush = new SolidBrush(Color.FromArgb(alpha, overlayColor)))
            {
                // Fill the entire bitmap with the overlay brush
                graphics.FillRectangle(brush, 0, 0, bitmap.Width, bitmap.Height);
            }
        }
    }
}