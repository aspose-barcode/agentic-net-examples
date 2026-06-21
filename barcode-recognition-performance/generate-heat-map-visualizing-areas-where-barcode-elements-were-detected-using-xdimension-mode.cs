using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating multiple barcodes on a single canvas,
/// saving the combined image, and creating a heat‑map overlay of detected barcode regions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates barcodes, saves a combined image, reads the barcodes,
    /// and produces a heat‑map visualizing detection regions.
    /// </summary>
    static void Main()
    {
        // Canvas dimensions for the combined image
        const int canvasWidth = 800;
        const int canvasHeight = 600;

        // Create a bitmap that will hold all barcodes
        using (var canvas = new Bitmap(canvasWidth, canvasHeight))
        {
            // Obtain a graphics object for drawing onto the canvas
            using (var g = Graphics.FromImage(canvas))
            {
                // Fill the background with white color
                g.Clear(Color.White);

                // Define the barcodes to generate: type, text, and placement coordinates
                var barcodes = new (BaseEncodeType type, string text, int x, int y)[]
                {
                    (EncodeTypes.Code128, "ABC123", 50, 50),
                    (EncodeTypes.QR, "https://example.com", 300, 200),
                    (EncodeTypes.DataMatrix, "DataMatrixSample", 550, 400)
                };

                // Iterate over each barcode definition, generate it, and draw onto the canvas
                foreach (var (type, text, x, y) in barcodes)
                {
                    using (var generator = new BarcodeGenerator(type, text))
                    {
                        // Set a modest XDimension for better visibility
                        generator.Parameters.Barcode.XDimension.Point = 2f;

                        // Render the barcode to a memory stream in PNG format
                        using (var ms = new MemoryStream())
                        {
                            generator.Save(ms, BarCodeImageFormat.Png);
                            ms.Position = 0; // Reset stream position for reading

                            // Load the rendered barcode image from the stream
                            using (var barcodeImage = new Bitmap(ms))
                            {
                                // Draw the barcode image onto the canvas at the specified location
                                g.DrawImage(barcodeImage, x, y, barcodeImage.Width, barcodeImage.Height);
                            }
                        }
                    }
                }
            }

            // Save the combined barcode image to disk (optional, for inspection)
            const string combinedPath = "combined.png";
            canvas.Save(combinedPath, ImageFormat.Png);
            Console.WriteLine($"Combined barcode image saved to {combinedPath}");

            // Initialize a barcode reader to detect all supported types on the canvas
            using (var reader = new BarCodeReader(canvas, DecodeType.AllSupportedTypes))
            {
                // Configure quality settings to use minimal XDimension detection
                reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                // Optional: define a minimal XDimension threshold
                reader.QualitySettings.MinimalXDimension = 2f;

                // Perform barcode recognition
                var results = reader.ReadBarCodes();

                // Create a heat‑map bitmap with the same dimensions as the canvas
                using (var heatMap = new Bitmap(canvasWidth, canvasHeight))
                {
                    using (var gHeat = Graphics.FromImage(heatMap))
                    {
                        // Draw the original combined image as the background of the heat‑map
                        gHeat.DrawImage(canvas, 0, 0, canvasWidth, canvasHeight);

                        // Semi‑transparent red brush for overlaying detection regions
                        using (var overlayBrush = new SolidBrush(Color.FromArgb(120, 255, 0, 0)))
                        {
                            // Iterate over each detection result and overlay its region
                            foreach (var result in results)
                            {
                                // Retrieve the bounding rectangle of the detected barcode
                                var rectF = result.Region.Rectangle;

                                // Convert floating‑point rectangle to integer rectangle for drawing
                                var rect = new Rectangle(
                                    (int)Math.Round((double)rectF.X),
                                    (int)Math.Round((double)rectF.Y),
                                    (int)Math.Round((double)rectF.Width),
                                    (int)Math.Round((double)rectF.Height));

                                // Fill the rectangle with the semi‑transparent brush
                                gHeat.FillRectangle(overlayBrush, rect);
                            }
                        }
                    }

                    // Save the heat‑map image to disk
                    const string heatMapPath = "heatmap.png";
                    heatMap.Save(heatMapPath, ImageFormat.Png);
                    Console.WriteLine($"Heat map image saved to {heatMapPath}");
                }
            }
        }
    }
}