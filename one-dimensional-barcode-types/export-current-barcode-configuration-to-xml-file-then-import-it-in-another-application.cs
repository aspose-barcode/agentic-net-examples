using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates exporting and importing barcode generator configuration using XML,
/// and generating a barcode image with the imported settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Exports a barcode configuration to XML, then imports it to generate a barcode image.
    /// </summary>
    static void Main()
    {
        // Define file paths for the XML configuration and the output barcode image.
        string xmlPath = "barcodeConfig.xml";
        string imagePath = "barcode.png";

        // Create a barcode generator with Code128 symbology and sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Configure barcode appearance and resolution.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes; // Enable checksum.
            generator.Parameters.Barcode.BarColor = Color.Blue;                 // Set barcode color.
            generator.Parameters.Resolution = 300f;                             // Set image resolution (dpi).

            // Export the current generator configuration to an XML file.
            bool exportSuccess = generator.ExportToXml(xmlPath);
            Console.WriteLine($"Export to XML {(exportSuccess ? "succeeded" : "failed")} at '{Path.GetFullPath(xmlPath)}'.");

            // The generator instance will be disposed automatically at the end of this block.
        }

        // Create a new generator by importing the previously saved XML configuration.
        using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Save the barcode image using the imported configuration.
            importedGenerator.Save(imagePath);
            Console.WriteLine($"Barcode image saved to '{Path.GetFullPath(imagePath)}' using imported configuration.");
        }
    }
}