using System;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeDemo
{
    class Program
    {
        static void Main()
        {
            var samples = new List<string> { "ABC123", "9876543210", "CODE128TEST" };
            int index = 1;
            foreach (var text in samples)
            {
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
                {
                    // Show the human‑readable text below the barcode
                    generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
                    generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

                    string fileName = $"code128_{index}.png";
                    generator.Save(fileName);
                    Console.WriteLine($"Saved {fileName} with text '{text}'.");
                }
                index++;
            }
        }
    }
}