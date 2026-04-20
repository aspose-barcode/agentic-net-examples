using System;
using Aspose.BarCode.Generation;

namespace Code16KQuietZoneDemo
{
    class Program
    {
        static void Main()
        {
            const string codeText = "1234567890";

            int[] leftCoefficients = { 10, 15, 20 };
            int[] rightCoefficients = { 1, 2, 3 };

            int total = Math.Max(leftCoefficients.Length, rightCoefficients.Length);

            for (int i = 0; i < total; i++)
            {
                int leftCoef = leftCoefficients[i % leftCoefficients.Length];
                int rightCoef = rightCoefficients[i % rightCoefficients.Length];

                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
                {
                    generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = leftCoef;
                    generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = rightCoef;

                    string fileName = $"Code16K_Left{leftCoef}_Right{rightCoef}.png";

                    generator.Save(fileName);
                }
            }
        }
    }
}