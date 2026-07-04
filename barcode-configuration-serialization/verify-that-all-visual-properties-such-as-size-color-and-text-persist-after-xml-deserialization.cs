// Title: Barcode visual properties persistence after XML deserialization
// Description: Demonstrates creating a barcode with specific visual settings, exporting to XML, importing back, and verifying that size, colors, text, and padding remain unchanged.
// Prompt: Verify that all visual properties such as size, color, and text persist after XML deserialization.
// Tags: barcode, code128, xml, serialization, visual properties, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a barcode, saves its configuration to XML,
/// reloads it, and checks that visual properties are preserved.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Executes the barcode creation, XML export/import, and verification steps.
    /// </summary>
    static void Main()
    {
        // Define file paths for the XML configuration and optional PNG image.
        string xmlPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.xml");
        string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // --------------------------------------------------------------------
        // Create a barcode generator and configure its visual appearance.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            // Set foreground (barcode) and background colors.
            generator.Parameters.Barcode.BarColor = Color.Blue;
            generator.Parameters.BackColor = Color.Yellow;

            // Configure image size. AutoSizeMode.Interpolation uses the explicit dimensions.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Define human‑readable text (code text) appearance.
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

            // Apply uniform padding around the barcode.
            generator.Parameters.Barcode.Padding.Left.Point = 5f;
            generator.Parameters.Barcode.Padding.Top.Point = 5f;
            generator.Parameters.Barcode.Padding.Right.Point = 5f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 5f;

            // Save the barcode image (optional, provides a visual reference).
            generator.Save(imagePath, BarCodeImageFormat.Png);

            // Export the complete generator configuration to an XML file.
            generator.ExportToXml(xmlPath);
        }

        // --------------------------------------------------------------------
        // Import the generator configuration from the previously saved XML.
        // --------------------------------------------------------------------
        using (var imported = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Verify that colors persisted correctly.
            bool colorsMatch = imported.Parameters.Barcode.BarColor.Equals(Color.Blue) &&
                               imported.Parameters.BackColor.Equals(Color.Yellow);

            // Verify that size and auto‑size mode persisted.
            bool sizeMatch = imported.Parameters.ImageWidth.Point == 300f &&
                             imported.Parameters.ImageHeight.Point == 150f &&
                             imported.Parameters.AutoSizeMode == AutoSizeMode.Interpolation;

            // Verify that code‑text font settings persisted.
            bool textFontMatch = imported.Parameters.Barcode.CodeTextParameters.Font.FamilyName == "Arial" &&
                                 imported.Parameters.Barcode.CodeTextParameters.Font.Size.Point == 12f &&
                                 imported.Parameters.Barcode.CodeTextParameters.Alignment == TextAlignment.Center;

            // Verify that padding values persisted.
            bool paddingMatch = imported.Parameters.Barcode.Padding.Left.Point == 5f &&
                                imported.Parameters.Barcode.Padding.Top.Point == 5f &&
                                imported.Parameters.Barcode.Padding.Right.Point == 5f &&
                                imported.Parameters.Barcode.Padding.Bottom.Point == 5f;

            // Output verification results to the console.
            Console.WriteLine($"Colors persisted: {colorsMatch}");
            Console.WriteLine($"Size persisted: {sizeMatch}");
            Console.WriteLine($"Text font persisted: {textFontMatch}");
            Console.WriteLine($"Padding persisted: {paddingMatch}");
        }

        // --------------------------------------------------------------------
        // Clean up generated files (optional).
        // --------------------------------------------------------------------
        try
        {
            if (File.Exists(xmlPath)) File.Delete(xmlPath);
            if (File.Exists(imagePath)) File.Delete(imagePath);
        }
        catch
        {
            // Suppress any exceptions that occur during cleanup.
        }
    }
}