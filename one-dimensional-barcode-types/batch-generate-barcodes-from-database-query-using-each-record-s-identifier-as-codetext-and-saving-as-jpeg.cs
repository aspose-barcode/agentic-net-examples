// Title: Batch generate Code128 barcodes and save as JPEG files
// Description: Demonstrates how to create a series of Code128 barcodes from a collection of identifiers and store each image as a JPEG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Typical scenarios include bulk barcode creation for inventory, shipping labels, or product catalogs, where developers need to automate image output for many records.
// Prompt: Batch generate barcodes from a database query, using each record’s identifier as CodeText and saving as JPEG.
// Tags: code128, barcode generation, jpeg, aspose.barcode, batch, file-output

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides an entry point for generating a batch of Code128 barcodes and saving them as JPEG images.
/// </summary>
class Program
{
    /// <summary>
    /// Generates barcodes for a simulated list of identifiers, saves each as a JPEG file, and writes progress to the console.
    /// </summary>
    static void Main()
    {
        // Simulated database records: list of identifiers to be encoded as barcodes.
        List<string> identifiers = new List<string>
        {
            "ID001",
            "ID002",
            "ID003",
            "ID004",
            "ID005"
        };

        // Create a unique temporary folder for the generated JPEG barcode images.
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine("Barcodes will be saved to: " + outputFolder);

        // Iterate over each identifier, generate a barcode, and save it as a JPEG file.
        for (int i = 0; i < identifiers.Count; i++)
        {
            string codeText = identifiers[i];
            string fileName = $"barcode_{i + 1}.jpg";
            string filePath = Path.Combine(outputFolder, fileName);

            // Initialize the barcode generator with Code128 symbology and the current identifier.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Optional: set image resolution and suppress exceptions for invalid CodeText.
                generator.Parameters.Resolution = 300f;
                generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

                // Save the generated barcode image as a JPEG file.
                generator.Save(filePath, BarCodeImageFormat.Jpeg);
            }

            Console.WriteLine($"Generated barcode for '{codeText}' -> {filePath}");
        }

        // Note: In a real scenario, replace the simulated list with a database query,
        // e.g., using SqlConnection, SqlCommand, and SqlDataReader to fetch identifiers.
        // The barcode generation logic would remain unchanged.
    }
}