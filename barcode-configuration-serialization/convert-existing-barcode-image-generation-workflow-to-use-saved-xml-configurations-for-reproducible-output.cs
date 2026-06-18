using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates creating a sample barcode configuration XML and generating a barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define file paths for the XML configuration and the output barcode image.
        string xmlConfigPath = "barcodeConfig.xml";
        string outputImagePath = "barcode.png";

        // If the XML configuration file does not exist, create a sample configuration.
        if (!File.Exists(xmlConfigPath))
        {
            // Initialize a barcode generator with QR encoding and sample text.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
            {
                // Set generation parameters: auto-size mode, image dimensions, error correction level, resolution, and font.
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 300f;
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
                generator.Parameters.Resolution = 300f;
                generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
                generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

                // Export the current configuration to an XML file for later reuse.
                generator.ExportToXml(xmlConfigPath);
                Console.WriteLine($"Sample XML configuration created at '{xmlConfigPath}'.");
            }
        }

        // Load the barcode generator from the saved XML configuration.
        using (var generator = BarcodeGenerator.ImportFromXml(xmlConfigPath))
        {
            // Optionally override the CodeText after loading.
            // generator.CodeText = "New Text";

            // Save the generated barcode image to the specified path.
            generator.Save(outputImagePath);
            Console.WriteLine($"Barcode image generated and saved to '{outputImagePath}'.");
        }
    }
}