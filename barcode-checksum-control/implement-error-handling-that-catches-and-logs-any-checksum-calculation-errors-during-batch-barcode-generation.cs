// Title: Batch Barcode Generation with Checksum Error Handling
// Description: Demonstrates generating a batch of barcodes while catching and logging checksum calculation errors.
// Prompt: Implement error handling that catches and logs any checksum calculation errors during batch barcode generation.
// Tags: barcode, checksum, error handling, batch generation, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a set of barcodes, handling checksum errors gracefully.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcodes in a batch and logs any checksum errors.
    /// </summary>
    static void Main()
    {
        // Define the output folder for generated barcode images
        string outputFolder = "Barcodes";

        // Ensure the output directory exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Prepare a sample batch of barcodes (including some with intentional checksum errors)
        var batch = new (BaseEncodeType Type, string Text)[]
        {
            (EncodeTypes.EAN13, "1234567890128"), // valid EAN13
            (EncodeTypes.EAN13, "1234567890123"), // invalid checksum
            (EncodeTypes.UPCA,  "012345678905"), // valid UPCA
            (EncodeTypes.UPCA,  "012345678904"), // invalid checksum
            (EncodeTypes.Code128, "ABC123")      // Code128 (checksum handled internally)
        };

        // Iterate over each barcode definition in the batch
        for (int i = 0; i < batch.Length; i++)
        {
            var (type, text) = batch[i];
            string filePath = Path.Combine(outputFolder, $"barcode_{i + 1}.png");

            try
            {
                // Create a barcode generator for the specified type and text
                using (var generator = new BarcodeGenerator(type, text))
                {
                    // Throw an exception if the provided text is incorrect (e.g., wrong checksum)
                    generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

                    // Enable checksum generation where applicable
                    generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

                    // Save the generated barcode image to the file system
                    generator.Save(filePath);
                    Console.WriteLine($"Generated barcode saved to: {filePath}");
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during barcode generation, such as checksum failures
                Console.WriteLine($"Error generating barcode #{i + 1} (Type: {type.TypeName}, Text: {text}): {ex.Message}");
            }
        }

        // Indicate that the batch processing has finished
        Console.WriteLine("Batch barcode generation completed.");
    }
}