// Title: Generate and Decode Mailmark 2D Barcode
// Description: Demonstrates creating a Mailmark 2D barcode, saving it as an image, and decoding it to retrieve the original codetext fields.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation and recognition category. It showcases the use of ComplexBarcodeGenerator to build a Mailmark 2D barcode, BarCodeReader to read DataMatrix symbols, and ComplexCodetextReader to parse the specialized Mailmark 2D codetext. Developers working with postal or logistics solutions often need to generate and validate Mailmark 2D symbols for tracking and routing purposes. The snippet serves as a reference for typical workflows involving these key API classes.
/// Prompt: Use ComplexCodetextReader.TryDecodeMailmark2D to obtain a Mailmark2DCodetext object from the decoded result.
/// Tags: mailmark2d, barcode, generation, recognition, complexbarcode, datamatrix, aspose.barcode

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that creates a Mailmark 2D barcode, saves it to a temporary file,
/// reads the barcode back, and extracts the individual fields from the decoded codetext.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the generate‑read‑decode workflow.
    /// </summary>
    static void Main()
    {
        // Ensure Unicode characters are displayed correctly in the console.
        Console.OutputEncoding = Encoding.Unicode;

        // Create a unique temporary folder to store the generated barcode image.
        string tempFolder = Path.Combine(Path.GetTempPath(), "Mailmark2D_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "mailmark2d.png");

        // --------------------------------------------------------------------
        // Build the Mailmark 2D codetext with sample data.
        // --------------------------------------------------------------------
        var mailmark2D = new Mailmark2DCodetext
        {
            UPUCountryID = "JGB ",
            InformationTypeID = "0",
            VersionID = "1",
            Class = "1",
            SupplyChainID = 384224,
            ItemID = 16563762,
            DestinationPostCodeAndDPS = "EF61AH8T ",
            RTSFlag = "0",
            ReturnToSenderPostCode = " QWE2 ",
            CustomerContent = "CUSTOM",
            CustomerContentEncodeMode = DataMatrixEncodeMode.C40,
            DataMatrixType = Mailmark2DType.Type_7
        };

        // --------------------------------------------------------------------
        // Generate the barcode image using ComplexBarcodeGenerator.
        // --------------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(mailmark2D))
        {
            // Adjust the X‑dimension (module size) for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Save(imagePath);
        }

        // --------------------------------------------------------------------
        // Read and decode the barcode from the saved image.
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.DataMatrix))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Attempt to parse the Mailmark 2D codetext from the raw string.
                Mailmark2DCodetext decoded = ComplexCodetextReader.TryDecodeMailmark2D(result.CodeText);
                if (decoded == null)
                    continue; // Skip if parsing failed.

                // Output each field of the decoded Mailmark 2D codetext.
                Console.WriteLine($"UPUCountryID: {decoded.UPUCountryID}");
                Console.WriteLine($"InformationTypeID: {decoded.InformationTypeID}");
                Console.WriteLine($"VersionID: {decoded.VersionID}");
                Console.WriteLine($"Class: {decoded.Class}");
                Console.WriteLine($"SupplyChainID: {decoded.SupplyChainID}");
                Console.WriteLine($"ItemID: {decoded.ItemID}");
                Console.WriteLine($"DestinationPostCodeAndDPS: {decoded.DestinationPostCodeAndDPS}");
                Console.WriteLine($"RTSFlag: {decoded.RTSFlag}");
                Console.WriteLine($"ReturnToSenderPostCode: {decoded.ReturnToSenderPostCode}");
                Console.WriteLine($"CustomerContent: {decoded.CustomerContent}");
            }
        }
    }
}