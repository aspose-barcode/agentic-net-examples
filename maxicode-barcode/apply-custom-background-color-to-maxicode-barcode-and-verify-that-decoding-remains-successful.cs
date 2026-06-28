using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation and decoding of a MaxiCode barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a MaxiCode (Mode 2) with a custom background, saves it to a memory stream,
    /// and then reads/decodes the barcode from that stream.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare MaxiCode codetext (Mode 2 with a standard second message)
        // ------------------------------------------------------------
        var maxiCodeCodetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140", // 9‑digit US postal code
            CountryCode = 56,         // USA
            ServiceCategory = 999
        };

        // Create the optional second message for the MaxiCode
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Sample MaxiCode"
        };
        maxiCodeCodetext.SecondMessage = secondMessage;

        // ------------------------------------------------------------
        // Generate the barcode with a custom background color
        // ------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            // Set a custom background (e.g., light yellow)
            generator.Parameters.BackColor = Color.FromArgb(255, 255, 224); // LightYellow

            // --------------------------------------------------------
            // Save the generated barcode to a memory stream in PNG format
            // --------------------------------------------------------
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // --------------------------------------------------------
                // Read and decode the barcode from the generated image stream
                // --------------------------------------------------------
                using (var reader = new BarCodeReader(ms, DecodeType.MaxiCode))
                {
                    var results = reader.ReadBarCodes();

                    // Check if any barcodes were detected
                    if (results.Length == 0)
                    {
                        Console.WriteLine("No barcode detected.");
                    }
                    else
                    {
                        // Iterate through all detected barcodes
                        foreach (var result in results)
                        {
                            // Verify that decoding succeeded (non‑empty CodeText)
                            if (!string.IsNullOrEmpty(result.CodeText))
                            {
                                Console.WriteLine("Decoding successful.");
                                Console.WriteLine("Decoded CodeText: " + result.CodeText);
                            }
                            else
                            {
                                Console.WriteLine("Decoding failed: empty CodeText.");
                            }
                        }
                    }
                }
            }
        }
    }
}