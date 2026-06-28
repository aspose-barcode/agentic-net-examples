using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode and saving it as an image file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode with specified parameters and saves it to disk.
    /// </summary>
    static void Main()
    {
        // Define the barcode symbology (Code128) and the text to encode.
        BaseEncodeType encodeType = EncodeTypes.Code128;
        string codeText = "123ABC";

        // Output file path for the generated barcode image.
        string outputPath = "barcode.png";

        try
        {
            // Create a BarcodeGenerator instance with the chosen symbology and text.
            using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Set barcode generation parameters.
                generator.Parameters.Resolution = 300;                     // Image resolution in DPI.
                generator.Parameters.RotationAngle = 0;                  // No rotation applied.
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes; // Enable checksum.

                // Output the configuration details to the console for verification.
                Console.WriteLine("Generating barcode:");
                Console.WriteLine($"  Symbology: {encodeType}");
                Console.WriteLine($"  CodeText: {codeText}");
                Console.WriteLine($"  Resolution: {generator.Parameters.Resolution} DPI");
                Console.WriteLine($"  RotationAngle: {generator.Parameters.RotationAngle} degrees");
                Console.WriteLine($"  ChecksumEnabled: {generator.Parameters.Barcode.IsChecksumEnabled}");

                // Save the generated barcode image to the specified file.
                generator.Save(outputPath);
            }

            // Inform the user that the barcode was generated successfully and display the full path.
            Console.WriteLine($"Barcode generated successfully. Saved to '{Path.GetFullPath(outputPath)}'.");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during barcode generation to the error output stream.
            Console.Error.WriteLine($"Failed to generate barcode: {ex.Message}");
        }
    }
}