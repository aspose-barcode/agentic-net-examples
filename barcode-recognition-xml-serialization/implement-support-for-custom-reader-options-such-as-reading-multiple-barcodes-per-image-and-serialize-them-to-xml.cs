// Title: Read Multiple Barcodes from a Combined Image with Custom Reader Settings and XML Serialization
// Description: Demonstrates generating Code128 and QR barcodes, merging them into a single image, reading both barcodes using custom reader options, and persisting the reader configuration to XML.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases key API classes such as BarcodeGenerator, BarCodeReader, and related settings (BarcodeSettings, QualitySettings). Typical use cases include batch scanning of images containing several barcodes, customizing decoding behavior, and exporting/importing reader configurations for repeatable processing. Developers often need to fine‑tune reader options and serialize them for consistent deployments.
// Prompt: Implement support for custom reader options, such as reading multiple barcodes per image, and serialize them to XML.
// Tags: barcode, generation, recognition, multiple, xml, custom-reader-options, aspose.barcode, code128, qr

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation, image composition, custom reader configuration,
/// and XML serialization of reader settings using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample barcodes, combines them,
    /// reads them with custom options, and shows how to export/import reader settings.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for all generated files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcode images (Code128 and QR)
        string code128Path = Path.Combine(tempFolder, "code128.png");
        string qrPath = Path.Combine(tempFolder, "qr.png");
        GenerateBarcode(EncodeTypes.Code128, "1234567890", code128Path);
        GenerateBarcode(EncodeTypes.QR, "https://example.com", qrPath);

        // Combine the two barcode images into a single image
        string combinedPath = Path.Combine(tempFolder, "combined.png");
        CombineImages(new[] { code128Path, qrPath }, combinedPath);

        // Verify that the combined image was created successfully
        if (!File.Exists(combinedPath))
        {
            Console.WriteLine("Combined image not found.");
            return;
        }

        // Initialize BarCodeReader with the option to decode all supported types
        BaseDecodeType decodeAll = DecodeType.AllSupportedTypes;
        using (var reader = new BarCodeReader(combinedPath, decodeAll))
        {
            // Apply custom reader options
            reader.BarcodeSettings.StripFNC = true;          // Remove FNC characters from the result
            reader.QualitySettings = QualitySettings.HighQuality; // Use high‑quality decoding

            // Export the current reader configuration to an XML file
            string xmlPath = Path.Combine(tempFolder, "readerSettings.xml");
            reader.ExportToXml(xmlPath);
            Console.WriteLine($"Reader settings exported to: {xmlPath}");

            // Read all barcodes present in the combined image
            BarCodeResult[] results = reader.ReadBarCodes();
            Console.WriteLine($"Barcodes detected in combined image: {results.Length}");
            foreach (var result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Import the previously saved settings from XML and read the image again
        string importedXmlPath = Path.Combine(tempFolder, "readerSettings.xml");
        if (File.Exists(importedXmlPath))
        {
            using (var importedReader = BarCodeReader.ImportFromXml(importedXmlPath))
            {
                importedReader.SetBarCodeImage(combinedPath);
                BarCodeResult[] importedResults = importedReader.ReadBarCodes();
                Console.WriteLine($"Barcodes detected after importing settings: {importedResults.Length}");
                foreach (var result in importedResults)
                {
                    Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                }
            }
        }

        // Optional cleanup (commented out to allow inspection of generated files)
        // Directory.Delete(tempFolder, true);
    }

    /// <summary>
    /// Generates a barcode image using the specified encoding type and text.
    /// </summary>
    /// <param name="encodeType">The barcode symbology to use.</param>
    /// <param name="codeText">The data to encode.</param>
    /// <param name="outputPath">File path where the image will be saved.</param>
    static void GenerateBarcode(BaseEncodeType encodeType, string codeText, string outputPath)
    {
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Set a readable font for the human‑readable text
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }

    /// <summary>
    /// Combines multiple images horizontally into a single image.
    /// </summary>
    /// <param name="imagePaths">Array of file paths to the source images.</param>
    /// <param name="outputPath">File path where the combined image will be saved.</param>
    static void CombineImages(string[] imagePaths, string outputPath)
    {
        var bitmaps = new List<Bitmap>();

        // Load each image into a Bitmap object
        foreach (string path in imagePaths)
        {
            if (File.Exists(path))
            {
                bitmaps.Add(new Bitmap(path));
            }
        }

        if (bitmaps.Count == 0)
        {
            Console.WriteLine("No images available for combination.");
            return;
        }

        // Calculate total width and maximum height for the combined image
        int totalWidth = 0;
        int maxHeight = 0;
        foreach (var bmp in bitmaps)
        {
            totalWidth += bmp.Width;
            if (bmp.Height > maxHeight) maxHeight = bmp.Height;
        }

        // Create the combined bitmap and draw each source bitmap side by side
        using (var combined = new Bitmap(totalWidth, maxHeight))
        {
            using (var graphics = Graphics.FromImage(combined))
            {
                int offsetX = 0;
                foreach (var bmp in bitmaps)
                {
                    graphics.DrawImage(bmp, offsetX, 0, bmp.Width, bmp.Height);
                    offsetX += bmp.Width;
                }
            }

            // Save the combined image as PNG
            combined.Save(outputPath, ImageFormat.Png);
        }

        // Release resources held by the source bitmaps
        foreach (var bmp in bitmaps)
        {
            bmp.Dispose();
        }
    }
}