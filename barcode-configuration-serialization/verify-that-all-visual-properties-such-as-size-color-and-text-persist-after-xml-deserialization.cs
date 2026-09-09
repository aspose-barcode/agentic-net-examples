// Title: Barcode visual property persistence after XML deserialization
// Description: Demonstrates creating a barcode, exporting its configuration to XML, re-importing it, and verifying that visual settings such as size, colors, and text remain unchanged.
// Category-Description: This example belongs to the Aspose.BarCode generation and serialization category. It showcases the use of BarcodeGenerator, its Parameters, and the ImportFromXml/ExportToXml APIs to persist barcode settings. Typical scenarios include saving barcode configurations for later reuse, sharing settings across services, or version‑controlling visual designs. Developers often need to ensure that exported XML retains all visual properties when deserialized.
// Prompt: Verify that all visual properties such as size, color, and text persist after XML deserialization.
// Tags: barcode, xml, serialization, visual-properties, aspose.barcode, code128, image-generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates that visual properties of a barcode persist after exporting to XML and re‑importing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a barcode, saves its configuration to XML, reloads it,
    /// generates images, and validates that all visual settings are retained.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary working directory for all generated files.
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeXmlTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define file paths for the XML configuration and the two PNG images.
        string xmlPath = Path.Combine(tempDir, "generator.xml");
        string originalImagePath = Path.Combine(tempDir, "original.png");
        string importedImagePath = Path.Combine(tempDir, "imported.png");

        // --------------------------------------------------------------------
        // Create and configure the original barcode generator.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            // ----- Visual properties -----
            generator.Parameters.ImageWidth.Pixels = 300f;
            generator.Parameters.ImageHeight.Pixels = 150f;
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Parameters.Barcode.BarColor = Color.Blue;
            generator.Parameters.BackColor = Color.Yellow;

            // ----- Text properties -----
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

            // Export the generator's state to an XML file.
            generator.ExportToXml(xmlPath);

            // Generate the barcode image and save it as the original reference.
            using (Bitmap bmp = generator.GenerateBarCodeImage())
            {
                bmp.Save(originalImagePath, ImageFormat.Png);
            }
        }

        // --------------------------------------------------------------------
        // Load the generator from the previously saved XML and verify properties.
        // --------------------------------------------------------------------
        using (var importedGen = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Generate the barcode image from the imported configuration.
            using (Bitmap bmp = importedGen.GenerateBarCodeImage())
            {
                bmp.Save(importedImagePath, ImageFormat.Png);
            }

            // Compare each property with the expected values.
            bool allMatch = true;

            allMatch &= CompareFloat(importedGen.Parameters.ImageWidth.Pixels, 300f, "ImageWidth");
            allMatch &= CompareFloat(importedGen.Parameters.ImageHeight.Pixels, 150f, "ImageHeight");
            allMatch &= CompareFloat(importedGen.Parameters.Barcode.XDimension.Point, 2f, "XDimension");
            allMatch &= CompareColor(importedGen.Parameters.Barcode.BarColor, Color.Blue, "BarColor");
            allMatch &= CompareColor(importedGen.Parameters.BackColor, Color.Yellow, "BackColor");
            allMatch &= CompareString(importedGen.CodeText, "Test123", "CodeText");
            allMatch &= CompareEnum(importedGen.Parameters.Barcode.CodeTextParameters.Location, CodeLocation.Below, "CodeText Location");
            allMatch &= CompareString(importedGen.Parameters.Barcode.CodeTextParameters.Font.FamilyName, "Helvetica", "Font Family");
            allMatch &= CompareFloat(importedGen.Parameters.Barcode.CodeTextParameters.Font.Size.Point, 12f, "Font Size");
            allMatch &= CompareEnum(importedGen.Parameters.Barcode.CodeTextParameters.Alignment, TextAlignment.Center, "Text Alignment");

            Console.WriteLine(allMatch
                ? "All visual properties persisted after XML deserialization."
                : "Some properties did not match after XML deserialization.");
        }

        // --------------------------------------------------------------------
        // Cleanup temporary files and directory.
        // --------------------------------------------------------------------
        try
        {
            File.Delete(xmlPath);
            File.Delete(originalImagePath);
            File.Delete(importedImagePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignored – cleanup is not critical for the demonstration.
        }
    }

    /// <summary>
    /// Compares two floating‑point values within a tolerance and writes the result to the console.
    /// </summary>
    static bool CompareFloat(float actual, float expected, string name)
    {
        const float tolerance = 0.001f;
        bool result = Math.Abs(actual - expected) <= tolerance;
        Console.WriteLine($"{name}: {(result ? "OK" : $"FAIL (expected {expected}, got {actual})")}");
        return result;
    }

    /// <summary>
    /// Compares two <see cref="Color"/> values and writes the result to the console.
    /// </summary>
    static bool CompareColor(Color actual, Color expected, string name)
    {
        bool result = actual.ToArgb() == expected.ToArgb();
        Console.WriteLine($"{name}: {(result ? "OK" : $"FAIL (expected {expected}, got {actual})")}");
        return result;
    }

    /// <summary>
    /// Compares two strings using ordinal comparison and writes the result to the console.
    /// </summary>
    static bool CompareString(string actual, string expected, string name)
    {
        bool result = string.Equals(actual, expected, StringComparison.Ordinal);
        Console.WriteLine($"{name}: {(result ? "OK" : $"FAIL (expected \"{expected}\", got \"{actual}\")")}");
        return result;
    }

    /// <summary>
    /// Compares two enum values and writes the result to the console.
    /// </summary>
    static bool CompareEnum<T>(T actual, T expected, string name) where T : Enum
    {
        bool result = actual.Equals(expected);
        Console.WriteLine($"{name}: {(result ? "OK" : $"FAIL (expected {expected}, got {actual})")}");
        return result;
    }
}