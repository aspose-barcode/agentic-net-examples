using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating QR barcodes with ASCII and non‑ASCII text using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates two QR codes: one with ASCII text and one with non‑ASCII text.
    /// </summary>
    static void Main()
    {
        // Define sample texts
        string asciiText = "HelloWorld123";   // ASCII only
        string nonAsciiText = "Hello世界";    // Contains non‑ASCII characters

        // Determine output file paths in the current directory
        string asciiOutput = Path.Combine(Directory.GetCurrentDirectory(), "qr_ascii.png");
        string nonAsciiOutput = Path.Combine(Directory.GetCurrentDirectory(), "qr_nonascii.png");

        // Generate QR code for ASCII text (expected to succeed)
        GenerateQrBarcode(asciiText, asciiOutput);

        // Generate QR code for non‑ASCII text using Binary mode (should be handled gracefully)
        GenerateQrBarcode(nonAsciiText, nonAsciiOutput);
    }

    /// <summary>
    /// Generates a QR barcode from the specified text and saves it to the given path.
    /// </summary>
    /// <param name="codeText">The text to encode in the QR barcode.</param>
    /// <param name="outputPath">The file path where the barcode image will be saved.</param>
    static void GenerateQrBarcode(string codeText, string outputPath)
    {
        try
        {
            // Initialize the barcode generator for QR type
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                // Configure the QR generator to use Binary encoding mode
                generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Binary;

                // Set the text to be encoded
                generator.CodeText = codeText;

                // Save the generated barcode image to the specified file
                generator.Save(outputPath);
                Console.WriteLine($"Barcode saved: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Log any errors, such as unsupported encoding for non‑ASCII characters
            Console.WriteLine($"Failed to generate barcode for text \"{codeText}\": {ex.Message}");
        }
    }
}