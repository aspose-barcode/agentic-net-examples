using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Paths for temporary images
        string barcodePath = "barcode.png";
        string canvasPath = "canvas.png";

        // Generate a simple Code128 barcode and save it
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(barcodePath);
        }

        // Verify barcode image was created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine($"Failed to create barcode image at '{barcodePath}'.");
            return;
        }

        // Load the generated barcode image
        using (var barcodeBmp = new Bitmap(barcodePath))
        {
            // Create a larger canvas and draw the barcode onto it at a specific offset
            int canvasWidth = 400;
            int canvasHeight = 300;
            int offsetX = 120;
            int offsetY = 80;

            using (var canvasBmp = new Bitmap(canvasWidth, canvasHeight, PixelFormat.Format32bppArgb))
            {
                using (var graphics = Graphics.FromImage(canvasBmp))
                {
                    // Fill background with white
                    graphics.Clear(Color.White);
                    // Draw the barcode onto the canvas
                    graphics.DrawImage(barcodeBmp, offsetX, offsetY, barcodeBmp.Width, barcodeBmp.Height);
                }

                // Save the composite image (optional, for visual verification)
                canvasBmp.Save(canvasPath);

                // Define the rectangular region that exactly covers the barcode area
                var targetRegion = new Rectangle(offsetX, offsetY, barcodeBmp.Width, barcodeBmp.Height);

                // Initialize BarCodeReader with the canvas bitmap and the target region
                using (var reader = new BarCodeReader(canvasBmp, targetRegion, DecodeType.Code128))
                {
                    // Perform recognition
                    var results = reader.ReadBarCodes();

                    if (results.Length == 0)
                    {
                        Console.WriteLine("No barcodes detected within the specified region.");
                    }
                    else
                    {
                        foreach (var result in results)
                        {
                            Console.WriteLine($"Detected Barcode Type: {result.CodeTypeName}");
                            Console.WriteLine($"Detected Barcode Text: {result.CodeText}");
                            // Show the region of the detected barcode
                            var bounds = result.Region.Rectangle;
                            Console.WriteLine($"Barcode Region: X={bounds.X}, Y={bounds.Y}, Width={bounds.Width}, Height={bounds.Height}");
                        }
                    }
                }
            }
        }

        // Clean up temporary files (optional)
        try
        {
            if (File.Exists(barcodePath)) File.Delete(barcodePath);
            if (File.Exists(canvasPath)) File.Delete(canvasPath);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}