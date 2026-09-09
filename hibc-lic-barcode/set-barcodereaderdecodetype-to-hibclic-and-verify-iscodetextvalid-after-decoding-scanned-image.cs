// Title: Demonstrate HIBCLIC barcode generation and validation using Aspose.BarCode
// Description: This example creates a HIBCLIC barcode image, decodes it with the HIBC QRLIC decode type, and checks if the decoded text is non‑empty as a simple validity test.
// Category-Description: Shows how to work with Aspose.BarCode's ComplexBarcodeGenerator and BarCodeReader for HIBCLIC symbology. The example covers creating primary and secondary data, saving the barcode as PNG, reading it back with a specific DecodeType, and performing a basic validation of the decoded text. Developers dealing with healthcare barcodes often need to generate and verify HIBCLIC codes using these core API classes.
// Prompt: Set BarCodeReader.DecodeType to HIBCLIC and verify IsCodeTextValid after decoding a scanned image.
// Tags: hibc, decode, validation, png, complexbarcode, barcodereader

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a HIBCLIC barcode, reads it back, and validates the decoded text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, decodes it, and outputs validation results.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory to store the generated barcode image
        string tempDir = Path.Combine(Path.GetTempPath(), "HIBCLICDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "hibclic.png");

        // Prepare primary data required for HIBCLIC
        var primaryData = new PrimaryData
        {
            ProductOrCatalogNumber = "12345",
            LabelerIdentificationCode = "A999",
            UnitOfMeasureID = 1
        };

        // Prepare secondary data with the required link character
        var secondaryCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            Data = new SecondaryAndAdditionalData { LotNumber = "LOT123" },
            LinkCharacter = '+'
        };

        // Combine primary and secondary data into a single codetext object
        var combinedCodetext = new HIBCLICCombinedCodetext
        {
            PrimaryData = primaryData,
            SecondaryAndAdditionalData = secondaryCodetext.Data
        };

        // Generate the barcode image and save it as PNG
        using (var generator = new ComplexBarcodeGenerator(combinedCodetext))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode using the HIBC QRLIC decode type
        using (var reader = new BarCodeReader(imagePath, DecodeType.HIBCQRLIC))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Simple validation: consider the code text valid if it is not null or empty
                bool isValid = !string.IsNullOrEmpty(result.CodeText);
                Console.WriteLine($"Decoded CodeText: {result.CodeText}");
                Console.WriteLine($"IsCodeTextValid (simulated): {isValid}");
            }
        }

        // Clean up temporary files and directory
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}