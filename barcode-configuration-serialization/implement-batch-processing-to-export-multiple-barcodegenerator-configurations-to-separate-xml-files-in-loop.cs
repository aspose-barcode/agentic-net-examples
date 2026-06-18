using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch export of barcode generator configurations to XML files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcode configurations and exports them as XML.
    /// </summary>
    static void Main()
    {
        // Define a set of barcode configurations to export.
        var configs = new (BaseEncodeType Type, string CodeText, string FileName)[]
        {
            (EncodeTypes.Code128, "ABC123456", "code128.xml"),
            (EncodeTypes.QR, "https://example.com", "qr.xml"),
            (EncodeTypes.DataMatrix, "DataMatrixSample", "datamatrix.xml"),
            (EncodeTypes.Pdf417, "PDF417 Sample Text", "pdf417.xml")
        };

        // Ensure the output directory exists.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "BarcodesXml");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Process each configuration.
        foreach (var cfg in configs)
        {
            // Create a BarcodeGenerator with the specified type and code text.
            using (var generator = new BarcodeGenerator(cfg.Type, cfg.CodeText))
            {
                // Set common barcode parameters.
                generator.Parameters.Barcode.XDimension.Point = 2f;      // Module size.
                generator.Parameters.Barcode.BarHeight.Point = 40f;    // Bar height for 1D barcodes.
                generator.Parameters.Resolution = 300f;                // Image resolution.

                // Export the generator settings to an XML file.
                string xmlPath = Path.Combine(outputDir, cfg.FileName);
                generator.ExportToXml(xmlPath);

                // Inform the user about the successful export.
                Console.WriteLine($"Exported {cfg.Type.TypeName} configuration to {xmlPath}");
            }
        }

        // Indicate that the batch export process has finished.
        Console.WriteLine("Batch export completed.");
    }
}