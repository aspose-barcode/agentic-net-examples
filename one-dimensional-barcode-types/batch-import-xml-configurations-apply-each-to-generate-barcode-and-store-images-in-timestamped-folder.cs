// Title: Batch generate barcodes from XML configurations
// Description: Demonstrates creating barcode configuration XML files, importing them, generating barcode images, and saving them to a timestamped folder.
// Category-Description: This example belongs to the Aspose.BarCode generation and XML import/export category. It showcases the use of BarcodeGenerator for creating barcodes, exporting configuration to XML with ExportToXml, and later importing those XML files via ImportFromXml to produce barcode images. Developers often need to store barcode settings, share them across services, or batch‑process multiple configurations, making this pattern a common solution in automated barcode workflows.
// Prompt: Batch import XML configurations, apply each to generate a barcode, and store images in a timestamped folder.
// Tags: barcode, xml, batch, generation, import, export, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates sample barcode XML configurations,
/// imports each configuration, generates a barcode image, and saves
/// the images to a timestamped output folder.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the full workflow:
    /// 1. Generates temporary XML configuration files.
    /// 2. Imports each XML file to create a barcode.
    /// 3. Saves the generated barcode images.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Create a temporary folder to store sample XML configuration files.
        // --------------------------------------------------------------------
        string configFolder = Path.Combine(Path.GetTempPath(), "BarcodeConfigs_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(configFolder);

        // --------------------------------------------------------------------
        // 2. Define a set of sample barcode configurations (type, text, file name).
        // --------------------------------------------------------------------
        var sampleConfigs = new List<(BaseEncodeType encodeType, string codeText, string fileName)>
        {
            (EncodeTypes.QR, "Sample QR", "qr.xml"),
            (EncodeTypes.Code128, "CODE12812345", "code128.xml"),
            (EncodeTypes.Pdf417, "PDF417 Sample Text", "pdf417.xml")
        };

        // --------------------------------------------------------------------
        // 3. Export each configuration to an individual XML file.
        // --------------------------------------------------------------------
        foreach (var (encodeType, codeText, fileName) in sampleConfigs)
        {
            string xmlPath = Path.Combine(configFolder, fileName);
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Example parameter adjustment – set X dimension to 2 pixels.
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.ExportToXml(xmlPath);
            }
        }

        // --------------------------------------------------------------------
        // 4. Prepare a timestamped output folder for the generated barcode images.
        // --------------------------------------------------------------------
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + DateTime.Now.ToString("yyyyMMdd_HHmmss"));
        Directory.CreateDirectory(outputFolder);

        // --------------------------------------------------------------------
        // 5. Build a list of XML file paths to process (explicit list, no scanning).
        // --------------------------------------------------------------------
        var xmlFiles = new List<string>();
        foreach (var (_, _, fileName) in sampleConfigs)
        {
            xmlFiles.Add(Path.Combine(configFolder, fileName));
        }

        // --------------------------------------------------------------------
        // 6. Import each XML configuration, generate the barcode, and save as PNG.
        // --------------------------------------------------------------------
        int index = 1;
        foreach (string xmlFile in xmlFiles)
        {
            if (!File.Exists(xmlFile))
            {
                Console.WriteLine($"Config file not found: {xmlFile}");
                continue;
            }

            try
            {
                using (var generator = BarcodeGenerator.ImportFromXml(xmlFile))
                {
                    string imagePath = Path.Combine(outputFolder, $"barcode_{index}.png");
                    generator.Save(imagePath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Generated barcode saved to: {imagePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to process '{xmlFile}': {ex.Message}");
            }

            index++;
        }

        // --------------------------------------------------------------------
        // 7. Optional cleanup: remove the temporary configuration folder.
        // --------------------------------------------------------------------
        // Directory.Delete(configFolder, true);
    }
}