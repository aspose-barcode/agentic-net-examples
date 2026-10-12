// Title: Read Mailmark 2D barcode from an image using BarCodeReader
// Description: This example generates a Mailmark 2D barcode, saves it as a PNG file, and then reads and decodes it using BarCodeReader with DecodeType.DataMatrix.
// Category-Description: Demonstrates Aspose.BarCode operations for complex barcode generation and recognition. It uses ComplexBarcodeGenerator to create a Mailmark2D barcode, BarCodeReader to decode DataMatrix symbology, and ComplexCodetextReader to parse the Mailmark 2D codetext. Developers working with postal barcodes or other specialized 2D symbologies can reference this pattern for creating, persisting, and extracting data from such barcodes.
// Prompt: Read a Mailmark 2D barcode from an image file using BarCodeReader with DecodeType.DataMatrix.
// Tags: mailmark, 2d, datamatrix, barcode, generation, reading, aspose.barcode, complexbarcode

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that creates a Mailmark 2D barcode, saves it to a temporary PNG file,
/// and reads it back using <see cref="BarCodeReader"/> configured for DataMatrix decoding.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, reads it, displays decoded fields, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "Mailmark2D_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "mailmark2d.png");

        // Build a Mailmark 2D codetext with sample data
        var mailmark2d = new Mailmark2DCodetext
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

        // Generate the barcode image and save it as PNG
        using (var generator = new ComplexBarcodeGenerator(mailmark2d))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Read the Mailmark 2D barcode from the saved image using DataMatrix decoding
        using (var reader = new BarCodeReader(imagePath, DecodeType.DataMatrix))
        {
            BarCodeResult[] results = reader.ReadBarCodes();
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
                return;
            }

            // Process each detected barcode
            foreach (var result in results)
            {
                // Attempt to decode the Mailmark 2D specific codetext
                var mailmarkResult = ComplexCodetextReader.TryDecodeMailmark2D(result.CodeText);
                if (mailmarkResult == null)
                {
                    Console.WriteLine("Failed to decode Mailmark 2D codetext.");
                    continue;
                }

                // Output the decoded fields to the console
                Console.WriteLine($"UPUCountryID: {mailmarkResult.UPUCountryID}");
                Console.WriteLine($"InformationTypeID: {mailmarkResult.InformationTypeID}");
                Console.WriteLine($"VersionID: {mailmarkResult.VersionID}");
                Console.WriteLine($"Class: {mailmarkResult.Class}");
                Console.WriteLine($"SupplyChainID: {mailmarkResult.SupplyChainID}");
                Console.WriteLine($"ItemID: {mailmarkResult.ItemID}");
                Console.WriteLine($"DestinationPostCodeAndDPS: {mailmarkResult.DestinationPostCodeAndDPS}");
                Console.WriteLine($"RTSFlag: {mailmarkResult.RTSFlag}");
                Console.WriteLine($"ReturnToSenderPostCode: {mailmarkResult.ReturnToSenderPostCode}");
                Console.WriteLine($"CustomerContent: {mailmarkResult.CustomerContent}");
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
            // Ignore any cleanup errors
        }
    }
}