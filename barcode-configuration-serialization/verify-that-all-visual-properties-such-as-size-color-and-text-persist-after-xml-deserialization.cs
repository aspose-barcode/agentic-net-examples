// Title: Persist Barcode Visual Properties via XML Serialization
// Description: Demonstrates how to set visual properties on a barcode, export the configuration to XML, and verify that those properties are retained after importing the XML.
// Category-Description: This example belongs to the Aspose.BarCode serialization category, showcasing the use of BarcodeGenerator, ExportToXml, and ImportFromXml to persist barcode settings. Typical use cases include saving barcode appearance for later reuse, configuration sharing, or automated testing of visual consistency. Developers often need to ensure that colors, dimensions, fonts, and captions survive round‑trip serialization.
// Prompt: Verify that all visual properties such as size, color, and text persist after XML deserialization.
// Tags: barcode, qr, xml, serialization, deserialization, visual-properties, aspose.barcode, aspose.drawing, color, font, caption

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a QR barcode with custom visual settings,
/// serializes the configuration to XML, deserializes it back, and validates
/// that all visual properties are preserved.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the create‑export‑import‑validation workflow.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the test files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeXmlTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        string xmlPath = Path.Combine(tempFolder, "barcode_config.xml");

        // -----------------------------------------------------------------
        // 1. Generate a barcode with explicit visual properties
        // -----------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Test123"))
        {
            // Set barcode colors
            generator.Parameters.Barcode.BarColor = Color.Blue;
            generator.Parameters.BackColor = Color.Yellow;

            // Set size‑related properties
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Parameters.Barcode.BarHeight.Point = 50f;

            // Configure code‑text appearance
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Configure caption placed above the barcode
            generator.Parameters.CaptionAbove.Text = "Above Caption";
            generator.Parameters.CaptionAbove.Font.FamilyName = "Helvetica";
            generator.Parameters.CaptionAbove.Font.Size.Point = 10f;
            generator.Parameters.CaptionAbove.TextColor = Color.Red;

            // Export the full configuration to an XML file
            generator.ExportToXml(xmlPath);
        }

        // -----------------------------------------------------------------
        // 2. Import the configuration from XML and verify persistence
        // -----------------------------------------------------------------
        using (var imported = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            bool allMatch = true;

            // Compare each visual property with the original values
            allMatch &= CompareColors(imported.Parameters.Barcode.BarColor, Color.Blue, "BarColor");
            allMatch &= CompareColors(imported.Parameters.BackColor, Color.Yellow, "BackColor");
            allMatch &= CompareFloats(imported.Parameters.Barcode.XDimension.Point, 2f, "XDimension");
            allMatch &= CompareFloats(imported.Parameters.Barcode.BarHeight.Point, 50f, "BarHeight");
            allMatch &= CompareEnum(imported.Parameters.Barcode.CodeTextParameters.Location, CodeLocation.Below, "CodeText Location");
            allMatch &= CompareStrings(imported.Parameters.Barcode.CodeTextParameters.Font.FamilyName, "Helvetica", "CodeText Font Family");
            allMatch &= CompareFloats(imported.Parameters.Barcode.CodeTextParameters.Font.Size.Point, 12f, "CodeText Font Size");
            allMatch &= CompareStrings(imported.Parameters.CaptionAbove.Text, "Above Caption", "CaptionAbove Text");
            allMatch &= CompareStrings(imported.Parameters.CaptionAbove.Font.FamilyName, "Helvetica", "CaptionAbove Font Family");
            allMatch &= CompareFloats(imported.Parameters.CaptionAbove.Font.Size.Point, 10f, "CaptionAbove Font Size");
            allMatch &= CompareColors(imported.Parameters.CaptionAbove.TextColor, Color.Red, "CaptionAbove TextColor");

            Console.WriteLine(allMatch
                ? "All visual properties persisted after XML deserialization."
                : "Some visual properties did NOT persist.");
        }

        // -----------------------------------------------------------------
        // 3. Clean up temporary files
        // -----------------------------------------------------------------
        try
        {
            if (File.Exists(xmlPath))
                File.Delete(xmlPath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – cleanup failures should not affect the test result
        }
    }

    // -----------------------------------------------------------------
    // Helper comparison methods
    // -----------------------------------------------------------------
    static bool CompareColors(Color actual, Color expected, string propertyName)
    {
        bool equal = actual.ToArgb() == expected.ToArgb();
        if (!equal)
            Console.WriteLine($"{propertyName} mismatch: expected {expected}, actual {actual}");
        return equal;
    }

    static bool CompareFloats(float actual, float expected, string propertyName, float tolerance = 0.001f)
    {
        bool equal = Math.Abs(actual - expected) <= tolerance;
        if (!equal)
            Console.WriteLine($"{propertyName} mismatch: expected {expected}, actual {actual}");
        return equal;
    }

    static bool CompareEnum<T>(T actual, T expected, string propertyName) where T : Enum
    {
        bool equal = actual.Equals(expected);
        if (!equal)
            Console.WriteLine($"{propertyName} mismatch: expected {expected}, actual {actual}");
        return equal;
    }

    static bool CompareStrings(string actual, string expected, string propertyName)
    {
        bool equal = string.Equals(actual, expected, StringComparison.Ordinal);
        if (!equal)
            Console.WriteLine($"{propertyName} mismatch: expected \"{expected}\", actual \"{actual}\"");
        return equal;
    }
}