using System;
using System.IO;
using System.Threading;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    private static void ThreadRecognize(object readerObj)
    {
        BarCodeReader reader = (BarCodeReader)readerObj;
        try
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                Console.WriteLine($"BarCode CodeText: {result.CodeText}");
            }
        }
        catch (RecognitionAbortedException)
        {
            Console.WriteLine("Recognition was aborted.");
        }
    }

    static void Main()
    {
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            using (MemoryStream ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;

                using (BarCodeReader reader = new BarCodeReader(ms, DecodeType.Code128))
                {
                    Thread thread = new Thread(ThreadRecognize);
                    thread.Start(reader);
                    thread.Join();
                }
            }
        }
    }
}