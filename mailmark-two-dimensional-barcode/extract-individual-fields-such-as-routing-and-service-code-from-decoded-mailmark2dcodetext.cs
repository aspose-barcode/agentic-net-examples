// Title: Extract fields from a Mailmark2D barcode
// Description: Demonstrates generating a Mailmark2D barcode, decoding it, and extracting individual data fields such as routing and service codes.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation and recognition category. It showcases the use of Mailmark2DCodetext, ComplexBarcodeGenerator, BarCodeReader, and ComplexCodetextReader to create, read, and parse Mailmark2D barcodes—commonly used in postal automation, mail sorting, and logistics for encoding routing and service information. Developers looking for end‑to‑end barcode handling patterns can reference this snippet for typical workflows.
// Prompt: Extract individual fields such as routing and service code from the decoded Mailmark2DCodetext.
// Tags: mailmark2d, barcode, generation, recognition, aspose.barcode, datamatrix, complexbarcode, extraction

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates creating a Mailmark2D barcode, decoding it, and extracting its individual fields.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Mailmark2D barcode, decodes it, and prints each field.
    /// </summary>
    static void Main()
    {
        // Ensure Unicode characters are displayed correctly in the console.
        Console.OutputEncoding = Encoding.Unicode;

        // Create a unique temporary folder for the generated barcode image.
        string tempFolder = Path.Combine(Path.GetTempPath(), "Mailmark2D_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the barcode image file.
        string barcodePath = Path.Combine(tempFolder, "mailmark2d.png");

        // Prepare the Mailmark2D codetext with sample data.
        var mailmark2D = new Mailmark2DCodetext
        {
            UPUCountryID = "JGB ",
            InformationTypeID = "0",
            VersionID = "1",
            Class = "1",
            SupplyChainID = 123,
            ItemID = 1234,
            DestinationPostCodeAndDPS = "EF61AH8T ",
            RTSFlag = "0",
            ReturnToSenderPostCode = " QWE2 ",
            CustomerContent = "CUSTOM",
            CustomerContentEncodeMode = DataMatrixEncodeMode.C40,
            DataMatrixType = Mailmark2DType.Type_7
        };

        // Generate the barcode image using ComplexBarcodeGenerator.
        using (var generator = new ComplexBarcodeGenerator(mailmark2D))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4; // Set module size.
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was successfully created.
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        // Read and decode the barcode from the generated image.
        using (var reader = new BarCodeReader(barcodePath, DecodeType.DataMatrix))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Attempt to decode the Mailmark2D codetext.
                Mailmark2DCodetext decoded = ComplexCodetextReader.TryDecodeMailmark2D(result.CodeText);
                if (decoded == null)
                {
                    Console.WriteLine("Unable to decode Mailmark2D codetext.");
                    continue;
                }

                // Extract and display individual fields from the decoded codetext.
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
                // Additional fields can be accessed similarly.
            }
        }

        // Cleanup temporary files (optional).
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore cleanup errors.
        }
    }
}