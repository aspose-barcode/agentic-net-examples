using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Sample Unicode text containing Cyrillic characters
        const string originalText = "Привет мир";

        // Create barcode image in memory using UTF-8 encoding
        using (var ms = new MemoryStream())
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                // Manually encode the text with UTF-8
                generator.SetCodeText(originalText, Encoding.UTF8);
                // Save to the memory stream as PNG
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Prepare the stream for reading
            ms.Position = 0;

            // Read the barcode with automatic encoding detection enabled
            using (var reader = new BarCodeReader(ms, DecodeType.QR))
            {
                // Ensure detection is turned on (default is true)
                reader.BarcodeSettings.DetectEncoding = true;

                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // CodeText obtained via automatic detection
                    string autoDetected = result.CodeText;

                    // CodeText obtained by manually specifying UTF-8
                    string manualDecoded = result.GetCodeText(Encoding.UTF8);

                    // Compare the two results
                    if (autoDetected == manualDecoded && autoDetected == originalText)
                    {
                        Console.WriteLine("Test passed: automatic detection matches manual UTF-8 decoding.");
                    }
                    else
                    {
                        Console.WriteLine("Test failed:");
                        Console.WriteLine($"  Original text      : {originalText}");
                        Console.WriteLine($"  Auto-detected text : {autoDetected}");
                        Console.WriteLine($"  Manual UTF-8 text  : {manualDecoded}");
                    }

                    // Only one barcode expected; exit after first result
                    break;
                }
            }
        }
    }
}