// Title: Generate Mailmark 2D Barcode and Return Image Stream
// Description: Demonstrates how to create a Mailmark 2D barcode using Aspose.BarCode, populate its fields, and obtain the barcode image as a MemoryStream.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, focusing on Mailmark 2D symbology. It showcases the use of ComplexBarcodeGenerator, Mailmark2DCodetext, and related parameters to produce PNG images. Developers working with postal services, logistics, or any system requiring Mailmark encoding can reuse the helper method to generate barcode streams for further processing or storage.
// Prompt: Develop a reusable helper method that accepts Mailmark fields and returns a generated barcode image stream.
// Tags: mailmark, 2d, barcode, generation, png, aspose.barcode, complexbarcode, stream

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Mailmark 2D barcode and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a sample Mailmark 2D barcode, saves it to disk, and writes the output path.
    /// </summary>
    static void Main()
    {
        // Sample data for Mailmark 2D barcode
        string upuCountryID = "JGB ";
        string informationTypeID = "0";
        string versionID = "1";
        string classValue = "1";
        int supplyChainID = 384224;
        int itemID = 16563762;
        string destinationPostCodeAndDPS = "EF61AH8T ";
        string customerContent = "CUSTOMDATAEXCEEDINGLENGTH";
        Mailmark2DType dataMatrixType = Mailmark2DType.Type_7;

        // Generate the barcode and obtain it as a memory stream
        using (MemoryStream barcodeStream = GenerateMailmark2DBarcode(
            upuCountryID,
            informationTypeID,
            versionID,
            classValue,
            supplyChainID,
            itemID,
            destinationPostCodeAndDPS,
            customerContent,
            dataMatrixType))
        {
            // Save the generated barcode to a file for verification
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "Mailmark2D.png");
            using (FileStream file = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                barcodeStream.CopyTo(file);
            }

            Console.WriteLine($"Barcode image saved to: {outputPath}");
        }
    }

    // Helper method that creates a Mailmark 2D barcode and returns the image as a MemoryStream
    static MemoryStream GenerateMailmark2DBarcode(
        string upuCountryID,
        string informationTypeID,
        string versionID,
        string classValue,
        int supplyChainID,
        int itemID,
        string destinationPostCodeAndDPS,
        string customerContent,
        Mailmark2DType dataMatrixType)
    {
        // Determine the maximum allowed length for CustomerContent based on the selected DataMatrix type
        int maxLength = dataMatrixType switch
        {
            Mailmark2DType.Type_7 => 6,
            Mailmark2DType.Type_9 => 12,
            Mailmark2DType.Type_29 => 30,
            _ => 6
        };

        // Truncate CustomerContent if it exceeds the allowed length
        if (customerContent.Length > maxLength)
        {
            customerContent = customerContent.Substring(0, maxLength);
        }

        // Populate the Mailmark2DCodetext object with provided field values
        Mailmark2DCodetext mailmark2D = new Mailmark2DCodetext
        {
            UPUCountryID = upuCountryID,
            InformationTypeID = informationTypeID,
            VersionID = versionID,
            Class = classValue,
            SupplyChainID = supplyChainID,
            ItemID = itemID,
            DestinationPostCodeAndDPS = destinationPostCodeAndDPS,
            CustomerContent = customerContent,
            DataMatrixType = dataMatrixType
        };

        // Generate the barcode image into a memory stream
        MemoryStream resultStream = new MemoryStream();
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(mailmark2D))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Save(resultStream, BarCodeImageFormat.Png);
        }

        // Reset stream position for downstream consumers
        resultStream.Position = 0;
        return resultStream;
    }
}