using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating Code128 barcodes at different DPI settings
/// and reports the resulting file sizes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates barcodes for a set of DPI values, saves them as PNG files,
    /// and writes the file size information to the console.
    /// </summary>
    static void Main()
    {
        // Define the DPI values to test.
        float[] dpis = new float[] { 96f, 150f, 300f };

        // The text to encode in the barcode.
        string codeText = "123456789";

        // Iterate over each DPI setting.
        foreach (float dpi in dpis)
        {
            // Build an output file name that includes the DPI value for easy identification.
            string fileName = $"barcode_{(int)dpi}.png";

            // Create a barcode generator for the Code128 symbology with the specified text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Apply the desired resolution (dots per inch) to the generator.
                generator.Parameters.Resolution = dpi;

                // Save the generated barcode image to a PNG file.
                generator.Save(fileName);
            }

            // Retrieve the size of the generated file in bytes.
            long fileSize = new FileInfo(fileName).Length;

            // Output the DPI, file name, and file size to the console.
            Console.WriteLine($"DPI: {dpi}, File: {fileName}, Size: {fileSize} bytes");
        }
    }
}