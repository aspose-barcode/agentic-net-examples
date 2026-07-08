// Title: Generate Code128 barcode without human‑readable text
// Description: Demonstrates disabling the code text to create a clean barcode image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode appearance using BarcodeGenerator and its Parameters. Developers often need to hide the human‑readable text for printing or embedding barcodes in UI where only the bars are required. The example shows setting CodeTextParameters.Location to CodeLocation.None and saving the result.
// Prompt: Create a barcode with ShowCodeText disabled to produce an image without human‑readable text.
// Tags: code128, hidecodetext, png, generation, aspnet, aspnetcore, aspose.barcode

using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

namespace BarcodeSample
{
    /// <summary>
    /// Demonstrates creating a Code128 barcode image with the human‑readable text disabled.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point. Generates the barcode and saves it as a PNG file.
        /// </summary>
        static void Main()
        {
            // Define the output file path for the generated barcode image.
            string outputPath = "barcode.png";

            // Initialize a BarcodeGenerator for Code128 symbology with sample code text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Disable the human‑readable text (code text) below the barcode.
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

                // Save the generated barcode image in PNG format to the specified path.
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            // Inform the user where the barcode image has been saved.
            Console.WriteLine($"Barcode image saved to {outputPath}");
        }
    }
}