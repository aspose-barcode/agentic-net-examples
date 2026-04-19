using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        string imagesFolder = "Images";

        if (!Directory.Exists(imagesFolder))
        {
            Console.WriteLine($"Folder not found: {imagesFolder}");
            return;
        }

        string[] jpegFiles = Directory.GetFiles(imagesFolder, "*.jpg");
        if (jpegFiles.Length == 0)
        {
            Console.WriteLine("No JPEG files found.");
            return;
        }

        DeconvolutionMode[] modes = new DeconvolutionMode[]
        {
            DeconvolutionMode.Fast,
            DeconvolutionMode.Normal,
            DeconvolutionMode.Slow
        };

        foreach (string filePath in jpegFiles)
        {
            Console.WriteLine($"Processing file: {Path.GetFileName(filePath)}");
            foreach (DeconvolutionMode mode in modes)
            {
                using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.Code39, DecodeType.Code128, DecodeType.QR))
                {
                    reader.QualitySettings.Deconvolution = mode;

                    BarCodeResult[] results = reader.ReadBarCodes();

                    if (results.Length > 0)
                    {
                        Console.WriteLine($"  Deconvolution: {mode} - Barcodes detected: {results.Length}");
                        foreach (BarCodeResult result in results)
                        {
                            Console.WriteLine($"    Type: {result.CodeTypeName}, Text: {result.CodeText}, Confidence: {result.Confidence}, ReadingQuality: {result.ReadingQuality}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"  Deconvolution: {mode} - No barcode detected.");
                    }
                }
            }
        }
    }
}