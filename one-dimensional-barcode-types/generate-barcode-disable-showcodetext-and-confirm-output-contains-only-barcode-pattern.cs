// Title: Generate Code128 barcode without human‑readable text and verify pattern
// Description: This example creates a Code128 barcode image with the human‑readable text hidden, saves it as PNG, and then reads it back to confirm the encoded value.
// Category-Description: Demonstrates Aspose.BarCode barcode generation and recognition. It uses BarcodeGenerator to create a barcode image and BarCodeReader to decode it, a common workflow for developers who need to produce barcodes for packaging, inventory, or ticketing and verify that the generated pattern encodes the intended data. The example highlights key API classes (BarcodeGenerator, BarCodeReader) and typical use cases such as hiding the human‑readable text (ShowCodeText) while ensuring data integrity.
// Prompt: Generate a barcode, disable ShowCodeText, and confirm output contains only the barcode pattern.
// Tags: code128, barcode generation, barcode recognition, hide codetext, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode with hidden human‑readable text,
/// saves it as a PNG file, and then verifies the encoded value by reading the barcode back.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode creation, saves the image, and validates the result.
    /// </summary>
    static void Main()
    {
        // Define the temporary file path for the generated barcode image.
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode.png");

        // Generate a Code128 barcode with the code text hidden (no human‑readable text displayed).
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Disable the display of the code text by setting its location to None.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Save the barcode image as PNG to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");

        // Verify that the saved barcode can be read and that it encodes the expected value.
        using (var reader = new BarCodeReader(outputPath, DecodeType.Code128))
        {
            // Read all barcodes found in the image.
            var results = reader.ReadBarCodes();

            // Check that at least one barcode was read and that its code text matches the original.
            if (results.Length > 0 && results[0].CodeText == "123456")
            {
                Console.WriteLine("Barcode pattern confirmed (code text correctly encoded).");
            }
            else
            {
                Console.WriteLine("Failed to read barcode or code text mismatch.");
            }
        }
    }
}