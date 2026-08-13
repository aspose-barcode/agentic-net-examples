// Title: Configure DotCode barcode with rectangular layout and 20 columns
// Description: Demonstrates generating a DotCode barcode using Aspose.BarCode, configuring a rectangular layout with 20 columns to increase data capacity.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on DotCode symbology configuration. It shows how to use the BarcodeGenerator class and its Parameters to adjust layout settings such as column count, resolution, and image output. Developers working with high‑density 2‑D barcodes can use similar patterns to customize size, shape, and quality for various output formats.
// Prompt: Configure DotCode to use rectangular layout with 20 columns for increased data capacity.
// Tags: dotcode, barcode, generation, image, aspose.barcode, rectangular layout, columns, resolution

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace DotCodeExample
{
    /// <summary>
    /// Provides an entry point that generates a DotCode barcode with a rectangular layout of 20 columns.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Generates a DotCode barcode, sets a rectangular layout with 20 columns, and saves it as a PNG image.
        /// </summary>
        static void Main()
        {
            // Define the data to be encoded in the barcode.
            string codeText = "Sample DotCode Data";

            // Initialize the BarcodeGenerator for DotCode symbology with the provided text.
            using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, codeText))
            {
                // Set the rectangular layout to use 20 columns.
                // The number of rows will be calculated automatically by the encoder.
                generator.Parameters.Barcode.DotCode.Columns = 20;

                // Optionally specify the image resolution (dots per inch) for higher quality output.
                generator.Parameters.Resolution = 300;

                // Define the output file path and save the generated barcode as a PNG image.
                string outputPath = "dotcode_20cols.png";
                generator.Save(outputPath);

                // Inform the user where the barcode image has been saved.
                Console.WriteLine($"DotCode barcode saved to {outputPath}");
            }
        }
    }
}