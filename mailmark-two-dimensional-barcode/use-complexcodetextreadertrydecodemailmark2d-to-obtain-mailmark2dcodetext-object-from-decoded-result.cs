// Title: Generate and Decode a Mailmark 2D Barcode
// Description: Demonstrates creating a Mailmark 2D barcode image, saving it to a temporary folder, and decoding the image back into a Mailmark2DCodetext object.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation and recognition category. It showcases the use of ComplexBarcodeGenerator to build a Mailmark 2D codetext, BarCodeReader for DataMatrix decoding, and ComplexCodetextReader.TryDecodeMailmark2D to parse the decoded string into a strongly‑typed Mailmark2DCodetext. Developers working with postal barcodes, logistics, or supply‑chain tracking often need to generate and validate Mailmark 2D symbols programmatically.
// Prompt: Use ComplexCodetextReader.TryDecodeMailmark2D to obtain a Mailmark2DCodetext object from the decoded result.
// Tags: mailmark2d, barcode, generation, decoding, aspose.barcode, complexbarcode, datamatrix, csharp

using System;
using System.IO;
using System.Text;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a Mailmark 2D barcode, saves it as an image,
/// reads the image back, and extracts the original codetext using Aspose.BarCode APIs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs generation, saving, decoding, and cleanup.
    /// </summary>
    static void Main()
    {
        // Ensure Unicode characters are displayed correctly in the console.
        Console.OutputEncoding = Encoding.Unicode;

        // Create a unique temporary directory for the barcode image.
        string tempDir = Path.Combine(Path.GetTempPath(), "Mailmark2D_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "Mailmark2D.png");

        // --------------------------------------------------------------------
        // 1. Build the Mailmark 2D codetext with required fields.
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
        // 2. Generate the barcode image using ComplexBarcodeGenerator.
        // --------------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(mailmark2D))
        {
            // Adjust the X‑dimension for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // --------------------------------------------------------------------
        // 3. Read and decode the barcode from the saved image.
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.DataMatrix))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Attempt to parse the raw CodeText into a strongly‑typed Mailmark2DCodetext.
                Mailmark2DCodetext decoded = ComplexCodetextReader.TryDecodeMailmark2D(result.CodeText);
                if (decoded == null)
                {
                    Console.WriteLine("Unable to decode Mailmark 2D codetext.");
                    continue;
                }

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

        // --------------------------------------------------------------------
        // 4. Clean up temporary files and directories.
        // --------------------------------------------------------------------
        try
        {
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Suppress any exceptions during cleanup to avoid breaking the example flow.
        }
    }
}