// Title: ImportFromXml State Restoration Test
// Description: Demonstrates exporting a BarcodeGenerator to XML and importing it back, verifying that the generator's settings and generated image are preserved.
// Category-Description: This example belongs to the Aspose.BarCode generation and persistence category. It showcases the use of BarcodeGenerator, its ExportToXml and ImportFromXml methods, and typical validation steps such as comparing parameters and rendered images. Developers working with barcode creation often need to serialize generator configurations for later reuse or for sharing across services, making this pattern a common requirement.
// Prompt: Design a unit test that verifies ImportFromXml correctly restores results after exporting to a temporary XML file.
// Tags: barcode symbology, import, export, xml, unit-test, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that exports a BarcodeGenerator configuration to XML,
/// imports it back, and validates that the restored generator produces the same results.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that performs the export, import, and verification steps.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for test artifacts
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define file paths for XML state and generated images
        string xmlPath = Path.Combine(tempDir, "generator.xml");
        string imgPath1 = Path.Combine(tempDir, "original.png");
        string imgPath2 = Path.Combine(tempDir, "imported.png");

        try
        {
            // ------------------------------------------------------------
            // 1. Create original generator, configure parameters, save image, export state to XML
            // ------------------------------------------------------------
            using (var gen1 = new BarcodeGenerator(EncodeTypes.QR, "Test123"))
            {
                gen1.Parameters.Barcode.XDimension.Pixels = 4f;
                gen1.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;
                gen1.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;

                // Save the barcode image generated with the original settings
                gen1.Save(imgPath1, BarCodeImageFormat.Png);

                // Export the generator's configuration to an XML file
                gen1.ExportToXml(xmlPath);
            }

            // ------------------------------------------------------------
            // 2. Import generator from the previously saved XML and verify restored state
            // ------------------------------------------------------------
            using (var gen2 = BarcodeGenerator.ImportFromXml(xmlPath))
            {
                // Save the barcode image generated from the imported settings
                gen2.Save(imgPath2, BarCodeImageFormat.Png);

                // Validate that key parameters match the original configuration
                bool sameXDim = Math.Abs(gen2.Parameters.Barcode.XDimension.Pixels - 4f) < 0.001f;
                bool sameCode = gen2.CodeText == "Test123";
                bool sameColor = gen2.Parameters.Barcode.BarColor.ToArgb() == Aspose.Drawing.Color.Blue.ToArgb();

                // Load both images to compare dimensions (size equality is a simple proxy for visual similarity)
                using (var bmp1 = new Bitmap(imgPath1))
                using (var bmp2 = new Bitmap(imgPath2))
                {
                    bool sameSize = bmp1.Width == bmp2.Width && bmp1.Height == bmp2.Height;

                    // Aggregate all checks into a single test result
                    bool testPass = sameXDim && sameCode && sameColor && sameSize;

                    // Output the test result
                    Console.WriteLine(testPass
                        ? "PASS: ImportFromXml restored state correctly."
                        : "FAILED: State mismatch after ImportFromXml.");
                }
            }
        }
        finally
        {
            // ------------------------------------------------------------
            // 3. Cleanup temporary files and directory
            // ------------------------------------------------------------
            try
            {
                if (Directory.Exists(tempDir))
                {
                    Directory.Delete(tempDir, true);
                }
            }
            catch
            {
                // Ignored - cleanup failure should not affect test result
            }
        }
    }
}