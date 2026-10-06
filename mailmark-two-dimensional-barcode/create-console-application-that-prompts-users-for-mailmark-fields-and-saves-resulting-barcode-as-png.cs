// Title: Generate Mailmark 2D Barcode and Save as PNG
// Description: Demonstrates how to create a Mailmark 2D barcode using Aspose.BarCode, populate its fields from command‑line arguments, and save the image as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as Mailmark 2D. It showcases the use of Mailmark2DCodetext, ComplexBarcodeGenerator, and related parameter settings. Developers working with postal services, logistics, or any scenario requiring Mailmark symbology can reference this pattern for creating and exporting barcodes in common image formats.
// Prompt: Create a console application that prompts users for Mailmark fields and saves the resulting barcode as PNG.
// Tags: mailmark, barcode, generation, png, console, aspose.barcode, complexbarcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Console application that builds a Mailmark 2D barcode from supplied values
/// and writes the resulting image to a temporary PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Parses optional command‑line arguments, constructs the
    /// Mailmark2DCodetext object, generates the barcode, and saves it as PNG.
    /// </summary>
    /// <param name="args">
    /// Optional parameters in the following order:
    /// 0: UPUCountryID, 1: InformationTypeID, 2: VersionID, 3: Class,
    /// 4: SupplyChainID, 5: ItemID, 6: DestinationPostCodeAndDPS,
    /// 7: RTSFlag, 8: ReturnToSenderPostCode, 9: CustomerContent,
    /// 10: DataMatrixType (7, 9, or 29).
    /// </param>
    static void Main(string[] args)
    {
        // Default values for Mailmark 2D fields
        string upuCountryID = "JGB ";
        string informationTypeID = "0";
        string versionID = "1";
        string classValue = "1";
        int supplyChainID = 123;
        int itemID = 1234;
        string destinationPostCodeAndDPS = "EF61AH8T ";
        string rtsFlag = "0";
        string returnToSenderPostCode = " QWE2 ";
        string customerContent = "CUSTOM";
        Mailmark2DType dataMatrixType = Mailmark2DType.Type_7;

        // Assign from command‑line arguments if supplied
        // Expected order:
        // 0: UPUCountryID
        // 1: InformationTypeID
        // 2: VersionID
        // 3: Class
        // 4: SupplyChainID
        // 5: ItemID
        // 6: DestinationPostCodeAndDPS
        // 7: RTSFlag
        // 8: ReturnToSenderPostCode
        // 9: CustomerContent
        // 10: DataMatrixType (7,9,29)
        if (args.Length > 0) upuCountryID = args[0];
        if (args.Length > 1) informationTypeID = args[1];
        if (args.Length > 2) versionID = args[2];
        if (args.Length > 3) classValue = args[3];
        if (args.Length > 4 && int.TryParse(args[4], out int sc)) supplyChainID = sc;
        if (args.Length > 5 && int.TryParse(args[5], out int it)) itemID = it;
        if (args.Length > 6) destinationPostCodeAndDPS = args[6];
        if (args.Length > 7) rtsFlag = args[7];
        if (args.Length > 8) returnToSenderPostCode = args[8];
        if (args.Length > 9) customerContent = args[9];
        if (args.Length > 10)
        {
            switch (args[10])
            {
                case "7":
                    dataMatrixType = Mailmark2DType.Type_7;
                    break;
                case "9":
                    dataMatrixType = Mailmark2DType.Type_9;
                    break;
                case "29":
                    dataMatrixType = Mailmark2DType.Type_29;
                    break;
            }
        }

        // Create Mailmark2D codetext object with populated fields
        Mailmark2DCodetext mailmark2D = new Mailmark2DCodetext
        {
            UPUCountryID = upuCountryID,
            InformationTypeID = informationTypeID,
            VersionID = versionID,
            Class = classValue,
            SupplyChainID = supplyChainID,
            ItemID = itemID,
            DestinationPostCodeAndDPS = destinationPostCodeAndDPS,
            RTSFlag = rtsFlag,
            ReturnToSenderPostCode = returnToSenderPostCode,
            CustomerContent = customerContent,
            DataMatrixType = dataMatrixType
        };

        // Determine output file path in the system temporary folder
        string outputPath = Path.Combine(Path.GetTempPath(), "Mailmark2D.png");

        // Generate the barcode and save it as PNG
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(mailmark2D))
        {
            // Set module size (X‑dimension) to 4 pixels for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Mailmark 2D barcode saved to: {outputPath}");
    }
}