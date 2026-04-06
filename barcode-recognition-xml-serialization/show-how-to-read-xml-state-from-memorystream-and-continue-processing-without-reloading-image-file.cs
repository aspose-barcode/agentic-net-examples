using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Step 1: Create a barcode generator and export its state to XML in a memory stream.
        using (var originalGenerator = new BarcodeGenerator(EncodeTypes.Code128, "ABC123"))
        {
            // Export the generator settings to XML.
            using (var xmlStream = new MemoryStream())
            {
                originalGenerator.ExportToXml(xmlStream);
                xmlStream.Position = 0; // Reset for reading.

                // Step 2: Import the generator from the XML memory stream.
                using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlStream))
                {
                    // Step 3: Generate the barcode image into another memory stream (PNG format).
                    using (var imageStream = new MemoryStream())
                    {
                        importedGenerator.Save(imageStream, BarCodeImageFormat.Png);
                        imageStream.Position = 0; // Reset for reading.

                        // Step 4: Read the barcode directly from the image memory stream without reloading from file.
                        using (var reader = new BarCodeReader(imageStream))
                        {
                            foreach (BarCodeResult result in reader.ReadBarCodes())
                            {
                                Console.WriteLine("Detected Type: " + result.CodeTypeName);
                                Console.WriteLine("Detected Text: " + result.CodeText);
                            }
                        }
                    }
                }
            }
        }
    }
}