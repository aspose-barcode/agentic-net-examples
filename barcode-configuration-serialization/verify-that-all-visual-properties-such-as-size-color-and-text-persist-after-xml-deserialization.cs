// Title: Barcode XML Serialization Persistence Demo
// Description: Demonstrates exporting a barcode generator's visual settings to XML and verifying they persist after deserialization.
// Category-Description: This example belongs to the Aspose.BarCode serialization category, showcasing how to use BarcodeGenerator, its Parameters, and ImportFromXml/ExportToXml methods to save and restore barcode configurations. Typical use cases include persisting barcode appearance across sessions or sharing settings between applications. Developers often need to ensure properties like size, colors, and text remain unchanged after round‑trip serialization.
// Prompt: Verify that all visual properties such as size, color, and text persist after XML deserialization.
// Tags: barcode, serialization, xml, visual-properties, aspose.barcode, generation, code128

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a barcode, exports its configuration to XML,
/// re‑imports it, and validates that visual properties are preserved.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs the export/import round‑trip and checks property consistency.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory for the XML file
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeXmlTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string xmlPath = Path.Combine(tempDir, "barcode.xml");

        try
        {
            // ------------------------------------------------------------
            // 1. Create and configure a BarcodeGenerator instance
            // ------------------------------------------------------------
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
            {
                // Set visual properties
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.ImageWidth.Point = 200f;
                generator.Parameters.ImageHeight.Point = 100f;
                generator.Parameters.Barcode.BarColor = Color.Blue;
                generator.Parameters.BackColor = Color.Yellow;
                generator.Parameters.Barcode.FilledBars = false;
                generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

                // Export the configuration to an XML file
                generator.ExportToXml(xmlPath);
            }

            // Verify that the XML file was created
            if (!File.Exists(xmlPath))
            {
                Console.WriteLine("Failed to create XML file.");
                return;
            }

            // ------------------------------------------------------------
            // 2. Import the configuration from XML and validate properties
            // ------------------------------------------------------------
            using (var imported = BarcodeGenerator.ImportFromXml(xmlPath))
            {
                bool allMatch = true;

                // Validate code text
                if (imported.CodeText != "Test123")
                {
                    Console.WriteLine($"CodeText mismatch: expected 'Test123', got '{imported.CodeText}'");
                    allMatch = false;
                }

                // Validate XDimension
                if (Math.Abs(imported.Parameters.Barcode.XDimension.Point - 2f) > 0.001f)
                {
                    Console.WriteLine($"XDimension mismatch: expected 2, got {imported.Parameters.Barcode.XDimension.Point}");
                    allMatch = false;
                }

                // Validate image width
                if (Math.Abs(imported.Parameters.ImageWidth.Point - 200f) > 0.001f)
                {
                    Console.WriteLine($"ImageWidth mismatch: expected 200, got {imported.Parameters.ImageWidth.Point}");
                    allMatch = false;
                }

                // Validate image height
                if (Math.Abs(imported.Parameters.ImageHeight.Point - 100f) > 0.001f)
                {
                    Console.WriteLine($"ImageHeight mismatch: expected 100, got {imported.Parameters.ImageHeight.Point}");
                    allMatch = false;
                }

                // Validate bar color
                if (!imported.Parameters.Barcode.BarColor.Equals(Color.Blue))
                {
                    Console.WriteLine($"BarColor mismatch: expected Blue, got {imported.Parameters.Barcode.BarColor}");
                    allMatch = false;
                }

                // Validate background color
                if (!imported.Parameters.BackColor.Equals(Color.Yellow))
                {
                    Console.WriteLine($"BackColor mismatch: expected Yellow, got {imported.Parameters.BackColor}");
                    allMatch = false;
                }

                // Validate FilledBars flag
                if (imported.Parameters.Barcode.FilledBars != false)
                {
                    Console.WriteLine($"FilledBars mismatch: expected false, got {imported.Parameters.Barcode.FilledBars}");
                    allMatch = false;
                }

                // Validate ThrowExceptionWhenCodeTextIncorrect flag
                if (imported.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect != false)
                {
                    Console.WriteLine($"ThrowExceptionWhenCodeTextIncorrect mismatch: expected false, got {imported.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect}");
                    allMatch = false;
                }

                // Output overall result
                Console.WriteLine(allMatch
                    ? "All visual properties persisted after XML deserialization."
                    : "Some properties did not persist.");
            }
        }
        catch (Exception ex)
        {
            // Report any unexpected errors
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // ------------------------------------------------------------
            // Clean up temporary files and directory
            // ------------------------------------------------------------
            if (Directory.Exists(tempDir))
            {
                try
                {
                    Directory.Delete(tempDir, true);
                }
                catch
                {
                    // Ignored – cleanup failure should not affect example outcome
                }
            }
        }
    }
}