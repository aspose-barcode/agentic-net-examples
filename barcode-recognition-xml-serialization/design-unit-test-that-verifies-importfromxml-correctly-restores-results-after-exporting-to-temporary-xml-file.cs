// Title: ImportFromXml restores barcode generator settings
// Description: Demonstrates exporting a barcode generator's configuration to XML, importing it back, and verifying that the settings and generated barcode are identical.
// Prompt: Design a unit test that verifies ImportFromXml correctly restores results after exporting to a temporary XML file.
// Tags: barcode, import, export, xml, unit-test, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that exports a barcode generator configuration to XML,
/// imports it back, and validates that the restored settings produce the same barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the export, import, and verification steps.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare temporary file paths for XML configuration and barcode image
        // ------------------------------------------------------------
        string xmlPath = Path.Combine(Path.GetTempPath(), "barcode_config.xml");
        string imagePath = Path.Combine(Path.GetTempPath(), "barcode_image.png");

        // ------------------------------------------------------------
        // Create original barcode generator with custom visual settings
        // ------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            // Set visual appearance
            generator.Parameters.Barcode.BarColor = Color.Blue;
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Parameters.Barcode.Padding.Left.Point = 5f;
            generator.Parameters.Barcode.Padding.Top.Point = 5f;
            generator.Parameters.Barcode.Padding.Right.Point = 5f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 5f;
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Export the generator's configuration to an XML file
            bool exportSuccess = generator.ExportToXml(xmlPath);
            if (!exportSuccess)
            {
                Console.WriteLine("FAILED: ExportToXml returned false.");
                return;
            }

            // Save the generated barcode image (used later for visual verification)
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Import the configuration from XML into a new generator instance
        // ------------------------------------------------------------
        using (BarcodeGenerator imported = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Verify that key settings were correctly restored
            bool settingsMatch = true;
            settingsMatch &= imported.CodeText == "Test123";
            settingsMatch &= imported.Parameters.Barcode.BarColor.Equals(Color.Blue);
            settingsMatch &= Math.Abs(imported.Parameters.Barcode.XDimension.Point - 2f) < 0.001f;
            settingsMatch &= Math.Abs(imported.Parameters.Barcode.Padding.Left.Point - 5f) < 0.001f;
            settingsMatch &= imported.Parameters.AutoSizeMode == AutoSizeMode.Interpolation;

            if (!settingsMatch)
            {
                Console.WriteLine("FAILED: Imported settings do not match original.");
                return;
            }

            // ------------------------------------------------------------
            // Generate a barcode image from the imported settings into memory
            // ------------------------------------------------------------
            using (MemoryStream ms = new MemoryStream())
            {
                imported.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // ------------------------------------------------------------
                // Decode the barcode from the generated image to verify content
                // ------------------------------------------------------------
                using (BarCodeReader reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
                {
                    var results = reader.ReadBarCodes();
                    if (results.Length == 0)
                    {
                        Console.WriteLine("FAILED: No barcode detected in the generated image.");
                        return;
                    }

                    // Ensure the decoded text matches the original code text
                    bool decodeMatch = true;
                    foreach (var result in results)
                    {
                        if (string.IsNullOrEmpty(result.CodeText) || result.CodeText != "Test123")
                        {
                            decodeMatch = false;
                            break;
                        }
                    }

                    if (!decodeMatch)
                    {
                        Console.WriteLine("FAILED: Decoded CodeText does not match original.");
                        return;
                    }

                    // All verification steps passed
                    Console.WriteLine("SUCCESS: ImportFromXml restored settings and barcode decoded correctly.");
                }
            }
        }

        // ------------------------------------------------------------
        // Clean up temporary files (ignore any errors during cleanup)
        // ------------------------------------------------------------
        try
        {
            if (File.Exists(xmlPath)) File.Delete(xmlPath);
            if (File.Exists(imagePath)) File.Delete(imagePath);
        }
        catch
        {
            // Cleanup failures are non‑critical for the test outcome
        }
    }
}