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
        // Sample UTF‑8 text containing non‑ASCII characters
        const string utf8Text = "Пример UTF‑8 текста";

        // Generate a PDF417 barcode with the UTF‑8 encoded text
        using (MemoryStream ms = new MemoryStream())
        {
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Pdf417))
            {
                // Encode the text using UTF‑8; the generator will embed the proper ECI identifier
                generator.SetCodeText(utf8Text, Encoding.UTF8);
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Prepare the stream for reading
            ms.Position = 0;

            // Create a reader for PDF417 barcodes
            using (BarCodeReader reader = new BarCodeReader(ms, DecodeType.Pdf417))
            {
                // Enable automatic detection of the text encoding
                reader.BarcodeSettings.DetectEncoding = true;

                // Read all barcodes in the image
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // The CodeText property already contains the correctly decoded string
                    Console.WriteLine("Detected barcode type: " + result.CodeTypeName);
                    Console.WriteLine("Decoded text: " + result.CodeText);
                }
            }
        }
    }
}