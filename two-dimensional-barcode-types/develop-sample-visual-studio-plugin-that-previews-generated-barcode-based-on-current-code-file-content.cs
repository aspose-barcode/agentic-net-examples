// Title: Generate a Code128 barcode preview from file or sample text
// Description: Demonstrates reading source content, truncating it, and creating a PNG barcode image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator, EncodeTypes, and image format classes to produce barcodes. Typical use cases include creating printable labels, embedding barcodes in documents, or previewing barcodes in development tools. Developers often need to configure appearance, resolution, and output format while handling input text safely.
// Prompt: Develop a sample Visual Studio plugin that previews generated barcode based on current code file content.
// Tags: barcode symbology, barcode generation, png output, aspose.barcode, aspose.drawing, code128

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Console application that reads text (from a file or fallback), generates a Code128 barcode,
/// and saves it as a PNG image for preview purposes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Handles input, barcode creation, and output path reporting.
    /// </summary>
    /// <param name="args">Command‑line arguments; optionally the path to a source text file.</param>
    static void Main(string[] args)
    {
        // Determine source file to read. If a path is provided as an argument and the file exists, use it.
        // Otherwise, use a fallback sample text.
        string sourceContent;
        if (args.Length > 0 && File.Exists(args[0]))
        {
            try
            {
                // Read the entire file using UTF‑8 encoding.
                sourceContent = File.ReadAllText(args[0], Encoding.UTF8);
            }
            catch (Exception ex)
            {
                // On failure, log the error and fall back to a default string.
                Console.WriteLine($"Failed to read file '{args[0]}': {ex.Message}");
                sourceContent = "FallbackSample";
            }
        }
        else
        {
            // No valid file argument supplied; use a predefined sample.
            sourceContent = "FallbackSample";
        }

        // Truncate content to a reasonable length for barcode generation (e.g., 100 characters).
        if (sourceContent.Length > 100)
        {
            sourceContent = sourceContent.Substring(0, 100);
        }

        // Prepare output path in the temporary folder.
        string outputPath = Path.Combine(Path.GetTempPath(), "preview_barcode.png");

        // Generate barcode using Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, ""))
        {
            // Set the code text with explicit UTF‑8 encoding.
            generator.SetCodeText(sourceContent, Encoding.UTF8);

            // Optional appearance settings.
            generator.Parameters.Barcode.BarColor = Color.Black;   // Barcode bars color.
            generator.Parameters.BackColor = Color.White;          // Background color.
            generator.Parameters.Barcode.XDimension.Point = 2f;   // Module width.
            generator.Parameters.Resolution = 300f;                // Image resolution (dpi).

            // Save the barcode image in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the preview image was saved.
        Console.WriteLine($"Barcode preview generated at: {outputPath}");
        Console.WriteLine("Note: This console sample demonstrates core barcode generation logic.");
        Console.WriteLine("A full Visual Studio plugin would embed the image in the IDE UI, which is not possible in this console environment.");
    }
}