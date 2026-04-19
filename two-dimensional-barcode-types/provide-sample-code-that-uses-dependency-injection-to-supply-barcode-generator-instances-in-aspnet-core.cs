using System;
using Microsoft.Extensions.DependencyInjection;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing;

namespace AsposeBarcodeDiSample
{
    public interface IBarcodeGeneratorFactory
    {
        BarcodeGenerator CreateGenerator(BaseEncodeType type, string codeText);
    }

    public class BarcodeGeneratorFactory : IBarcodeGeneratorFactory
    {
        public BarcodeGenerator CreateGenerator(BaseEncodeType type, string codeText)
        {
            return new BarcodeGenerator(type, codeText);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddTransient<IBarcodeGeneratorFactory, BarcodeGeneratorFactory>();
            var provider = services.BuildServiceProvider();

            var factory = provider.GetRequiredService<IBarcodeGeneratorFactory>();

            using (var generator = factory.CreateGenerator(EncodeTypes.Code128, "DI12345"))
            {
                // Set barcode appearance
                generator.Parameters.Barcode.BarColor = Color.Blue;
                generator.Parameters.Barcode.XDimension.Point = 2f;

                // Set image size
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 150f;

                // Save the barcode image
                generator.Save("di_barcode.png");
            }
        }
    }
}