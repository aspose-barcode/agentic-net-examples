// Title: Generate QR Code with Zero Margin and Save as JPEG
// Description: Demonstrates how to create a QR Code barcode using Aspose.BarCode, set all padding to zero, and export the image as a JPEG file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Typical use cases include creating compact QR codes for web or mobile applications where minimal whitespace is required. Developers often need to control barcode padding, symbology, and output format when integrating barcode generation into automated workflows.
// Prompt: Generate a QR Code barcode with margin set to zero and save as JPEG image.
// Tags: qr code, zero margin, barcode generation, jpeg output, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeExample
{
    /// <summary>
    /// Demonstrates generating a QR Code barcode with zero margin and saving it as a JPEG image.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the example. Creates a QR Code, removes all padding, and saves the result.
        /// </summary>
        static void Main()
        {
            // Define the output file name and format
            const string outputPath = "qr_zero_margin.jpg";

            // Initialize the barcode generator with QR symbology and the desired text
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
            {
                // Remove all padding (margin) by setting each side to 0 points
                generator.Parameters.Barcode.Padding.Left.Point = 0f;
                generator.Parameters.Barcode.Padding.Right.Point = 0f;
                generator.Parameters.Barcode.Padding.Top.Point = 0f;
                generator.Parameters.Barcode.Padding.Bottom.Point = 0f;

                // Save the generated barcode as a JPEG image
                generator.Save(outputPath, BarCodeImageFormat.Jpeg);
            }

            // Output the location of the saved file for verification
            Console.WriteLine($"QR Code saved to {outputPath}");
        }
    }
}