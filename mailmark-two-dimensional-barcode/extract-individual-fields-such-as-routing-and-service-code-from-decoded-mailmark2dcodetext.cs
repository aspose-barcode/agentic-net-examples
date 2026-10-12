// Title: Extract fields from a Mailmark2D barcode
// Description: Demonstrates generating a Mailmark2D barcode, decoding it, and extracting individual fields such as routing and service codes.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation and recognition category. It showcases the use of ComplexBarcodeGenerator to create a Mailmark2D barcode and BarCodeReader with DecodeType.DataMatrix to read it. Developers working with postal automation, logistics, or any scenario requiring Mailmark2D data extraction can use these APIs to encode detailed shipment information and later parse it for routing, service, and customer data.
// Prompt: Extract individual fields such as routing and service code from the decoded Mailmark2DCodetext.
// Tags: mailmark2d, barcode, generation, recognition, datamatrix, extraction, csharp, aspose.barcode

using System;
using System.IO;
using System.Text;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a Mailmark2D barcode, decodes it, and prints each individual field.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a temporary Mailmark2D barcode image, reads it back,
    /// decodes the codetext, and writes each component to the console.
    /// </summary>
    static void Main()
    {
        // Ensure Unicode characters are displayed correctly in the console.
        Console.OutputEncoding = Encoding.Unicode;

        // --------------------------------------------------------------------
        // Prepare a temporary folder and file path for the generated barcode.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "Mailmark2D_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "mailmark2d.png");

        // ---------------------------------------------------------------
        // Create a Mailmark2DCodetext instance with sample data fields.
        // ---------------------------------------------------------------
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
            CustomerContent = "CUSTOMER123",
            CustomerContentEncodeMode = DataMatrixEncodeMode.C40,
            DataMatrixType = Mailmark2DType.Type_9
        };

        // -------------------------------------------------
        // Generate the barcode image using ComplexBarcodeGenerator.
        // -------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(mailmark2D))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f; // Set module size.
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // -------------------------------------------------
        // Verify that the barcode image was created successfully.
        // -------------------------------------------------
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Barcode image not found.");
            return;
        }

        // -------------------------------------------------
        // Read and decode the barcode using BarCodeReader.
        // -------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.DataMatrix))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Attempt to decode the Mailmark2D codetext.
                Mailmark2DCodetext decoded = ComplexCodetextReader.TryDecodeMailmark2D(result.CodeText);
                if (decoded == null)
                {
                    Console.WriteLine("Failed to decode Mailmark2D codetext.");
                    continue;
                }

                // Output each individual field to the console.
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
                // Additional fields can be accessed similarly if needed.
            }
        }

        // -------------------------------------------------
        // Clean up temporary files and directories.
        // -------------------------------------------------
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore any cleanup errors.
        }
    }
}