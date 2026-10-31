// Title: Read Multiple Barcodes with Custom Options and XML Serialization
// Description: Generates a Code128 and a QR barcode, combines them into a single image, reads both barcodes using custom reader settings, and demonstrates exporting/importing the reader configuration to XML.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases key API classes such as BarcodeGenerator, BarCodeReader, and related settings (BarcodeSettings, QualitySettings). Typical use cases include processing images that contain several barcodes, customizing decoding behavior, and persisting reader configurations for later reuse. Developers often need to fine‑tune reader options and serialize them for batch processing or distributed environments.
// Prompt: Implement support for custom reader options, such as reading multiple barcodes per image, and serialize them to XML.
// Tags: barcode, generation, recognition, multiple-barcodes, custom-reader-options, xml-serialization, aspose.barcode, code128, qr

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation, combining images, reading multiple barcodes with custom settings,
/// and exporting/importing the reader configuration to XML using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for all generated files.
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarCodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the combined image and the exported reader state.
        string combinedImagePath = Path.Combine(tempFolder, "combined.png");
        string readerXmlPath = Path.Combine(tempFolder, "readerState.xml");

        // Generate the first barcode (Code128) and the second barcode (QR), then combine them.
        using (var generator1 = new BarcodeGenerator(EncodeTypes.Code128, "ABC123"))
        {
            using (var bitmap1 = generator1.GenerateBarCodeImage())
            {
                using (var generator2 = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
                {
                    using (var bitmap2 = generator2.GenerateBarCodeImage())
                    {
                        // Determine combined image dimensions.
                        int width = Math.Max(bitmap1.Width + bitmap2.Width + 30, 400);
                        int height = Math.Max(Math.Max(bitmap1.Height, bitmap2.Height) + 20, 200);

                        // Create a larger bitmap to hold both barcodes side by side.
                        using (var combined = new Bitmap(width, height))
                        {
                            using (var graphics = Graphics.FromImage(combined))
                            {
                                graphics.Clear(Aspose.Drawing.Color.White);
                                graphics.DrawImage(bitmap1, new Rectangle(10, 10, bitmap1.Width, bitmap1.Height));
                                graphics.DrawImage(bitmap2, new Rectangle(bitmap1.Width + 20, 10, bitmap2.Width, bitmap2.Height));

                                // Save the combined image to disk.
                                combined.Save(combinedImagePath, ImageFormat.Png);
                            }
                        }
                    }
                }
            }
        }

        // Prepare a reader that can decode all supported barcode types.
        BaseDecodeType decodeAll = DecodeType.AllSupportedTypes;
        using (var reader = new BarCodeReader(combinedImagePath, decodeAll))
        {
            // Apply custom reader options.
            reader.BarcodeSettings.StripFNC = true;
            reader.QualitySettings.XDimension = XDimensionMode.Small;
            reader.QualitySettings.AllowIncorrectBarcodes = true;

            // Read all barcodes present in the combined image.
            BarCodeResult[] results = reader.ReadBarCodes();
            Console.WriteLine($"Barcodes found: {results.Length}");
            foreach (var result in results)
            {
                Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }

            // Export the current reader configuration (settings only) to an XML file.
            reader.ExportToXml(readerXmlPath);
            Console.WriteLine($"Reader state exported to: {readerXmlPath}");
        }

        // Import the previously saved reader configuration and read the barcodes again.
        using (var importedReader = BarCodeReader.ImportFromXml(readerXmlPath))
        {
            // Associate the same image with the imported reader (image data is not stored in XML).
            importedReader.SetBarCodeImage(combinedImagePath);

            // Perform barcode detection using the imported settings.
            BarCodeResult[] importedResults = importedReader.ReadBarCodes();
            Console.WriteLine($"Barcodes read after import: {importedResults.Length}");
            foreach (var result in importedResults)
            {
                Console.WriteLine($"[Imported] Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }

        // Optional cleanup: delete the temporary folder and its contents.
        // Directory.Delete(tempFolder, true);
    }
}