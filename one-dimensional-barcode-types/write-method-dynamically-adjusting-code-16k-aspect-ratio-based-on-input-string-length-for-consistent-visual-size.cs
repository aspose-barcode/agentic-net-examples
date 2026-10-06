// Title: Dynamic Code 16K Barcode Generation with Length‑Based Aspect Ratio
// Description: Demonstrates how to generate Code 16K barcodes using Aspose.BarCode while adjusting the aspect ratio according to the input string length to keep the visual size consistent.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on symbology configuration and image output. It showcases the BarcodeGenerator class, EncodeTypes.Code16K, and the Code16K.AspectRatio property, which developers commonly use when they need to produce scalable Code 16K barcodes for varying data lengths in applications such as inventory, shipping, or document automation.
// Prompt: Write method dynamically adjusting Code 16K aspect ratio based on input string length for consistent visual size.
// Tags: barcode, code16k, aspectratio, dynamic, image, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates Code 16K barcodes with an aspect ratio that scales based on the length of the input string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a temporary folder, generates sample barcodes of different lengths, and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for output images
        string outputFolder = Path.Combine(Path.GetTempPath(), "Code16K_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Sample code texts of varying lengths
        string[] samples = new string[]
        {
            "A",
            "ABCDEFGHIJ",
            "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
            "1234567890123456789012345678901234567890"
        };

        // Generate a barcode for each sample text
        foreach (string text in samples)
        {
            GenerateCode16K(text, outputFolder);
        }

        Console.WriteLine("All barcodes generated in: " + outputFolder);
    }

    /// <summary>
    /// Generates a Code 16K barcode, adjusting its aspect ratio based on the length of <paramref name="codeText"/>.
    /// </summary>
    /// <param name="codeText">The data to encode in the barcode.</param>
    /// <param name="folderPath">The folder where the generated image will be saved.</param>
    static void GenerateCode16K(string codeText, string folderPath)
    {
        // Base aspect ratio for a reference length of 10 characters
        const float baseAspect = 10f;
        const int referenceLength = 10;

        // Calculate aspect ratio proportionally to the length of the code text
        float aspect = baseAspect * codeText.Length / referenceLength;
        if (aspect < 8f)
            aspect = 8f; // Minimum recommended value

        // Initialize the barcode generator with Code16K symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
        {
            // Set X-dimension (module width) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Apply the calculated aspect ratio
            generator.Parameters.Barcode.Code16K.AspectRatio = aspect;

            // Build the output file name and path
            string fileName = $"Code16K_{codeText.Length}.png";
            string filePath = Path.Combine(folderPath, fileName);

            // Save the barcode image as PNG
            generator.Save(filePath, BarCodeImageFormat.Png);

            Console.WriteLine($"Generated Code16K barcode (length={codeText.Length}, aspect={aspect}) -> {filePath}");
        }
    }
}