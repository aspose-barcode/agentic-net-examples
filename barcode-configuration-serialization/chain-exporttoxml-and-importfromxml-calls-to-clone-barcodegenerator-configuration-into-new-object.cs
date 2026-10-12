// Title: Clone BarcodeGenerator Configuration Using ExportToXml and ImportFromXml
// Description: Demonstrates how to duplicate a BarcodeGenerator's settings by exporting its configuration to XML and importing it into a new instance, then saving both barcodes as PNG files.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, illustrating the use of ExportToXml and ImportFromXml methods to clone barcode generator settings. It showcases key API classes such as BarcodeGenerator, EncodeTypes, and BarCodeImageFormat, which developers commonly use for generating, customizing, and persisting barcodes in various formats.
// Prompt: Chain ExportToXml and ImportFromXml calls to clone a BarcodeGenerator configuration into a new object.
// Tags: barcode, cloning, exporttoxml, importfromxml, qrcode, png, aspose.barcode, configuration

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates cloning a BarcodeGenerator configuration using XML export/import.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates an original QR code, clones its settings via XML, and saves both images.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output directory for the generated images
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeCloneDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Create the original barcode generator with sample QR code settings
        using (var originalGenerator = new BarcodeGenerator(EncodeTypes.QR, "SampleText"))
        {
            // Customize barcode appearance
            originalGenerator.Parameters.Barcode.XDimension.Pixels = 4f;
            originalGenerator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica";
            originalGenerator.Parameters.Barcode.CodeTextParameters.Font.Size.Pixels = 12f;

            // Export the generator's configuration to an in‑memory XML stream
            using (var xmlStream = new MemoryStream())
            {
                originalGenerator.ExportToXml(xmlStream);
                xmlStream.Position = 0; // Reset stream position for reading

                // Import the XML configuration into a new generator instance (clone)
                using (var clonedGenerator = BarcodeGenerator.ImportFromXml(xmlStream))
                {
                    // Define file paths for the original and cloned barcode images
                    string originalPath = Path.Combine(outputDir, "original.png");
                    string clonedPath = Path.Combine(outputDir, "cloned.png");

                    // Save both barcodes as PNG files
                    originalGenerator.Save(originalPath, BarCodeImageFormat.Png);
                    clonedGenerator.Save(clonedPath, BarCodeImageFormat.Png);

                    // Output the locations of the saved images
                    Console.WriteLine($"Original barcode saved to: {originalPath}");
                    Console.WriteLine($"Cloned barcode saved to: {clonedPath}");
                }
            }
        }
    }
}