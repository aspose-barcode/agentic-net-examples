// Title: Create GS1 DataMatrix barcode with grayscale filter and save as TIFF
// Description: Demonstrates generating a GS1 DataMatrix barcode, converting the image to grayscale, and saving it as a TIFF file.
// Category-Description: This example belongs to the Aspose.BarCode image processing category, illustrating how to use BarcodeGenerator to create GS1 DataMatrix symbols, manipulate the resulting Bitmap with Aspose.Drawing, and export the image in a specific format. Developers often need to generate barcodes, apply visual effects, and store them in formats like TIFF for printing or archival purposes.
// Prompt: Create a GS1 DataMatrix barcode, apply a grayscale filter, and save the result as a TIFF image.
// Tags: datamatrix, gs1, grayscale, tiff, aspose.barcode, imageprocessing, bitmap

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a GS1 DataMatrix barcode, applies a grayscale filter, and saves the result as a TIFF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, processes the image, and writes the output file.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "GS1DataMatrix_Grayscale.tif");

        // GS1 DataMatrix barcode text containing a valid GTIN-14 Application Identifier (01).
        string gs1CodeText = "(01)00123456789012";

        // Initialize the barcode generator for GS1 DataMatrix with the specified text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, gs1CodeText))
        {
            // Generate the barcode image as a Bitmap.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Iterate over each pixel to convert the image to grayscale.
                for (int y = 0; y < bitmap.Height; y++)
                {
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        // Retrieve the original pixel color.
                        Color original = bitmap.GetPixel(x, y);

                        // Compute the luminance using standard NTSC coefficients.
                        int gray = (int)(original.R * 0.3f + original.G * 0.59f + original.B * 0.11f);

                        // Create a new grayscale color preserving the original alpha channel.
                        Color grayColor = Color.FromArgb(original.A, gray, gray, gray);

                        // Set the pixel to the new grayscale color.
                        bitmap.SetPixel(x, y, grayColor);
                    }
                }

                // Save the processed bitmap as a TIFF image to the defined path.
                bitmap.Save(outputPath, ImageFormat.Tiff);
            }
        }

        // Inform the user where the file has been saved.
        Console.WriteLine($"GS1 DataMatrix barcode saved as grayscale TIFF at: {outputPath}");
    }
}