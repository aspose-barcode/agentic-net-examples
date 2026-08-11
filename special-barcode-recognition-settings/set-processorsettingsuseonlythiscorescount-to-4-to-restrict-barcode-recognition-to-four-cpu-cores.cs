// Title: Restrict barcode recognition to a specific number of CPU cores
// Description: Demonstrates how to limit Aspose.BarCode barcode recognition to four processor cores using ProcessorSettings.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, showcasing how to control multithreading behavior via the ProcessorSettings class. Developers often need to balance performance and resource usage when processing large batches of images; setting UseOnlyThisCoresCount allows precise core allocation. Typical use cases include server environments, CI pipelines, or desktop applications where CPU usage must be constrained.
// Prompt: Set ProcessorSettings.UseOnlyThisCoresCount to 4 to restrict barcode recognition to four CPU cores.
// Tags: barcode symbology, recognition, multithreading, processor settings, core count, aspose.barcode, code128, image generation

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode, restricts recognition to four CPU cores,
/// and reads the barcode back from the generated image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the file path for the sample barcode image.
        string imagePath = "sample_barcode.png";

        // Generate a simple barcode image if it does not already exist.
        if (!File.Exists(imagePath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
            {
                // Set visual appearance: black bars on a white background.
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Save the generated barcode image to the specified file.
                generator.Save(imagePath);
                Console.WriteLine($"Barcode image created at: {Path.GetFullPath(imagePath)}");
            }
        }

        // Restrict barcode recognition to use only 4 CPU cores.
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 4;
        Console.WriteLine($"ProcessorSettings.UseOnlyThisCoresCount set to {BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount}");

        // Perform barcode recognition on the generated image.
        if (File.Exists(imagePath))
        {
            using (var reader = new BarCodeReader(imagePath))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                    Console.WriteLine($"Detected Text: {result.CodeText}");
                }
            }
        }
        else
        {
            Console.WriteLine($"Image file not found: {imagePath}");
        }
    }
}