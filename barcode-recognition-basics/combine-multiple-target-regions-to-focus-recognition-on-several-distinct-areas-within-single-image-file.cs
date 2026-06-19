using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating two different barcodes, compositing them onto a single canvas,
/// and recognizing each barcode within its specific region.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Code128 and a QR barcode, draws them on a combined image,
    /// and reads them back using region‑specific recognition.
    /// </summary>
    static void Main()
    {
        // Create first barcode (Code128) and store it in a memory stream
        using (var ms1 = new MemoryStream())
        using (var generator1 = new BarcodeGenerator(EncodeTypes.Code128, "ABC123"))
        {
            // Save the generated barcode as PNG into the stream
            generator1.Save(ms1, BarCodeImageFormat.Png);
            ms1.Position = 0; // Reset stream position for reading

            // Create second barcode (QR) and store it in a separate memory stream
            using (var ms2 = new MemoryStream())
            using (var generator2 = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
            {
                // Save the QR code as PNG into the second stream
                generator2.Save(ms2, BarCodeImageFormat.Png);
                ms2.Position = 0; // Reset stream position for reading

                // Load both barcode images from the memory streams as Bitmap objects
                using (var bmp1 = new Bitmap(ms1))
                using (var bmp2 = new Bitmap(ms2))
                {
                    // Determine canvas size with padding between images and borders
                    int padding = 10;
                    int canvasWidth = bmp1.Width + bmp2.Width + padding * 3;
                    int canvasHeight = Math.Max(bmp1.Height, bmp2.Height) + padding * 2;

                    // Create a blank canvas bitmap and obtain a Graphics object for drawing
                    using (var canvas = new Bitmap(canvasWidth, canvasHeight))
                    using (var graphics = Graphics.FromImage(canvas))
                    {
                        // Fill the canvas background with white
                        graphics.Clear(Color.White);

                        // Draw the first barcode at the left side of the canvas
                        int x1 = padding;
                        int y1 = padding;
                        graphics.DrawImage(bmp1, x1, y1, bmp1.Width, bmp1.Height);

                        // Draw the second barcode to the right of the first, separated by padding
                        int x2 = x1 + bmp1.Width + padding;
                        int y2 = padding;
                        graphics.DrawImage(bmp2, x2, y2, bmp2.Width, bmp2.Height);

                        // Define the rectangular regions that correspond to each barcode's location
                        var region1 = new Rectangle(x1, y1, bmp1.Width, bmp1.Height);
                        var region2 = new Rectangle(x2, y2, bmp2.Width, bmp2.Height);

                        // Initialize a barcode reader to recognize barcodes within the defined regions
                        using (var reader = new BarCodeReader())
                        {
                            // Allow reading of all supported barcode types
                            reader.SetBarCodeReadType(DecodeType.AllSupportedTypes);
                            // Provide the canvas image and the regions to scan
                            reader.SetBarCodeImage(canvas, new Rectangle[] { region1, region2 });

                            // Iterate over each detected barcode and output its details
                            foreach (var result in reader.ReadBarCodes())
                            {
                                var rect = result.Region.Rectangle;
                                Console.WriteLine($"Detected Barcode:");
                                Console.WriteLine($"  Type      : {result.CodeTypeName}");
                                Console.WriteLine($"  Text      : {result.CodeText}");
                                Console.WriteLine($"  Region    : X={rect.X}, Y={rect.Y}, W={rect.Width}, H={rect.Height}");
                                Console.WriteLine($"  Angle     : {result.Region.Angle}");
                            }
                        }
                    }
                }
            }
        }
    }
}