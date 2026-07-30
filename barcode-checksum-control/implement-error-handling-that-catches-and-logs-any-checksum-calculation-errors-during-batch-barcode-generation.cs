// Title: Batch Barcode Generation with Checksum Error Handling
// Description: This example generates a series of barcodes, enables checksum calculation, and logs any checksum mismatches that occur during generation.
// Category-Description: The sample belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator, set generation parameters, and handle validation errors. It is useful for developers who need to produce multiple barcodes in a batch while ensuring data integrity through checksum verification. Typical scenarios include inventory labeling, shipping documents, and bulk barcode creation where error logging is required.
// Prompt: Implement error handling that catches and logs any checksum calculation errors during batch barcode generation.
// Tags: barcode, symbology, generation, checksum, error handling, aspose.barcode, png, logging

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates batch barcode generation with checksum validation and error logging using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes, enables checksum, and logs any generation errors.
    /// </summary>
    static void Main()
    {
        // Define the output directory for generated barcode images
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Prepare a log file to capture checksum or generation errors
        string logFile = Path.Combine(outputDir, "error.log");
        if (File.Exists(logFile))
        {
            File.Delete(logFile);
        }

        // Define a batch of barcodes: each tuple contains the symbology type and the code text
        var batch = new (BaseEncodeType type, string text)[]
        {
            (EncodeTypes.EAN13, "1234567890128"), // valid checksum
            (EncodeTypes.EAN13, "1234567890123"), // invalid checksum
            (EncodeTypes.Code128, "ABC123"),      // Code128 (checksum always applied)
            (EncodeTypes.Code39FullASCII, "12345*"), // valid Code39
            (EncodeTypes.Interleaved2of5, "1234567") // possibly invalid length
        };

        // Iterate through the batch and generate each barcode
        for (int i = 0; i < batch.Length; i++)
        {
            var (type, text) = batch[i];
            try
            {
                // Initialize the barcode generator with the specified type and text
                using (var generator = new BarcodeGenerator(type, text))
                {
                    // Enable checksum generation where the symbology supports it
                    generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

                    // Instruct the generator to throw an exception if the code text is incorrect (e.g., checksum mismatch)
                    generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

                    // Construct a unique file name and save the barcode image as PNG
                    string fileName = $"{type.TypeName}_{i + 1}.png";
                    string filePath = Path.Combine(outputDir, fileName);
                    generator.Save(filePath);
                    Console.WriteLine($"Generated: {filePath}");
                }
            }
            catch (Exception ex)
            {
                // Capture and log any errors that occur during barcode generation
                string message = $"Error generating barcode #{i + 1} (Type: {type.TypeName}, Text: {text}): {ex.Message}";
                Console.WriteLine(message);
                File.AppendAllText(logFile, message + Environment.NewLine);
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}