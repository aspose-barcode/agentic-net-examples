// Title: Generate DataMatrix barcode with top caption, padding and centered alignment
// Description: Creates a DataMatrix barcode image, adds a caption above it, pads the top by 10 pixels, and centers the caption.
// Category-Description: This example belongs to the Aspose.BarCode generation category, demonstrating how to customize barcode appearance using the BarcodeGenerator class. It covers setting caption text, alignment, and image padding—common tasks when integrating barcodes into reports, labels, or UI elements. Developers often need to adjust visual layout without altering the encoded data, and this snippet shows the essential API calls for such scenarios.
// Prompt: Develop a script that generates DataMatrix barcodes with top caption padded 10 pixels and centered alignment.
// Tags: datamatrix, caption, padding, alignment, png, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace DataMatrixCaptionExample
{
    /// <summary>
    /// Demonstrates generating a DataMatrix barcode with a top caption,
    /// applying 10‑pixel top padding and centering the caption text.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the example. Generates the barcode and saves it as a PNG file.
        /// </summary>
        static void Main()
        {
            // Define the output file path for the generated barcode image.
            string outputPath = "datamatrix_caption.png";

            // Initialize a BarcodeGenerator for DataMatrix with sample codetext.
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Sample123"))
            {
                // Set the caption text that will appear above the barcode.
                generator.Parameters.CaptionAbove.Text = "Top Caption";

                // Align the caption horizontally to the center.
                generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;

                // Apply a 10‑pixel padding to the top side of the barcode image.
                generator.Parameters.Barcode.Padding.Top.Pixels = 10f;

                // Optional visual settings: white background and black bars.
                generator.Parameters.BackColor = Color.White;
                generator.Parameters.Barcode.BarColor = Color.Black;

                // Save the configured barcode image to the specified path.
                generator.Save(outputPath);
            }

            // Inform the user that the barcode has been saved.
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}