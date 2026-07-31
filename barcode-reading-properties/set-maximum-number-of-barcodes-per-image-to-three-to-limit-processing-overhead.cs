// Title: Limit barcode detection to three per image
// Description: Demonstrates how to generate a barcode image and read up to three barcodes from it, reducing processing overhead.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes and BarCodeReader to detect them, illustrating typical scenarios where developers need to limit the number of decoded barcodes per image for performance reasons. Common use cases include batch processing, real‑time scanning, and resource‑constrained environments.
// Prompt: Set maximum number of barcodes per image to three to limit processing overhead.
// Tags: barcode symbology, generation, recognition, png, barcodegenerator, barcodereader, limit

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a barcode image and reads up to three barcodes from it.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a sample barcode, then reads a maximum of three barcodes from the image.
    /// </summary>
    static void Main()
    {
        // Define the output path for the generated barcode image
        const string imagePath = "sample.png";

        // Generate a Code128 barcode and save it as a PNG file
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Barcode image not found.");
            return;
        }

        // Initialize a barcode reader to detect all supported barcode types in the image
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            int count = 0; // Counter for the number of barcodes processed

            // Iterate through detected barcodes, stopping after three have been processed
            foreach (var result in reader.ReadBarCodes())
            {
                if (count >= 3)
                    break; // Exit loop once the maximum count is reached

                Console.WriteLine($"Detected Barcode {count + 1}: Type={result.CodeTypeName}, Text={result.CodeText}");
                count++;
            }

            // Inform the user if no barcodes were found in the image
            if (count == 0)
                Console.WriteLine("No barcodes detected.");
        }
    }
}