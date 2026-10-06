// Title: Batch generate GS1 DataMatrix barcodes from AI strings and store them in a ZIP archive
// Description: Demonstrates reading GS1 Application Identifier (AI) strings from text files, generating GS1 DataMatrix barcodes with Aspose.BarCode, and packaging the PNG images into a ZIP file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to perform batch barcode creation using the BarcodeGenerator class. Typical use cases include encoding product identifiers, serial numbers, or lot codes into GS1 DataMatrix symbols for inventory and logistics. Developers often need to automate processing of multiple inputs, customize barcode dimensions, and export results in common image formats for downstream systems.
// Prompt: Batch process a folder of AI strings, generating GS1 DataMatrix barcodes and storing them in a ZIP archive.
// Tags: gs1, datamatrix, barcode, generation, batch, zip, aspose.barcode, image, png

using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides a console application that reads GS1 AI strings from temporary text files,
/// generates GS1 DataMatrix barcodes for each string, and saves the resulting PNG images
/// into a ZIP archive.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Executes the batch barcode generation workflow.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Create a unique temporary folder to hold input AI string files.
        // --------------------------------------------------------------------
        string inputFolder = Path.Combine(Path.GetTempPath(), "GS1Input_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(inputFolder);

        // --------------------------------------------------------------------
        // 2. Define a collection of sample AI strings to be encoded.
        // --------------------------------------------------------------------
        List<string> aiStrings = new List<string>
        {
            "(01)12345678901231(21)ASPOSE(30)9876",
            "(01)98765432109876(10)LOT123",
            "(01)55555555555555(21)ITEM001",
            "(01)11111111111111(21)PRODUCTX",
            "(01)22222222222222(21)ITEMY"
        };

        // --------------------------------------------------------------------
        // 3. Write each AI string to an individual .txt file and collect file paths.
        // --------------------------------------------------------------------
        List<string> inputFiles = new List<string>();
        for (int i = 0; i < aiStrings.Count; i++)
        {
            string filePath = Path.Combine(inputFolder, $"AIString_{i + 1}.txt");
            File.WriteAllText(filePath, aiStrings[i]);
            inputFiles.Add(filePath);
        }

        // --------------------------------------------------------------------
        // 4. Prepare the output ZIP archive path in the temporary folder.
        // --------------------------------------------------------------------
        string zipPath = Path.Combine(Path.GetTempPath(), "GS1Barcodes_" + Guid.NewGuid().ToString("N") + ".zip");

        // --------------------------------------------------------------------
        // 5. Create the ZIP archive and add generated barcode images.
        // --------------------------------------------------------------------
        using (FileStream zipStream = new FileStream(zipPath, FileMode.Create, FileAccess.Write))
        {
            using (ZipArchive zip = new ZipArchive(zipStream, ZipArchiveMode.Create, leaveOpen: true))
            {
                foreach (string file in inputFiles)
                {
                    // Verify that the source file exists.
                    if (!File.Exists(file))
                    {
                        Console.WriteLine($"File not found: {file}");
                        continue;
                    }

                    // Read and trim the AI string from the file.
                    string codeText = File.ReadAllText(file).Trim();
                    if (string.IsNullOrEmpty(codeText))
                    {
                        Console.WriteLine($"Empty code text in file: {file}");
                        continue;
                    }

                    try
                    {
                        // Initialize the barcode generator for GS1 DataMatrix.
                        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
                        {
                            // Set the X-dimension (module size) to 8 pixels.
                            generator.Parameters.Barcode.XDimension.Pixels = 8f;

                            // Render the barcode to a memory stream in PNG format.
                            using (MemoryStream ms = new MemoryStream())
                            {
                                generator.Save(ms, BarCodeImageFormat.Png);
                                ms.Position = 0;

                                // Create a ZIP entry named after the source file (with .png extension).
                                string entryName = Path.GetFileNameWithoutExtension(file) + ".png";
                                ZipArchiveEntry entry = zip.CreateEntry(entryName);

                                // Copy the PNG data into the ZIP entry.
                                using (Stream entryStream = entry.Open())
                                {
                                    ms.CopyTo(entryStream);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to generate barcode for file '{file}': {ex.Message}");
                    }
                }
            }
        }

        // --------------------------------------------------------------------
        // 6. Report the location of the created ZIP archive.
        // --------------------------------------------------------------------
        Console.WriteLine($"Barcode ZIP archive created at: {zipPath}");

        // --------------------------------------------------------------------
        // 7. Clean up the temporary input folder (ignore any errors).
        // --------------------------------------------------------------------
        try
        {
            Directory.Delete(inputFolder, true);
        }
        catch
        {
            // Suppress cleanup exceptions to avoid terminating the program.
        }
    }
}