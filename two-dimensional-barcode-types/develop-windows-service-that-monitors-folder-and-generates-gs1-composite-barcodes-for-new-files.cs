using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating GS1 Composite barcodes from files in a folder using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Processes each file in the input folder, creates a GS1 Composite barcode, and saves it to the output folder.
    /// </summary>
    /// <param name="args">
    /// Optional command‑line arguments:
    /// args[0] – input folder path,
    /// args[1] – output folder path.
    /// If not provided, temporary folders are used.
    /// </param>
    static void Main(string[] args)
    {
        // Determine input and output directories (use temporary folders as defaults)
        string inputFolder = args.Length > 0 ? args[0] : Path.Combine(Path.GetTempPath(), "BarcodeInput");
        string outputFolder = args.Length > 1 ? args[1] : Path.Combine(Path.GetTempPath(), "BarcodeOutput");

        // Verify that the input folder exists; abort if it does not
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure the output folder exists (create it if necessary)
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Retrieve all files in the input folder (non‑recursive)
        string[] files = Directory.GetFiles(inputFolder);
        if (files.Length == 0)
        {
            Console.WriteLine("No files found to process.");
            return;
        }

        // Process each file individually
        foreach (string filePath in files)
        {
            try
            {
                // Derive a base name for the output image from the source file name
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                string outputPath = Path.Combine(outputFolder, $"{fileName}_GS1Composite.png");

                // Read the entire file content (used as codetext if it contains a '|' separator)
                string fileContent = File.ReadAllText(filePath);

                // Sample linear and 2D components for GS1 Composite codetext
                string linearPart = "(01)03212345678906"; // GTIN
                string twoDPart = "(21)A12345678";       // Serial number

                // Determine the final codetext:
                // - If the file already contains a '|' separator, treat the whole content as codetext.
                // - Otherwise, combine the sample linear and 2D parts.
                string codetext = fileContent.Contains("|")
                    ? fileContent.Trim()
                    : $"{linearPart}|{twoDPart}";

                // Initialize the barcode generator for GS1 Composite barcodes
                BaseEncodeType encodeType = EncodeTypes.GS1CompositeBar;
                using (var generator = new BarcodeGenerator(encodeType, codetext))
                {
                    // Set the component types: linear part as GS1‑Code128, 2D part as CC‑A
                    generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
                    generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

                    // Optional visual settings
                    generator.Parameters.Barcode.Pdf417.AspectRatio = 3f;
                    generator.Parameters.Barcode.XDimension.Pixels = 3f;
                    generator.Parameters.Barcode.BarHeight.Pixels = 100f;

                    // Save the generated barcode image to the output path
                    generator.Save(outputPath);
                }

                Console.WriteLine($"Generated barcode for '{Path.GetFileName(filePath)}' -> {outputPath}");
            }
            catch (Exception ex)
            {
                // Report any errors that occur while processing the current file
                Console.WriteLine($"Error processing file '{Path.GetFileName(filePath)}': {ex.Message}");
            }
        }
    }
}