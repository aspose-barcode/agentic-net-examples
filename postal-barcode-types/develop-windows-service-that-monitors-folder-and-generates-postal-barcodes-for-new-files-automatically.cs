// Title: Generate RM4SCC Postal Barcodes from Files
// Description: Demonstrates creating temporary input files, generating RM4SCC postal barcodes for each file, and saving them as PNG images.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It shows how to use BarcodeGenerator (EncodeTypes.RM4SCC) to create postal barcodes, customize dimensions, and save them as images. It also illustrates reading back the generated barcode with BarCodeReader (DecodeType.RM4SCC) for verification. Developers working with postal services, logistics, or any scenario requiring RM4SCC barcodes will find this pattern useful.
// Prompt: Develop a Windows service that monitors a folder and generates postal barcodes for new files automatically.
// Tags: postal barcode, rm4scc, barcode generation, barcode recognition, image output, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Sample console application that creates temporary files, generates RM4SCC postal barcodes for each file,
/// saves the barcodes as PNG images, and optionally verifies them by reading back the generated images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Executes the barcode generation workflow.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Create a unique temporary input folder and populate it with sample files.
        // --------------------------------------------------------------------
        string inputFolder = Path.Combine(Path.GetTempPath(), "PostalInput_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(inputFolder);
        var inputFiles = new string[3];
        for (int i = 0; i < inputFiles.Length; i++)
        {
            string filePath = Path.Combine(inputFolder, $"Sample{i + 1}.txt");
            File.WriteAllText(filePath, $"Sample content {i + 1}");
            inputFiles[i] = filePath;
        }

        // --------------------------------------------------------------------
        // 2. Create a unique temporary output folder for the generated barcode images.
        // --------------------------------------------------------------------
        string outputFolder = Path.Combine(Path.GetTempPath(), "PostalOutput_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // --------------------------------------------------------------------
        // 3. Process each input file: generate an RM4SCC barcode based on the file index,
        //    save the barcode image, and optionally read it back for verification.
        // --------------------------------------------------------------------
        for (int i = 0; i < inputFiles.Length; i++)
        {
            string file = inputFiles[i];
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            // Build a code text suitable for RM4SCC (example format).
            string codeText = $"RM9996050{i + 1:D2}CH";

            // Generate the barcode image using Aspose.BarCode.
            using (var generator = new BarcodeGenerator(EncodeTypes.RM4SCC, codeText))
            {
                // Set the X-dimension (module width) to 2 pixels for better readability.
                generator.Parameters.Barcode.XDimension.Pixels = 2f;

                // Determine the output PNG file path.
                string outputPath = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(file) + ".png");

                // Save the generated barcode as a PNG image.
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Generated barcode for '{Path.GetFileName(file)}' at '{outputPath}'");

                // ----------------------------------------------------------------
                // Optional verification: read back the generated barcode image.
                // ----------------------------------------------------------------
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    using (var reader = new BarCodeReader(bitmap, DecodeType.RM4SCC))
                    {
                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"Read back: Type={result.CodeTypeName}, Text={result.CodeText}");
                        }
                    }
                }
            }
        }

        // --------------------------------------------------------------------
        // 4. Cleanup (optional): delete the temporary folders created for the demo.
        // --------------------------------------------------------------------
        // Directory.Delete(inputFolder, true);
        // Directory.Delete(outputFolder, true);
    }
}