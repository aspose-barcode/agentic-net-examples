// Title: Export barcode configurations to XML using Append mode
// Description: Demonstrates how to export multiple Aspose.BarCode generator configurations to a single XML file by appending each snapshot, and then import the first configuration back.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, illustrating the use of BarcodeGenerator.ExportToXml and BarcodeGenerator.ImportFromXml for persisting and reusing barcode settings. Developers often need to store generator parameters, share them across services, or version‑control configurations; the key API classes involved are BarcodeGenerator, its Parameters property, and the XML import/export methods.
// Prompt: Use ExportToXml with a FileStream opened in Append mode to concatenate multiple configuration snapshots.
// Tags: barcode symbology, export, xml, configuration, aspose.barcode, fileio

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Shows how to concatenate multiple barcode generator configurations into a single XML file
/// and later import a configuration to generate a barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Exports two barcode configurations to an XML file,
    /// then imports the first configuration and saves the resulting barcode image.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Create the first barcode generator (Code128) and modify a setting
        // ------------------------------------------------------------
        using (var generator1 = new BarcodeGenerator(EncodeTypes.Code128, "FirstSample"))
        {
            // Change the X-dimension (module width) to 2 points
            generator1.Parameters.Barcode.XDimension.Point = 2f;

            // Append the generator's configuration to the XML file
            using (var appendStream = new FileStream("barcodeConfigs.xml", FileMode.Append, FileAccess.Write, FileShare.Read))
            {
                generator1.ExportToXml(appendStream);
            }
        }

        // ------------------------------------------------------------
        // Create the second barcode generator (QR) and set error correction level
        // ------------------------------------------------------------
        using (var generator2 = new BarcodeGenerator(EncodeTypes.QR, "SecondSample"))
        {
            // Set QR error correction to the highest level (Level H)
            generator2.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Append the second configuration to the same XML file
            using (var appendStream = new FileStream("barcodeConfigs.xml", FileMode.Append, FileAccess.Write, FileShare.Read))
            {
                generator2.ExportToXml(appendStream);
            }
        }

        // ------------------------------------------------------------
        // Load the first configuration from the concatenated XML file
        // ------------------------------------------------------------
        using (var readStream = new FileStream("barcodeConfigs.xml", FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            // ImportFromXml reads the first XML document found in the stream
            using (var importedGenerator = BarcodeGenerator.ImportFromXml(readStream))
            {
                // Generate and save the barcode image based on the imported settings
                importedGenerator.Save("importedBarcode.png");
                Console.WriteLine("Imported barcode saved as importedBarcode.png");
            }
        }
    }
}