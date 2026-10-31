// Title: Read Mailmark 2D barcode from image using BarCodeReader
// Description: Demonstrates generating a Mailmark 2D barcode image and reading it back with BarCodeReader configured for DataMatrix decoding.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation and recognition category. It showcases the use of ComplexBarcodeGenerator to create a Mailmark 2D barcode, and BarCodeReader with DecodeType.DataMatrix to extract the encoded information. Developers working with postal services, logistics, or any system that requires Mailmark 2D barcode handling will find this pattern useful for both creating and decoding such barcodes using the Aspose.BarCode API.
/// Prompt: Read a Mailmark 2D barcode from an image file using BarCodeReader with DecodeType.DataMatrix.
/// Tags: mailmark, datamatrix, barcode, generation, recognition, aspose.barcode, complexbarcode

using System;
using System.IO;
using System.Text;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Sample program that generates a Mailmark 2D barcode image,
/// reads it using BarCodeReader, and outputs the decoded fields.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Ensure Unicode characters are displayed correctly in the console.
        Console.OutputEncoding = Encoding.Unicode;

        // --------------------------------------------------------------------
        // Create a temporary folder to store the generated barcode image.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "MailmarkDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "mailmark2d.png");

        // --------------------------------------------------------------------
        // Configure the Mailmark 2D codetext with sample data.
        // --------------------------------------------------------------------
        var mailmark2D = new Mailmark2DCodetext
        {
            VersionID = "1",
            InformationTypeID = "0",
            Class = "1",
            SupplyChainID = 384224,
            ItemID = 16563762,
            DestinationPostCodeAndDPS = "EF61AH8T ",
            RTSFlag = "0",
            ReturnToSenderPostCode = " QWE2 ",
            CustomerContent = "CUSTOM",
            DataMatrixType = Mailmark2DType.Type_9
        };

        // --------------------------------------------------------------------
        // Generate the barcode image using ComplexBarcodeGenerator.
        // --------------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(mailmark2D))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Save(imagePath);
        }

        // --------------------------------------------------------------------
        // Verify that the image file was created successfully.
        // --------------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // --------------------------------------------------------------------
        // Read the Mailmark 2D barcode from the image using BarCodeReader.
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.DataMatrix))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Attempt to decode the Mailmark 2D codetext from the raw result.
                Mailmark2DCodetext decoded = ComplexCodetextReader.TryDecodeMailmark2D(result.CodeText);
                if (decoded == null)
                {
                    Console.WriteLine("Unable to decode Mailmark 2D codetext.");
                    continue;
                }

                // Output each decoded field to the console.
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
        // Clean up temporary files and directories.
        // --------------------------------------------------------------------
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup not critical.
        }
    }
}