// Title: Demonstrate default checksum behavior for optional and obligatory barcode symbologies
// Description: Shows how Aspose.BarCode handles checksum generation by default for symbologies where the checksum is optional versus those where it is mandatory, saving example images to a temporary folder.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarcodeParameters.IsChecksumEnabled. Developers often need to understand default checksum settings when creating barcodes for Code39 (optional checksum) and Code128 (obligatory checksum) to ensure data integrity without manually configuring the checksum.
// Prompt: Write documentation comments explaining default checksum behavior for obligatory and optional symbologies.
// Tags: barcode symbology, checksum, optional checksum, obligatory checksum, generation, aspnet, aspose.barcode, encode types, image output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Contains examples that demonstrate the default checksum behavior for barcode symbologies
/// with optional and obligatory checksum requirements using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Demonstrates the default checksum behavior for symbologies with an optional checksum.
    /// For such symbologies the <see cref="BarcodeParameters.IsChecksumEnabled"/> property
    /// defaults to <see cref="EnableChecksum.No"/>, meaning the checksum digit is not generated
    /// unless explicitly enabled.
    /// </summary>
    /// <param name="outputFolder">Folder where the generated barcode image will be saved.</param>
    static void OptionalChecksumDemo(string outputFolder)
    {
        // Build the full file path for the optional checksum example image.
        string filePath = Path.Combine(outputFolder, "OptionalChecksum_Code39.png");

        // Create a generator for Code39, which has an optional checksum.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39, "CODE39"))
        {
            // By default, optional checksum is disabled.
            Console.WriteLine($"[Optional] Default IsChecksumEnabled: {generator.Parameters.Barcode.IsChecksumEnabled}");

            // Save the generated barcode image as PNG.
            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved.
        Console.WriteLine($"Optional checksum barcode saved to: {filePath}");
    }

    /// <summary>
    /// Demonstrates the default checksum behavior for symbologies with an obligatory checksum.
    /// For these symbologies the <see cref="BarcodeParameters.IsChecksumEnabled"/> property
    /// defaults to <see cref="EnableChecksum.Yes"/>, meaning the checksum digit is always generated.
    /// </summary>
    /// <param name="outputFolder">Folder where the generated barcode image will be saved.</param>
    static void ObligatoryChecksumDemo(string outputFolder)
    {
        // Build the full file path for the obligatory checksum example image.
        string filePath = Path.Combine(outputFolder, "ObligatoryChecksum_Code128.png");

        // Create a generator for Code128, which requires a checksum.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "CODE128"))
        {
            // By default, obligatory checksum is enabled.
            Console.WriteLine($"[Obligatory] Default IsChecksumEnabled: {generator.Parameters.Barcode.IsChecksumEnabled}");

            // Save the generated barcode image as PNG.
            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved.
        Console.WriteLine($"Obligatory checksum barcode saved to: {filePath}");
    }

    /// <summary>
    /// Entry point of the demonstration program. Creates a temporary folder,
    /// runs both checksum examples, and reports completion.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for demonstration files.
        string tempFolder = Path.Combine(Path.GetTempPath(), "ChecksumDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        Console.WriteLine("Running checksum behavior demonstration...");

        // Execute the optional checksum example.
        OptionalChecksumDemo(tempFolder);

        // Execute the obligatory checksum example.
        ObligatoryChecksumDemo(tempFolder);

        Console.WriteLine("Demonstration completed.");
    }
}