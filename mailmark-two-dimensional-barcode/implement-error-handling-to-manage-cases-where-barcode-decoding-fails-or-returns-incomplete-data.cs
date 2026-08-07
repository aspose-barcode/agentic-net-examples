// Title: Generate and Decode Code128 Barcode with Error Handling
// Description: This example creates a Code128 barcode image, saves it to disk, and then reads it back while handling possible decoding failures or incomplete data.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition workflows. It uses BarcodeGenerator to produce barcodes and BarCodeReader to decode them, covering typical scenarios such as inventory labeling, shipping, and point‑of‑sale systems. Developers often need to validate barcode presence, handle missing or corrupt data, and manage exceptions from the Aspose.BarCode API.
// Prompt: Implement error handling to manage cases where barcode decoding fails or returns incomplete data.
// Tags: code128, barcode generation, barcode decoding, error handling, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates creating a Code128 barcode image and decoding it with robust error handling.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, saves it, and attempts to decode it while handling errors.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputImagePath = "barcode.png";

        // ------------------------------
        // Barcode Generation
        // ------------------------------
        try
        {
            // Initialize the generator with Code128 symbology and the desired text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
            {
                // Optional visual settings: set barcode and background colors.
                generator.Parameters.Barcode.BarColor = Color.Black;
                generator.Parameters.BackColor = Color.White;

                // Save the generated barcode image to the specified path.
                generator.Save(outputImagePath);
                Console.WriteLine($"Barcode image saved to '{outputImagePath}'.");
            }
        }
        catch (Exception ex)
        {
            // Handle any errors that occur during barcode generation.
            Console.WriteLine($"Error during barcode generation: {ex.Message}");
            return;
        }

        // ------------------------------
        // Pre‑decoding Validation
        // ------------------------------
        // Ensure the image file exists before attempting to read it.
        if (!File.Exists(outputImagePath))
        {
            Console.WriteLine($"File '{outputImagePath}' does not exist. Decoding aborted.");
            return;
        }

        // ------------------------------
        // Barcode Decoding with Error Handling
        // ------------------------------
        try
        {
            // Initialize the reader for Code128 barcodes using the generated image.
            using (var reader = new BarCodeReader(outputImagePath, DecodeType.Code128))
            {
                // Configure reader settings, e.g., enforce checksum validation.
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                // Perform the reading operation and retrieve all detected barcodes.
                var results = reader.ReadBarCodes();

                // Verify that at least one barcode was detected.
                if (results == null || results.Length == 0)
                {
                    Console.WriteLine("No barcode detected in the image.");
                    return;
                }

                // Process each detected barcode result.
                foreach (var result in results)
                {
                    // Validate that the decoded text is present.
                    if (string.IsNullOrWhiteSpace(result.CodeText))
                    {
                        Console.WriteLine("Barcode detected but CodeText is missing or empty.");
                    }
                    else
                    {
                        Console.WriteLine($"Detected Barcode Type: {result.CodeTypeName}");
                        Console.WriteLine($"CodeText: {result.CodeText}");
                    }

                    // Optional: check the confidence level of the recognition.
                    if (result.Confidence == BarCodeConfidence.None)
                    {
                        Console.WriteLine("Warning: Low confidence in the recognized barcode.");
                    }
                }
            }
        }
        catch (BarCodeException ex)
        {
            // Handle specific Aspose.BarCode exceptions that may arise during decoding.
            Console.WriteLine($"BarCodeException during decoding: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handle any other unexpected exceptions.
            Console.WriteLine($"Unexpected error during decoding: {ex.Message}");
        }
    }
}