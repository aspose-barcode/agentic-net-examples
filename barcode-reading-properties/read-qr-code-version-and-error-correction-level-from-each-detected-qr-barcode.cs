// Title: Read QR Code version and error correction level from detected barcodes
// Description: Generates a QR code image, then reads the image to extract each QR code's version and error correction level.
// Category-Description: This example belongs to the Aspose.BarCode QR code recognition category, demonstrating how to use BarcodeGenerator and BarCodeReader to create QR codes and retrieve extended QR parameters such as version and error correction level. Developers working with QR code generation and decoding often need to access these properties for validation, analytics, or adaptive processing. The example showcases key classes like BarcodeGenerator, BarCodeReader, QRExtendedParameters, and QRErrorLevel.
// Prompt: Read QR Code version and error correction level from each detected QR barcode.
// Tags: qr,barcode,recognition,generation,version,error-correction,aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;

/// <summary>
/// Demonstrates generating a QR code, saving it to a file, and then reading the QR code
/// to obtain its version and error correction level using Aspose.BarCode APIs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code image, verifies its existence,
    /// reads the QR code(s) from the image, and outputs version and error correction level.
    /// </summary>
    static void Main()
    {
        // Define the output image path for the generated QR code
        string imagePath = "sample_qr.png";

        // Generate a QR code with sample text and a high error correction level
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Text"))
        {
            // Set a specific error correction level (optional, LevelH provides the highest redundancy)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the generated QR code image to the specified path
            generator.Save(imagePath);
        }

        // Ensure the image file was created before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Initialize a barcode reader configured to decode QR codes from the image
        using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            // Iterate through all detected QR barcodes in the image
            foreach (var result in reader.ReadBarCodes())
            {
                // Output the decoded text of the QR code
                Console.WriteLine($"Detected QR Code Text: {result.CodeText}");

                // Prepare default values for version and error correction level
                string version = "N/A";
                string errorLevel = "N/A";

                // Attempt to retrieve the QR version (1‑40) from extended parameters
                try
                {
                    version = result.Extended.QR.Version.ToString();
                }
                catch
                {
                    // If the property is unavailable, keep the default "N/A"
                }

                // Attempt to retrieve the error correction level from extended parameters
                try
                {
                    errorLevel = result.Extended.QR.ErrorLevel.ToString();
                }
                catch
                {
                    // If the property is unavailable, keep the default "N/A"
                }

                // Output the extracted QR version and error correction level
                Console.WriteLine($"QR Version: {version}");
                Console.WriteLine($"Error Correction Level: {errorLevel}");
                Console.WriteLine();
            }
        }
    }
}