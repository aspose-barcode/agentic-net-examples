using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates exporting a barcode generator's settings to XML,
/// importing them back, and saving the resulting barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create a barcode generator with Code128 symbology and sample data.
        using (var originalGenerator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set a custom bar color (blue) for the generated barcode.
            originalGenerator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;

            // Export the generator's configuration to an in‑memory XML stream.
            using (var xmlStream = new MemoryStream())
            {
                originalGenerator.ExportToXml(xmlStream);

                // Rewind the stream to the beginning so it can be read.
                xmlStream.Position = 0;

                // Import a new BarcodeGenerator instance from the XML configuration.
                using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlStream))
                {
                    // Define the output file path for the barcode image.
                    string outputPath = "imported_barcode.png";

                    // Save the barcode image using the imported settings.
                    importedGenerator.Save(outputPath);

                    // Inform the user where the image was saved.
                    Console.WriteLine($"Barcode image saved to: {outputPath}");
                }
            }
        }
    }
}