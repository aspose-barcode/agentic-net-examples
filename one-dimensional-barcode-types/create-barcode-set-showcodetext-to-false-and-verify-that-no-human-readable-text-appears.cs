// Title: Hide human‑readable text in a Code128 barcode and verify decoding
// Description: Demonstrates how to generate a Code128 barcode without displaying the human‑readable text and confirms that the encoded data remains intact by decoding the image.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, illustrating the use of BarcodeGenerator to customize visual appearance (e.g., suppressing CodeText) and BarCodeReader to validate the encoded value. Developers often need to hide human‑readable text for aesthetic or security reasons while still being able to read the barcode programmatically. The snippet showcases key API classes such as BarcodeGenerator, CodeTextParameters, and BarCodeReader.
// Prompt: Create a barcode, set ShowCodeText to false, and verify that no human‑readable text appears.
// Tags: code128, hide-text, png, barcodelibrary, barcodegenerator, barcodereader

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode without human‑readable text,
/// saves it as a PNG file, and then decodes it to verify the encoded value.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, hides the code text,
    /// saves the image, and reads it back to confirm the data.
    /// </summary>
    static void Main()
    {
        // Define the temporary output file path for the barcode image.
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode_no_text.png");

        // Create a barcode generator for Code128 with the sample text "Test123".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            // Suppress the human‑readable text by setting its location to None.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Save the generated barcode as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved and the text visibility setting.
        Console.WriteLine($"Barcode saved to: {outputPath}");
        Console.WriteLine("Human‑readable text visibility set to: " + CodeLocation.None);

        // Verify the barcode by decoding it; the encoded text should still be "Test123".
        using (var reader = new BarCodeReader(outputPath, DecodeType.Code128))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Decoded CodeText: {result.CodeText}");
            }
        }
    }
}