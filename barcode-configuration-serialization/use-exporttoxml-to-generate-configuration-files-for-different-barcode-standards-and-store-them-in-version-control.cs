// Title: Export barcode generator configurations to XML files
// Description: Demonstrates how to use Aspose.BarCode's ExportToXml method to create XML configuration files for various barcode symbologies, useful for version‑controlled settings.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, showcasing the use of BarcodeGenerator and its Parameters API to define visual and encoding options, then persisting them with ExportToXml. Developers often need to store barcode settings in source control to ensure consistent generation across environments and CI pipelines.
// Prompt: Use ExportToXml to generate configuration files for different barcode standards and store them in version control.
// Tags: barcode symbology, export, xml, configuration, aspose.barcode, generator, version control

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing;

/// <summary>
/// Generates barcode configuration XML files for multiple symbologies using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates an output folder, defines barcode configurations,
    /// exports each configuration to XML, and reports the result.
    /// </summary>
    static void Main()
    {
        // Ensure the output directory exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "BarcodeConfigs");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Define a set of barcode configurations to export
        var configs = new (BaseEncodeType EncodeType, string CodeText, string FileName)[]
        {
            (EncodeTypes.Code128, "ABC123456", "Code128Config.xml"),
            (EncodeTypes.QR, "https://example.com", "QRConfig.xml"),
            (EncodeTypes.DataMatrix, "DataMatrixSample", "DataMatrixConfig.xml"),
            (EncodeTypes.AustraliaPost, "1100000000", "AustraliaPostConfig.xml"),
            (EncodeTypes.OneCode, "12345678901234567890", "OneCodeConfig.xml")
        };

        // Export each configuration to an XML file
        foreach (var cfg in configs)
        {
            ExportBarcodeConfiguration(cfg.EncodeType, cfg.CodeText, Path.Combine(outputDir, cfg.FileName));
        }

        Console.WriteLine("Barcode configuration XML files have been generated in: " + outputDir);
    }

    /// <summary>
    /// Creates a <see cref="BarcodeGenerator"/> with the specified encoding type and text,
    /// applies optional visual settings, and exports the configuration to an XML file.
    /// </summary>
    /// <param name="encodeType">The barcode symbology to use.</param>
    /// <param name="codeText">The data to encode.</param>
    /// <param name="xmlPath">The full path where the XML configuration will be saved.</param>
    static void ExportBarcodeConfiguration(BaseEncodeType encodeType, string codeText, string xmlPath)
    {
        // Initialize the generator with the desired symbology and data
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Optional visual settings
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;
            generator.Parameters.Barcode.XDimension.Point = 2f; // module size
            generator.Parameters.Barcode.Padding.Left.Point = 5f;
            generator.Parameters.Barcode.Padding.Top.Point = 5f;
            generator.Parameters.Barcode.Padding.Right.Point = 5f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 5f;

            // Export the generator's settings to an XML file
            bool success = generator.ExportToXml(xmlPath);
            if (!success)
            {
                Console.WriteLine($"Failed to export configuration for {encodeType.TypeName} to {xmlPath}");
            }
        }
    }
}