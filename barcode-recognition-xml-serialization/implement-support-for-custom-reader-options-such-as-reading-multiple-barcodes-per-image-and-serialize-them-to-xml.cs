// Title: Custom Reader Options and XML Serialization Example
// Description: Demonstrates how to configure BarCodeReader with custom options, read multiple barcodes from a combined image, and serialize/deserialize the settings to XML.
// Category-Description: This example belongs to the Aspose.BarCode reading and generation category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for recognizing multiple symbologies, and the QualitySettings and XML export/import features for persisting custom reader configurations. Developers working with barcode scanning, batch processing, or custom recognition pipelines often need to adjust reader options and reuse them across sessions.
// Prompt: Implement support for custom reader options, such as reading multiple barcodes per image, and serialize them to XML.
// Tags: barcode, symbology, generation, recognition, custom-options, xml, aspose.barcode, aspose.barcode.generation, aspose.barcode.recognition

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates creating two barcodes, combining them into a single image,
/// configuring custom reader options, and persisting those settings to XML.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes, reads them with custom options,
    /// and shows how to export and import reader settings via XML.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare output directory and file paths
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        Directory.CreateDirectory(outputDir);
        string combinedPath = Path.Combine(outputDir, "combined.png");
        string xmlPath = Path.Combine(outputDir, "readerSettings.xml");

        // --------------------------------------------------------------------
        // Generate a Code128 barcode and store it in a memory stream
        // --------------------------------------------------------------------
        MemoryStream code128Stream = new MemoryStream();
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "CODE128-123"))
        {
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;
            generator.Save(code128Stream, BarCodeImageFormat.Png);
        }
        code128Stream.Position = 0;
        Bitmap code128Bmp = new Bitmap(code128Stream);

        // --------------------------------------------------------------------
        // Generate a QR code and store it in a memory stream
        // --------------------------------------------------------------------
        MemoryStream qrStream = new MemoryStream();
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;
            generator.Save(qrStream, BarCodeImageFormat.Png);
        }
        qrStream.Position = 0;
        Bitmap qrBmp = new Bitmap(qrStream);

        // --------------------------------------------------------------------
        // Combine the two barcode images side by side into a single bitmap
        // --------------------------------------------------------------------
        int combinedWidth = code128Bmp.Width + qrBmp.Width;
        int combinedHeight = Math.Max(code128Bmp.Height, qrBmp.Height);
        using (var combinedBmp = new Bitmap(combinedWidth, combinedHeight))
        {
            using (var graphics = Graphics.FromImage(combinedBmp))
            {
                graphics.Clear(Aspose.Drawing.Color.White);
                graphics.DrawImage(code128Bmp, 0, 0, code128Bmp.Width, code128Bmp.Height);
                graphics.DrawImage(qrBmp, code128Bmp.Width, 0, qrBmp.Width, qrBmp.Height);
            }
            combinedBmp.Save(combinedPath, ImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Verify that the combined image was created successfully
        // --------------------------------------------------------------------
        if (!File.Exists(combinedPath))
        {
            Console.WriteLine("Failed to create the combined barcode image.");
            return;
        }

        // --------------------------------------------------------------------
        // Initialize BarCodeReader with custom quality settings
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(combinedPath, DecodeType.AllSupportedTypes))
        {
            // Enable reading of potentially imperfect barcodes
            reader.QualitySettings.AllowIncorrectBarcodes = true;
            // Use fast deconvolution for quicker processing
            reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;

            // Read and display all detected barcodes
            Console.WriteLine("Reading barcodes with custom options:");
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }

            // Export the current reader configuration to an XML file
            reader.ExportToXml(xmlPath);
        }

        // --------------------------------------------------------------------
        // Import reader settings from the previously saved XML and read again
        // --------------------------------------------------------------------
        var importedReader = BarCodeReader.ImportFromXml(xmlPath);
        if (importedReader == null)
        {
            Console.WriteLine("Failed to import reader settings from XML.");
            return;
        }

        // Assign the same combined image to the imported reader instance
        importedReader.SetBarCodeImage(combinedPath);
        Console.WriteLine("\nReading barcodes after importing settings from XML:");
        foreach (var result in importedReader.ReadBarCodes())
        {
            Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
        }

        // --------------------------------------------------------------------
        // Clean up the imported reader instance
        // --------------------------------------------------------------------
        importedReader.Dispose();
    }
}