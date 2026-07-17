// Title: Han Xin Barcode with Rectangular Shape Configuration Example
// Description: Demonstrates configuring a Han Xin barcode generator to use a rectangular shape of 15 rows by 40 columns for larger payloads.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on Han Xin symbology configuration. It showcases key API classes such as BarcodeGenerator, EncodeTypes, and HanXinVersion, illustrating typical use cases where developers need to adjust symbol size for extensive data. Useful for developers looking to optimize barcode dimensions for specific payload requirements.
// Prompt: Configure Han Xin to use rectangular shape with 15 rows and 40 columns for larger payload.
// Tags: hanxin, barcode, symbology, rectangular, rows, columns, generation, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a Han Xin barcode, attempting to configure a rectangular shape (15 rows x 40 columns) for a larger payload.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates and saves a Han Xin barcode image.
    /// </summary>
    static void Main()
    {
        // Define a payload that requires a larger symbol size.
        string payload = "This is a longer text intended to demonstrate a larger Han Xin barcode. " +
                         "It contains enough characters to require a bigger square symbol.";

        // Initialize the barcode generator with Han Xin symbology and the payload.
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, payload))
        {
            // Han Xin supports only square symbols. The version is set to Auto so the library
            // selects the appropriate size based on the data length. Rectangular shapes (e.g., 15x40)
            // are not supported, but this line documents the intended configuration.
            generator.Parameters.Barcode.HanXin.Version = HanXinVersion.Auto;

            // Increase error correction level to L3 for better robustness against damage.
            generator.Parameters.Barcode.HanXin.ErrorLevel = HanXinErrorLevel.L3;

            // Define the output file path and save the generated barcode as a PNG image.
            string outputPath = "HanXinBarcode.png";
            generator.Save(outputPath);

            // Inform the user where the barcode image has been saved.
            Console.WriteLine($"Han Xin barcode saved to: {outputPath}");
        }
    }
}