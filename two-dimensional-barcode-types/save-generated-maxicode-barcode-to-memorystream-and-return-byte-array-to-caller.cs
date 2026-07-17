// Title: Generate MaxiCode barcode and return as byte array
// Description: Demonstrates creating a MaxiCode barcode, saving it to a MemoryStream, and returning the PNG byte array to the caller.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator with MaxiCodeCodetextMode2 to produce MaxiCode symbols, a common requirement for shipping and logistics applications. Developers often need to generate such barcodes programmatically and obtain the image data as a byte array for further processing or storage.
// Prompt: Save generated MaxiCode barcode to a MemoryStream and return the byte array to the caller.
// Tags: maxicode, barcode generation, memory stream, png, aspose.barcode, complexbarcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Provides an example of generating a MaxiCode barcode and returning the image as a byte array.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a MaxiCode barcode and writes the byte array length to the console.
    /// </summary>
    static void Main()
    {
        // Generate the MaxiCode barcode and obtain the PNG image bytes
        byte[] imageBytes = GenerateMaxiCode();

        // Output the size of the generated image byte array
        Console.WriteLine($"Generated MaxiCode image byte array length: {imageBytes.Length}");
    }

    /// <summary>
    /// Creates a MaxiCode barcode using Mode 2 encoding, saves it to a MemoryStream in PNG format,
    /// and returns the resulting byte array.
    /// </summary>
    /// <returns>Byte array containing the PNG image of the generated MaxiCode barcode.</returns>
    static byte[] GenerateMaxiCode()
    {
        // Prepare MaxiCode codetext (Mode 2 with a standard second message)
        var maxiCodeCodetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",
            CountryCode = 56,
            ServiceCategory = 999
        };

        // Set the optional second message for the barcode
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Test message"
        };
        maxiCodeCodetext.SecondMessage = secondMessage;

        // Generate the barcode and save it to a memory stream in PNG format
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            using (var memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                // Return the image data as a byte array
                return memoryStream.ToArray();
            }
        }
    }
}