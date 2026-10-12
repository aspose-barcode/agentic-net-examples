// Title: Generate GS1 DataMatrix Barcode, Apply Grayscale Filter, Save as TIFF
// Description: This example creates a GS1 DataMatrix barcode, converts the generated image to grayscale, and saves it as a TIFF file.
// Category-Description: Demonstrates Aspose.BarCode barcode generation (BarcodeGenerator, EncodeTypes) combined with Aspose.Drawing image manipulation (Bitmap, Color). Typical for developers needing to produce machine‑readable GS1 DataMatrix symbols, apply custom visual effects, and export to lossless formats such as TIFF for printing or archival. This snippet belongs to a collection of barcode rendering and post‑processing examples.
// Prompt: Create a GS1 DataMatrix barcode, apply a grayscale filter, and save the result as a TIFF image.
// Tags: gs1datamatrix, datamatrix, barcode, grayscale, tiff, image processing, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates creating a GS1 DataMatrix barcode, applying a grayscale filter, and saving it as a TIFF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates the barcode, processes the image, and writes the output file.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output TIFF file
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "gs1datamatrix_grayscale.tiff");

        // GS1 DataMatrix barcode content (Application Identifier 01 and 21)
        string codeText = "(01)12345678901231(21)ASPOSE";

        // Initialize the barcode generator for GS1 DataMatrix with the specified text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Generate the barcode image as an Aspose.Drawing.Bitmap
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Iterate over each pixel to convert the image to grayscale
                for (int y = 0; y < bitmap.Height; y++)
                {
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        // Retrieve the original pixel color
                        Color original = bitmap.GetPixel(x, y);

                        // Compute the average of RGB components to obtain a gray value
                        int gray = (original.R + original.G + original.B) / 3;

                        // Preserve the original alpha channel while setting RGB to the gray value
                        Color grayColor = Color.FromArgb(original.A, gray, gray, gray);

                        // Apply the grayscale color back to the pixel
                        bitmap.SetPixel(x, y, grayColor);
                    }
                }

                // Save the processed bitmap as a TIFF image using a file stream
                using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    bitmap.Save(fs, ImageFormat.Tiff);
                }
            }
        }

        // Inform the user where the file has been saved
        Console.WriteLine($"GS1 DataMatrix barcode saved as grayscale TIFF at: {outputPath}");
    }
}