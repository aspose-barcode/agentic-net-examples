// Title: Generate Heat Map of Detected Barcode Regions Using XDimension Mode
// Description: This example creates a composite image with multiple Code128 barcodes, reads them using XDimension mode, and visualizes the detected barcode areas as a semi‑transparent red heat map.
// Category-Description: Demonstrates Aspose.BarCode image generation and recognition workflow, covering BarcodeGenerator, BarCodeReader, and image manipulation with Aspose.Drawing. Typical use cases include visual analysis of barcode placement, quality inspection, and creating heat‑map overlays for debugging detection algorithms. Developers working with barcode imaging often need to generate test images, extract barcode regions, and render visual feedback.
// Prompt: Generate a heat map visualizing areas where barcode elements were detected using XDimension mode.
// Tags: barcode, code128, heatmap, xdimension, image generation, barcode recognition, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using Aspose.Drawing.Drawing2D;

/// <summary>
/// Demonstrates generating a composite image with several barcodes,
/// detecting them using XDimension mode, and creating a heat‑map overlay
/// that highlights the detected barcode regions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes, reads them,
    /// builds a heat map, and saves the resulting images to a temporary folder.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary folder for output files
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeHeatMap_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Canvas dimensions and output file paths
        int canvasWidth = 800;
        int canvasHeight = 600;
        string combinedPath = Path.Combine(tempFolder, "combined.png");
        string heatmapPath = Path.Combine(tempFolder, "heatmap.png");

        // --------------------------------------------------------------------
        // Create a blank canvas and draw several barcodes onto it
        // --------------------------------------------------------------------
        using (Bitmap canvas = new Bitmap(canvasWidth, canvasHeight, PixelFormat.Format32bppArgb))
        {
            using (Graphics gCanvas = Graphics.FromImage(canvas))
            {
                // Fill background with white
                gCanvas.Clear(Color.White);

                // Generate and place 5 Code128 barcodes
                for (int i = 0; i < 5; i++)
                {
                    string codeText = $"Sample{i + 1}";
                    using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
                    {
                        // Adjust XDimension if needed (example uses 2 points)
                        generator.Parameters.Barcode.XDimension.Point = 2f;

                        using (Bitmap barcodeBmp = generator.GenerateBarCodeImage())
                        {
                            int x = 50 + i * 140; // horizontal offset for each barcode
                            int y = 100;          // vertical position
                            gCanvas.DrawImage(barcodeBmp, x, y, barcodeBmp.Width, barcodeBmp.Height);
                        }
                    }
                }
            }

            // Save the combined barcode image to disk
            using (MemoryStream ms = new MemoryStream())
            {
                canvas.Save(ms, Aspose.Drawing.Imaging.ImageFormat.Png);
                File.WriteAllBytes(combinedPath, ms.ToArray());
            }
        }

        // --------------------------------------------------------------------
        // Read barcodes from the combined image using XDimension mode
        // --------------------------------------------------------------------
        List<RectangleF> detectedRegions = new List<RectangleF>();
        if (File.Exists(combinedPath))
        {
            using (BarCodeReader reader = new BarCodeReader(combinedPath, DecodeType.Code128))
            {
                // Enable small XDimension detection
                reader.QualitySettings.XDimension = XDimensionMode.Small;

                BarCodeResult[] results = reader.ReadBarCodes();
                foreach (BarCodeResult result in results)
                {
                    // Store the bounding rectangle of each detected barcode
                    RectangleF rect = result.Region.Rectangle;
                    detectedRegions.Add(rect);
                }
            }
        }
        else
        {
            Console.WriteLine("Combined image not found.");
            return;
        }

        // --------------------------------------------------------------------
        // Build a heat map image based on the detected barcode regions
        // --------------------------------------------------------------------
        using (Bitmap heatmap = new Bitmap(canvasWidth, canvasHeight, PixelFormat.Format32bppArgb))
        {
            using (Graphics gHeat = Graphics.FromImage(heatmap))
            {
                // Start with a fully transparent background
                gHeat.Clear(Color.Transparent);

                // Draw a semi‑transparent red rectangle for each detected region
                foreach (RectangleF region in detectedRegions)
                {
                    using (SolidBrush brush = new SolidBrush(Color.FromArgb(80, 255, 0, 0)))
                    {
                        gHeat.FillRectangle(brush, region);
                    }
                }
            }

            // Save the heat map image to disk
            using (MemoryStream msHeat = new MemoryStream())
            {
                heatmap.Save(msHeat, Aspose.Drawing.Imaging.ImageFormat.Png);
                File.WriteAllBytes(heatmapPath, msHeat.ToArray());
            }
        }

        // --------------------------------------------------------------------
        // Output the locations of the generated files
        // --------------------------------------------------------------------
        Console.WriteLine($"Combined image saved to: {combinedPath}");
        Console.WriteLine($"Heat map image saved to: {heatmapPath}");
    }
}