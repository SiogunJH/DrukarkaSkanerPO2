using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base;

namespace Zadanie5
{
    public class Printer : IPrinter
    {
        public IDevice.State state = IDevice.State.off;
        public IDevice.State GetState() => state;
        public void SetState(IDevice.State state) => this.state = state;
        public int Counter { get; private set; } = 0;
        public void PowerOn()
        {
            if (state == IDevice.State.on) return;
            state = IDevice.State.on;
        }
        public void PowerOff()
        {
            if (state == IDevice.State.off) return;
            state = IDevice.State.off;
        }
        public void StandbyOn()
        {
            if (state == IDevice.State.off) return;
            state = IDevice.State.standby;
        }
        public void StandbyOff()
        {
            if (state == IDevice.State.off) return;
            state = IDevice.State.on;
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
            Counter++;
            if (Counter%3==0) SetState(IDevice.State.standby);
        }
    }
}
