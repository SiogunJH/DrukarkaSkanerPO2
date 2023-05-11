using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base;

namespace Zadanie3
{
    public class Printer : BaseDevice, IPrinter
    {
        public new int Counter { get; private set; } = 0;
        public new void PowerOn()
        {
            if (state == IDevice.State.on) return;
            state = IDevice.State.on;
        }
        public new void PowerOff()
        {
            if (state == IDevice.State.off) return;
            state = IDevice.State.off;
        }
        public void Print(in IDocument document)
        {
            if (GetState() == IDevice.State.off) return;

            var outputString = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss ");
            outputString += $"Print: {document.GetFileName()}.";
            switch (document.GetFormatType())
            {
                case IDocument.FormatType.PDF:
                    outputString += "pdf";
                    break;
                case IDocument.FormatType.TXT:
                    outputString += "txt";
                    break;
                case IDocument.FormatType.JPG:
                    outputString += "jpg";
                    break;
            }
            Console.WriteLine(outputString);
            Console.Write($"Succesfully printed some stuff! {Counter} -> ");
            Counter++;
            Console.WriteLine($"{Counter}");
        }
    }
}
