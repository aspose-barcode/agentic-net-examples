// Title: Cap Additional Worker Threads for Barcode Reading
// Description: Demonstrates how to limit the number of extra worker threads used by Aspose.BarCode's barcode reader by setting ProcessorSettings.MaxAdditionalAllowedThreads.
// Category-Description: This example belongs to the Aspose.BarCode multithreading control category. It shows how to configure the BarCodeReader's ProcessorSettings to cap additional allowed threads, a common requirement when integrating barcode processing into applications with constrained resources. Developers often need to balance performance and resource usage using key API classes like BarCodeReader, ProcessorSettings, and BarcodeGenerator.
// Prompt: Set ProcessorSettings.MaxAdditionalAllowedThreads to 2 to cap extra worker threads for controlled multithreading.
// Tags: barcode symbology, multithreading, qr, generation, recognition, aspose.barcode, processor settings

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a QR code, limits barcode reader threading, and reads the code.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR barcode, configures thread limits, reads the barcode, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for storing the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the sample barcode image
        string imagePath = Path.Combine(tempFolder, "sample.png");

        // Generate a QR barcode with the text "Hello Aspose" and save it as a PNG file
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Configure the barcode reader to allow a maximum of 2 additional worker threads
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 2;

        // Read the barcode from the generated image using the configured processor settings
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            BarCodeResult[] results = reader.ReadBarCodes();
            Console.WriteLine($"Barcodes found: {results.Length}");
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }

        // Attempt to delete the temporary image file and folder; ignore any errors during cleanup
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Cleanup failures are non‑critical; they do not affect program execution
        }
    }
}