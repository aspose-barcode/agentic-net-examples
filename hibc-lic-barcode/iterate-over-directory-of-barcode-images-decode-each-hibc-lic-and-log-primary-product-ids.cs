using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

class Program
{
    static void Main()
    {
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");

        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        string[] imageFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
        if (imageFiles.Length == 0)
        {
            Console.WriteLine("No files found in the input folder.");
            return;
        }

        foreach (string filePath in imageFiles)
        {
            // Process only common image extensions
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (ext != ".png" && ext != ".jpg" && ext != ".jpeg" && ext != ".bmp" && ext != ".tif" && ext != ".tiff")
                continue;

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File does not exist: {filePath}");
                continue;
            }

            using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (BarCodeReader reader = new BarCodeReader(stream, DecodeType.AllSupportedTypes))
            {
                BarCodeResult[] results = reader.ReadBarCodes();
                if (results == null || results.Length == 0)
                {
                    Console.WriteLine($"No barcode detected in file: {Path.GetFileName(filePath)}");
                    continue;
                }

                foreach (BarCodeResult result in results)
                {
                    // Attempt to decode HIBC LIC complex codetext
                    HIBCLICComplexCodetext complex = ComplexCodetextReader.TryDecodeHIBCLIC(result.CodeText);
                    if (complex == null)
                    {
                        Console.WriteLine($"File: {Path.GetFileName(filePath)} - Detected barcode but not HIBC LIC.");
                        continue;
                    }

                    string productId = null;

                    if (complex is HIBCLICCombinedCodetext combined)
                    {
                        productId = combined.PrimaryData?.ProductOrCatalogNumber;
                    }
                    else if (complex is HIBCLICPrimaryDataCodetext primary)
                    {
                        productId = primary.Data?.ProductOrCatalogNumber;
                    }

                    if (!string.IsNullOrEmpty(productId))
                    {
                        Console.WriteLine($"File: {Path.GetFileName(filePath)} - Product ID: {productId}");
                    }
                    else
                    {
                        Console.WriteLine($"File: {Path.GetFileName(filePath)} - Primary product ID not found.");
                    }
                }
            }
        }
    }
}