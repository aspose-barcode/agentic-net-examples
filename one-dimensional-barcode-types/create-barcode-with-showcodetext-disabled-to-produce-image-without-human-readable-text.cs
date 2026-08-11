// Title: Generate Code128 Barcode without Human‑Readable Text
// Description: Demonstrates how to create a Code128 barcode image with the human‑readable text hidden, saving it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to control barcode appearance. Developers often need to produce barcodes without the accompanying code text for cleaner scans or aesthetic reasons, such as in packaging or labeling scenarios.
// Prompt: Create a barcode with ShowCodeText disabled to produce an image without human‑readable text.
// Tags: code128, barcode generation, hide codetext, png output, aspose.barcode, barcodegenerator

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeExample
{
    /// <summary>
    /// Demonstrates creating a barcode image with the code text hidden.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Creates a Code128 barcode without human‑readable text and saves it as a PNG file.
        /// </summary>
        static void Main()
        {
            // Define the output file path for the generated barcode image.
            string outputPath = "barcode.png";

            // Initialize a BarcodeGenerator for Code128 symbology with the desired data.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
            {
                // Hide the human‑readable text by setting its location to None.
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

                // Save the generated barcode as a PNG image to the specified path.
                generator.Save(outputPath);
            }

            // Inform the user where the barcode image has been saved.
            Console.WriteLine($"Barcode image saved to: {outputPath}");
        }
    }
}