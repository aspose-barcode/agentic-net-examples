using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates creating a sample barcode settings XML, loading it,
/// modifying some parameters, and generating a barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        const string xmlPath = "barcodeSettings.xml";
        const string outputImage = "generatedBarcode.png";

        // --------------------------------------------------------------------
        // Ensure a sample XML file exists. If it does not, create one with
        // default barcode settings and export it to XML.
        // --------------------------------------------------------------------
        if (!File.Exists(xmlPath))
        {
            // Create a generator with a default symbology and code text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Configure sample barcode parameters.
                generator.Parameters.Barcode.XDimension.Point = 2f;               // Width of the smallest bar (in points).
                generator.Parameters.Barcode.BarHeight.Point = 40f;              // Height of the barcode (in points).
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes; // Enable checksum calculation.
                generator.Parameters.Barcode.ChecksumAlwaysShow = true;          // Show checksum in the human‑readable text.

                // Export the configured settings to an XML file.
                generator.ExportToXml(xmlPath);
                Console.WriteLine($"Sample XML created at '{xmlPath}'.");
            }
        }

        // --------------------------------------------------------------------
        // Verify that the XML file now exists before attempting to load it.
        // --------------------------------------------------------------------
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine($"Error: XML file '{xmlPath}' not found.");
            return;
        }

        // --------------------------------------------------------------------
        // Import barcode settings from the XML file.
        // --------------------------------------------------------------------
        using (var generator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Display loaded properties (simulating a property editor view).
            Console.WriteLine("Loaded Barcode Settings:");
            Console.WriteLine($"  Symbology: {generator.BarcodeType.TypeName}");
            Console.WriteLine($"  CodeText : {generator.CodeText}");
            Console.WriteLine($"  XDimension (pt): {generator.Parameters.Barcode.XDimension.Point}");
            Console.WriteLine($"  BarHeight (pt) : {generator.Parameters.Barcode.BarHeight.Point}");
            Console.WriteLine($"  Checksum Enabled: {generator.Parameters.Barcode.IsChecksumEnabled}");
            Console.WriteLine($"  Show Checksum: {generator.Parameters.Barcode.ChecksumAlwaysShow}");

            // ----------------------------------------------------------------
            // Modify a couple of properties to demonstrate editing capabilities.
            // ----------------------------------------------------------------
            generator.Parameters.Barcode.XDimension.Point = 3f;   // Increase bar width.
            generator.Parameters.Barcode.BarHeight.Point = 50f;  // Increase barcode height.

            // Save the barcode image using the updated settings.
            generator.Save(outputImage);
            Console.WriteLine($"Barcode image saved to '{outputImage}'.");
        }
    }
}