// Title: Generate and decode a Mailmark 2D barcode using Aspose.BarCode
// Description: Demonstrates creating a Mailmark2D barcode image, saving it to a temporary folder, and decoding it back to a Mailmark2DCodetext object.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation and recognition category. It shows how to use ComplexBarcodeGenerator to create a Mailmark2D barcode, BarCodeReader with DecodeType.DataMatrix to read it, and ComplexCodetextReader.TryDecodeMailmark2D to parse the codetext. Developers working with postal barcodes, especially Mailmark 2D, can use these APIs for generating shipping labels and validating encoded data.
// Prompt: Use ComplexCodetextReader.TryDecodeMailmark2D to obtain a Mailmark2DCodetext object from the decoded result.
// Tags: mailmark2d, barcode, generation, recognition, complexbarcode, datamatrix, aspnet, csharp

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generation of a Mailmark2D barcode, saving it to a temporary location,
/// decoding it, and extracting the individual fields from the resulting <see cref="Mailmark2DCodetext"/>.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the full create‑read‑cleanup cycle for a Mailmark2D barcode.
    /// </summary>
    static void Main()
    {
        // -----------------------------------------------------------------
        // 1. Create a unique temporary folder to store the generated image.
        // -----------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "Mailmark2D_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // -------------------------------------------------
        // 2. Define the full path for the barcode image file.
        // -------------------------------------------------
        string barcodePath = Path.Combine(tempFolder, "mailmark2d.png");

        // -------------------------------------------------
        // 3. Prepare the Mailmark2D codetext with sample data.
        // -------------------------------------------------
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

        // -------------------------------------------------
        // 4. Generate the barcode image using ComplexBarcodeGenerator.
        // -------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(mailmark2D))
        {
            // Set the X-dimension (module size) to 4 pixels for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // -------------------------------------------------
        // 5. Verify that the image file was successfully created.
        // -------------------------------------------------
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // -------------------------------------------------
        // 6. Read and decode the barcode from the saved image.
        // -------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.DataMatrix))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Attempt to parse the decoded text into a Mailmark2DCodetext object.
                Mailmark2DCodetext decoded = ComplexCodetextReader.TryDecodeMailmark2D(result.CodeText);
                if (decoded == null)
                {
                    Console.WriteLine("Failed to decode Mailmark2D codetext.");
                    continue;
                }

                // Output each field of the decoded Mailmark2D codetext.
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

        // -------------------------------------------------
        // 7. Clean up temporary files and directories.
        // -------------------------------------------------
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – cleanup failures should not affect program exit.
        }
    }
}