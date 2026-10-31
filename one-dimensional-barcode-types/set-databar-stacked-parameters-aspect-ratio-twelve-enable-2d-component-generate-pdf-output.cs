// Title: Generate DataBar Stacked Barcode with 2D Composite Component and Save as PDF
// Description: Demonstrates how to create a DataBar Stacked barcode, set its aspect ratio, enable the 2‑D composite component, and export the result to a PDF file using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator with EncodeTypes.DatabarStacked. It shows how to configure DataBar specific parameters such as aspect ratio and 2D composite component, and how to save the generated barcode in PDF format. Developers working with product identification, retail scanning, or any scenario requiring high‑density stacked barcodes will find these API calls essential.
// Prompt: Set DataBar stacked parameters aspect ratio twelve, enable 2D component, generate PDF output.
// Tags: databar stacked, aspect ratio, 2d component, pdf output, aspose.barcode, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a DataBar Stacked barcode with a custom aspect ratio,
/// enables the 2‑D composite component, and saves the result as a PDF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes it to a PDF file.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output PDF file in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "DataBarStacked.pdf");

        // Initialize the barcode generator for the DataBar Stacked symbology with sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.DatabarStacked, "(01)12345678901231"))
        {
            // Optional: set the module (X) size in pixels to control barcode density.
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Set the DataBar aspect ratio to 12, as required by the example scenario.
            generator.Parameters.Barcode.DataBar.AspectRatio = 12;

            // Enable the 2‑D composite component to embed additional data within the barcode.
            generator.Parameters.Barcode.DataBar.Is2DCompositeComponent = true;

            try
            {
                // Save the generated barcode as a PDF document.
                generator.Save(outputPath, BarCodeImageFormat.Pdf);
                Console.WriteLine($"Barcode saved to PDF: {outputPath}");
            }
            catch (BarCodeException ex)
            {
                // Handle evaluation‑edition limitations or other barcode generation errors.
                Console.WriteLine("Failed to save as PDF due to evaluation limitations:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}