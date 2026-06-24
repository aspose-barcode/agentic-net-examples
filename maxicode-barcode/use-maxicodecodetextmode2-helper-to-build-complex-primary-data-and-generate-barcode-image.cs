using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demo program that generates a MaxiCode barcode, saves it, and optionally reads it back.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a MaxiCode barcode, saves to file, and reads it back.
    /// </summary>
    static void Main()
    {
        // Prepare the full path for the output PNG file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "maxicode.png");

        // Build MaxiCode primary data using MaxiCodeCodetextMode2.
        var maxiCodeData = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140", // 9‑digit US postal code for Mode 2.
            CountryCode = 56,         // Example country code.
            ServiceCategory = 999     // Example service category.
        };

        // Create a standard second message and assign it to the primary data.
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Sample MaxiCode message"
        };
        maxiCodeData.SecondMessage = secondMessage;

        // Generate the barcode image using ComplexBarcodeGenerator.
        using (var generator = new ComplexBarcodeGenerator(maxiCodeData))
        {
            // Optional: set image resolution (dots per inch).
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");

        // Demonstrate reading the generated barcode (optional).
        if (File.Exists(outputPath))
        {
            // Initialize a barcode reader for MaxiCode format.
            using (var reader = new BarCodeReader(outputPath, DecodeType.MaxiCode))
            {
                // Iterate through all detected barcodes in the image.
                foreach (var result in reader.ReadBarCodes())
                {
                    // Decode the complex codetext back to MaxiCodeCodetextMode2.
                    var decoded = ComplexCodetextReader.TryDecodeMaxiCode(
                        result.Extended.MaxiCode.MaxiCodeMode,
                        result.CodeText);

                    // If decoding succeeded and the result is of the expected type, display its fields.
                    if (decoded is MaxiCodeCodetextMode2 decodedData)
                    {
                        Console.WriteLine("Decoded PostalCode: " + decodedData.PostalCode);
                        Console.WriteLine("Decoded CountryCode: " + decodedData.CountryCode);
                        Console.WriteLine("Decoded ServiceCategory: " + decodedData.ServiceCategory);

                        // If a standard second message is present, display its content.
                        if (decodedData.SecondMessage is MaxiCodeStandardSecondMessage stdMsg)
                        {
                            Console.WriteLine("Decoded Message: " + stdMsg.Message);
                        }
                    }
                }
            }
        }
    }
}