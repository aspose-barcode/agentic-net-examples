// Title: Heat map of detected barcode regions using XDimension mode
// Description: Demonstrates generating multiple barcodes, combining them into a single image, detecting them with XDimension mode, and visualizing detection areas as a heat map.
// Category-Description: This example belongs to the Aspose.BarCode barcode detection and visualization category. It showcases the use of BarcodeGenerator, BarCodeReader, and related graphics classes to create barcodes, read them with XDimension settings, and overlay detection results. Developers often need to locate barcode positions in complex images, and this pattern provides a reusable approach for heat‑map visual feedback.
// Prompt: Generate a heat map visualizing areas where barcode elements were detected using XDimension mode.
// Tags: barcode symbology, detection, heat map, xdimension, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates sample barcodes, detects them using XDimension mode, and creates a heat‑map overlay
/// showing where barcode elements were found.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the barcode generation, detection, and heat‑map creation steps.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare output directory
        // --------------------------------------------------------------------
        string outputDir = "output";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Canvas dimensions for the combined image
        const int canvasWidth = 800;
        const int canvasHeight = 600;

        // --------------------------------------------------------------------
        // Create a white canvas and place randomly positioned barcodes on it
        // --------------------------------------------------------------------
        using (Bitmap canvas = new Bitmap(canvasWidth, canvasHeight))
        {
            // Fill the canvas with white background
            using (Graphics gCanvas = Graphics.FromImage(canvas))
            {
                gCanvas.Clear(Aspose.Drawing.Color.White);
            }

            var random = new Random();
            var barcodePositions = new List<RectangleF>();

            // Generate 5 sample barcodes
            for (int i = 0; i < 5; i++)
            {
                // Create a Code128 barcode generator with sample text
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"Sample{i + 1}"))
                {
                    // Set XDimension for better visibility
                    generator.Parameters.Barcode.XDimension.Point = 2f;

                    // Save barcode to a memory stream as PNG
                    using (var ms = new MemoryStream())
                    {
                        generator.Save(ms, BarCodeImageFormat.Png);
                        ms.Position = 0;

                        // Load the barcode image from the stream
                        using (Bitmap barcodeBmp = (Bitmap)Bitmap.FromStream(ms))
                        {
                            // Compute a random position that keeps the barcode inside the canvas
                            int maxX = canvasWidth - barcodeBmp.Width;
                            int maxY = canvasHeight - barcodeBmp.Height;
                            int posX = maxX > 0 ? random.Next(0, maxX) : 0;
                            int posY = maxY > 0 ? random.Next(0, maxY) : 0;

                            // Draw the barcode onto the canvas
                            using (Graphics g = Graphics.FromImage(canvas))
                            {
                                g.DrawImage(barcodeBmp, posX, posY, barcodeBmp.Width, barcodeBmp.Height);
                            }

                            // Record the barcode's location for later reference
                            barcodePositions.Add(new RectangleF(posX, posY, barcodeBmp.Width, barcodeBmp.Height));
                        }
                    }
                }
            }

            // Save the combined image containing all barcodes
            string combinedPath = Path.Combine(outputDir, "combined.png");
            canvas.Save(combinedPath, ImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Verify that the combined image was created successfully
        // --------------------------------------------------------------------
        string combinedImagePath = Path.Combine(outputDir, "combined.png");
        if (!File.Exists(combinedImagePath))
        {
            Console.WriteLine("Combined image not found.");
            return;
        }

        // --------------------------------------------------------------------
        // Detect barcodes using XDimension mode (minimal XDimension)
        // --------------------------------------------------------------------
        List<RectangleF> detectedRegions = new List<RectangleF>();
        using (var reader = new BarCodeReader(combinedImagePath))
        {
            // Read all supported barcode types
            reader.BarCodeReadType = DecodeType.AllSupportedTypes;

            // Configure XDimension mode to use the minimal XDimension value
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 1f;

            // Iterate through detection results
            foreach (var result in reader.ReadBarCodes())
            {
                RectangleF rect = result.Region.Rectangle;
                detectedRegions.Add(rect);
                Console.WriteLine($"Detected: {result.CodeTypeName}, Text: {result.CodeText}, Region: {rect}");
            }
        }

        // --------------------------------------------------------------------
        // Build a heat‑map overlay showing detected barcode regions
        // --------------------------------------------------------------------
        using (Bitmap heatMap = new Bitmap(canvasWidth, canvasHeight))
        {
            using (Graphics gHeat = Graphics.FromImage(heatMap))
            {
                // Transparent background for the heat‑map layer
                gHeat.Clear(Aspose.Drawing.Color.Transparent);

                // Draw semi‑transparent red rectangles over each detected region
                foreach (var rect in detectedRegions)
                {
                    using (var brush = new SolidBrush(Aspose.Drawing.Color.FromArgb(80, 255, 0, 0)))
                    {
                        gHeat.FillRectangle(brush, rect);
                    }
                }
            }

            // ----------------------------------------------------------------
            // Combine the original image with the heat‑map overlay
            // ----------------------------------------------------------------
            using (Bitmap finalImage = new Bitmap(canvasWidth, canvasHeight))
            {
                using (Graphics gFinal = Graphics.FromImage(finalImage))
                {
                    // Draw the original combined image as the base layer
                    using (var original = (Bitmap)Bitmap.FromFile(combinedImagePath))
                    {
                        gFinal.DrawImage(original, 0, 0, canvasWidth, canvasHeight);
                    }

                    // Overlay the heat‑map on top of the original image
                    gFinal.DrawImage(heatMap, 0, 0, canvasWidth, canvasHeight);
                }

                // Save the final heat‑map image
                string heatMapPath = Path.Combine(outputDir, "heatmap.png");
                finalImage.Save(heatMapPath, ImageFormat.Png);
                Console.WriteLine($"Heat map saved to: {heatMapPath}");
            }
        }
    }
}