using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a PDF417 barcode with the IsLinked flag set,
/// saving it to a temporary PNG file, and then reading back the barcode
/// to inspect the IsLinked property.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a PDF417 barcode, saves it, and reads it back to display its properties.
    /// </summary>
    static void Main()
    {
        // Define a temporary file path for the generated barcode image.
        string outputPath = Path.Combine(Path.GetTempPath(), "pdf417.png");

        // Generate a PDF417 barcode and set the IsLinked flag.
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Sample PDF417 Text"))
        {
            // Access PDF417‑specific parameters through the Barcode sub‑object.
            generator.Parameters.Barcode.Pdf417.IsLinked = true;

            // Save the barcode image as PNG to the temporary location.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was successfully created.
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Read the barcode from the saved image and inspect the extended PDF417 parameters.
        using (var reader = new BarCodeReader(outputPath, DecodeType.Pdf417))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                // Output the decoded text of the barcode.
                Console.WriteLine($"CodeText: {result.CodeText}");

                // Retrieve and display the IsLinked flag from the extended PDF417 data.
                bool isLinked = result.Extended.Pdf417.IsLinked;
                Console.WriteLine($"IsLinked (barcode linked to another segment): {isLinked}");
            }
        }
    }
}