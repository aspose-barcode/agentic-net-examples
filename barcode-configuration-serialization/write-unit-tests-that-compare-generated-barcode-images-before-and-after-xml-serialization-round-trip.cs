// Title: Barcode XML serialization round‑trip verification
// Description: Demonstrates generating barcodes, exporting settings to XML, re‑importing, and comparing the resulting images to ensure fidelity.
// Prompt: Write unit tests that compare generated barcode images before and after XML serialization round‑trip.
// Tags: barcode, xml serialization, roundtrip, image comparison, unit test, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates barcodes, serializes their settings to XML,
/// deserializes them back, and verifies that the generated images are identical.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Executes a series of round‑trip tests for different barcode symbologies.
    /// </summary>
    static void Main()
    {
        // Define test cases: each tuple contains a symbology type and the text to encode.
        var tests = new (BaseEncodeType type, string text)[]
        {
            (EncodeTypes.Code128, "Test123"),
            (EncodeTypes.QR, "https://example.com"),
            (EncodeTypes.DataMatrix, "DataMatrixSample")
        };

        // Run each test and report the result.
        foreach (var (type, text) in tests)
        {
            Console.WriteLine($"Testing {type.TypeName} with text \"{text}\"");
            bool result = RunRoundTripTest(type, text);
            Console.WriteLine(result ? "PASS" : "FAIL");
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Generates a barcode, saves its image, exports its settings to XML,
    /// re‑imports the settings, generates a second image, and compares the two.
    /// </summary>
    /// <param name="encodeType">The barcode symbology to use.</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <returns>True if the two generated images are pixel‑identical; otherwise false.</returns>
    static bool RunRoundTripTest(BaseEncodeType encodeType, string codeText)
    {
        // Create a temporary folder for all intermediate files.
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeRoundTrip");
        if (!Directory.Exists(tempDir))
        {
            Directory.CreateDirectory(tempDir);
        }

        // Build unique file paths for XML and PNG images.
        string xmlPath = Path.Combine(tempDir, $"barcode_{Guid.NewGuid()}.xml");
        string imgPath1 = Path.Combine(tempDir, $"barcode_original_{Guid.NewGuid()}.png");
        string imgPath2 = Path.Combine(tempDir, $"barcode_roundtrip_{Guid.NewGuid()}.png");

        // --------------------------------------------------------------------
        // Generate the original barcode and save its image.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Use deterministic size to avoid variations caused by auto‑sizing.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Save the first image (optional visual reference).
            generator.Save(imgPath1, BarCodeImageFormat.Png);

            // Export the generator's configuration to an XML file.
            generator.ExportToXml(xmlPath);
        }

        // --------------------------------------------------------------------
        // Import the configuration from XML and generate a second image.
        // --------------------------------------------------------------------
        using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            importedGenerator.Save(imgPath2, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Load both images and compare them pixel by pixel.
        // --------------------------------------------------------------------
        using (var bmp1 = new Bitmap(imgPath1))
        using (var bmp2 = new Bitmap(imgPath2))
        {
            bool identical = CompareBitmaps(bmp1, bmp2);

            // Clean up temporary files regardless of the comparison outcome.
            try { File.Delete(xmlPath); } catch { }
            try { File.Delete(imgPath1); } catch { }
            try { File.Delete(imgPath2); } catch { }

            return identical;
        }
    }

    /// <summary>
    /// Compares two bitmaps for exact pixel equality.
    /// </summary>
    /// <param name="bmp1">First bitmap.</param>
    /// <param name="bmp2">Second bitmap.</param>
    /// <returns>True if both bitmaps have identical dimensions and pixel data; otherwise false.</returns>
    static bool CompareBitmaps(Bitmap bmp1, Bitmap bmp2)
    {
        // Quick size check before per‑pixel comparison.
        if (bmp1.Width != bmp2.Width || bmp1.Height != bmp2.Height)
            return false;

        // Compare each pixel.
        for (int y = 0; y < bmp1.Height; y++)
        {
            for (int x = 0; x < bmp1.Width; x++)
            {
                if (bmp1.GetPixel(x, y) != bmp2.GetPixel(x, y))
                    return false;
            }
        }

        return true;
    }
}