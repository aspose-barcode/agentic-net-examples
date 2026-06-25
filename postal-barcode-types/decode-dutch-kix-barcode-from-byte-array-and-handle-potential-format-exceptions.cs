using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates decoding a Dutch KIX barcode from a byte array using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Reads a barcode image from a byte array,
    /// attempts to decode Dutch KIX barcodes, and outputs the results to the console.
    /// </summary>
    static void Main()
    {
        // Obtain a sample byte array containing a Dutch KIX barcode image.
        // In production, replace this with actual image data.
        byte[] barcodeImageBytes = GetSampleBarcodeImageBytes();

        // Validate that image data was provided.
        if (barcodeImageBytes == null || barcodeImageBytes.Length == 0)
        {
            Console.WriteLine("No barcode image data provided.");
            return;
        }

        try
        {
            // Wrap the byte array in a memory stream for the reader.
            using (var ms = new MemoryStream(barcodeImageBytes))
            {
                // Initialize the barcode reader for Dutch KIX type.
                using (var reader = new BarCodeReader(ms, DecodeType.DutchKIX))
                {
                    // Enable checksum validation (optional; set to Off if not required).
                    reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                    // Perform the barcode recognition.
                    var results = reader.ReadBarCodes();

                    // Check if any barcodes were detected.
                    if (results.Length == 0)
                    {
                        Console.WriteLine("No Dutch KIX barcode detected.");
                    }
                    else
                    {
                        // Output each detected barcode's type and decoded text.
                        foreach (var result in results)
                        {
                            Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                            Console.WriteLine($"Decoded Text: {result.CodeText}");
                        }
                    }
                }
            }
        }
        catch (ArgumentException ex)
        {
            // Thrown when the byte array does not represent a valid image.
            Console.WriteLine($"Invalid image data: {ex.Message}");
        }
        catch (Exception ex)
        {
            // General exception handling for unexpected errors.
            Console.WriteLine($"Error during barcode decoding: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates a sample Dutch KIX barcode image and returns its byte representation.
    /// This helper method is for demonstration purposes only.
    /// </summary>
    /// <returns>Byte array containing a PNG image of a generated Dutch KIX barcode.</returns>
    static byte[] GetSampleBarcodeImageBytes()
    {
        const string sampleCodeText = "1234567890123"; // Example KIX code text.

        // Create a memory stream to hold the generated barcode image.
        using (var ms = new MemoryStream())
        {
            // Generate the barcode using Aspose.BarCode.
            using (var generator = new BarcodeGenerator(EncodeTypes.DutchKIX, sampleCodeText))
            {
                // Save the generated barcode to the memory stream in PNG format.
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Return the image bytes.
            return ms.ToArray();
        }
    }
}