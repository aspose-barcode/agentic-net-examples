// Title: Generate HIBC LIC Secondary and Additional Data DataMatrix Barcode
// Description: Demonstrates how to build a HIBCLICSecondaryAndAdditionalDataCodetext with an expiration date and lot number, then encode it as a DataMatrix barcode image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on HIBC (Health Industry Bar Code) symbologies. It showcases the use of HIBCLICSecondaryAndAdditionalDataCodetext, EncodeTypes, and BarcodeGenerator classes to create a DataMatrix barcode containing secondary and additional data. Developers working with medical or pharmaceutical labeling often need to embed expiry dates, lot numbers, and other regulatory information in barcodes; this snippet provides a concise reference for that common requirement.
// Prompt: Create a HIBCLICSecondaryAndAdditionalDataCodetext, set expiration date, and generate a DataMatrix barcode.
// Tags: hibc, lic, secondary, additional, data, datamatrix, barcode, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a HIBCLICSecondaryAndAdditionalDataCodetext,
/// sets an expiration date and lot number, and generates a DataMatrix barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Builds the codetext, generates the barcode, and saves it as a PNG file.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Prepare HIBC LIC secondary and additional data codetext
        HIBCLICSecondaryAndAdditionalDataCodetext hibcCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext();
        hibcCodetext.BarcodeType = EncodeTypes.HIBCDataMatrixLIC; // Specify HIBC DataMatrix LIC type
        hibcCodetext.LinkCharacter = '+'; // Set the link character used in the codetext
        hibcCodetext.Data = new SecondaryAndAdditionalData();

        // Set expiration date and format
        hibcCodetext.Data.ExpiryDate = DateTime.Now;
        hibcCodetext.Data.ExpiryDateFormat = HIBCLICDateFormat.MMDDYY;

        // Set lot number
        hibcCodetext.Data.LotNumber = "LOT123";

        // Generate the codetext string that will be encoded in the barcode
        string codeText = hibcCodetext.GetConstructedCodetext();

        // Define output file path for the generated barcode image
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "HIBCLICSecondaryDataMatrix.png");

        // Create DataMatrix barcode with the generated codetext and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f; // Set module size
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}