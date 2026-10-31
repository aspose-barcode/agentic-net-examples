// Title: Extract fields from Mailmark2D barcode
// Description: Demonstrates generating a Mailmark2D barcode, saving it as an image, and decoding it to retrieve individual fields such as routing and service codes.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation and recognition category. It shows how to use ComplexBarcodeGenerator with Mailmark2DCodetext, configure barcode parameters, save the image, and then read it using BarCodeReader and ComplexCodetextReader. Developers working with postal and logistics solutions often need to create and parse Mailmark2D symbols to extract routing, service, and custom data.
// Prompt: Extract individual fields such as routing and service code from the decoded Mailmark2DCodetext.
// Tags: mailmark2d, barcode, generation, recognition, datamatrix, complexbarcode, aspose.barcode, csharp

using System;
using System.IO;
using System.Text;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a Mailmark2D barcode, saves it to a temporary file,
/// reads the barcode back, and prints each decoded field to the console.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates, saves, and decodes a Mailmark2D barcode.
    /// </summary>
    static void Main()
    {
        // Ensure Unicode characters are displayed correctly in the console.
        Console.OutputEncoding = Encoding.Unicode;

        // --------------------------------------------------------------------
        // Create a unique temporary folder to store the generated barcode image.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "Mailmark2D_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "mailmark2d.png");

        // ---------------------------------------------------------------
        // Prepare the Mailmark2D codetext with sample data for each field.
        // ---------------------------------------------------------------
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
            CustomerContent = "CUSTOM DATA",
            DataMatrixType = Mailmark2DType.Type_9
        };

        // -------------------------------------------------
        // Generate the barcode image using ComplexBarcodeGenerator.
        // -------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(mailmark2D))
        {
            // Set the X-dimension (module size) to 4 pixels for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            // Save the generated barcode as a PNG file.
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // -------------------------------------------------
        // Verify that the barcode image was successfully created.
        // -------------------------------------------------
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        // -------------------------------------------------
        // Read and decode the barcode from the saved image file.
        // -------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.DataMatrix))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Attempt to decode the Mailmark2D codetext from the raw result.
                Mailmark2DCodetext decoded = ComplexCodetextReader.TryDecodeMailmark2D(result.CodeText);
                if (decoded == null)
                {
                    Console.WriteLine("Unable to decode Mailmark2D codetext.");
                    continue;
                }

                // -------------------------------------------------
                // Output each individual field of the decoded Mailmark2D codetext.
                // -------------------------------------------------
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