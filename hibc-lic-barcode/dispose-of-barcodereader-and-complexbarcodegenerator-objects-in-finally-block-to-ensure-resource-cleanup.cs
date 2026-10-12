// Title: Generate and Read a MaxiCode Complex Barcode using Aspose.BarCode
// Description: Demonstrates creating a MaxiCode complex barcode, saving it as PNG, and reading it back with Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation and recognition category. It showcases the use of ComplexBarcodeGenerator to create a MaxiCode codetext, saving the image with BarCodeImageFormat, and then using BarCodeReader to decode the barcode. Developers working with advanced barcode symbologies often need to generate complex barcodes and validate them programmatically, making this pattern a common requirement.
// Prompt: Dispose of BarCodeReader and ComplexBarcodeGenerator objects in a finally block to ensure resource cleanup.
// Tags: maxicode, complex barcode, generation, recognition, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that generates a MaxiCode complex barcode, saves it to a file,
/// and then reads it back using Aspose.BarCode APIs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, saving, and reading,
    /// and ensures proper disposal of resources in a finally block.
    /// </summary>
    static void Main()
    {
        // Declare variables for generator and reader; they will be instantiated later.
        ComplexBarcodeGenerator generator = null;
        BarCodeReader reader = null;

        try
        {
            // Create a simple MaxiCode complex codetext (Mode 2) with sample data.
            var codetext = new MaxiCodeCodetextMode2
            {
                PostalCode = "123456",
                CountryCode = 56,
                ServiceCategory = 999
            };

            // Generate the barcode image from the codetext.
            generator = new ComplexBarcodeGenerator(codetext);
            string imagePath = Path.Combine(Path.GetTempPath(), "maxicode.png");
            generator.Save(imagePath, BarCodeImageFormat.Png);
            Console.WriteLine($"Barcode saved to: {imagePath}");

            // Read the generated barcode from the saved image file.
            reader = new BarCodeReader(imagePath, DecodeType.MaxiCode);
            var results = reader.ReadBarCodes();

            // Output each decoded result to the console.
            foreach (var result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }
        finally
        {
            // Ensure the BarCodeReader is disposed if it was created.
            if (reader != null)
            {
                reader.Dispose();
            }

            // Ensure the ComplexBarcodeGenerator is disposed if it was created.
            if (generator != null)
            {
                generator.Dispose();
            }
        }
    }
}