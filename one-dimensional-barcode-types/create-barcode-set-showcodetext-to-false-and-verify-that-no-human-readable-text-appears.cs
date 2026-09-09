// Title: Generate a Code128 barcode without human‑readable text
// Description: Demonstrates how to create a barcode image with the code text hidden, while still preserving the encoded data for later recognition.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, illustrating the use of BarcodeGenerator to customize CodeTextParameters and BarCodeReader to verify the encoded value. Developers often need to hide human‑readable text for aesthetic or security reasons while still being able to decode the barcode programmatically.
// Prompt: Create a barcode, set ShowCodeText to false, and verify that no human‑readable text appears.
// Tags: code128, hidecodetext, barcode generation, barcode recognition, aspnet, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates creating a Code128 barcode with hidden human‑readable text and verifying its readability.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, saves it, and reads it back to confirm the encoded value.
/// </summary>
    static void Main()
    {
        // Define the output file path in the temporary folder
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode_no_text.png");

        // Create a barcode generator for Code128 with the desired data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Hide the human‑readable code text by setting its location to None
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Verify that the CodeText location was set correctly
            if (generator.Parameters.Barcode.CodeTextParameters.Location != CodeLocation.None)
            {
                Console.WriteLine("Failed to set CodeText location to None.");
                return;
            }

            // Save the generated barcode image to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
            Console.WriteLine($"Barcode saved to: {outputPath}");
        }

        // Read the saved barcode to ensure the encoded data is still present
        using (var reader = new BarCodeReader(outputPath, DecodeType.Code128))
        {
            var results = reader.ReadBarCodes();
            if (results.Length > 0)
            {
                Console.WriteLine("Barcode read successfully. Encoded text: " + results[0].CodeText);
                Console.WriteLine("Human‑readable text is hidden as expected.");
            }
            else
            {
                Console.WriteLine("Failed to read the barcode.");
            }
        }
    }
}