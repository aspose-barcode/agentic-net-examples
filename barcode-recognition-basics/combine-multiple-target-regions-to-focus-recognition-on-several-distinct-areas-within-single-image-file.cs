// Title: Demonstrate combining multiple target regions for barcode recognition
// Description: Shows how to define separate image areas to focus barcode detection on distinct regions within a single image file.
// Prompt: Combine multiple target regions to focus recognition on several distinct areas within a single image file.
// Tags: barcode, target region, recognition, aspnet, csharp, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates an image containing two different barcodes,
/// then reads them by specifying multiple target regions within the same image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Path to the sample image that will contain multiple barcodes
        string imagePath = "sample_multi.png";

        // If the image does not exist, create it with two barcodes placed at different locations
        if (!File.Exists(imagePath))
        {
            // Create a blank bitmap large enough to hold two barcodes side by side
            using (Bitmap canvas = new Bitmap(800, 400))
            {
                using (Graphics g = Graphics.FromImage(canvas))
                {
                    // Fill background with white
                    g.Clear(Color.White);

                    // First barcode: Code128
                    using (BarcodeGenerator gen1 = new BarcodeGenerator(EncodeTypes.Code128, "ABC123"))
                    {
                        using (MemoryStream ms1 = new MemoryStream())
                        {
                            // Save generated barcode to memory stream as PNG
                            gen1.Save(ms1, BarCodeImageFormat.Png);
                            ms1.Position = 0;
                            using (Bitmap bmp1 = new Bitmap(ms1))
                            {
                                // Draw the first barcode at (50,50)
                                g.DrawImage(bmp1, new Point(50, 50));
                            }
                        }
                    }

                    // Second barcode: QR
                    using (BarcodeGenerator gen2 = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
                    {
                        using (MemoryStream ms2 = new MemoryStream())
                        {
                            // Save generated QR code to memory stream as PNG
                            gen2.Save(ms2, BarCodeImageFormat.Png);
                            ms2.Position = 0;
                            using (Bitmap bmp2 = new Bitmap(ms2))
                            {
                                // Draw the second barcode at (450,150)
                                g.DrawImage(bmp2, new Point(450, 150));
                            }
                        }
                    }
                }

                // Save the composed image to disk
                canvas.Save(imagePath, ImageFormat.Png);
            }

            Console.WriteLine($"Sample image created at '{imagePath}'.");
        }

        // Load the image for recognition
        using (Bitmap bitmap = new Bitmap(imagePath))
        {
            // Define two target regions (left and right halves of the image)
            Rectangle[] targetAreas = new Rectangle[]
            {
                new Rectangle(0, 0, bitmap.Width / 2, bitmap.Height),
                new Rectangle(bitmap.Width / 2, 0, bitmap.Width / 2, bitmap.Height)
            };

            // Initialize the barcode reader
            using (BarCodeReader reader = new BarCodeReader())
            {
                // Detect all supported barcode types
                reader.BarCodeReadType = DecodeType.AllSupportedTypes;

                // Assign the image and the target regions
                reader.SetBarCodeImage(bitmap, targetAreas);

                // Perform recognition
                BarCodeResult[] results = reader.ReadBarCodes();

                // Output information about each detected barcode
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"Code Text : {result.CodeText}");
                    Console.WriteLine($"Code Type : {result.CodeTypeName}");

                    // Region rectangle (pixel coordinates)
                    var rect = result.Region.Rectangle;
                    Console.WriteLine($"Region    : X={rect.X}, Y={rect.Y}, Width={rect.Width}, Height={rect.Height}");

                    // Orientation angle (degrees)
                    Console.WriteLine($"Angle     : {result.Region.Angle}");
                    Console.WriteLine(new string('-', 40));
                }

                if (results.Length == 0)
                {
                    Console.WriteLine("No barcodes were detected in the specified regions.");
                }
            }
        }
    }
}