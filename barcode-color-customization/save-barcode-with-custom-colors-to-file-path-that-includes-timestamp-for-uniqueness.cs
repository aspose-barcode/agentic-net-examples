// Title: Save Barcode with Custom Colors and Timestamped Filename
// Description: Demonstrates generating a Code128 barcode, applying custom foreground and background colors, and saving it to a uniquely named PNG file using a timestamp.
// Prompt: Save a barcode with custom colors to a file path that includes a timestamp for uniqueness.
// Tags: code128, barcode, custom colors, file output, timestamp, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeSample
{
    /// <summary>
    /// Sample program that creates a Code128 barcode with custom colors and saves it to a timestamped file.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the application.
        /// Generates the barcode, applies colors, and writes the image to a uniquely named file.
        /// </summary>
        /// <param name="args">Command‑line arguments (not used).</param>
        static void Main(string[] args)
        {
            // Create a unique file name using the current date and time down to milliseconds.
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmssfff");
            string fileName = $"barcode_{timestamp}.png";

            // Combine the current directory with the file name to get the full output path.
            string outputPath = Path.Combine(Environment.CurrentDirectory, fileName);

            // Initialize the barcode generator for Code128 with the desired data.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Set the color of the barcode bars (foreground).
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;

                // Set the background color of the image.
                generator.Parameters.BackColor = Aspose.Drawing.Color.Yellow;

                // Save the generated barcode image to the specified path.
                generator.Save(outputPath);
            }

            // Inform the user where the barcode image was saved.
            Console.WriteLine($"Barcode saved to: {outputPath}");
        }
    }
}