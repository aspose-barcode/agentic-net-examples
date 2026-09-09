// Title: Barcode generation with XML serialization round‑trip validation
// Description: Demonstrates creating a Code128 barcode, exporting its generator state to XML, re‑importing it, and verifying that the regenerated image matches the original.
// Category-Description: Shows Aspose.BarCode generation and serialization techniques. Uses BarcodeGenerator, ExportToXml, ImportFromXml, and image comparison to illustrate typical workflows where barcode settings need to be persisted and restored, such as configuration storage or automated testing. Developers often need to serialize generator state, recreate barcodes, and ensure visual consistency.
// Prompt: Write unit tests that compare generated barcode images before and after XML serialization round‑trip.
// Tags: barcode, code128, xml serialization, image comparison, aspose.barcode, generation, testing

using System;
using System.IO;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a barcode, serializes its configuration to XML,
/// re‑creates the barcode from the XML, and compares the two resulting images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, XML round‑trip,
    /// image comparison, and optional cleanup.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for all test artifacts
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the original image, the round‑trip image, and the XML state file
        string originalImagePath = Path.Combine(tempFolder, "original.png");
        string roundTripImagePath = Path.Combine(tempFolder, "roundtrip.png");
        string xmlPath = Path.Combine(tempFolder, "state.xml");

        // --------------------------------------------------------------------
        // Generate the original barcode and export its generator state to XML
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            // Apply custom visual settings
            generator.Parameters.Barcode.XDimension.Point = 0.5f;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.Resolution = 300f;

            // Save the barcode image to disk
            generator.Save(originalImagePath, BarCodeImageFormat.Png);

            // Serialize the generator configuration to an XML file
            generator.ExportToXml(xmlPath);
        }

        // ---------------------------------------------------------------
        // Import the barcode generator from the XML and generate a new image
        // ---------------------------------------------------------------
        using (var generatorFromXml = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            generatorFromXml.Save(roundTripImagePath, BarCodeImageFormat.Png);
        }

        // -------------------------------------------------
        // Compare the two images byte by byte for equality
        // -------------------------------------------------
        bool imagesEqual = false;
        if (File.Exists(originalImagePath) && File.Exists(roundTripImagePath))
        {
            byte[] originalBytes = File.ReadAllBytes(originalImagePath);
            byte[] roundTripBytes = File.ReadAllBytes(roundTripImagePath);
            imagesEqual = originalBytes.SequenceEqual(roundTripBytes);
        }

        // -----------------
        // Output test result
        // -----------------
        if (imagesEqual)
        {
            Console.WriteLine("PASS: Images are identical after XML round‑trip.");
        }
        else
        {
            Console.WriteLine("FAIL: Images differ after XML round‑trip.");
        }

        // -----------------
        // Cleanup (optional)
        // -----------------
        try
        {
            File.Delete(originalImagePath);
            File.Delete(roundTripImagePath);
            File.Delete(xmlPath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore any errors during cleanup to avoid disrupting the test flow
        }
    }
}