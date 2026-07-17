// Title: Set DotCode error correction level using Auto encode mode
// Description: Demonstrates configuring Aspose.BarCode to generate a DotCode barcode with built‑in error correction via the Auto encode mode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on DotCode symbology configuration. It showcases the use of BarcodeGenerator, EncodeTypes, and DotCode parameters such as DotCodeEncodeMode and ECIEncoding. Developers often need to adjust encoding settings to balance size and data integrity when creating DotCode barcodes for inventory, tracking, or authentication purposes.
// Prompt: Provide configuration to set DotCode error correction level for improved data integrity.
// Tags: dotcode, error-correction, barcode-generation, aspnet, aspnet-core, aspose.barcode, encode-types

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a DotCode barcode with automatic error‑correction settings using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Configures the barcode generator and saves the resulting image.
    /// </summary>
    static void Main()
    {
        // Sample data to encode in the DotCode barcode.
        string codeText = "Sample DotCode Data";

        // Initialize the barcode generator for DotCode symbology with the provided text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, codeText))
        {
            // Set the encode mode to Auto, which selects the most compact encoding
            // and activates the built‑in error‑correction mechanisms of DotCode.
            generator.Parameters.Barcode.DotCode.DotCodeEncodeMode = DotCodeEncodeMode.Auto;

            // Optional: specify the ECI encoding (UTF‑8) to support Unicode characters.
            generator.Parameters.Barcode.DotCode.ECIEncoding = ECIEncodings.UTF8;

            // Save the generated barcode as a PNG image file.
            generator.Save("dotcode.png");
        }

        // Inform the user that the barcode has been generated.
        Console.WriteLine("DotCode barcode generated successfully.");
    }
}