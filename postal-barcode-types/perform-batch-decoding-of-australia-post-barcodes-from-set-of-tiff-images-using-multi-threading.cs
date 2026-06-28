using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch processing of TIFF images to recognize Australia Post barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Scans a folder for TIFF files and processes each file in parallel
    /// to detect Australia Post barcodes, outputting the results to the console.
    /// </summary>
    static void Main()
    {
        // Determine the folder that contains the TIFF images (relative to the executable location).
        string imagesFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Barcodes");

        // Verify that the folder exists; otherwise, inform the user and exit.
        if (!Directory.Exists(imagesFolder))
        {
            Console.WriteLine($"Folder not found: {imagesFolder}");
            return;
        }

        // Retrieve up to 5 TIFF files from the folder for safe batch processing.
        string[] tiffFiles = Directory.GetFiles(imagesFolder, "*.tif")
                                      .Take(5)
                                      .ToArray();

        // If no TIFF files are found, notify the user and exit.
        if (tiffFiles.Length == 0)
        {
            Console.WriteLine("No TIFF files found in the folder.");
            return;
        }

        // Process each file concurrently to improve performance.
        Parallel.ForEach(tiffFiles, filePath =>
        {
            // Ensure the file still exists before attempting to process it.
            if (!File.Exists(filePath))
            {
                lock (Console.Out)
                {
                    Console.WriteLine($"File not found: {filePath}");
                }
                return;
            }

            // Load the image using Aspose.Drawing.Bitmap (implements IDisposable).
            using (var bitmap = new Bitmap(filePath))
            {
                // Create a barcode reader configured for Australia Post barcode type.
                using (var reader = new BarCodeReader(bitmap, DecodeType.AustraliaPost))
                {
                    // Set Australia Post specific decoding options.
                    reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;
                    reader.BarcodeSettings.AustraliaPost.IgnoreEndingFillingPatternsForCTable = true;

                    // Perform the barcode recognition.
                    var results = reader.ReadBarCodes();

                    // Output the results to the console in a thread‑safe manner.
                    lock (Console.Out)
                    {
                        Console.WriteLine($"File: {Path.GetFileName(filePath)}");
                        if (results.Length == 0)
                        {
                            Console.WriteLine("  No barcodes detected.");
                        }
                        else
                        {
                            foreach (var result in results)
                            {
                                Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                            }
                        }
                    }
                }
            }
        });
    }
}