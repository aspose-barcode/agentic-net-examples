using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const int canvasWidth = 800;
        const int canvasHeight = 600;
        const string combinedPath = "combined.png";
        const string heatMapPath = "heatmap.png";

        // Create a blank canvas
        using (var canvas = new Bitmap(canvasWidth, canvasHeight))
        using (var graphics = Graphics.FromImage(canvas))
        {
            graphics.Clear(Aspose.Drawing.Color.White);

            // Generate and place several barcodes at different positions
            var positions = new[]
            {
                new Rectangle(50, 50, 200, 100),
                new Rectangle(300, 80, 250, 120),
                new Rectangle(100, 250, 300, 150),
                new Rectangle(450, 300, 200, 100),
                new Rectangle(200, 450, 350, 120)
            };

            foreach (var rect in positions)
            {
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ABC123"))
                {
                    // Ensure the barcode fits the target rectangle
                    generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
                    generator.Parameters.ImageWidth.Point = rect.Width;
                    generator.Parameters.ImageHeight.Point = rect.Height;
                    generator.Parameters.Resolution = 96f;

                    // Generate barcode image
                    using (Bitmap barcodeBmp = generator.GenerateBarCodeImage())
                    {
                        // Draw barcode onto the canvas
                        graphics.DrawImage(barcodeBmp, rect);
                    }
                }
            }

            // Save the combined image
            canvas.Save(combinedPath, ImageFormat.Png);
        }

        // Verify the combined image exists
        if (!File.Exists(combinedPath))
        {
            Console.WriteLine("Failed to create the combined image.");
            return;
        }

        // Read barcodes using XDimension mode
        using (var reader = new BarCodeReader())
        {
            // Set to detect all supported types
            reader.BarCodeReadType = DecodeType.AllSupportedTypes;

            // Configure XDimension mode to use a minimal size (e.g., 2 pixels)
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 2f;

            // Load the image
            reader.SetBarCodeImage(combinedPath);

            // Perform recognition
            var results = reader.ReadBarCodes();

            // Prepare heat map bitmap (same size as source)
            using (var heatMap = new Bitmap(canvasWidth, canvasHeight))
            using (var gHeat = Graphics.FromImage(heatMap))
            {
                gHeat.Clear(Aspose.Drawing.Color.Transparent);

                // Semi‑transparent red brush for heat zones
                using (var brush = new SolidBrush(Aspose.Drawing.Color.FromArgb(128, 255, 0, 0)))
                {
                    foreach (var result in results)
                    {
                        // Get bounding rectangle of the detected barcode
                        var rect = result.Region.Rectangle;

                        // Draw the rectangle onto the heat map
                        gHeat.FillRectangle(brush, rect);
                    }
                }

                // Save the heat map image
                heatMap.Save(heatMapPath, ImageFormat.Png);
            }

            // Output simple report
            Console.WriteLine($"Detected {results.Length} barcode(s).");
            Console.WriteLine($"Combined image saved to: {Path.GetFullPath(combinedPath)}");
            Console.WriteLine($"Heat map saved to: {Path.GetFullPath(heatMapPath)}");
        }
    }
}