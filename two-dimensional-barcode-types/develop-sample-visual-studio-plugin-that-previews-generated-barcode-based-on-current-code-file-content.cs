// Title: Generate Barcode Preview from Source File Content
// Description: This example reads the content of a source code file (or a default string) and creates a Code128 barcode image for quick visual preview.
// Category-Description: Demonstrates Aspose.BarCode generation capabilities, focusing on the BarcodeGenerator class, EncodeTypes enumeration, and BarCodeImageFormat output options. Typical scenarios include creating barcodes from dynamic text, saving them as image files, and integrating previews into development tools. Developers often need to adjust appearance settings such as colors, resolution, and dimensions when generating barcodes programmatically.
// Prompt: Develop a sample Visual Studio plugin that previews generated barcode based on current code file content.
// Tags: barcode symbology, generation, png, aspose.barcode, code128, preview, file-io

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Sample program that generates a Code128 barcode image from the content of a source file and saves it for preview.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads a source file (or uses a default string), creates a barcode, and writes the PNG to a temporary location.
    /// </summary>
    /// <param name="args">Command‑line arguments; the first argument may specify the source file path.</param>
    static void Main(string[] args)
    {
        // Determine source file path: first argument or fallback to this source file if it exists.
        string sourcePath = args.Length > 0 ? args[0] : "Program.cs";
        string codeContent;

        if (File.Exists(sourcePath))
        {
            // Read the entire file using UTF‑8 encoding.
            codeContent = File.ReadAllText(sourcePath, Encoding.UTF8);
        }
        else
        {
            // Use a placeholder when the file cannot be found.
            codeContent = "No source file found. Using default text.";
        }

        // Limit length to avoid excessively large barcodes.
        if (codeContent.Length > 200)
        {
            codeContent = codeContent.Substring(0, 200);
        }

        // Define the temporary output path for the PNG image.
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode_preview.png");

        // Generate barcode image using Aspose.BarCode.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeContent))
        {
            // Basic appearance settings.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;
            generator.Parameters.Resolution = 300f;
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save to PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode preview saved to: {outputPath}");
    }
}