// Title: Generate and read a MaxiCode barcode using Aspose.BarCode
// Description: Demonstrates creating a MaxiCode barcode with ComplexBarcodeGenerator, saving it as PNG, then reading it with BarCodeReader.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use ComplexBarcodeGenerator for creating complex symbologies such as MaxiCode and BarCodeReader for decoding any supported barcode type. Developers often need to generate barcodes for shipping labels and then verify them programmatically, making this pattern useful for testing and automation.
// Prompt: Dispose of BarCodeReader and ComplexBarcodeGenerator objects in a finally block to ensure resource cleanup.
// Tags: maxicode, barcode generation, barcode recognition, complexbarcodegenerator, barcodereader, png, csharp, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that generates a MaxiCode barcode, saves it to a temporary file,
/// reads the barcode back, and cleans up all resources.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the barcode generation, saving, reading,
    /// and cleanup logic.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the demo files
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "maxicode.png");

        ComplexBarcodeGenerator generator = null;
        BarCodeReader reader = null;

        try
        {
            // Prepare MaxiCode codetext (Mode 2) with postal and message data
            var codetext = new MaxiCodeCodetextMode2
            {
                PostalCode = "123456",
                CountryCode = 56,
                ServiceCategory = 999,
                SecondMessage = new MaxiCodeStandardSecondMessage { Message = "Hello" }
            };

            // Generate the barcode image and save it as PNG
            generator = new ComplexBarcodeGenerator(codetext);
            generator.Save(imagePath, BarCodeImageFormat.Png);
            Console.WriteLine($"Generated barcode saved to: {imagePath}");

            // Initialize the reader to decode all supported barcode types from the image
            reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes);
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output each detected barcode type and its decoded text
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"Detected: {result.CodeTypeName} - {result.CodeText}");
            }
        }
        finally
        {
            // Ensure the reader is disposed to release file handles
            if (reader != null)
            {
                reader.Dispose();
            }

            // Ensure the generator is disposed to release any unmanaged resources
            if (generator != null)
            {
                generator.Dispose();
            }

            // Clean up the temporary directory and its contents
            try
            {
                if (Directory.Exists(tempDir))
                {
                    Directory.Delete(tempDir, true);
                }
            }
            catch
            {
                // Suppress any exceptions during cleanup to avoid breaking the finally block
            }
        }
    }
}