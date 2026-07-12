// Title: Generate postal barcodes in parallel
// Description: Demonstrates creating a batch of postal barcodes using Aspose.BarCode with parallel processing, ensuring each thread uses its own generator instance.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on postal symbologies such as Postnet, Planet, AustraliaPost, SingaporePost, and USPS Intelligent Mail. It showcases the use of BarcodeGenerator, EncodeTypes, and related parameter settings to produce PNG images. Developers often need thread‑safe barcode creation for high‑throughput scenarios, and this sample illustrates best practices for parallel execution.
// Prompt: Generate a batch of postal barcodes using parallel processing and ensure thread‑safe handling of generator instances.
// Tags: postal, barcode, parallel, thread-safe, generation, png, aspose.barcode, encode-types

using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a batch of postal barcodes in parallel,
/// demonstrating thread‑safe usage of <see cref="BarcodeGenerator"/> instances.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Prepares barcode data, creates an output folder,
    /// and processes the batch concurrently, saving each barcode as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define a small collection of postal barcode specifications.
        var barcodeData = new List<(string Symbology, string CodeText)>
        {
            ("Postnet", "12345"),
            ("Planet", "12345678"),
            ("AustraliaPost", "5912345678ABCde"),
            ("SingaporePost", "1234567890"),
            ("USPSIntelligentMail", "12345678901234567890")
        };

        // Determine the output directory and ensure it exists.
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Process each barcode definition in parallel.
        // Each iteration creates its own BarcodeGenerator instance, which is thread‑safe.
        Parallel.For(0, barcodeData.Count, i =>
        {
            var (symbologyName, codeText) = barcodeData[i];

            // Resolve the EncodeTypes enum value by name using reflection.
            var field = typeof(EncodeTypes).GetField(symbologyName);
            if (field == null)
            {
                Console.WriteLine($"Unknown symbology: {symbologyName}");
                return;
            }

            // Cast the reflected value to BaseEncodeType.
            BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

            // Create a generator for the current barcode.
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Configure basic visual appearance.
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                generator.Parameters.Barcode.XDimension.Point = 2f; // module size
                generator.Parameters.Barcode.Padding.Left.Point = 5f;
                generator.Parameters.Barcode.Padding.Top.Point = 5f;
                generator.Parameters.Barcode.Padding.Right.Point = 5f;
                generator.Parameters.Barcode.Padding.Bottom.Point = 5f;

                // Example of a symbology‑specific setting: AustraliaPost encoding table.
                if (encodeType == EncodeTypes.AustraliaPost)
                {
                    generator.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = CustomerInformationInterpretingType.CTable;
                }

                // Build a unique file name for the output image.
                string fileName = $"{symbologyName}_{i + 1}.png";
                string filePath = Path.Combine(outputFolder, fileName);

                // Save the generated barcode as a PNG file.
                generator.Save(filePath);
                Console.WriteLine($"Saved {filePath}");
            }
        });

        // Indicate that the batch processing has finished.
        Console.WriteLine("Barcode batch generation completed.");
    }
}