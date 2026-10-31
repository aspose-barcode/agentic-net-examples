// Title: Export multiple barcode configurations to a single XML file using Append mode
// Description: Demonstrates how to use Aspose.BarCode's ExportToXml method with a FileStream opened in Append mode to concatenate several configuration snapshots into one XML document.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category. It showcases the ExportToXml API of the BarcodeGenerator class, which is commonly used to persist barcode settings for later reuse, debugging, or documentation. Developers often need to capture multiple generator states in a single file to compare configurations or maintain versioned records.
// Prompt: Use ExportToXml with a FileStream opened in Append mode to concatenate multiple configuration snapshots.
// Tags: barcode, export, xml, append, configuration, aspose.barcode, generatortypes, fileio

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an example of exporting several <see cref="BarcodeGenerator"/> configurations
/// to a single XML file by appending each snapshot sequentially.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates three barcode generators with different settings
    /// and appends their XML configurations to a temporary file.
    /// </summary>
    static void Main()
    {
        // Define the output XML file that will hold all concatenated configuration snapshots.
        string outputPath = Path.Combine(Path.GetTempPath(), "BarcodesConfig.xml");

        // Ensure the output file starts empty by deleting any existing file.
        if (File.Exists(outputPath))
        {
            File.Delete(outputPath);
        }

        // First barcode configuration: QR code with a custom X-dimension.
        using (var generator1 = new BarcodeGenerator(EncodeTypes.QR, "FirstConfig"))
        {
            generator1.Parameters.Barcode.XDimension.Pixels = 4f;
            AppendConfiguration(generator1, outputPath);
        }

        // Second barcode configuration: Code128 with custom X-dimension and bar height.
        using (var generator2 = new BarcodeGenerator(EncodeTypes.Code128, "SecondConfig"))
        {
            generator2.Parameters.Barcode.XDimension.Pixels = 2f;
            generator2.Parameters.Barcode.BarHeight.Point = 30f;
            AppendConfiguration(generator2, outputPath);
        }

        // Third barcode configuration: DataMatrix with a custom X-dimension.
        using (var generator3 = new BarcodeGenerator(EncodeTypes.DataMatrix, "ThirdConfig"))
        {
            generator3.Parameters.Barcode.XDimension.Pixels = 3f;
            AppendConfiguration(generator3, outputPath);
        }

        Console.WriteLine($"Configuration snapshots have been appended to: {outputPath}");
    }

    /// <summary>
    /// Exports the current state of the provided <see cref="BarcodeGenerator"/> to the specified file,
    /// opening the file in Append mode so that multiple snapshots are concatenated.
    /// </summary>
    /// <param name="generator">The barcode generator whose configuration will be exported.</param>
    /// <param name="filePath">The path to the XML file that receives the appended configuration.</param>
    static void AppendConfiguration(BarcodeGenerator generator, string filePath)
    {
        // Open the file in Append mode and write the generator's XML representation.
        using (FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.Read))
        {
            generator.ExportToXml(fs);
        }
    }
}