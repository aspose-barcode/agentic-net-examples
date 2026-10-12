// Title: Generate a square DataMatrix barcode with default size
// Description: Demonstrates creating a DataMatrix barcode in square shape using Aspose.BarCode and saving it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode parameters such as symbology, version, and output format. It uses the BarcodeGenerator class with EncodeTypes.DataMatrix and shows typical steps like setting the DataMatrix version, generating the image, and saving it to disk. Developers working with 2‑D barcodes often need to produce square DataMatrix symbols for labeling, inventory, or tracking applications.
// Prompt: Generate a DataMatrix barcode with square shape and default size for given alphanumeric CodeText.
// Tags: datamatrix, barcode, generation, png, aspnet, aspose.barcode, encode types, square shape

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a square DataMatrix barcode and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts optional command‑line argument for the barcode text, generates the barcode, and writes the output path to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument is used as the CodeText if provided.</param>
    static void Main(string[] args)
    {
        // Use the first command‑line argument as the barcode text, or fall back to a default value.
        string codeText = args.Length > 0 ? args[0] : "Sample123";

        // Create a temporary directory to store the generated image.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Build the full file path for the PNG output.
        string outputPath = Path.Combine(outputDir, "DataMatrix.png");

        // Generate the DataMatrix barcode and save it to the specified path.
        GenerateDataMatrix(codeText, outputPath);

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"DataMatrix barcode saved to: {outputPath}");
    }

    /// <summary>
    /// Generates a square DataMatrix barcode using the provided text and saves it as a PNG file.
    /// </summary>
    /// <param name="codeText">The alphanumeric text to encode in the barcode.</param>
    /// <param name="outputPath">The file system path where the PNG image will be saved.</param>
    static void GenerateDataMatrix(string codeText, string outputPath)
    {
        // Initialize the barcode generator with DataMatrix symbology and the supplied text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Ensure a square shape by selecting a version that can contain the text.
            generator.Parameters.Barcode.DataMatrix.Version = DataMatrixVersion.ECC200_32x32;

            // Save the generated barcode image in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }
}