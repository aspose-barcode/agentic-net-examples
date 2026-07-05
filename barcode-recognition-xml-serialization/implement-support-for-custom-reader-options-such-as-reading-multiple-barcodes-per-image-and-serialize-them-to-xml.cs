// Title: Demonstrate reading multiple barcodes with custom options and XML serialization
// Description: Generates two barcodes, combines them into one image, reads them using custom reader options, and serializes the reader settings to XML.
// Prompt: Implement support for custom reader options, such as reading multiple barcodes per image, and serialize them to XML.
// Tags: barcode, symbology, multiread, xml, readeroptions, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program showing how to generate barcodes, combine them, read with custom options,
/// and serialize/deserialize reader settings to XML.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, combines them, reads using custom options,
    /// exports/imports settings to XML, and displays results.
    /// </summary>
    static void Main()
    {
        // Prepare temporary paths for output files
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);
        string combinedImagePath = Path.Combine(outputDir, "combined.png");
        string xmlSettingsPath = Path.Combine(outputDir, "readerSettings.xml");

        // Create two sample barcodes (Code128 and QR) in memory streams
        MemoryStream barcode1Stream = new MemoryStream();
        MemoryStream barcode2Stream = new MemoryStream();

        using (var generator1 = new BarcodeGenerator(EncodeTypes.Code128, "ABC123"))
        {
            generator1.Save(barcode1Stream, BarCodeImageFormat.Png);
        }
        using (var generator2 = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            generator2.Save(barcode2Stream, BarCodeImageFormat.Png);
        }

        // Reset streams to the beginning for reading
        barcode1Stream.Position = 0;
        barcode2Stream.Position = 0;

        // Load bitmaps from the streams and combine them side‑by‑side
        using (var bmp1 = new Bitmap(barcode1Stream))
        using (var bmp2 = new Bitmap(barcode2Stream))
        {
            int combinedWidth = bmp1.Width + bmp2.Width;
            int combinedHeight = Math.Max(bmp1.Height, bmp2.Height);

            using (var combinedBmp = new Bitmap(combinedWidth, combinedHeight))
            {
                using (var graphics = Graphics.FromImage(combinedBmp))
                {
                    graphics.Clear(Aspose.Drawing.Color.White);
                    graphics.DrawImage(bmp1, 0, 0, bmp1.Width, bmp1.Height);
                    graphics.DrawImage(bmp2, bmp1.Width, 0, bmp2.Width, bmp2.Height);
                }

                // Save the combined image to disk
                combinedBmp.Save(combinedImagePath, ImageFormat.Png);
            }
        }

        // Verify that the combined image was created successfully
        if (!File.Exists(combinedImagePath))
        {
            Console.WriteLine("Failed to create combined barcode image.");
            return;
        }

        // ---------- Read multiple barcodes with custom options ----------
        using (var reader = new BarCodeReader())
        {
            // Configure the reader to decode both Code128 and QR symbologies
            reader.BarCodeReadType = new MultiDecodeType(DecodeType.Code128, DecodeType.QR);

            // Example custom option: use fast deconvolution for quicker processing
            reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;

            // Assign the combined image as the source for reading
            reader.SetBarCodeImage(combinedImagePath);

            // Perform the reading operation and output results
            Console.WriteLine("Reading barcodes with custom options:");
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }

            // Export the current reader settings to an XML file
            reader.ExportToXml(xmlSettingsPath);
        }

        // Verify that the XML settings file was created
        if (!File.Exists(xmlSettingsPath))
        {
            Console.WriteLine("Failed to export reader settings to XML.");
            return;
        }

        // ---------- Import settings from XML and read again ----------
        var importedReader = BarCodeReader.ImportFromXml(xmlSettingsPath);
        if (importedReader == null)
        {
            Console.WriteLine("Failed to import reader settings from XML.");
            return;
        }

        using (importedReader)
        {
            // After importing, the image source must be set again
            importedReader.SetBarCodeImage(combinedImagePath);

            Console.WriteLine("Reading barcodes after importing settings from XML:");
            foreach (var result in importedReader.ReadBarCodes())
            {
                Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }

        // Cleanup temporary files (optional)
        // File.Delete(combinedImagePath);
        // File.Delete(xmlSettingsPath);
        // Directory.Delete(outputDir, true);
    }
}