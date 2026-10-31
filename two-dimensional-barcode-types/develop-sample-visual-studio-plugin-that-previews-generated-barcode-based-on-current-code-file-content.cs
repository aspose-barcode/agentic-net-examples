// Title: Generate and preview a Code128 barcode from the current source file
// Description: This example reads its own source code, trims it to a manageable length, and creates a Code128 barcode image saved to a temporary folder for preview.
// Category-Description: Demonstrates Aspose.BarCode generation in a console context, focusing on the BarcodeGenerator class, EncodeTypes enumeration, and image output via BarCodeImageFormat. Typical use cases include creating barcodes for documents, inventory, or UI previews. Developers often need quick, programmatic barcode creation without UI dependencies, making this pattern a common starting point for integration into larger applications or extensions.
// Prompt: Develop a sample Visual Studio plugin that previews generated barcode based on current code file content.
// Tags: code128, barcode generation, png, aspose.barcode, aspose.drawing, console sample

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode from the program's source file and saving it as a PNG preview.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads the source file, creates a barcode image, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Determine the path of this source file (Program.cs) assuming it resides alongside the executable.
        string exePath = AppDomain.CurrentDomain.BaseDirectory;
        string sourcePath = Path.Combine(exePath, "Program.cs");

        // Verify that the source file exists before attempting to read it.
        if (!File.Exists(sourcePath))
        {
            Console.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Read the entire content of the source file.
        string codeText = File.ReadAllText(sourcePath);
        if (string.IsNullOrWhiteSpace(codeText))
        {
            Console.WriteLine("Source file is empty; nothing to encode.");
            return;
        }

        // Truncate to a reasonable length for barcode generation (Code128 supports long texts, but keep it short for preview).
        const int maxLength = 100;
        if (codeText.Length > maxLength)
        {
            codeText = codeText.Substring(0, maxLength);
        }

        // Prepare output path in a temporary folder.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodePreview");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "CodePreview.png");

        // Generate the barcode using Aspose.BarCode.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Basic appearance settings.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the preview image was saved.
        Console.WriteLine($"Barcode preview generated at: {outputPath}");
        // Note: In a real Visual Studio extension, the image would be displayed in a tool window.
        // This console sample demonstrates the core barcode generation logic.
    }
}