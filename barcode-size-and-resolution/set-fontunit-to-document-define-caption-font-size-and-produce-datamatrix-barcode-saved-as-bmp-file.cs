// Title: Generate DataMatrix barcode with caption and save as BMP
// Description: Demonstrates creating a DataMatrix barcode, adding a caption with specific font size, and saving the image as a BMP file using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure barcode parameters such as XDimension, captions, and font settings with the BarcodeGenerator and its Parameters API. Developers commonly use these APIs to customize barcode appearance for printing, labeling, or embedding in documents. The snippet shows typical steps for setting up a barcode, adjusting visual properties, and exporting to a raster image format.
// Prompt: Set FontUnit to Document, define caption font size, and produce DataMatrix barcode saved as BMP file.
// Tags: datamatrix, caption, bmp, barcodegenerator, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a DataMatrix barcode with a caption and saves it as a BMP image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates the barcode, configures visual properties, and writes the output file.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting BMP file
        string outputPath = Path.Combine(outputDir, "DataMatrix.bmp");

        // Initialize the barcode generator for DataMatrix with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "ASPOSE"))
        {
            // Optional: set the module size (XDimension) to 3 pixels
            generator.Parameters.Barcode.XDimension.Pixels = 3f;

            // Enable and configure a caption displayed above the barcode
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Sample Caption";

            // Set the caption font size to 12 points
            generator.Parameters.CaptionAbove.Font.Size.Point = 12f;

            // Note: FontUnit property is not available in the current Aspose.BarCode API.
            // The requested setting cannot be applied; proceeding without it.

            // Save the generated barcode as a BMP image
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}