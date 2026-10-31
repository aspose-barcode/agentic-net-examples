// Title: Mailmark 2D Barcode Generation with Customer Content Comparison
// Description: Demonstrates generating Mailmark 2D barcodes of three different types while encoding customer information, allowing developers to observe the impact of content length on barcode capacity.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator and Mailmark2DCodetext to create Mailmark 2D barcodes, a common requirement in postal and logistics applications. Developers often need to encode various data fields, including customer content, and compare how different DataMatrix types affect the amount of information that can be stored.
/// Prompt: Encode customer information with an alternative encoding and assess its impact on barcode capacity.
/// Tags: mailmark, barcode, datamatrix, capacity, customer-content, aspose.barcode, complexbarcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates Mailmark 2D barcodes of three types, encodes customer information,
/// and saves the resulting images for capacity comparison.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary output folder, defines common
    /// Mailmark fields, iterates over selected DataMatrix types, generates barcodes,
    /// and writes status information to the console.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for output files
        string outputDir = Path.Combine(Path.GetTempPath(), "Mailmark2D_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Common fields for all Mailmark 2D barcodes
        const string upuCountryID = "JGB ";
        const string informationTypeID = "0";
        const string versionID = "1";
        const string classID = "1";
        const int supplyChainID = 123;
        const int itemID = 1234;
        const string destinationPostCodeAndDPS = "EF61AH8T "; // exact 9‑char value with trailing space
        const string customerContent = "CUST01"; // 6 characters, max for type 29

        // Define the three Mailmark 2D types to compare
        var types = new (Mailmark2DType Type, string Name)[]
        {
            (Mailmark2DType.Type_7, "Type 7"),
            (Mailmark2DType.Type_9, "Type 9"),
            (Mailmark2DType.Type_29, "Type 29")
        };

        // Iterate over each type, generate the barcode, and save it
        foreach (var (type, name) in types)
        {
            // Prepare the codetext object with shared fields and the current DataMatrix type
            var mailmark2D = new Mailmark2DCodetext
            {
                UPUCountryID = upuCountryID,
                InformationTypeID = informationTypeID,
                VersionID = versionID,
                Class = classID,
                SupplyChainID = supplyChainID,
                ItemID = itemID,
                DestinationPostCodeAndDPS = destinationPostCodeAndDPS,
                DataMatrixType = type,
                CustomerContent = customerContent
            };

            // Generate the barcode using ComplexBarcodeGenerator
            using (var generator = new ComplexBarcodeGenerator(mailmark2D))
            {
                // Build the output file path and save the image as PNG
                string filePath = Path.Combine(outputDir, $"Mailmark2D_{name}.png");
                generator.Save(filePath, BarCodeImageFormat.Png);

                // Output status information for the current barcode
                Console.WriteLine($"{name} barcode saved to: {filePath}");
                Console.WriteLine($"  Customer content length: {mailmark2D.CustomerContent.Length} characters");
                Console.WriteLine($"  DataMatrix type: {mailmark2D.DataMatrixType}");
            }
        }

        // Final message indicating that generation is complete
        Console.WriteLine("Generation completed. Review the output files to assess capacity differences.");
    }
}