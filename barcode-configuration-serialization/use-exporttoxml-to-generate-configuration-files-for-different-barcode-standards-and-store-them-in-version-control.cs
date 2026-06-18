using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates exporting various barcode configurations to XML files using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcode configurations and saves them as XML files.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Determine the output directory for the generated XML configuration files.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "BarcodeConfigs");
        // Ensure the directory exists.
        Directory.CreateDirectory(outputDir);

        // Define a collection of barcode configurations to export.
        // Each tuple contains:
        //   - The barcode type (BaseEncodeType)
        //   - The code text to encode
        //   - An optional configuration action to customize generator parameters.
        var barcodeConfigs = new (BaseEncodeType Type, string CodeText, Action<BarcodeGenerator> Configure)[]
        {
            // Code128 with checksum enabled.
            (EncodeTypes.Code128, "ABC123456", gen =>
            {
                gen.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            }),

            // QR code with high error correction level.
            (EncodeTypes.QR, "https://example.com", gen =>
            {
                gen.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
            }),

            // DataMatrix with a specific version.
            (EncodeTypes.DataMatrix, "DataMatrixSample", gen =>
            {
                gen.Parameters.Barcode.DataMatrix.DataMatrixVersion = DataMatrixVersion.ECC200_20x20;
            }),

            // PDF417 using default settings.
            (EncodeTypes.Pdf417, "PDF417 Sample Text", gen => { }),

            // AustraliaPost with CTable interpreting type.
            (EncodeTypes.AustraliaPost, "5912345678ABCde", gen =>
            {
                gen.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = CustomerInformationInterpretingType.CTable;
            })
        };

        // Iterate over each configuration, generate the barcode, and export its settings to XML.
        foreach (var cfg in barcodeConfigs)
        {
            // Create a new BarcodeGenerator for the specified type and code text.
            using (var generator = new BarcodeGenerator(cfg.Type, cfg.CodeText))
            {
                // Apply any custom configuration actions, if provided.
                cfg.Configure?.Invoke(generator);

                // Build the output file name and path.
                string fileName = $"{cfg.Type.TypeName}_config.xml";
                string filePath = Path.Combine(outputDir, fileName);

                // Export the generator's configuration to an XML file.
                generator.ExportToXml(filePath);

                // Inform the user about the successful export.
                Console.WriteLine($"Exported {cfg.Type.TypeName} configuration to: {filePath}");
            }
        }

        // Final status message.
        Console.WriteLine("All barcode configuration files have been generated.");
    }
}