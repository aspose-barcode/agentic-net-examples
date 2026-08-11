// Title: Generate Code128 barcode without human‑readable text and save as PNG
// Description: Demonstrates creating a Code128 barcode, disabling the displayed code text, and saving the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode appearance using BarcodeGenerator and its Parameters. Developers commonly use these APIs to customize symbology, hide or position human‑readable text, and export barcodes in various image formats for integration into documents, labels, or web applications.
// Prompt: Generate a barcode, disable ShowCodeText, and confirm output contains only the barcode pattern.
// Tags: code128, hidecodetext, png, barcodegenerator, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeSample
{
    /// <summary>
    /// Provides a simple console application that generates a Code128 barcode,
    /// disables the human‑readable text, and saves the image as a PNG file.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the application. Creates the barcode, configures visual settings,
        /// and writes the output file to the current directory.
        /// </summary>
        static void Main()
        {
            // Initialize a BarcodeGenerator for Code128 with the desired code text ("12345").
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
            {
                // Hide the human‑readable text by setting its location to None (equivalent to disabling ShowCodeText).
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

                // Determine the full path for the output PNG file in the current working directory.
                string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

                // Save the generated barcode image to the specified path in PNG format.
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            // Inform the user that the barcode has been generated without the code text.
            Console.WriteLine("Barcode generated and saved to 'barcode.png' with ShowCodeText disabled.");
        }
    }
}