// Title: Export Barcode Configurations to XML for Multiple Symbologies
// Description: Demonstrates how to generate barcode configuration XML files and sample PNG images for various barcode standards using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the ExportToXml API to persist barcode settings. It covers common configuration steps, symbology‑specific tweaks, and image generation, helping developers automate version‑controlled barcode definitions for QR, Code128, DataMatrix, PDF417, GS1 Composite, and Australia Post.
// Prompt: Use ExportToXml to generate configuration files for different barcode standards and store them in version control.
// Tags: barcode, export, xml, configuration, aspose.barcode, qrcode, code128, datamatrix, pdf417, gs1composite, australiapost

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates exporting barcode generation settings to XML files for several symbologies.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates output directory, generates XML configs and sample images for each barcode type.
    /// </summary>
    static void Main()
    {
        // Prepare output folder for generated configuration files and sample images
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "GeneratedConfigs");
        Directory.CreateDirectory(outputDir);

        // QR Code configuration
        GenerateBarcodeConfig(
            outputDir,
            EncodeTypes.QR,
            "https://example.com",
            "QR_Code");

        // Code128 configuration
        GenerateBarcodeConfig(
            outputDir,
            EncodeTypes.Code128,
            "1234567890",
            "Code128");

        // DataMatrix configuration
        GenerateBarcodeConfig(
            outputDir,
            EncodeTypes.DataMatrix,
            "DataMatrixTest",
            "DataMatrix");

        // PDF417 configuration
        GenerateBarcodeConfig(
            outputDir,
            EncodeTypes.Pdf417,
            "PDF417 Sample Text",
            "PDF417");

        // GS1 Composite Bar (linear|2D) configuration
        GenerateBarcodeConfig(
            outputDir,
            EncodeTypes.GS1CompositeBar,
            "(01)12345678901231|(21)ABC123",
            "GS1CompositeBar");

        // Australia Post configuration (symbology‑specific settings)
        GenerateAustraliaPostConfig(
            outputDir,
            "1100000000",
            "AustraliaPost");

        // Inform the user where files have been written
        Console.WriteLine("Barcode configuration files and sample images have been generated in:");
        Console.WriteLine(outputDir);
    }

    /// <summary>
    /// Generates an XML configuration file and a sample PNG image for a given barcode type.
    /// </summary>
    /// <param name="folder">Target folder for the output files.</param>
    /// <param name="encodeType">Barcode symbology to generate.</param>
    /// <param name="codeText">Data to encode in the barcode.</param>
    /// <param name="fileBaseName">Base name for the generated files (without extension).</param>
    static void GenerateBarcodeConfig(string folder, BaseEncodeType encodeType, string codeText, string fileBaseName)
    {
        string xmlPath = Path.Combine(folder, fileBaseName + ".xml");
        string imgPath = Path.Combine(folder, fileBaseName + ".png");

        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Common visual settings
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Persist the current generation state to an XML file
            generator.ExportToXml(xmlPath);

            // Create a PNG image for quick visual verification
            generator.Save(imgPath, BarCodeImageFormat.Png);
        }
    }

    /// <summary>
    /// Generates an XML configuration file and a sample PNG image for the Australia Post symbology,
    /// applying its specific encoding table setting.
    /// </summary>
    /// <param name="folder">Target folder for the output files.</param>
    /// <param name="codeText">Data to encode in the barcode.</param>
    /// <param name="fileBaseName">Base name for the generated files (without extension).</param>
    static void GenerateAustraliaPostConfig(string folder, string codeText, string fileBaseName)
    {
        string xmlPath = Path.Combine(folder, fileBaseName + ".xml");
        string imgPath = Path.Combine(folder, fileBaseName + ".png");

        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            // Australia Post specific setting: use the C table for encoding
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;

            // Common visual settings
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Persist the current generation state to an XML file
            generator.ExportToXml(xmlPath);

            // Create a PNG image for quick visual verification
            generator.Save(imgPath, BarCodeImageFormat.Png);
        }
    }
}