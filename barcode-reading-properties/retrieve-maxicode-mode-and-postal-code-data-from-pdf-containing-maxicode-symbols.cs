using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates reading MaxiCode barcodes from a PDF file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the program. Reads a PDF, extracts MaxiCode barcodes,
    /// decodes them, and prints postal codes.
    /// </summary>
    static void Main()
    {
        // Path to the PDF file containing MaxiCode symbols
        string pdfPath = "sample.pdf";

        // Verify that the specified PDF file exists before proceeding
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Initialize a barcode reader configured for MaxiCode symbology
        using (var reader = new BarCodeReader(pdfPath, DecodeType.MaxiCode))
        {
            // Iterate over all detected barcodes across all pages of the PDF
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Extract the MaxiCode mode from the extended result information
                var mode = result.Extended.MaxiCode.MaxiCodeMode;
                Console.WriteLine($"Detected MaxiCode mode: {mode}");

                // Attempt to decode the raw codetext into a structured MaxiCode object
                var decoded = ComplexCodetextReader.TryDecodeMaxiCode(mode, result.CodeText);
                if (decoded == null)
                {
                    Console.WriteLine("Failed to decode MaxiCode codetext.");
                    continue;
                }

                // Retrieve the postal code based on the specific MaxiCode mode
                string postalCode = null;
                if (decoded is MaxiCodeCodetextMode2 mode2)
                {
                    postalCode = mode2.PostalCode;
                }
                else if (decoded is MaxiCodeCodetextMode3 mode3)
                {
                    postalCode = mode3.PostalCode;
                }

                // Output the postal code if it is available; otherwise indicate its absence
                if (!string.IsNullOrEmpty(postalCode))
                {
                    Console.WriteLine($"Postal Code: {postalCode}");
                }
                else
                {
                    Console.WriteLine("Postal Code not available for this MaxiCode mode.");
                }

                Console.WriteLine(); // Insert a blank line to separate results
            }
        }
    }
}