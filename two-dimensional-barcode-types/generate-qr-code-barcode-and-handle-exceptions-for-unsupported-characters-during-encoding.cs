using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR code with Aspose.BarCode, handling potential encoding issues.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code image from a sample text.
    /// </summary>
    static void Main()
    {
        // Sample QR code text containing characters that may not be supported by the default encoding.
        string codeText = "Hello 世界";

        // Output file path for the generated QR code image.
        string outputPath = "qr_code.png";

        // Create the barcode generator inside a using block to ensure proper disposal of resources.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Set QR encoding mode to Auto – the generator will throw an exception if a character cannot be encoded.
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Auto;

            // Optionally set an error correction level (Level M provides a good balance between data capacity and error recovery).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            try
            {
                // Attempt to save the QR code image to the specified path.
                generator.Save(outputPath);
                Console.WriteLine($"QR code generated successfully: {outputPath}");
            }
            catch (Exception ex)
            {
                // Handle unsupported characters or any other generation errors.
                Console.WriteLine($"Failed to generate QR code: {ex.Message}");
            }
        }
    }
}