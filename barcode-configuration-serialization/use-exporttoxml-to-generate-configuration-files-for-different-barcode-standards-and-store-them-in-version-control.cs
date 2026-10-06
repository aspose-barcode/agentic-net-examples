// Title: Export barcode generation settings to XML for multiple symbologies
// Description: Demonstrates how to generate QR, Code128, Pdf417, and DataMatrix barcodes, export their configuration to XML files, and save PNG images.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, ExportToXml, and image saving. Developers often need to persist barcode generation settings for version control, automated testing, or reuse across projects. The snippet illustrates typical workflows for creating configuration files and visual assets for various barcode standards.
/// Prompt: Use ExportToXml to generate configuration files for different barcode standards and store them in version control.
/// Tags: barcode, symbology, export, xml, configuration, aspose.barcode, generation, qr, code128, pdf417, datamatrix, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates exporting barcode generation configurations to XML and saving barcode images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary folder, generates barcodes, exports configs, and saves images.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory to store generated XML and PNG files
        string basePath = Path.Combine(Path.GetTempPath(), "BarcodeConfigs_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(basePath);

        // Generate and export configurations for various barcode symbologies
        GenerateBarcode(basePath, "QR", EncodeTypes.QR, "Sample QR");
        GenerateBarcode(basePath, "Code128", EncodeTypes.Code128, "Sample128");
        GenerateBarcode(basePath, "Pdf417", EncodeTypes.Pdf417, "Sample Pdf417");
        GenerateBarcode(basePath, "DataMatrix", EncodeTypes.DataMatrix, "Sample DM");

        Console.WriteLine("Barcode generation and export completed.");
    }

    /// <summary>
    /// Generates a barcode, exports its configuration to an XML file, and saves the barcode image as PNG.
    /// </summary>
    /// <param name="folder">The folder where XML and PNG files will be saved.</param>
    /// <param name="name">Base name for the output files (without extension).</param>
    /// <param name="encodeType">The barcode symbology to use.</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    static void GenerateBarcode(string folder, string name, BaseEncodeType encodeType, string codeText)
    {
        // Determine file paths for XML configuration and PNG image
        string xmlPath = Path.Combine(folder, name + ".xml");
        string pngPath = Path.Combine(folder, name + ".png");

        // Initialize the barcode generator with the specified symbology and text
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Common visual settings
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Symbology‑specific tweaks (optional)
            if (encodeType == EncodeTypes.Pdf417)
            {
                generator.Parameters.Barcode.Pdf417.Columns = 4;
            }

            // Export the generator's configuration to an XML file
            generator.ExportToXml(xmlPath);

            // Save the generated barcode as a PNG image
            generator.Save(pngPath, BarCodeImageFormat.Png);
        }

        // Output the locations of the generated files
        Console.WriteLine($"{name} XML: {xmlPath}");
        Console.WriteLine($"{name} PNG: {pngPath}");
    }
}