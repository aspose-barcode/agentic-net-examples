// Title: Change DataMatrix barcode text color to orange
// Description: Demonstrates how to set the human‑readable text color of a DataMatrix barcode to orange while keeping the default background.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize visual properties of barcodes. It uses the BarcodeGenerator class together with EncodeTypes, BarCodeImageFormat, and CodeTextParameters to modify text appearance. Developers often need to adjust colors for branding or UI integration, and this snippet shows the typical steps for such customizations in C#.
/// <summary>
/// Shows how to generate a DataMatrix barcode and change only its text color to orange.
/// </summary>
using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeExample
{
    /// <summary>
    /// Entry point for the barcode generation example.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Generates a DataMatrix barcode with orange human‑readable text and saves it as a PNG file.
        /// </summary>
        static void Main()
        {
            // Define the output file path for the generated barcode image.
            string outputPath = "datamatrix.png";

            // Initialize the barcode generator for DataMatrix with sample content.
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Sample Text"))
            {
                // Set the color of the human‑readable text to orange.
                generator.Parameters.Barcode.CodeTextParameters.Color = Color.Orange;

                // Save the barcode image in PNG format to the specified path.
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            // Inform the user where the barcode image has been saved.
            Console.WriteLine($"DataMatrix barcode saved to {outputPath}");
        }
    }
}