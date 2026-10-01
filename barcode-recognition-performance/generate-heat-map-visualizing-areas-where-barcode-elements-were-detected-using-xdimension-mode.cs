// Title: Generate heat map of detected barcode regions using XDimension mode
// Description: This example creates a canvas with multiple Code128 barcodes, detects them with XDimension mode, and visualizes detection areas as a semi‑transparent red heat map overlay.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition workflow, covering BarcodeGenerator, BarCodeReader, and image composition with Aspose.Drawing. Typical use cases include visual analysis of barcode detection performance, debugging scanning setups, or creating diagnostic overlays. Developers often need to generate barcodes, read them with specific quality settings, and render results for reporting.
// Prompt: Generate a heat map visualizing areas where barcode elements were detected using XDimension mode.
// Tags: code128, barcode, heatmap, detection, xdimension, aspose.barcode, aspose.drawing, image-processing

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to generate a set of barcodes, detect them using XDimension mode,
/// and create a heat‑map overlay that highlights the detected regions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates source and heat‑map images and saves them to a temporary folder.
    /// </summary>
    static void Main()
    {
        // Create a temporary working directory for the generated images
        string workDir = Path.Combine(Path.GetTempPath(), "BarcodeHeatMap_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // Define file paths for the source canvas and the final heat‑map image
        string sourceImagePath = Path.Combine(workDir, "source.png");
        string heatMapImagePath = Path.Combine(workDir, "heatmap.png");

        // Canvas dimensions
        const int canvasWidth = 800;
        const int canvasHeight = 600;

        // Number of barcodes to place on the canvas
        const int barcodeCount = 10;

        // Random generator for positioning barcodes
        Random rnd = new Random();

        // -----------------------------------------------------------------
        // STEP 1: Create a blank canvas and draw random Code128 barcodes
        // -----------------------------------------------------------------
        using (Bitmap canvas = new Bitmap(canvasWidth, canvasHeight))
        {
            using (Graphics gCanvas = Graphics.FromImage(canvas))
            {
                // Fill the background with white
                gCanvas.Clear(Color.White);

                // Generate and draw each barcode at a random location
                for (int i = 0; i < barcodeCount; i++)
                {
                    // Initialise a Code128 barcode generator with a unique value
                    using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "CODE" + i))
                    {
                        // Set the XDimension (module size) to 2 points
                        generator.Parameters.Barcode.XDimension.Point = 2f;

                        // Render the barcode to a bitmap
                        using (Bitmap barcodeBmp = generator.GenerateBarCodeImage())
                        {
                            // Compute a random position that keeps the barcode fully inside the canvas
                            int maxX = Math.Max(0, canvasWidth - barcodeBmp.Width);
                            int maxY = Math.Max(0, canvasHeight - barcodeBmp.Height);
                            int posX = rnd.Next(0, maxX + 1);
                            int posY = rnd.Next(0, maxY + 1);

                            // Draw the barcode onto the canvas
                            gCanvas.DrawImage(barcodeBmp, posX, posY, barcodeBmp.Width, barcodeBmp.Height);
                        }
                    }
                }
            }

            // Save the generated source image
            canvas.Save(sourceImagePath, ImageFormat.Png);
        }

        // Verify that the source image was created successfully
        if (!File.Exists(sourceImagePath))
        {
            Console.WriteLine("Failed to create source image.");
            return;
        }

        // -----------------------------------------------------------------
        // STEP 2: Prepare a transparent bitmap that will hold the heat‑map
        // -----------------------------------------------------------------
        using (Bitmap heatMap = new Bitmap(canvasWidth, canvasHeight))
        {
            using (Graphics gHeat = Graphics.FromImage(heatMap))
            {
                // Start with a fully transparent background
                gHeat.Clear(Color.Transparent);
            }

            // -----------------------------------------------------------------
            // STEP 3: Detect barcodes in the source image using XDimension mode
            // -----------------------------------------------------------------
            using (BarCodeReader reader = new BarCodeReader(sourceImagePath, DecodeType.AllSupportedTypes))
            {
                // Configure high‑performance quality settings and enable minimal XDimension usage
                reader.QualitySettings = QualitySettings.HighPerformance;
                reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;

                // Perform detection
                BarCodeResult[] results = reader.ReadBarCodes();

                // -----------------------------------------------------------------
                // STEP 4: Draw a semi‑transparent red circle over each detected region
                // -----------------------------------------------------------------
                using (Graphics gHeatDraw = Graphics.FromImage(heatMap))
                {
                    foreach (BarCodeResult result in results)
                    {
                        // Retrieve the bounding rectangle of the detected barcode
                        var rect = result.Region.Rectangle;

                        // Compute a circle that comfortably covers the rectangle
                        float radius = Math.Max(rect.Width, rect.Height) * 0.6f;
                        float centerX = rect.X + rect.Width / 2f;
                        float centerY = rect.Y + rect.Height / 2f;
                        float ellipseX = centerX - radius;
                        float ellipseY = centerY - radius;
                        float diameter = radius * 2f;

                        // Use a semi‑transparent red brush for the heat‑map overlay
                        using (SolidBrush brush = new SolidBrush(Color.FromArgb(120, 255, 0, 0)))
                        {
                            gHeatDraw.FillEllipse(brush, ellipseX, ellipseY, diameter, diameter);
                        }
                    }
                }
            }

            // -----------------------------------------------------------------
            // STEP 5: Combine the original canvas with the heat‑map overlay
            // -----------------------------------------------------------------
            using (Bitmap finalImage = new Bitmap(canvasWidth, canvasHeight))
            {
                using (Graphics gFinal = Graphics.FromImage(finalImage))
                {
                    // Draw the original source image
                    using (Bitmap src = (Bitmap)Image.FromFile(sourceImagePath))
                    {
                        gFinal.DrawImage(src, 0, 0, src.Width, src.Height);
                    }

                    // Overlay the heat‑map on top of the source image
                    gFinal.DrawImage(heatMap, 0, 0, heatMap.Width, heatMap.Height);
                }

                // Save the combined image to disk
                finalImage.Save(heatMapImagePath, ImageFormat.Png);
            }
        }

        // Output the locations of the generated files
        Console.WriteLine("Source image saved to: " + sourceImagePath);
        Console.WriteLine("Heat map image saved to: " + heatMapImagePath);
    }
}