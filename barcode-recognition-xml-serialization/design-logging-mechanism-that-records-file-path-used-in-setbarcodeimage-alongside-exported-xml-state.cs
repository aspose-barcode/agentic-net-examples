// Title: Barcode generation, XML export, and decoding with Aspose.BarCode
// Description: Demonstrates creating a Code128 barcode image, exporting the generator configuration to XML, and decoding the barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It shows how to use BarcodeGenerator to create barcodes, export its settings to XML via ExportToXml, and employ BarCodeReader with SetBarCodeImage for decoding. Developers often need to persist barcode configurations, log file paths, and perform read‑back verification in automated workflows.
// Prompt: Design a logging mechanism that records the file path used in SetBarCodeImage alongside the exported XML state.
// Tags: barcode generation, barcode recognition, code128, xml export, logging, aspose.barcode, csharp

using System;
using System.IO;
using System.Text;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode creation, configuration export, and decoding using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, exports its XML configuration,
    /// logs relevant information, and decodes the barcode from the saved image.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for this demo
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define paths for the barcode image and the exported XML
        string barcodeImagePath = Path.Combine(tempFolder, "barcode.png");
        string exportedXmlPath = Path.Combine(tempFolder, "barcode_config.xml");

        // Generate a barcode and save it as an image
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            // Optional: set barcode appearance
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

            // Save the barcode image to the specified path
            generator.Save(barcodeImagePath, BarCodeImageFormat.Png);

            // Export the generator configuration to XML (in-memory)
            using (var xmlStream = new MemoryStream())
            {
                generator.ExportToXml(xmlStream);
                xmlStream.Position = 0;

                // Write XML to a physical file for inspection (optional)
                using (var fileStream = new FileStream(exportedXmlPath, FileMode.Create, FileAccess.Write))
                {
                    xmlStream.CopyTo(fileStream);
                }

                // Read XML as a string for logging purposes
                xmlStream.Position = 0;
                using (var reader = new StreamReader(xmlStream, Encoding.UTF8, leaveOpen: true))
                {
                    string xmlContent = reader.ReadToEnd();
                    Console.WriteLine("=== Exported Barcode XML Configuration ===");
                    Console.WriteLine(xmlContent);
                }
            }
        }

        // Log the file path used in SetBarCodeImage
        Console.WriteLine($"SetBarCodeImage called with path: {barcodeImagePath}");

        // Create a BarCodeReader and assign the image via SetBarCodeImage
        using (var reader = new BarCodeReader())
        {
            // Assign the barcode image file to the reader
            reader.SetBarCodeImage(barcodeImagePath);

            // Optionally specify a decode type (default detection works for Code128)
            BaseDecodeType decodeType = DecodeType.Code128;

            // Read barcodes from the image
            BarCodeResult[] results = reader.ReadBarCodes();

            Console.WriteLine("=== Decoding Results ===");
            if (results != null && results.Length > 0)
            {
                foreach (var result in results)
                {
                    Console.WriteLine($"CodeText: {result.CodeText}");
                    Console.WriteLine($"CodeType: {result.CodeTypeName}");
                    Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
                }
            }
            else
            {
                Console.WriteLine("No barcode detected.");
            }
        }

        // Cleanup: optionally delete temporary files (commented out to allow inspection)
        // Directory.Delete(tempFolder, true);
    }
}