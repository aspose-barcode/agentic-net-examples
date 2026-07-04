// Title: Load Barcode Settings from XML and Display Properties
// Description: Demonstrates loading barcode configuration from an XML file using Aspose.BarCode, then showing the settings and generating an image.
// Prompt: Design a UI component that loads barcode settings from an XML file and populates property editors.
// Tags: barcode symbology, import, xml, property editors, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that imports barcode settings from an XML file,
/// displays key properties (simulating property editors), and generates an image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Loads barcode settings from an XML file, prints them, optionally modifies a property,
    /// and saves the resulting barcode image.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument can be the XML file path.</param>
    static void Main(string[] args)
    {
        // Determine XML file path (argument or default)
        string xmlPath = args.Length > 0 ? args[0] : "barcodeSettings.xml";

        // Verify that the XML file exists before attempting import
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load barcode settings from the specified XML file
        using (var generator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Display key properties (simulating property editors)
            Console.WriteLine("=== Loaded Barcode Settings ===");
            Console.WriteLine($"Symbology          : {generator.BarcodeType.TypeName}");
            Console.WriteLine($"CodeText           : {generator.CodeText}");
            Console.WriteLine($"Bar Color (ARGB)   : {generator.Parameters.Barcode.BarColor.ToArgb()}");
            Console.WriteLine($"Background Color   : {generator.Parameters.BackColor.ToArgb()}");
            Console.WriteLine($"Bar Height (pt)    : {generator.Parameters.Barcode.BarHeight.Point} pt");
            Console.WriteLine($"X Dimension (pt)   : {generator.Parameters.Barcode.XDimension.Point} pt");
            Console.WriteLine($"Image Width (pt)   : {generator.Parameters.ImageWidth.Point} pt");
            Console.WriteLine($"Image Height (pt)  : {generator.Parameters.ImageHeight.Point} pt");
            Console.WriteLine($"Resolution (dpi)   : {generator.Parameters.Resolution}");
            Console.WriteLine($"AutoSizeMode       : {generator.Parameters.AutoSizeMode}");
            Console.WriteLine($"CodeText Alignment : {generator.Parameters.Barcode.CodeTextParameters.Alignment}");
            Console.WriteLine($"CodeText Location  : {generator.Parameters.Barcode.CodeTextParameters.Location}");
            Console.WriteLine($"Padding (pt) Left  : {generator.Parameters.Barcode.Padding.Left.Point} pt");
            Console.WriteLine($"Padding (pt) Top   : {generator.Parameters.Barcode.Padding.Top.Point} pt");
            Console.WriteLine($"Padding (pt) Right : {generator.Parameters.Barcode.Padding.Right.Point} pt");
            Console.WriteLine($"Padding (pt) Bottom: {generator.Parameters.Barcode.Padding.Bottom.Point} pt");

            // Example: modify a property (bar color) and save to a new image (optional)
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;
            string outputImage = "generated_from_xml.png";
            generator.Save(outputImage);
            Console.WriteLine($"Generated barcode saved to: {outputImage}");
        }
    }
}