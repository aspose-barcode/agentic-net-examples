using System;
using System.IO;
using System.Text;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Define input and output paths
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "InputImages");
        string outputCsv = Path.Combine(Directory.GetCurrentDirectory(), "BarcodeResults.csv");

        // Ensure the input folder exists; create it if missing
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
            // Create a sample barcode image so the example can run end‑to‑end
            string sampleImage = Path.Combine(inputFolder, "sample.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                generator.Save(sampleImage);
            }
        }

        // Prepare CSV file with header
        using (var writer = new StreamWriter(outputCsv, false, Encoding.UTF8))
        {
            writer.WriteLine("ImageFile,BarcodeType,CodeText,Confidence");

            // Get all image files in the folder (common extensions)
            string[] imageFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string imagePath in imageFiles)
            {
                string extension = Path.GetExtension(imagePath).ToLowerInvariant();
                if (extension != ".png" && extension != ".jpg" && extension != ".jpeg" && extension != ".bmp" && extension != ".gif")
                    continue; // Skip non‑image files

                // Read barcodes from the image
                using (var reader = new BarCodeReader(imagePath))
                {
                    // Optionally set decode types if you want to limit the search
                    // reader.SetBarCodeReadType(DecodeType.Code128, DecodeType.QR, DecodeType.EAN13);

                    BarCodeResult[] results = reader.ReadBarCodes();
                    foreach (BarCodeResult result in results)
                    {
                        // Write a line per detected barcode
                        string line = string.Format("{0},{1},{2},{3}",
                            Path.GetFileName(imagePath),
                            result.CodeTypeName,
                            result.CodeText,
                            result.Confidence);
                        writer.WriteLine(line);
                    }
                }
            }
        }

        Console.WriteLine("Barcode extraction completed. Results saved to:");
        Console.WriteLine(outputCsv);
    }
}