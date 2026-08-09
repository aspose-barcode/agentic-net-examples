// Title: Rotate MaxiCode barcode and save as GIF
// Description: Demonstrates generating a MaxiCode barcode, rotating it 90 degrees, and saving the result as a GIF image.
// Category-Description: This example belongs to the Aspose.BarCode image manipulation category, illustrating how to use the BarcodeGenerator class to create a barcode, adjust its rotation via the Parameters.RotationAngle property, and export the image in a specific format such as GIF. Typical use cases include preparing barcodes for printing on rotated media, integrating rotated barcodes into UI assets, or meeting layout requirements. Developers working with barcode generation often need to control orientation and output format, and this snippet shows the essential steps.
// Prompt: Rotate a generated MaxiCode barcode by 90 degrees and save the rotated image as GIF.
// Tags: maxicode, rotation, gif, barcode generation, aspose.barcode, image export

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a MaxiCode barcode, rotates it by 90 degrees, and saves the image as a GIF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, applies rotation, and writes the output file.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the rotated GIF image.
        string outputPath = "maxicode_rotated.gif";

        // Initialize a BarcodeGenerator for the MaxiCode symbology with the desired code text.
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Test"))
        {
            // Set the rotation angle to 90 degrees to rotate the generated barcode image.
            generator.Parameters.RotationAngle = 90f;

            // Save the rotated barcode to a memory stream in GIF format.
            using (var memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Gif);

                // Write the GIF data from the memory stream to the specified output file.
                File.WriteAllBytes(outputPath, memoryStream.ToArray());
            }
        }
    }
}