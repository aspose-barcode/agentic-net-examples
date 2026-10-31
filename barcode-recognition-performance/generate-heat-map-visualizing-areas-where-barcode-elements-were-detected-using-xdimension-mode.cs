// Title: Generate Heat Map of Detected Barcode Regions Using XDimension Mode
// Description: This example creates several barcode images, places them on a canvas, and then produces a heat‑map image highlighting the areas where barcodes were detected using the XDimension quality setting.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition workflow for visualizing detection results. It uses BarcodeGenerator, BarCodeReader, and related image classes to create barcodes, compose a composite image, and overlay detection regions. Typical use cases include debugging barcode placement, analyzing scan coverage, or creating visual reports for quality assurance. Developers working with barcode imaging often need to generate test images, read them, and visualize detection zones.
// Prompt: Generate a heat map visualizing areas where barcode elements were detected using XDimension mode.
// Tags: barcode, generation, recognition, heatmap, xdimension, aspose.barcode, png, image processing

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a heat map of barcode detection regions using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates barcodes, composes a canvas, reads barcodes with XDimension mode,
    /// and produces a heat‑map overlay image.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for all generated files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeHeatMap_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample barcode data (type and text)
        var barcodes = new List<(BaseEncodeType type, string text)>
        {
            (EncodeTypes.Code128, "ABC123"),
            (EncodeTypes.Code128, "XYZ789"),
            (EncodeTypes.Code128, "HELLO")
        };

        // Generate individual barcode images and store their file paths
        var barcodeFiles = new List<string>();
        int index = 0;
        foreach (var (type, text) in barcodes)
        {
            string filePath = Path.Combine(tempFolder, $"barcode_{index}.png");
            using (var generator = new BarcodeGenerator(type, text))
            {
                // Set a reasonable XDimension for visibility
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
            index++;
        }

        // Create a blank canvas and draw each barcode at a predefined position
        int canvasWidth = 800;
        int canvasHeight = 600;
        string canvasPath = Path.Combine(tempFolder, "canvas.png");
        using (var canvas = new Bitmap(canvasWidth, canvasHeight, PixelFormat.Format32bppArgb))
        {
            using (var graphics = Graphics.FromImage(canvas))
            {
                graphics.Clear(Aspose.Drawing.Color.White);
                var positions = new List<PointF>
                {
                    new PointF(50f, 50f),
                    new PointF(300f, 200f),
                    new PointF(500f, 400f)
                };

                for (int i = 0; i < barcodeFiles.Count; i++)
                {
                    using (var barcodeImg = new Bitmap(barcodeFiles[i]))
                    {
                        graphics.DrawImage(barcodeImg, positions[i]);
                    }
                }
            }
            canvas.Save(canvasPath, Aspose.Drawing.Imaging.ImageFormat.Png);
        }

        // Prepare a transparent bitmap that will hold the heat‑map overlay
        string heatMapPath = Path.Combine(tempFolder, "heatmap.png");
        using (var heatMap = new Bitmap(canvasWidth, canvasHeight, PixelFormat.Format32bppArgb))
        {
            using (var graphics = Graphics.FromImage(heatMap))
            {
                graphics.Clear(Aspose.Drawing.Color.Transparent);

                // Configure the reader to use XDimension mode for detection
                using (var reader = new BarCodeReader(canvasPath, DecodeType.AllSupportedTypes))
                {
                    reader.QualitySettings.XDimension = XDimensionMode.Normal;
                    // Optionally, use minimal XDimension mode:
                    // reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                    // reader.QualitySettings.MinimalXDimension = 1f;

                    // Read all barcodes from the canvas
                    var results = reader.ReadBarCodes();
                    foreach (var result in results)
                    {
                        // Get the bounding rectangle of the detected barcode region
                        var rect = result.Region.Rectangle;

                        // Draw a semi‑transparent red overlay over the region
                        using (var brush = new SolidBrush(Aspose.Drawing.Color.FromArgb(128, 255, 0, 0)))
                        {
                            graphics.FillRectangle(brush, rect.X, rect.Y, rect.Width, rect.Height);
                        }
                    }
                }
            }
            heatMap.Save(heatMapPath, Aspose.Drawing.Imaging.ImageFormat.Png);
        }

        // Output the locations of the generated images
        Console.WriteLine("Canvas image saved to: " + canvasPath);
        Console.WriteLine("Heat map image saved to: " + heatMapPath);
    }
}