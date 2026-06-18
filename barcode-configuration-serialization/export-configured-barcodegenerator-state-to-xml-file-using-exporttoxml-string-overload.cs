using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates exporting a barcode configuration to an XML file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Code128 barcode, configures its appearance,
    /// and exports the configuration to an XML file.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the XML file in the current working directory.
        string xmlPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode_config.xml");

        // Initialize a BarcodeGenerator for Code128 with the sample text "Sample123".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Enable checksum calculation and always display it.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;

            // Set visual appearance: blue barcode on a white background.
            generator.Parameters.Barcode.BarColor = Color.Blue;
            generator.Parameters.BackColor = Color.White;

            // Define the output resolution (dots per inch).
            generator.Parameters.Resolution = 300f;

            // Export the configured barcode generator state to the specified XML file.
            bool success = generator.ExportToXml(xmlPath);

            // Inform the user whether the export succeeded.
            Console.WriteLine(success
                ? $"Barcode configuration exported successfully to '{xmlPath}'."
                : $"Failed to export barcode configuration to '{xmlPath}'.");
        }
    }
}