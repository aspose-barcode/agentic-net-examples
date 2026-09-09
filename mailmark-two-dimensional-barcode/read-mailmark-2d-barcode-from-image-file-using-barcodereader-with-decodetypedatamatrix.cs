// Title: Read Mailmark 2D barcode from an image using BarCodeReader
// Description: Demonstrates generating a Mailmark 2D barcode image and then reading it back with BarCodeReader configured for DataMatrix decoding.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use ComplexBarcodeGenerator to create a Mailmark 2D barcode (Mailmark2DCodetext) and how to employ BarCodeReader with DecodeType.DataMatrix to extract the encoded information. Developers working with postal barcodes, especially UPU Mailmark, can use these APIs for creating and validating barcode data in logistics applications.
// Prompt: Read a Mailmark 2D barcode from an image file using BarCodeReader with DecodeType.DataMatrix.
// Tags: mailmark, datamatrix, barcode generation, barcode reading, aspnet, aspnetcore, aspose.barcode, complexbarcode, codetext

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Sample program that creates a Mailmark 2D barcode image and reads it back using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a temporary Mailmark 2D barcode image, reads it, prints decoded fields, and cleans up.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the sample image
        string tempFolder = Path.Combine(Path.GetTempPath(), "Mailmark2D_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "mailmark2d.png");

        // Build Mailmark 2D codetext with required fields
        var mailmark2D = new Mailmark2DCodetext
        {
            InformationTypeID = "0",
            VersionID = "1",
            Class = "1",
            RTSFlag = "0",
            SupplyChainID = 384224,
            ItemID = 16563762,
            DestinationPostCodeAndDPS = "EF61AH8T ",
            CustomerContent = "CUSTOM",
            DataMatrixType = Mailmark2DType.Type_7
        };

        // Generate the barcode image using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(mailmark2D))
        {
            // Set X-dimension (pixel size) for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Save(imagePath);
        }

        // Verify that the image was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Read the Mailmark 2D barcode from the image using DataMatrix decoding
        using (var reader = new BarCodeReader(imagePath, DecodeType.DataMatrix))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Attempt to decode the result into a Mailmark2DCodetext object
                Mailmark2DCodetext decoded = ComplexCodetextReader.TryDecodeMailmark2D(result.CodeText);
                if (decoded == null)
                {
                    continue;
                }

                // Output each decoded field to the console
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

        // Clean up temporary files and folder
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}