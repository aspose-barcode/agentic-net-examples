// Title: QR Code Generation with Binary Encode Mode and Error Handling
// Description: Demonstrates generating QR codes in binary encode mode, handling errors when non‑ASCII text is supplied, and correctly using raw byte data.
// Category-Description: This example belongs to the Aspose.BarCode QR code generation category, illustrating how to configure QREncodeMode, handle unsupported encoding scenarios, and use the BarcodeGenerator class. Developers working with QR symbology often need to switch between text and binary modes, validate input encoding, and generate PNG images. The snippet shows typical use cases such as error handling for invalid text in binary mode and proper byte array handling.
// Prompt: Implement error handling for unsupported encoding mode when Binary mode receives non‑ASCII text.
// Tags: qr code,binary encode mode,error handling,aspose.barcode,barcode generation,png output

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that demonstrates QR code generation in binary mode,
/// including error handling for unsupported non‑ASCII text and correct byte array usage.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates QR codes using binary encode mode,
    /// first with invalid non‑ASCII text to show error handling, then with proper byte data.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output directory for generated barcode images
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // ------------------------------------------------------------
        // Example 1: Attempt binary mode with non‑ASCII text (expected to fail)
        // ------------------------------------------------------------
        string nonAsciiText = "こんにちは"; // Japanese Hiragana characters
        string binaryFailPath = Path.Combine(outputDir, "qr_binary_fail.png");

        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                // Set QR code to binary encode mode
                generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Binary;

                // Attempt to assign Unicode text directly (will raise an exception)
                generator.SetCodeText(nonAsciiText, Encoding.UTF8);

                // Save the barcode image (this line should not be reached)
                generator.Save(binaryFailPath, BarCodeImageFormat.Png);
                Console.WriteLine("Generated barcode (unexpectedly succeeded): " + binaryFailPath);
            }
        }
        catch (Exception ex)
        {
            // Expected error handling for unsupported encoding in binary mode
            Console.WriteLine("Binary mode error (expected for non‑ASCII text): " + ex.Message);
        }

        // ------------------------------------------------------------
        // Example 2: Correct binary usage with a byte array
        // ------------------------------------------------------------
        byte[] binaryData = Encoding.UTF8.GetBytes(nonAsciiText);
        string binarySuccessPath = Path.Combine(outputDir, "qr_binary_success.png");

        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                // Set QR code to binary encode mode
                generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Binary;

                // Provide raw byte data directly to the generator
                generator.SetCodeText(binaryData);

                // Save the generated barcode image
                generator.Save(binarySuccessPath, BarCodeImageFormat.Png);
                Console.WriteLine("Generated binary barcode successfully: " + binarySuccessPath);
            }
        }
        catch (Exception ex)
        {
            // Handle any unexpected errors during barcode generation
            Console.WriteLine("Failed to generate binary barcode: " + ex.Message);
        }
    }
}