using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;

/// <summary>
/// Sample program demonstrating barcode generation and reading using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, reads them in parallel, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for sample barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodesSample");
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcode images (CODE001 to CODE005)
        for (int i = 1; i <= 5; i++)
        {
            string codeText = $"CODE{i:D3}";
            string imagePath = Path.Combine(tempFolder, $"barcode{i}.png");

            // Use BarcodeGenerator to create a Code128 barcode and save it as PNG
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Save(imagePath);
            }
        }

        // Configure the barcode reader to use all available processor cores (default behavior)
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;

        // Retrieve all generated PNG files from the temporary folder
        string[] imageFiles = Directory.GetFiles(tempFolder, "*.png");

        // Process each image in parallel to read barcodes
        Parallel.ForEach(imageFiles, filePath =>
        {
            // Initialize a reader that supports all barcode types
            using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                // Iterate through all detected barcodes in the image
                foreach (var result in reader.ReadBarCodes())
                {
                    // Output file name, barcode type, and decoded text to the console
                    Console.WriteLine($"File: {Path.GetFileName(filePath)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                }
            }
        });

        // Cleanup temporary files (optional). Errors are ignored to avoid crashing the program.
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored
        }
    }
}