using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Step 1: Generate a sample barcode image to work with
        const string barcodeFile = "sample.png";
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "PasswordProtected123";
            generator.Save(barcodeFile);
        }

        // Step 2: Prepare to read a password‑protected image
        // If the image file is encrypted (e.g., PDF, TIFF with password),
        // you would need to open it with the appropriate library that supports
        // password handling (such as Aspose.Imaging) and obtain a decrypted stream.
        // The decrypted stream is then supplied to BarCodeReader.
        // Below is a placeholder demonstrating this approach.

        Stream imageStream;
        // ---- Begin placeholder for password handling ----
        // Example (pseudo‑code):
        // var loadOptions = new Aspose.Imaging.ImageLoadOptions { Password = "myPassword" };
        // var image = Aspose.Imaging.Image.Load(barcodeFile, loadOptions);
        // using (var ms = new MemoryStream())
        // {
        //     image.Save(ms, Aspose.Imaging.ImageFormat.Png);
        //     ms.Position = 0;
        //     imageStream = ms;
        // }
        // ---- End placeholder ----

        // For the purpose of this self‑contained example, the image is not actually protected,
        // so we simply open a FileStream.
        using (var fs = new FileStream(barcodeFile, FileMode.Open, FileAccess.Read))
        {
            imageStream = fs;
            // Note: Do not dispose the stream here because BarCodeReader will use it.
            // The using block will dispose after reading.
            using (var reader = new BarCodeReader())
            {
                // Supply the image (or decrypted stream) to the reader
                reader.SetBarCodeImage(imageStream);
                // Optionally specify which barcode types to look for
                reader.BarCodeReadType = DecodeType.Code128;

                // Perform recognition
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                    Console.WriteLine($"Detected Text: {result.CodeText}");
                }
            }
        }
    }
}