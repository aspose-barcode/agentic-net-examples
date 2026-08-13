// Title: Parallel Generation of Australia Post Barcodes
// Description: Demonstrates generating a batch of Australia Post (FCC 59) barcodes in parallel, saving each as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use BarcodeGenerator with EncodeTypes.AustraliaPost, configure encoding tables, and employ parallel processing for high‑throughput scenarios. Developers creating bulk postal barcode images for mailing applications can reference this pattern for thread‑safe generator usage and file naming.
// Prompt: Generate a batch of postal barcodes using parallel processing and ensure thread‑safe handling of generator instances.
// Tags: australia post,postal barcode,generation,parallel processing,thread safety,aspose.barcode,encode types,png output

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a set of Australia Post barcodes using parallel processing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcode images and writes status messages to the console.
    /// </summary>
    static void Main()
    {
        // Define the output folder for generated barcode images.
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Prepare a small batch of Australia Post barcode texts (FCC 59 allows up to 5 CTable chars).
        List<string> codeTexts = GenerateAustraliaPostCodeTexts();

        // Pair each code text with its index to create unique file names.
        var indexedCodes = codeTexts
            .Select((code, idx) => new { Code = code, Index = idx })
            .ToList();

        // Generate barcodes in parallel; each iteration creates its own BarcodeGenerator instance for thread safety.
        Parallel.ForEach(indexedCodes, item =>
        {
            // Build the full file path for the current barcode image.
            string filePath = Path.Combine(outputFolder, $"barcode_{item.Index + 1}.png");

            // Use a using block to ensure the generator is disposed after saving.
            using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, item.Code))
            {
                // Set the encoding table to CTable for customer information interpretation.
                generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;

                // Save the generated barcode directly as a PNG file.
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Output progress information to the console.
            Console.WriteLine($"Generated barcode {item.Index + 1}: {item.Code}");
        });

        // Indicate that all barcode generation tasks have completed.
        Console.WriteLine("Barcode generation completed.");
    }

    /// <summary>
    /// Generates a few valid Australia Post code texts using FCC 59 format.
    /// </summary>
    /// <returns>List of barcode text strings.</returns>
    private static List<string> GenerateAustraliaPostCodeTexts()
    {
        var list = new List<string>();
        // Base FCC (59) and DPID (8 digits) components.
        string fcc = "59";
        for (int i = 0; i < 5; i++)
        {
            // Create an 8‑digit DPID value.
            string dpid = i.ToString("D8");

            // Append up to 5 CTable characters (e.g., "ABCD").
            string customerInfo = "ABCD".Substring(0, i % 5);
            string codeText = fcc + dpid + customerInfo;
            list.Add(codeText);
        }
        return list;
    }
}