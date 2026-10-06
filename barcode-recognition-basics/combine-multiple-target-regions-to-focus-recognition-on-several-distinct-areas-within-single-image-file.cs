// Title: Combine Multiple Target Regions for Barcode Recognition
// Description: Demonstrates generating QR and Code128 barcodes, merging them into one image, and using target regions to limit barcode recognition to each distinct area.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to work with BarcodeGenerator, BarCodeReader, and region-based decoding. Developers often need to process images containing multiple barcodes and isolate each using Rectangle regions to improve accuracy and performance. The snippet illustrates typical use cases such as scanning composite documents or combined label images.
// Prompt: Combine multiple target regions to focus recognition on several distinct areas within a single image file.
// Tags: qr, code128, barcode recognition, target region, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates combining multiple barcode images into a single canvas and recognizing each barcode
/// by specifying separate target regions for the Aspose.BarCode reader.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates QR and Code128 barcodes, composes them into one image,
    /// defines distinct regions for each barcode, and performs region‑limited recognition.
    /// </summary>
    static void Main()
    {
        // Create a temporary working directory for intermediate files
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeRegionDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Path for the combined image that will contain both barcodes
        string combinedImagePath = Path.Combine(tempDir, "combined.png");

        // -------------------- Generate QR barcode --------------------
        MemoryStream qrStream = new MemoryStream();
        using (BarcodeGenerator qrGenerator = new BarcodeGenerator(EncodeTypes.QR, "QR Sample"))
        {
            // Save QR barcode to memory stream in PNG format
            qrGenerator.Save(qrStream, BarCodeImageFormat.Png);
        }
        qrStream.Position = 0; // Reset stream position for reading

        // -------------------- Generate Code128 barcode --------------------
        MemoryStream code128Stream = new MemoryStream();
        using (BarcodeGenerator code128Generator = new BarcodeGenerator(EncodeTypes.Code128, "CODE128"))
        {
            // Save Code128 barcode to memory stream in PNG format
            code128Generator.Save(code128Stream, BarCodeImageFormat.Png);
        }
        code128Stream.Position = 0; // Reset stream position for reading

        // -------------------- Load generated barcode images --------------------
        Image qrImage;
        Image code128Image;
        using (qrImage = Image.FromStream(qrStream))
        using (code128Image = Image.FromStream(code128Stream))
        {
            // Retrieve dimensions of each barcode image
            int qrWidth = qrImage.Width;
            int qrHeight = qrImage.Height;
            int code128Width = code128Image.Width;
            int code128Height = code128Image.Height;

            // Create a blank canvas large enough to hold both barcodes side by side with padding
            int canvasWidth = qrWidth + code128Width + 20; // 20 px total horizontal padding
            int canvasHeight = Math.Max(qrHeight, code128Height) + 20; // 20 px total vertical padding

            using (Bitmap canvas = new Bitmap(canvasWidth, canvasHeight, PixelFormat.Format32bppArgb))
            {
                using (Graphics g = Graphics.FromImage(canvas))
                {
                    // Fill background with white
                    g.Clear(Color.White);

                    // Draw QR barcode at (10, 10)
                    g.DrawImage(qrImage, 10, 10, qrWidth, qrHeight);

                    // Draw Code128 barcode to the right of the QR barcode
                    g.DrawImage(code128Image, qrWidth + 20, 10, code128Width, code128Height);
                }

                // Save the combined image to disk
                canvas.Save(combinedImagePath, ImageFormat.Png);
            }

            // -------------------- Define target regions for each barcode --------------------
            Rectangle rectQr = new Rectangle(10, 10, qrWidth, qrHeight);
            Rectangle rectCode128 = new Rectangle(qrWidth + 20, 10, code128Width, code128Height);

            // -------------------- Perform region‑limited barcode recognition --------------------
            using (Bitmap combinedBmp = new Bitmap(combinedImagePath))
            using (BarCodeReader reader = new BarCodeReader())
            {
                // Set the image and the array of regions to scan
                reader.SetBarCodeImage(combinedBmp, new Rectangle[] { rectQr, rectCode128 });

                // Restrict decoding to the expected symbologies
                reader.SetBarCodeReadType(DecodeType.QR, DecodeType.Code128);

                Console.WriteLine("Recognition results within specified regions:");
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                }
            }
        }

        // -------------------- Clean up temporary files --------------------
        try
        {
            File.Delete(combinedImagePath);
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored – cleanup failures should not affect program outcome
        }
    }
}