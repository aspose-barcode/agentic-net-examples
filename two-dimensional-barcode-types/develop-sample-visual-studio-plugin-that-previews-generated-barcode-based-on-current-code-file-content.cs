// Title: Visual Studio Plugin Sample: Barcode Preview from Code File
// Description: Demonstrates generating a barcode image from the contents of a source file, illustrating the core Aspose.BarCode API used in a Visual Studio extension preview.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to create barcodes programmatically with the BarcodeGenerator class. Typical use cases include previewing barcodes in IDE extensions, generating labels, or embedding barcodes in documents. Developers often need to select symbologies, configure dimensions, and output common image formats such as PNG.
// Prompt: Develop a sample Visual Studio plugin that previews generated barcode based on current code file content.
// Tags: barcode symbology, generation, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Sample console application that mimics the core barcode generation logic
/// which would be used inside a Visual Studio extension to preview a barcode
/// based on the current code file's content.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Reads a source file (if provided), selects a barcode symbology,
    /// generates the barcode image, and saves it as PNG.
    /// </summary>
    /// <param name="args">
    /// args[0] – optional path to a source file whose text will be encoded.
    /// args[1] – optional name of the barcode symbology (e.g., "Code128").
    /// </param>
    static void Main(string[] args)
    {
        // Determine the source file to read. If a valid path is supplied as the first argument,
        // use its contents; otherwise fall back to a sample text.
        string sourcePath = args.Length > 0 ? args[0] : null;
        string codeText;

        if (!string.IsNullOrEmpty(sourcePath) && File.Exists(sourcePath))
        {
            // Read the entire file content to encode into the barcode.
            codeText = File.ReadAllText(sourcePath);
        }
        else
        {
            // Use a default placeholder when no file is provided.
            codeText = "SampleBarcode";
        }

        // Determine the barcode symbology. If a second argument is supplied,
        // resolve it to an EncodeTypes field via reflection; otherwise default to Code128.
        string symbologyName = args.Length > 1 ? args[1] : "Code128";
        var field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology '{symbologyName}'. Defaulting to Code128.");
            field = typeof(EncodeTypes).GetField("Code128");
        }

        // Cast the reflected field value to the base encode type used by the generator.
        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Create the barcode generator, configure a few basic parameters, and save the image.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Example of setting module size and image dimensions.
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Save the barcode as a PNG file in the current directory.
            string outputFile = "barcode.png";
            generator.Save(outputFile);
            Console.WriteLine($"Barcode generated and saved to '{outputFile}'.");
        }
    }
}