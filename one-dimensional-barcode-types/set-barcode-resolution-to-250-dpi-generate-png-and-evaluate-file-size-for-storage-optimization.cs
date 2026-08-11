// Title: Generate Code128 barcode PNG at 250 DPI and assess file size
// Description: This example creates a Code128 barcode, sets the image resolution to 250 DPI, saves it as a PNG, and reports the resulting file size for storage considerations.
// Category-Description: Demonstrates Aspose.BarCode generation features such as barcode symbology selection, image resolution configuration, and output format handling. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce high‑resolution barcode images, a common requirement for printing, scanning, and archival storage scenarios. Developers looking for barcode creation, image quality tuning, and file size evaluation will find this pattern useful.
// Prompt: Set barcode resolution to 250 DPI, generate PNG, and evaluate file size for storage optimization.
// Tags: code128, barcode, resolution, png, file-size, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode PNG with a custom resolution and measuring its file size.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, sets resolution, saves to PNG, and outputs file size.
    /// </summary>
    static void Main()
    {
        // Define the output file name for the generated PNG image
        const string outputFile = "barcode.png";

        // Initialize a barcode generator for the Code128 symbology with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "1234567890";

            // Configure the image resolution to 250 DPI for higher quality output
            generator.Parameters.Resolution = 250f;

            // Save the barcode image to a memory stream in PNG format
            using (var memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);

                // Determine the size of the generated PNG file in bytes
                long fileSize = memoryStream.Length;
                Console.WriteLine($"Generated barcode size: {fileSize} bytes");

                // Reset the stream position before writing to disk
                memoryStream.Position = 0;

                // Write the PNG image from the memory stream to the specified file
                using (var fileStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
                {
                    memoryStream.CopyTo(fileStream);
                }
            }
        }
    }
}