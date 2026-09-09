// Title: Generate a DataMatrix barcode and output as PNG Base64
// Description: Demonstrates creating a DataMatrix barcode from a text string using Aspose.BarCode and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.DataMatrix. Typical use cases include generating machine‑readable DataMatrix images for inventory, packaging, or mobile scanning. Developers often need to configure barcode parameters and export the result in common image formats such as PNG.
// Prompt: Expose a REST endpoint that accepts text and returns a DataMatrix barcode image in PNG format.
// Tags: datamatrix, barcode generation, png, aspose.barcode, rest endpoint, image output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Program that generates a DataMatrix barcode image and prints its Base64 representation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a DataMatrix barcode from sample text, saves it as PNG, and writes the Base64 string to console.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the DataMatrix barcode.
        string inputText = "Sample DataMatrix Text";

        // Determine a temporary file path for the generated PNG image.
        string outputPath = Path.Combine(Path.GetTempPath(), "datamatrix.png");

        // Create a BarcodeGenerator for DataMatrix with the specified text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, inputText))
        {
            // Optional: set the size of each module (pixel dimension) for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the generated barcode as a PNG file to the temporary location.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully.
        if (File.Exists(outputPath))
        {
            // Read the PNG file bytes.
            byte[] imageBytes = File.ReadAllBytes(outputPath);

            // Convert the image bytes to a Base64 string for easy transport or display.
            string base64 = Convert.ToBase64String(imageBytes);

            // Output the Base64 representation to the console.
            Console.WriteLine("DataMatrix PNG Base64:");
            Console.WriteLine(base64);
        }
        else
        {
            // Inform the user that barcode generation failed.
            Console.WriteLine("Failed to generate barcode image.");
        }

        // Note: In a real application this logic would be placed inside a REST endpoint
        // that accepts text and returns the PNG image (or its Base64 representation) as the response.
    }
}