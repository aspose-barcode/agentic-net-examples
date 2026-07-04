// Title: Export barcode configurations to XML files
// Description: Demonstrates using ExportToXml to create XML configuration files for various barcode symbologies, useful for version‑controlled settings.
// Prompt: Use ExportToXml to generate configuration files for different barcode standards and store them in version control.
// Tags: barcode symbology, export, xml, configuration, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates XML configuration files for multiple barcode symbologies using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a folder, iterates over barcode definitions, applies specific settings, and exports each configuration to XML.
    /// </summary>
    static void Main()
    {
        // Define the directory where XML configuration files will be saved
        string configDir = Path.Combine(Directory.GetCurrentDirectory(), "Configs");
        if (!Directory.Exists(configDir))
        {
            // Create the directory if it does not already exist
            Directory.CreateDirectory(configDir);
        }

        // List of barcode specifications: symbology type, sample code text, and target XML file name
        var barcodeInfos = new (BaseEncodeType type, string codeText, string fileName)[]
        {
            (EncodeTypes.Code128, "1234567890", "Code128Config.xml"),
            (EncodeTypes.QR, "Hello QR", "QRConfig.xml"),
            (EncodeTypes.DataMatrix, "DMTest", "DataMatrixConfig.xml"),
            (EncodeTypes.AustraliaPost, "5912345678ABCde", "AustraliaPostConfig.xml")
        };

        // Process each barcode definition
        foreach (var info in barcodeInfos)
        {
            // Initialize a generator for the specified symbology and code text
            using (var generator = new BarcodeGenerator(info.type, info.codeText))
            {
                // Apply symbology‑specific parameters when required
                if (info.type == EncodeTypes.QR)
                {
                    // Set a high error correction level for QR codes
                    generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
                }
                else if (info.type == EncodeTypes.AustraliaPost)
                {
                    // Use CTable encoding for Australian Post barcodes (customer information)
                    generator.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = CustomerInformationInterpretingType.CTable;
                }

                // Build the full path for the XML output file
                string xmlPath = Path.Combine(configDir, info.fileName);

                // Export the generator's configuration to an XML file
                bool success = generator.ExportToXml(xmlPath);

                // Report the result of the export operation
                Console.WriteLine($"{info.fileName}: Export {(success ? "succeeded" : "failed")}");
            }
        }

        // Indicate that all exports have completed
        Console.WriteLine("Barcode configuration export completed.");
    }
}