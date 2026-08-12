// Title: Visual Studio plugin sample that generates a QR code preview from source file content
// Description: Demonstrates reading a C# source file (or fallback text) and creating a QR code image using Aspose.BarCode. The image is saved to a temporary location for preview.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator with EncodeTypes.QR, configure basic parameters, and export to PNG. Developers working on IDE extensions, reporting tools, or any scenario that needs on‑the‑fly barcode creation can adapt this pattern. Typical use cases include previewing barcodes in Visual Studio extensions, generating documentation assets, or embedding barcodes in reports.
// Prompt: Develop a sample Visual Studio plugin that previews generated barcode based on current code file content.
// Tags: qr code, barcode generation, image output, aspose.barcode, aspose.drawing, visual studio extension

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Sample console application that could be used as the core of a Visual Studio extension
/// to generate a QR code preview from the current code file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads a source file path from arguments or locates the program's own .cs file,
    /// generates a QR code image, and saves it to a temporary PNG file.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument may be a path to a source file.</param>
    static void Main(string[] args)
    {
        // ------------------------------------------------------------
        // Determine the source code file to read
        // ------------------------------------------------------------
        string sourcePath = null;
        if (args.Length > 0 && File.Exists(args[0]))
        {
            // Use the file path supplied via command line
            sourcePath = args[0];
        }
        else
        {
            // Attempt to locate this program's .cs file in the same directory as the executable
            string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string dir = Path.GetDirectoryName(exePath);
            string candidate = Path.Combine(dir, "Program.cs");
            if (File.Exists(candidate))
                sourcePath = candidate;
        }

        // ------------------------------------------------------------
        // Read file content or fall back to a default string
        // ------------------------------------------------------------
        string codeText;
        if (!string.IsNullOrEmpty(sourcePath) && File.Exists(sourcePath))
        {
            codeText = File.ReadAllText(sourcePath);
        }
        else
        {
            codeText = "Sample barcode preview content";
        }

        // ------------------------------------------------------------
        // QR codes have a practical length limit; truncate if necessary
        // ------------------------------------------------------------
        if (codeText.Length > 2000)
            codeText = codeText.Substring(0, 2000);

        // ------------------------------------------------------------
        // Prepare output path (temporary folder)
        // ------------------------------------------------------------
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode_preview.png");

        // ------------------------------------------------------------
        // Generate the barcode image using Aspose.BarCode
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Example parameter customizations
            generator.Parameters.Barcode.XDimension.Point = 2f;               // Set module size
            generator.Parameters.Barcode.FilledBars = false;                // Use non‑filled bars
            generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false; // Suppress validation errors

            // Save the generated QR code as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the preview image was saved
        Console.WriteLine($"Barcode preview saved to: {outputPath}");
    }
}