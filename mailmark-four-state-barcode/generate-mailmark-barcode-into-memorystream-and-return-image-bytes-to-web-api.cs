// Title: Generate Mailmark 2D Barcode and Return Image Bytes
// Description: This example creates a Mailmark 2D barcode using Aspose.BarCode, writes it to a MemoryStream, and obtains the PNG image bytes, suitable for returning from a web API.
// Category-Description: Demonstrates Aspose.BarCode complex barcode generation for Mailmark symbology. It showcases the use of Mailmark2DCodetext, ComplexBarcodeGenerator, and image format settings. Developers building web services that need to embed barcode images in responses will find this pattern useful for creating PNG byte arrays on the fly.
// Prompt: Generate a Mailmark barcode into a MemoryStream and return image bytes to a web API.
// Tags: mailmark, barcode, generation, memory stream, png, aspose.barcode, complexbarcode, image bytes

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Mailmark 2D barcode and extracts the image bytes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a Mailmark 2D codetext, generates the barcode,
    /// saves it to a MemoryStream as PNG, and outputs the length of the resulting byte array.
    /// </summary>
    static void Main()
    {
        // Initialize Mailmark 2D codetext with required fields
        var mailmark2D = new Mailmark2DCodetext
        {
            UPUCountryID = "JGB ",
            InformationTypeID = "0",
            VersionID = "1",
            Class = "1",
            SupplyChainID = 123,
            ItemID = 1234,
            DestinationPostCodeAndDPS = "EF61AH8T ",
            DataMatrixType = Mailmark2DType.Type_7,
            CustomerContent = "CUSTOM"
        };

        // Create a memory stream to hold the generated barcode image
        using (var memoryStream = new MemoryStream())
        {
            // Use ComplexBarcodeGenerator to render the Mailmark barcode
            using (var generator = new ComplexBarcodeGenerator(mailmark2D))
            {
                // Set the X-dimension (module size) in pixels for better resolution
                generator.Parameters.Barcode.XDimension.Pixels = 4f;

                // Save the barcode as a PNG image into the memory stream
                generator.Save(memoryStream, BarCodeImageFormat.Png);
            }

            // Convert the memory stream contents to a byte array
            byte[] imageBytes = memoryStream.ToArray();

            // Output the size of the generated image byte array (for demonstration)
            Console.WriteLine($"Generated Mailmark 2D barcode byte array length: {imageBytes.Length}");
        }
    }
}