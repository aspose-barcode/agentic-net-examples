// Title: Barcode visual properties persistence after XML deserialization
// Description: Demonstrates generating a barcode with customized visual settings, exporting its configuration to XML, importing it back, and verifying that all visual attributes remain unchanged.
// Category-Description: This example belongs to the Aspose.BarCode serialization and visual customization category. It showcases the use of BarcodeGenerator, ExportToXml, and ImportFromXml to persist barcode appearance settings such as colors, dimensions, captions, and fonts. Developers often need to store barcode configurations for later reuse or for dynamic generation scenarios, making this pattern essential for reliable barcode rendering across sessions.
// Prompt: Verify that all visual properties such as size, color, and text persist after XML deserialization.
// Tags: barcode, xml, serialization, deserialization, visual properties, color, size, caption, aspose.barcode, generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a barcode with specific visual properties,
/// saves its configuration to XML, reloads it, and validates that all visual
/// settings persist after deserialization.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the generation, export, import,
    /// and verification steps.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary folder for all test files.
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeXmlTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the original image, deserialized image, and XML configuration.
        string originalImagePath = Path.Combine(tempFolder, "original.png");
        string deserializedImagePath = Path.Combine(tempFolder, "deserialized.png");
        string xmlPath = Path.Combine(tempFolder, "config.xml");

        // Expected visual properties that will be applied to the barcode.
        Color expectedBarColor = Color.Blue;
        Color expectedBackColor = Color.Yellow;
        Color expectedBorderColor = Color.Green;
        float expectedBorderWidth = 2f;
        float expectedXDimension = 2f;
        CodeLocation expectedCodeLocation = CodeLocation.Below;
        string expectedCaptionAbove = "Above";
        Color expectedCaptionColor = Color.Red;
        string expectedFontFamily = "Helvetica";
        float expectedFontSize = 10f;

        // Generate the barcode with the defined visual properties and save the image.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            generator.Parameters.Barcode.BarColor = expectedBarColor;
            generator.Parameters.BackColor = expectedBackColor;
            generator.Parameters.Border.Color = expectedBorderColor;
            generator.Parameters.Border.Width.Pixels = expectedBorderWidth;
            generator.Parameters.Barcode.XDimension.Point = expectedXDimension;
            generator.Parameters.Barcode.CodeTextParameters.Location = expectedCodeLocation;
            generator.Parameters.CaptionAbove.Text = expectedCaptionAbove;
            generator.Parameters.CaptionAbove.TextColor = expectedCaptionColor;
            generator.Parameters.CaptionAbove.Font.FamilyName = expectedFontFamily;
            generator.Parameters.CaptionAbove.Font.Size.Point = expectedFontSize;

            // Save the original barcode image.
            generator.Save(originalImagePath, BarCodeImageFormat.Png);

            // Export the generator's configuration to an XML file.
            generator.ExportToXml(xmlPath);
        }

        // Import the barcode configuration from XML and generate a second image.
        using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Save the image generated from the deserialized configuration.
            importedGenerator.Save(deserializedImagePath, BarCodeImageFormat.Png);

            // Verify that each visual property matches the expected values.
            bool allMatch = true;

            if (!ColorsEqual(importedGenerator.Parameters.Barcode.BarColor, expectedBarColor))
            {
                Console.WriteLine("BarColor mismatch.");
                allMatch = false;
            }
            if (!ColorsEqual(importedGenerator.Parameters.BackColor, expectedBackColor))
            {
                Console.WriteLine("BackColor mismatch.");
                allMatch = false;
            }
            if (!ColorsEqual(importedGenerator.Parameters.Border.Color, expectedBorderColor))
            {
                Console.WriteLine("Border.Color mismatch.");
                allMatch = false;
            }
            if (Math.Abs(importedGenerator.Parameters.Border.Width.Pixels - expectedBorderWidth) > 0.001f)
            {
                Console.WriteLine("Border.Width mismatch.");
                allMatch = false;
            }
            if (Math.Abs(importedGenerator.Parameters.Barcode.XDimension.Point - expectedXDimension) > 0.001f)
            {
                Console.WriteLine("XDimension mismatch.");
                allMatch = false;
            }
            if (importedGenerator.Parameters.Barcode.CodeTextParameters.Location != expectedCodeLocation)
            {
                Console.WriteLine("CodeText location mismatch.");
                allMatch = false;
            }
            if (importedGenerator.Parameters.CaptionAbove.Text != expectedCaptionAbove)
            {
                Console.WriteLine("CaptionAbove text mismatch.");
                allMatch = false;
            }
            if (!ColorsEqual(importedGenerator.Parameters.CaptionAbove.TextColor, expectedCaptionColor))
            {
                Console.WriteLine("CaptionAbove text color mismatch.");
                allMatch = false;
            }
            if (importedGenerator.Parameters.CaptionAbove.Font.FamilyName != expectedFontFamily)
            {
                Console.WriteLine("CaptionAbove font family mismatch.");
                allMatch = false;
            }
            if (Math.Abs(importedGenerator.Parameters.CaptionAbove.Font.Size.Point - expectedFontSize) > 0.001f)
            {
                Console.WriteLine("CaptionAbove font size mismatch.");
                allMatch = false;
            }

            // Verify that the image dimensions of the original and deserialized images are identical.
            using (var bmpOriginal = new Bitmap(originalImagePath))
            using (var bmpDeserialized = new Bitmap(deserializedImagePath))
            {
                if (bmpOriginal.Width != bmpDeserialized.Width || bmpOriginal.Height != bmpDeserialized.Height)
                {
                    Console.WriteLine("Image dimensions mismatch.");
                    allMatch = false;
                }
            }

            Console.WriteLine(allMatch
                ? "All visual properties persisted after XML deserialization."
                : "Some visual properties did NOT persist.");
        }

        // Cleanup temporary files and directories (optional).
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore any errors that occur during cleanup.
        }
    }

    /// <summary>
    /// Compares two <see cref="Color"/> values for equality based on their ARGB representation.
    /// </summary>
    /// <param name="a">First color to compare.</param>
    /// <param name="b">Second color to compare.</param>
    /// <returns>True if the colors are identical; otherwise, false.</returns>
    static bool ColorsEqual(Color a, Color b)
    {
        return a.ToArgb() == b.ToArgb();
    }
}