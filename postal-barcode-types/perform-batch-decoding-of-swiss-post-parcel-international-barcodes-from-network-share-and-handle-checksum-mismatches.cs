using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main(string[] args)
    {
        // Determine input folder (network share or local fallback)
        string inputFolder = args.Length > 0 ? args[0] : @"\\NetworkShare\Barcodes";
        if (!Directory.Exists(inputFolder))
        {
            // Fallback to a local folder within the executable directory
            inputFolder = Path.Combine(Environment.CurrentDirectory, "Barcodes");
            if (!Directory.Exists(inputFolder))
            {
                Directory.CreateDirectory(inputFolder);
                Console.WriteLine($"Created empty folder: {inputFolder}");
                Console.WriteLine("Place barcode image files there and rerun the program.");
                return;
            }
        }

        // Supported image extensions
        string[] extensions = new[] { ".png", ".jpg", ".jpeg", ".tif", ".tiff", ".bmp" };
        string[] files = Directory.GetFiles(inputFolder);
        bool anyFile = false;

        foreach (string filePath in files)
        {
            if (Array.IndexOf(extensions, Path.GetExtension(filePath).ToLowerInvariant()) < 0)
                continue;

            anyFile = true;
            Console.WriteLine($"Processing file: {Path.GetFileName(filePath)}");

            // Use BarCodeReader for Swiss Post Parcel barcode type
            using (var reader = new BarCodeReader(filePath, DecodeType.SwissPostParcel))
            {
                // Enable checksum validation and allow reading even if checksum is wrong
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
                reader.QualitySettings.AllowIncorrectBarcodes = true;

                bool found = false;
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    found = true;
                    Console.WriteLine($"  Type      : {result.CodeTypeName}");
                    Console.WriteLine($"  CodeText  : {result.CodeText}");

                    // Extended data may contain checksum information for 1D barcodes
                    if (result.Extended != null && result.Extended.OneD != null)
                    {
                        Console.WriteLine($"  Value     : {result.Extended.OneD.Value}");
                        Console.WriteLine($"  Checksum  : {result.Extended.OneD.CheckSum}");
                    }
                }

                if (!found)
                {
                    Console.WriteLine("  No Swiss Post Parcel barcode detected.");
                }
            }

            Console.WriteLine(); // Blank line between files
        }

        if (!anyFile)
        {
            Console.WriteLine("No supported image files found in the folder:");
            Console.WriteLine(inputFolder);
        }
    }
}