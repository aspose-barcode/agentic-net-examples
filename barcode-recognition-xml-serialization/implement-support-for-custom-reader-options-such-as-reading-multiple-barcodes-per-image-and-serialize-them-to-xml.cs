using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating two different barcodes, combining them into a single image,
/// reading the combined image with custom reader options, and exporting those options to XML.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Define file paths for the combined image and the exported XML file.
        // --------------------------------------------------------------------
        string combinedImagePath = Path.Combine(Directory.GetCurrentDirectory(), "combined.png");
        string readerOptionsXmlPath = Path.Combine(Directory.GetCurrentDirectory(), "readerOptions.xml");

        // --------------------------------------------------------------------
        // Generate the first barcode (Code128) and store it in a bitmap.
        // --------------------------------------------------------------------
        Bitmap bmpCode128;
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ABC123"))
        {
            using (var ms = new MemoryStream())
            {
                // Save the barcode image to a memory stream in PNG format.
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.
                bmpCode128 = new Bitmap(ms);
            }
        }

        // --------------------------------------------------------------------
        // Generate the second barcode (QR) and store it in a bitmap.
        // --------------------------------------------------------------------
        Bitmap bmpQr;
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            using (var ms = new MemoryStream())
            {
                // Save the QR code image to a memory stream in PNG format.
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.
                bmpQr = new Bitmap(ms);
            }
        }

        // --------------------------------------------------------------------
        // Combine the two barcode bitmaps side by side into a single image.
        // --------------------------------------------------------------------
        int combinedWidth = bmpCode128.Width + bmpQr.Width;
        int combinedHeight = Math.Max(bmpCode128.Height, bmpQr.Height);
        using (var combinedBitmap = new Bitmap(combinedWidth, combinedHeight))
        {
            using (var graphics = Graphics.FromImage(combinedBitmap))
            {
                // Fill background with white.
                graphics.Clear(Color.White);
                // Draw the first barcode on the left.
                graphics.DrawImage(bmpCode128, 0, 0, bmpCode128.Width, bmpCode128.Height);
                // Draw the second barcode on the right.
                graphics.DrawImage(bmpQr, bmpCode128.Width, 0, bmpQr.Width, bmpQr.Height);
            }

            // Save the combined image to disk as PNG.
            combinedBitmap.Save(combinedImagePath, ImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Release the temporary bitmap resources.
        // --------------------------------------------------------------------
        bmpCode128.Dispose();
        bmpQr.Dispose();

        // --------------------------------------------------------------------
        // Create a BarCodeReader for the combined image with all supported types.
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(combinedImagePath, DecodeType.AllSupportedTypes))
        {
            // Configure custom reader options for higher quality and specific settings.
            reader.QualitySettings = QualitySettings.HighQuality;               // Use high‑quality preset.
            reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;    // Fast deconvolution mode.
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 2f;                    // Minimal X dimension in pixels.
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On; // Enforce checksum validation.

            // Read all barcodes present in the combined image.
            var results = reader.ReadBarCodes();
            Console.WriteLine($"Detected {results.Length} barcode(s):");
            foreach (var result in results)
            {
                Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }

            // Export the configured reader options to an XML file.
            reader.ExportToXml(readerOptionsXmlPath);
            Console.WriteLine($"Reader options exported to: {readerOptionsXmlPath}");
        }
    }
}