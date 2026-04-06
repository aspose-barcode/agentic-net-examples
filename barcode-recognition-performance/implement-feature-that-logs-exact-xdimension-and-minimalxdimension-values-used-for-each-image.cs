using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Define XDimension values (in pixels) to test
        float[] xDimensions = { 2f, 3f, 4f };

        foreach (float xDim in xDimensions)
        {
            // ---------- Generate barcode ----------
            string fileName = $"barcode_{xDim}pt.png";
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                // Set the XDimension using the Pixels unit
                generator.Parameters.Barcode.XDimension.Pixels = xDim;

                // Example code text
                generator.CodeText = "123456";

                // Save the barcode image
                generator.Save(fileName);
                Console.WriteLine($"Generated '{fileName}' with XDimension = {generator.Parameters.Barcode.XDimension.Pixels} pixels.");
            }

            // ---------- Recognize barcode and log MinimalXDimension ----------
            using (var reader = new BarCodeReader(fileName, DecodeType.Code128))
            {
                // Use MinimalXDimension mode for recognition
                reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;

                // Set MinimalXDimension to the same value used during generation
                reader.QualitySettings.MinimalXDimension = xDim;

                // Read barcodes (there will be one in this example)
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Read code text: {result.CodeText}");
                }

                // Log the MinimalXDimension used by the recognizer
                Console.WriteLine($"Recognition used MinimalXDimension = {reader.QualitySettings.MinimalXDimension} pixels.");
            }

            Console.WriteLine(new string('-', 50));
        }
    }
}