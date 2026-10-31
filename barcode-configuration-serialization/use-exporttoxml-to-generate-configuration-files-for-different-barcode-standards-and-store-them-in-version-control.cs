// Title: Export barcode configurations to XML for version control
// Description: Demonstrates how to generate barcodes of various symbologies, export their configuration to XML files, and save PNG images.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, showcasing the use of BarcodeGenerator, ExportToXml, and image saving APIs. Developers often need to store barcode generation settings in source control to ensure reproducible builds and easy updates across environments.
// Prompt: Use ExportToXml to generate configuration files for different barcode standards and store them in version control.
// Tags: barcode, configuration, exporttoxml, png, qrcode, datamatrix, code128, pdf417, gs1composite, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates exporting barcode generation settings to XML files and saving barcode images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes for several symbologies, saves PNG images, and exports configuration XML files.
    /// </summary>
    static void Main()
    {
        // Define base directory for all generated configuration files and images
        string basePath = Path.Combine(Directory.GetCurrentDirectory(), "BarcodeConfigs");
        Directory.CreateDirectory(basePath);

        // QR Code configuration and export
        GenerateAndExport(
            Path.Combine(basePath, "qr"),
            EncodeTypes.QR,
            "QR Sample",
            gen =>
            {
                gen.Parameters.Barcode.XDimension.Pixels = 3f;
                gen.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
                gen.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            });

        // DataMatrix configuration and export
        GenerateAndExport(
            Path.Combine(basePath, "datamatrix"),
            EncodeTypes.DataMatrix,
            "DM12345",
            gen =>
            {
                gen.Parameters.Barcode.XDimension.Pixels = 2f;
                gen.Parameters.Barcode.DataMatrix.Version = DataMatrixVersion.ECC200_32x32;
                gen.Parameters.Barcode.DataMatrix.EccType = DataMatrixEccType.Ecc200;
                gen.Parameters.Barcode.BarColor = Aspose.Drawing.Color.DarkBlue;
            });

        // Code128 configuration and export
        GenerateAndExport(
            Path.Combine(basePath, "code128"),
            EncodeTypes.Code128,
            "CODE128-TEST",
            gen =>
            {
                gen.Parameters.Barcode.XDimension.Pixels = 2f;
                gen.Parameters.Barcode.BarHeight.Point = 50f;
                gen.Parameters.Barcode.BarColor = Aspose.Drawing.Color.DarkGreen;
            });

        // PDF417 configuration and export
        GenerateAndExport(
            Path.Combine(basePath, "pdf417"),
            EncodeTypes.Pdf417,
            "PDF417 Sample Text",
            gen =>
            {
                gen.Parameters.Barcode.XDimension.Pixels = 2f;
                gen.Parameters.Barcode.Pdf417.Columns = 5;
                gen.Parameters.Barcode.Pdf417.IsCode128Emulation = true;
                gen.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Purple;
            });

        // GS1 Composite Bar (linear + 2D) configuration and export
        GenerateAndExport(
            Path.Combine(basePath, "gs1composite"),
            EncodeTypes.GS1CompositeBar,
            "(01)01234567890128|HelloWorld",
            gen =>
            {
                gen.Parameters.Barcode.XDimension.Pixels = 2f;
                gen.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
                gen.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_C;
                gen.Parameters.Barcode.Pdf417.Columns = 30;
                gen.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;
                gen.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Brown;
            });

        Console.WriteLine("Barcode configuration files and images have been generated.");
    }

    /// <summary>
    /// Generates a barcode image, saves it as PNG, and exports the generator's configuration to an XML file.
    /// </summary>
    /// <param name="baseFilePath">Base file path without extension; .png and .xml will be appended.</param>
    /// <param name="encodeType">The barcode symbology to generate.</param>
    /// <param name="codeText">The text/value encoded in the barcode.</param>
    /// <param name="configure">An action that customizes the BarcodeGenerator settings.</param>
    static void GenerateAndExport(string baseFilePath, BaseEncodeType encodeType, string codeText, Action<BarcodeGenerator> configure)
    {
        // Determine full file paths for image and XML
        string imagePath = baseFilePath + ".png";
        string xmlPath = baseFilePath + ".xml";

        // Create and configure the barcode generator
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            configure?.Invoke(generator);

            // Save the generated barcode as a PNG image
            generator.Save(imagePath, BarCodeImageFormat.Png);

            // Export the generator's configuration to an XML file for version control
            generator.ExportToXml(xmlPath);
        }
    }
}