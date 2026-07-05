// Title: Heat Map of Detected Barcode Elements Using XDimension Mode
// Description: Creates a barcode image (if missing), reads it with minimal XDimension settings, and overlays semi‑transparent red rectangles on detected barcode regions to produce a heat map.
// Prompt: Generate a heat map visualizing areas where barcode elements were detected using XDimension mode.
// Tags: barcode, detection, heatmap, xdimension, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to generate a heat map that highlights barcode detection areas
/// using Aspose.BarCode with XDimension mode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a sample barcode (if needed),
    /// detects it with minimal XDimension settings, and saves a heat‑mapped image.
    /// </summary>
    static void Main()
    {
        const string barcodePath = "barcode.png";
        const string heatMapPath = "heatmap.png";

        // ------------------------------------------------------------
        // Step 1: Create a sample barcode image if it does not already exist.
        // ------------------------------------------------------------
        if (!File.Exists(barcodePath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Use interpolation auto‑size mode for better visual quality.
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

                // Set the module (X‑dimension) size to 2 points.
                generator.Parameters.Barcode.XDimension.Point = 2f;

                // Save the generated barcode to disk.
                generator.Save(barcodePath);
            }
        }

        // ------------------------------------------------------------
        // Step 2: Load the barcode image and initialise the reader.
        // ------------------------------------------------------------
        using (var bitmap = new Bitmap(barcodePath))
        {
            // Enable detection of all supported symbologies.
            using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
            {
                // Configure the reader to use minimal XDimension mode.
                reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                reader.QualitySettings.MinimalXDimension = 2f; // minimal module size in pixels.

                // Perform barcode recognition.
                var results = reader.ReadBarCodes();

                // ------------------------------------------------------------
                // Step 3: Draw a semi‑transparent red overlay on each detected region.
                // ------------------------------------------------------------
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    // Define a brush with 50 % opacity red colour.
                    using (var brush = new SolidBrush(Color.FromArgb(128, Color.Red)))
                    {
                        foreach (var result in results)
                        {
                            // Retrieve the bounding rectangle of the detected barcode.
                            var rect = result.Region.Rectangle;

                            // Fill the rectangle on the image to create the heat‑map effect.
                            graphics.FillRectangle(brush, rect.X, rect.Y, rect.Width, rect.Height);
                        }
                    }
                }

                // ------------------------------------------------------------
                // Step 4: Save the resulting heat‑mapped image.
                // ------------------------------------------------------------
                bitmap.Save(heatMapPath, ImageFormat.Png);
            }
        }

        // Inform the user where the heat map was saved.
        Console.WriteLine("Heat map saved to: " + Path.GetFullPath(heatMapPath));
    }
}