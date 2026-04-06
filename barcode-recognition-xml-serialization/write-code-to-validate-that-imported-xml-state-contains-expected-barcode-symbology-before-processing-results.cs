using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        BaseEncodeType expectedType = EncodeTypes.Code128;

        string xmlPath = Path.Combine(Path.GetTempPath(), "barcode_state.xml");
        string imagePath = Path.Combine(Path.GetTempPath(), "barcode_image.png");

        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            generator.ExportToXml(xmlPath);
            generator.Save(imagePath);
        }

        using (BarcodeGenerator importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            if (importedGenerator.BarcodeType != expectedType)
            {
                throw new ArgumentException(
                    $"Unexpected barcode symbology. Expected: {expectedType}, but found: {importedGenerator.BarcodeType}.");
            }

            Console.WriteLine("Symbology validation passed: " + importedGenerator.BarcodeType);

            using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code128))
            {
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine("Decoded Type: " + result.CodeTypeName);
                    Console.WriteLine("Decoded Text: " + result.CodeText);
                }
            }
        }

        try { File.Delete(xmlPath); } catch { }
        try { File.Delete(imagePath); } catch { }
    }
}