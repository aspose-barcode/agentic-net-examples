// Title: Export barcode generator configurations to XML files
// Description: Demonstrates how to use Aspose.BarCode's ExportToXml method to create XML configuration files for various barcode symbologies, which can be checked into version control.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category. It shows how to configure barcode generators (BarcodeGenerator, EncodeTypes) for different standards such as Code128, QR, DataMatrix, AustraliaPost, and GS1 Composite Bar, and export their settings to XML using ExportToXml. Developers often need to store barcode settings in source control to ensure consistent generation across environments.
// Prompt: Use ExportToXml to generate configuration files for different barcode standards and store them in version control.
// Tags: barcode, export, xml, configuration, code128, qr, datamatrix, australiapost, gs1composite, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates XML configuration files for multiple barcode types
/// using Aspose.BarCode's ExportToXml method.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Initiates the configuration export process.
    /// </summary>
    static void Main()
    {
        GenerateBarcodeConfigurations();
    }

    /// <summary>
    /// Creates a temporary folder, configures several barcode generators,
    /// and exports each generator's settings to an XML file.
    /// </summary>
    private static void GenerateBarcodeConfigurations()
    {
        // Create a unique temporary directory to store the exported XML files
        string configFolder = Path.Combine(Path.GetTempPath(), "BarcodeConfigs_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(configFolder);
        Console.WriteLine("Exported configuration files will be stored in: " + configFolder);

        // 1. Code128 configuration
        using (var code128Gen = new BarcodeGenerator(EncodeTypes.Code128, "ABC123"))
        {
            // Set visual appearance
            code128Gen.Parameters.Barcode.BarColor = Color.Black;
            code128Gen.Parameters.BackColor = Color.White;

            // Export settings to XML
            string filePath = Path.Combine(configFolder, "Code128Config.xml");
            code128Gen.ExportToXml(filePath);
        }

        // 2. QR configuration (with specific version and error level)
        using (var qrGen = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Configure QR version and error correction level
            qrGen.Parameters.Barcode.QR.Version = QRVersion.Version05;
            qrGen.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
            qrGen.Parameters.Barcode.BarColor = Color.DarkBlue;

            // Export settings to XML
            string filePath = Path.Combine(configFolder, "QRConfig.xml");
            qrGen.ExportToXml(filePath);
        }

        // 3. DataMatrix configuration (rectangular ECC200 32x32)
        using (var dmGen = new BarcodeGenerator(EncodeTypes.DataMatrix, "DM12345"))
        {
            // Set DataMatrix version and ECC type
            dmGen.Parameters.Barcode.DataMatrix.Version = DataMatrixVersion.ECC200_32x32;
            dmGen.Parameters.Barcode.DataMatrix.EccType = DataMatrixEccType.Ecc200;
            dmGen.Parameters.Barcode.BarColor = Color.DarkGreen;

            // Export settings to XML
            string filePath = Path.Combine(configFolder, "DataMatrixConfig.xml");
            dmGen.ExportToXml(filePath);
        }

        // 4. AustraliaPost configuration (using CTable encoding)
        using (var auPostGen = new BarcodeGenerator(EncodeTypes.AustraliaPost, "1100000000"))
        {
            // Use CTable encoding for Australian Post barcodes
            auPostGen.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;
            auPostGen.Parameters.Barcode.BarColor = Color.Maroon;

            // Export settings to XML
            string filePath = Path.Combine(configFolder, "AustraliaPostConfig.xml");
            auPostGen.ExportToXml(filePath);
        }

        // 5. GS1 Composite Bar configuration
        using (var gs1CompGen = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, "(01)01234567890123|HelloWorld"))
        {
            // Configure linear and 2D components
            gs1CompGen.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
            gs1CompGen.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_C;
            gs1CompGen.Parameters.Barcode.Pdf417.Columns = 30;
            gs1CompGen.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;
            gs1CompGen.Parameters.Barcode.BarColor = Color.Purple;

            // Export settings to XML
            string filePath = Path.Combine(configFolder, "GS1CompositeBarConfig.xml");
            gs1CompGen.ExportToXml(filePath);
        }

        Console.WriteLine("Configuration export completed.");
    }
}