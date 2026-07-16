// Title: Generate Han Xin Barcode with Default Square Module Size
// Description: Demonstrates creating a Han Xin barcode from alphanumeric data using default square modules and automatic version selection.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the BarcodeGenerator class for encoding data into Han Xin symbology. Typical use cases include generating compact 2‑D barcodes for inventory, tracking, or authentication where square modules are required. Developers often need to set symbology, input text, and output format while relying on default settings for module size and version selection.
// Prompt: Generate a Han Xin barcode with default square module size for alphanumeric input.
// Tags: hanxin, barcode, generation, png, aspnet.barcode, encode, symbology

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Han Xin barcode with default square module size.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Alphanumeric input for the Han Xin barcode
        string codeText = "ABC123XYZ";

        // Initialize a BarcodeGenerator for Han Xin symbology with the provided code text
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
        {
            // Default settings already use a square module size and auto version selection
            // Define the output file name
            string outputFile = "HanXinBarcode.png";

            // Save the generated barcode as a PNG image
            generator.Save(outputFile);

            // Inform the user where the barcode was saved
            Console.WriteLine($"Han Xin barcode saved to {outputFile}");
        }
    }
}