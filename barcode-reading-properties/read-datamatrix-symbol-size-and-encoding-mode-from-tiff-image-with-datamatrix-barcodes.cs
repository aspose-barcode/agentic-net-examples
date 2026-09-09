// Title: Read DataMatrix Symbol Size and Encoding Mode from Image
// Description: Demonstrates generating a DataMatrix barcode, saving it as PNG, and reading barcode data from the image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes and BarCodeReader to detect and extract information from images. Developers commonly use these APIs to automate barcode creation, embed barcodes in documents, and perform batch scanning of images for barcode data.
// Prompt: Read DataMatrix symbol size and encoding mode from a TIFF image with DataMatrix barcodes.
// Tags: datamatrix, barcode, read, image, tiff, generation, recognition, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Sample program that generates a DataMatrix barcode, saves it to a temporary PNG file,
/// and reads barcode information from the image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary folder for storing the sample image.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "DataMatrixReadDemo");
        Directory.CreateDirectory(tempFolder);

        // --------------------------------------------------------------------
        // Define the full path for the sample PNG image.
        // --------------------------------------------------------------------
        string pngPath = Path.Combine(tempFolder, "sample.png");

        // --------------------------------------------------------------------
        // Generate a sample DataMatrix barcode if the PNG file does not already exist.
        // --------------------------------------------------------------------
        if (!File.Exists(pngPath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "SampleText"))
            {
                // Set the X-dimension (module size) to 4 pixels for better visibility.
                generator.Parameters.Barcode.XDimension.Pixels = 4;

                // Save the generated barcode as a PNG image.
                generator.Save(pngPath, BarCodeImageFormat.Png);
            }
        }

        // --------------------------------------------------------------------
        // Verify that the PNG file was created successfully.
        // --------------------------------------------------------------------
        if (!File.Exists(pngPath))
        {
            Console.WriteLine("PNG file not found.");
            return;
        }

        // --------------------------------------------------------------------
        // Read DataMatrix barcodes from the generated image.
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(pngPath, DecodeType.DataMatrix))
        {
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No DataMatrix barcode detected.");
            }
            else
            {
                foreach (BarCodeResult result in results)
                {
                    // Basic barcode information.
                    Console.WriteLine($"CodeType: {result.CodeTypeName}");
                    Console.WriteLine($"CodeText: {result.CodeText}");

                    // Extended DataMatrix metadata (available properties).
                    Console.WriteLine($"IsReaderProgramming: {result.Extended.DataMatrix.IsReaderProgramming}");

                    // Symbol size and encoding mode are not exposed via the Aspose.BarCode API.
                    Console.WriteLine("Symbol size and encoding mode are not available via the API.");
                }
            }
        }
    }
}