using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generation of a Mailmark type 7 (24x24 modules) barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Mailmark 2D barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "mailmark_type7.png");

        // Create and populate a Mailmark2DCodetext instance with required data.
        var mailmark = new Mailmark2DCodetext
        {
            // Version identifier (string)
            VersionID = "1",
            // Information type identifier (string)
            InformationTypeID = "0",
            // Service class code (string)
            Class = "0",
            // Return to sender flag (string)
            RTSFlag = "0",
            // Supply chain identifier (int)
            SupplyChainID = 384224,
            // Unique item identifier (int)
            ItemID = 16563762,
            // Destination postcode plus DPS (must be 9 characters, padded with spaces if needed)
            DestinationPostCodeAndDPS = "EF61AH8T ",
            // Optional customer content (left empty)
            CustomerContent = string.Empty,
            // UPU country identifier (optional, set to GB)
            UPUCountryID = "GB",
            // Define the 2D Mailmark type (type 7 = 24x24 modules)
            DataMatrixType = Mailmark2DType.Type_7,
            // Use default encoding mode for customer content
            CustomerContentEncodeMode = DataMatrixEncodeMode.C40
        };

        // Generate the barcode image using ComplexBarcodeGenerator.
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Mailmark type 7 barcode saved to: {outputPath}");
    }
}