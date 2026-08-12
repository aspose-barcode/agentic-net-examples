// Title: Generate and Auto‑Rotate Barcode Image Based on Orientation Metadata
// Description: Demonstrates creating a barcode image and automatically rotating it according to an orientation value stored in a metadata file.
// Category-Description: This example belongs to the Aspose.BarCode image generation and manipulation category. It shows how to use BarcodeGenerator, EncodeTypes, and the RotationAngle parameter to produce a barcode and adjust its orientation. Developers often need to align barcodes with physical media or UI layouts, and this pattern illustrates reading external metadata and applying rotation before saving the image.
// Prompt: Implement feature to automatically rotate generated barcode image to match specified orientation metadata.
// Tags: barcode, code128, rotation, image generation, aspose.barcode, encode types, metadata, png, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a barcode and rotates it based on external orientation metadata.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Prepares sample data, ensures metadata exists, generates the barcode, and reports the output location.
    /// </summary>
    static void Main()
    {
        // Sample barcode configuration
        string barcodeType = "Code128";
        string codeText = "12345";

        // Define output paths in the temporary folder
        string outputPath = Path.Combine(Path.GetTempPath(), "rotated_barcode.png");
        string metadataPath = Path.Combine(Path.GetTempPath(), "barcode_orientation.txt");

        // Create a simple metadata file for demonstration (angle = 90 degrees)
        if (!File.Exists(metadataPath))
        {
            File.WriteAllText(metadataPath, "90");
        }

        // Generate the barcode image applying rotation from metadata
        GenerateBarcodeWithAutoRotation(barcodeType, codeText, outputPath, metadataPath);

        // Inform the user where the image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }

    /// <summary>
    /// Generates a barcode image and rotates it according to an orientation value read from a metadata file.
    /// </summary>
    /// <param name="symbologyName">Name of the barcode symbology (e.g., "Code128").</param>
    /// <param name="codeText">Text to encode in the barcode.</param>
    /// <param name="outputFile">File path where the generated image will be saved.</param>
    /// <param name="metadataFile">File path containing the rotation angle (in degrees).</param>
    static void GenerateBarcodeWithAutoRotation(string symbologyName, string codeText, string outputFile, string metadataFile)
    {
        // Resolve the symbology name to an EncodeTypes value using reflection
        var field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {symbologyName}");
            return;
        }
        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Default rotation angle (no rotation)
        float rotationAngle = 0f;

        // Attempt to read rotation angle from the metadata file
        if (File.Exists(metadataFile))
        {
            string content = File.ReadAllText(metadataFile).Trim();
            if (float.TryParse(content, out float parsedAngle))
            {
                rotationAngle = parsedAngle;
            }
            else
            {
                Console.WriteLine($"Invalid rotation angle in metadata file: {content}");
            }
        }

        // Create the barcode generator, apply the rotation, and save the image
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            generator.Parameters.RotationAngle = rotationAngle;
            generator.Save(outputFile);
        }
    }
}