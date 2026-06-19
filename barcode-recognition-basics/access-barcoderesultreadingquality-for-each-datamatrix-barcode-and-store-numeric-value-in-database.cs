using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

namespace DataMatrixReadingQualityDemo
{
    /// <summary>
    /// Simple DTO to hold barcode information.
    /// </summary>
    public class BarcodeInfo
    {
        public string CodeText { get; set; }
        public double ReadingQuality { get; set; }
    }

    /// <summary>
    /// Demonstrates generation of a DataMatrix barcode, its recognition,
    /// and recording of the reading quality to a JSON file.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the demo application.
        /// </summary>
        static void Main()
        {
            // Sample DataMatrix barcode text to encode.
            const string sampleText = "SampleDataMatrix123";

            // Create a barcode generator for DataMatrix using the sample text.
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, sampleText))
            {
                // Save the generated barcode image to a memory stream in PNG format.
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    ms.Position = 0; // Reset stream position for reading.

                    // Load the PNG image into a Bitmap for barcode recognition.
                    using (var bitmap = new Bitmap(ms))
                    {
                        // Initialize a reader that decodes only DataMatrix barcodes.
                        using (var reader = new BarCodeReader(bitmap, DecodeType.DataMatrix))
                        {
                            // Perform the recognition and retrieve all detected barcodes.
                            BarCodeResult[] results = reader.ReadBarCodes();

                            // Prepare a list to hold reading quality information.
                            var records = new List<BarcodeInfo>();

                            // Iterate over each recognition result.
                            foreach (var result in results)
                            {
                                // Filter to ensure the result is a DataMatrix barcode.
                                if (result.CodeTypeName == "DataMatrix")
                                {
                                    // Add the barcode text and its reading quality to the list.
                                    records.Add(new BarcodeInfo
                                    {
                                        CodeText = result.CodeText,
                                        ReadingQuality = result.ReadingQuality
                                    });
                                }
                            }

                            // Serialize the collected records to a formatted JSON string.
                            string json = JsonSerializer.Serialize(
                                records,
                                new JsonSerializerOptions { WriteIndented = true });

                            // Define the output file path.
                            const string outputPath = "datamatrix_reading_quality.json";

                            // Write the JSON data to the file system.
                            File.WriteAllText(outputPath, json);

                            // Inform the user where the data was saved.
                            Console.WriteLine($"Reading qualities saved to {outputPath}");
                        }
                    }
                }
            }
        }
    }
}