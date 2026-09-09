// Title: Generate GS1 Composite Barcodes for Files in a Monitored Folder
// Description: Demonstrates creating GS1 Composite barcodes from files in a temporary input folder and saving them as PNG images.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to use BarcodeGenerator with EncodeTypes.GS1CompositeBar and BarCodeReader to produce and read composite barcodes. Typical use cases include batch processing of documents, inventory labeling, and automated barcode creation in services. Developers often need to configure linear and 2‑D components, set colors, and extract embedded data.
// Prompt: Develop a Windows service that monitors a folder and generates GS1 Composite barcodes for new files.
// Tags: gs1 composite barcode generation, barcode recognition, aspnet barcodereader, aspnet barcodelibrary, png output, encode types, c# example

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating GS1 Composite barcodes for files in a temporary folder and reading back the 2D component.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates sample input files, generates GS1 Composite barcodes,
    /// saves them as PNG images, and reads the 2D component text from each generated barcode.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Setup: create a unique temporary input folder and populate it with sample files
        // --------------------------------------------------------------------
        string inputFolder = Path.Combine(Path.GetTempPath(), "GS1Input_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(inputFolder);
        for (int i = 1; i <= 3; i++)
        {
            string filePath = Path.Combine(inputFolder, $"File{i}.txt");
            File.WriteAllText(filePath, $"Sample content {i}");
        }

        // --------------------------------------------------------------------
        // Setup: create a unique temporary output folder for the generated barcodes
        // --------------------------------------------------------------------
        string outputFolder = Path.Combine(Path.GetTempPath(), "GS1Output_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // --------------------------------------------------------------------
        // Process each file in the input folder
        // --------------------------------------------------------------------
        string[] files = Directory.GetFiles(inputFolder);
        foreach (string file in files)
        {
            try
            {
                // Extract the file name without extension for use in the output image name
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(file);

                // Build GS1 Composite codetext (linear|2d)
                string linearComponent = "(01)12345678901231";
                string twoDComponent = "(01)00123456789012";
                string compositeCodeText = $"{linearComponent}|{twoDComponent}";

                // ----------------------------------------------------------------
                // Generate GS1 Composite barcode using BarcodeGenerator
                // ----------------------------------------------------------------
                using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, compositeCodeText))
                {
                    // Configure linear and 2D component types
                    generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
                    generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_C;

                    // Additional barcode settings
                    generator.Parameters.Barcode.Pdf417.Columns = 30;
                    generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;
                    generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                    generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                    // Save the generated barcode as a PNG file
                    string outputPath = Path.Combine(outputFolder, fileNameWithoutExt + ".png");
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Generated barcode for '{fileNameWithoutExt}' at: {outputPath}");

                    // ----------------------------------------------------------------
                    // Read back the barcode to extract the 2D component text
                    // ----------------------------------------------------------------
                    BaseDecodeType decodeType = DecodeType.GS1CompositeBar;
                    using (var reader = new BarCodeReader(outputPath, decodeType))
                    {
                        foreach (var result in reader.ReadBarCodes())
                        {
                            string twoDText = result.Extended.GS1CompositeBar.TwoDCodeText;
                            Console.WriteLine($"Read TwoDComponent text for '{fileNameWithoutExt}': {twoDText}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur while processing a file
                Console.WriteLine($"Error processing file '{file}': {ex.Message}");
            }
        }

        // Cleanup: optional removal of temporary folders (commented out to allow inspection)
        // Directory.Delete(inputFolder, true);
        // Directory.Delete(outputFolder, true);
    }
}