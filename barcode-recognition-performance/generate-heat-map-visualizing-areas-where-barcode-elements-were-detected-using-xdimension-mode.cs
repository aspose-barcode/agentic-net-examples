// Title: Heat map of detected barcode regions using XDimension mode
// Description: This example creates a composite image containing several Code128 barcodes, reads them with XDimension mode, and visualizes the detection areas as a semi‑transparent red heat map.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition workflows, focusing on XDimension settings for improved detection accuracy. The example uses BarcodeGenerator, BarCodeReader, and related quality settings to detect barcode regions and overlay a heat map. Ideal for developers needing visual diagnostics of barcode scanning performance in images.
// Prompt: Generate a heat map visualizing areas where barcode elements were detected using XDimension mode.
// Tags: barcode, generation, recognition, heatmap, xdimension, code128, png, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates creating a composite barcode image, recognizing barcodes with XDimension mode,
/// and generating a heat map of detected regions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes, reads them, and saves a heat map image.
    /// </summary>
    static void Main()
    {
        // Create a temporary working folder
        string workFolder = Path.Combine(Path.GetTempPath(), "HeatMapDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Parameters for the composite image
        int canvasWidth = 800;
        int canvasHeight = 600;

        // Positions where individual barcodes will be placed
        var positions = new List<(int X, int Y, string Text)>
        {
            (50, 50, "ABC123"),
            (300, 80, "DEF456"),
            (150, 250, "GHI789"),
            (500, 200, "JKL012"),
            (400, 400, "MNO345")
        };

        // Create the canvas bitmap
        using (Bitmap canvas = new Bitmap(canvasWidth, canvasHeight))
        {
            using (Graphics gCanvas = Graphics.FromImage(canvas))
            {
                // Fill background with white
                gCanvas.Clear(Aspose.Drawing.Color.White);

                // Generate each barcode and draw onto the canvas
                foreach (var (x, y, text) in positions)
                {
                    using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
                    {
                        // Generate barcode image as bitmap
                        using (Bitmap barcodeBmp = generator.GenerateBarCodeImage())
                        {
                            // Draw the barcode onto the canvas at the specified position
                            gCanvas.DrawImage(barcodeBmp, x, y, barcodeBmp.Width, barcodeBmp.Height);
                        }
                    }
                }
            }

            // Save the composite image (optional, for inspection)
            string compositePath = Path.Combine(workFolder, "composite.png");
            canvas.Save(compositePath, Aspose.Drawing.Imaging.ImageFormat.Png);

            // Read barcodes using XDimension mode
            BaseDecodeType decodeType = DecodeType.AllSupportedTypes;
            using (var reader = new BarCodeReader(compositePath, decodeType))
            {
                // Set XDimension mode to Small and minimal dimension
                reader.QualitySettings.XDimension = XDimensionMode.Small;
                reader.QualitySettings.MinimalXDimension = 1f;

                // Perform recognition
                BarCodeResult[] results = reader.ReadBarCodes();

                // Create a copy of the canvas for heat map overlay
                using (Bitmap heatMap = new Bitmap(canvasWidth, canvasHeight))
                {
                    using (Graphics gHeat = Graphics.FromImage(heatMap))
                    {
                        // Copy original canvas as background
                        gHeat.DrawImage(canvas, 0, 0, canvasWidth, canvasHeight);

                        // Semi‑transparent red brush for heat overlay
                        using (SolidBrush brush = new SolidBrush(Aspose.Drawing.Color.FromArgb(128, 255, 0, 0)))
                        {
                            foreach (BarCodeResult result in results)
                            {
                                var rect = result.Region.Rectangle;
                                gHeat.FillRectangle(brush, rect.X, rect.Y, rect.Width, rect.Height);
                            }
                        }
                    }

                    // Save the heat map image
                    string heatMapPath = Path.Combine(workFolder, "heatmap.png");
                    heatMap.Save(heatMapPath, Aspose.Drawing.Imaging.ImageFormat.Png);
                    Console.WriteLine($"Heat map saved to: {heatMapPath}");
                }
            }
        }

        // Cleanup: optionally delete the temporary folder
        // Directory.Delete(workFolder, true);
    }
}