// Title: Dynamic Australia Post barcode generation with runtime interpreting type selection
// Description: Demonstrates how to generate and read an Australia Post barcode while switching the CustomerInformationInterpretingType at runtime based on a command‑line argument.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator, BarCodeReader, and AustraliaPost settings such as CustomerInformationInterpretingType. Developers often need to adapt barcode encoding tables dynamically for different postal services or customer data formats, making this pattern useful for flexible barcode processing pipelines.
// Prompt: Write code that switches AustraliaPostSettings.CustomerInformationInterpretingType at runtime based on user selection.
// Tags: barcode, australia post, interpreting type, runtime, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates dynamic selection of Australia Post barcode interpreting type for generation and recognition.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Parses a command‑line argument to choose the interpreting type, generates a barcode, reads it back, and cleans up temporary files.
    /// </summary>
    /// <param name="args">Command‑line arguments where the first argument specifies the interpreting type (e.g., CTable, DTable).</param>
    static void Main(string[] args)
    {
        // Determine interpreting type from command‑line argument or default to CTable
        string arg = args.Length > 0 ? args[0] : "CTable";
        if (!Enum.TryParse<CustomerInformationInterpretingType>(arg, true, out var interpretingType))
        {
            Console.WriteLine($"Invalid interpreting type '{arg}'. Falling back to CTable.");
            interpretingType = CustomerInformationInterpretingType.CTable;
        }

        // Prepare a temporary output folder and file path for the generated barcode image
        string outputDir = Path.Combine(Path.GetTempPath(), "AustraliaPostDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        string barcodePath = Path.Combine(outputDir, "AustraliaPost.png");

        // Australia Post code text (FCC 59 + 8‑digit DPID, no customer info)
        string codeText = "5901234567";

        // Generate barcode using the selected interpreting type
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;
            generator.Parameters.Barcode.AustralianPost.EncodingTable = interpretingType;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to: {barcodePath}");
        Console.WriteLine($"Interpreting type used for generation: {interpretingType}");

        // Read the barcode using the same interpreting type
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AustraliaPost))
        {
            reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = interpretingType;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Read Code Type: {result.CodeTypeName}");
                Console.WriteLine($"Read Code Text: {result.CodeText}");
            }
        }

        // Clean up temporary files (optional)
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            Directory.Delete(outputDir, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}