using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation and recognition of a MaxiCode (Mode 2) barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a MaxiCode barcode, attempts to read it up to three times,
    /// and outputs the decoded information to the console.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Create a sample MaxiCode codetext (Mode 2) with required fields.
        // --------------------------------------------------------------------
        var maxiCode = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",
            CountryCode = 56,
            ServiceCategory = 999,
            SecondMessage = new MaxiCodeStandardSecondMessage { Message = "Sample message" }
        };

        // ---------------------------------------------------------------
        // 2. Generate the barcode image and store it in a memory stream.
        // ---------------------------------------------------------------
        using (var barcodeStream = new MemoryStream())
        {
            using (var generator = new ComplexBarcodeGenerator(maxiCode))
            {
                // Save the generated barcode as PNG into the memory stream.
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
            }

            // -----------------------------------------------------------
            // 3. Prepare for up to three read attempts of the generated image.
            // -----------------------------------------------------------
            bool decoded = false;
            const int maxAttempts = 3;

            for (int attempt = 1; attempt <= maxAttempts && !decoded; attempt++)
            {
                try
                {
                    // Reset the stream position to the beginning before each read.
                    barcodeStream.Position = 0;

                    // Initialize the barcode reader for MaxiCode type.
                    using (var reader = new BarCodeReader(barcodeStream, DecodeType.MaxiCode))
                    {
                        // Allow recognition of imperfect barcodes.
                        reader.QualitySettings.AllowIncorrectBarcodes = true;

                        // Perform the read operation.
                        var results = reader.ReadBarCodes();

                        if (results.Length > 0)
                        {
                            // Process each detected barcode.
                            foreach (var result in results)
                            {
                                // Decode the complex MaxiCode codetext.
                                var decodedCodetext = ComplexCodetextReader.TryDecodeMaxiCode(
                                    result.Extended.MaxiCode.MaxiCodeMode,
                                    result.CodeText);

                                // Verify that the decoded codetext matches the expected Mode 2 type.
                                if (decodedCodetext is MaxiCodeCodetextMode2 mode2)
                                {
                                    Console.WriteLine($"Attempt {attempt}: Decoded successfully.");
                                    Console.WriteLine($"PostalCode: {mode2.PostalCode}");
                                    Console.WriteLine($"CountryCode: {mode2.CountryCode}");
                                    Console.WriteLine($"ServiceCategory: {mode2.ServiceCategory}");

                                    // Output the optional second message if present.
                                    if (mode2.SecondMessage is MaxiCodeStandardSecondMessage msg)
                                    {
                                        Console.WriteLine($"Message: {msg.Message}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"Attempt {attempt}: Decoded but unexpected codetext type.");
                                }
                            }

                            // Mark as successfully decoded to exit the retry loop.
                            decoded = true;
                        }
                        else
                        {
                            Console.WriteLine($"Attempt {attempt}: No barcode detected.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log any exception that occurs during the read attempt.
                    Console.WriteLine($"Attempt {attempt}: Exception - {ex.Message}");
                }

                // If not decoded and more attempts remain, indicate a retry.
                if (!decoded && attempt < maxAttempts)
                {
                    Console.WriteLine("Retrying...");
                }
            }

            // -----------------------------------------------------------
            // 4. Final outcome after all attempts.
            // -----------------------------------------------------------
            if (!decoded)
            {
                Console.WriteLine("Failed to decode after 3 attempts.");
            }
        }
    }
}