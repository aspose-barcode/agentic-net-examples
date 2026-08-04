// Title: Mailmark Barcode Generation, Detection, and Decoding from TIFF Files
// Description: Demonstrates creating sample Mailmark barcodes, saving them as TIFF images, scanning the images, and logging decoded fields.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, showcasing the ComplexBarcodeGenerator for creating Mailmark symbols and BarCodeReader for extracting them. Typical use cases include bulk processing of mail items, automated verification of Mailmark data, and integration into logistics workflows. Developers often need to generate, read, and parse Mailmark barcodes using Aspose.BarCode's API classes such as ComplexBarcodeGenerator, BarCodeReader, and ComplexCodetextReader.
// Prompt: Develop a console app that reads multiple TIFF files, extracts Mailmark barcodes, and logs decoded fields.
// Tags: mailmark, barcode, generation, recognition, tiff, console, aspose.barcode, complexbarcodegenerator, barcodereader

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample console application that generates Mailmark barcodes, saves them as TIFF files,
/// reads the files, decodes the Mailmark data, and writes the extracted fields to the console.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Performs generation, scanning, and decoding of Mailmark barcodes.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary folder for sample TIFF files
        // --------------------------------------------------------------------
        string sampleFolder = Path.Combine(Path.GetTempPath(), "MailmarkSamples");
        if (!Directory.Exists(sampleFolder))
        {
            Directory.CreateDirectory(sampleFolder);
        }

        // --------------------------------------------------------------------
        // Create a few sample Mailmark codetext objects
        // --------------------------------------------------------------------
        var samples = new List<MailmarkCodetext>
        {
            new MailmarkCodetext
            {
                Format = 4,
                VersionID = 1,
                Class = "0",
                SupplychainID = 384224,
                ItemID = 16563762,
                DestinationPostCodePlusDPS = "EF61AH8T "
            },
            new MailmarkCodetext
            {
                Format = 4,
                VersionID = 1,
                Class = "1",
                SupplychainID = 384224,
                ItemID = 16563763,
                DestinationPostCodePlusDPS = "EF61AH8T "
            },
            new MailmarkCodetext
            {
                Format = 4,
                VersionID = 1,
                Class = "2",
                SupplychainID = 384224,
                ItemID = 16563764,
                DestinationPostCodePlusDPS = "EF61AH8T "
            }
        };

        // --------------------------------------------------------------------
        // Generate a TIFF file for each sample Mailmark barcode
        // --------------------------------------------------------------------
        int index = 0;
        foreach (var mailmark in samples)
        {
            string filePath = Path.Combine(sampleFolder, $"mailmark_{index}.tif");
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                generator.Save(filePath, BarCodeImageFormat.Tiff);
            }
            index++;
        }

        // --------------------------------------------------------------------
        // Scan the folder for TIFF files and decode any Mailmark barcodes found
        // --------------------------------------------------------------------
        string[] tiffFiles = Directory.GetFiles(sampleFolder, "*.tif");
        foreach (string tiffFile in tiffFiles)
        {
            if (!File.Exists(tiffFile))
            {
                Console.WriteLine($"File not found: {tiffFile}");
                continue;
            }

            using (var reader = new BarCodeReader(tiffFile, DecodeType.Mailmark))
            {
                // Optional: improve detection speed/quality
                reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;

                foreach (var result in reader.ReadBarCodes())
                {
                    // Decode the Mailmark codetext into its structured object
                    MailmarkCodetext decoded = ComplexCodetextReader.TryDecodeMailmark(result.CodeText);
                    if (decoded != null)
                    {
                        Console.WriteLine($"File: {Path.GetFileName(tiffFile)}");
                        Console.WriteLine($"  Format: {decoded.Format}");
                        Console.WriteLine($"  VersionID: {decoded.VersionID}");
                        Console.WriteLine($"  Class: {decoded.Class}");
                        Console.WriteLine($"  SupplychainID: {decoded.SupplychainID}");
                        Console.WriteLine($"  ItemID: {decoded.ItemID}");
                        Console.WriteLine($"  DestinationPostCodePlusDPS: \"{decoded.DestinationPostCodePlusDPS}\"");
                    }
                    else
                    {
                        Console.WriteLine($"File: {Path.GetFileName(tiffFile)} - Unable to decode Mailmark codetext.");
                    }
                }
            }
        }

        // --------------------------------------------------------------------
        // Cleanup: optionally delete the temporary files
        // --------------------------------------------------------------------
        // foreach (var file in tiffFiles) { File.Delete(file); }
        // Directory.Delete(sampleFolder, true);
    }
}