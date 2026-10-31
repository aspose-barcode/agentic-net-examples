// Title: Generate and Read a Mailmark Complex Barcode
// Description: Demonstrates creating a Mailmark complex barcode, saving it as a PNG image, and decoding it using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of ComplexBarcodeGenerator to produce a Mailmark barcode and BarCodeReader to decode it. Developers working with postal services, logistics, or any scenario requiring complex barcode creation and validation will find these APIs essential for generating compliant barcodes and extracting their data.
// Prompt: Dispose of BarCodeReader and ComplexBarcodeGenerator objects in a finally block to ensure resource cleanup.
// Tags: mailmark, complexbarcode, generation, recognition, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generation of a Mailmark complex barcode, saving it to disk,
/// and reading it back using Aspose.BarCode APIs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Mailmark barcode, saves it as PNG,
    /// then reads and prints the decoded text.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the demo files.
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "mailmark.png");

        // Define the Mailmark code text with required fields.
        var mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "1",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        ComplexBarcodeGenerator complexGenerator = null;
        BarCodeReader reader = null;

        try
        {
            // Generate the complex barcode and save it as a PNG image.
            complexGenerator = new ComplexBarcodeGenerator(mailmark);
            complexGenerator.Save(imagePath, BarCodeImageFormat.Png);
            Console.WriteLine($"Barcode saved to {imagePath}");

            // Verify the image exists before attempting to read it.
            if (File.Exists(imagePath))
            {
                // Initialize the reader for Mailmark decoding.
                reader = new BarCodeReader(imagePath, DecodeType.Mailmark);
                var results = reader.ReadBarCodes();

                // Output each decoded result.
                foreach (var result in results)
                {
                    Console.WriteLine($"Decoded: {result.CodeText}");
                }
            }
            else
            {
                Console.WriteLine("Generated image not found.");
            }
        }
        finally
        {
            // Ensure the reader is disposed safely.
            if (reader != null)
            {
                try { reader.Dispose(); } catch { }
            }

            // Ensure the generator is disposed safely.
            if (complexGenerator != null)
            {
                try { complexGenerator.Dispose(); } catch { }
            }
        }
    }
}