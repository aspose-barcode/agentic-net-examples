// Title: Batch Barcode Generation with Rotation
// Description: Generates a set of Code128 barcodes, rotates each by 90 degrees, and saves them as PNG files.
// Prompt: Create a batch process that rotates each generated barcode by 90 degrees before saving as PNG files.
// Tags: barcode, code128, rotation, png, batch, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch creation of Code128 barcodes, applying a 90‑degree rotation,
/// and saving each image as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates, rotates, and saves barcodes.
    /// </summary>
    static void Main()
    {
        // Define a collection of sample code texts for the batch process
        string[] codeTexts = { "12345", "ABCDE", "987654321", "HelloWorld", "20230607" };

        // Iterate over each code text, generate a barcode, rotate it, and save as PNG
        for (int i = 0; i < codeTexts.Length; i++)
        {
            // Initialise a Code128 barcode generator with the current code text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeTexts[i]))
            {
                // Set rotation angle to 90 degrees (clockwise)
                generator.Parameters.RotationAngle = 90f;

                // Construct a unique file name for the output image
                string fileName = $"barcode_{i + 1}.png";

                // Save the rotated barcode image in PNG format
                generator.Save(fileName, BarCodeImageFormat.Png);
            }
        }

        // Output a confirmation message to the console
        Console.WriteLine("Barcodes generated and rotated successfully.");
    }
}