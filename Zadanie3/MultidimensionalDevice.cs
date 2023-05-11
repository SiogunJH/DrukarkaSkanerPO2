using Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie3
{
    public class MultidimensionalDevice : BaseDevice
    {
        public Printer Printer { get; set; }
        public Scanner Scanner { get; set; }
        public FaxMachine FaxMachine { get; set; }

        public int PrintCounter { get => Printer.Counter; }
        public int ScanCounter { get => Scanner.Counter; }
        public int FaxCounter { get => FaxMachine.Counter; }
        public new int Counter { get; private set; } = 0;
        
        public new void PowerOn()
        {
            if (state == IDevice.State.on) return;
            state = IDevice.State.on;
            Counter++;
            Console.WriteLine("MultiDevice is on!");
        }
        public new void PowerOff()
        {
            state = IDevice.State.off;
            Console.WriteLine("MultiDevice is off!");
        }

        public MultidimensionalDevice()
        {
            Printer = new Printer();
            Scanner = new Scanner();
            FaxMachine = new FaxMachine();
        }

        public void Print(in IDocument doc)
        {
            if (GetState() == IDevice.State.off) return;
            Printer.Print(in doc);
        }
        public void Scan(out IDocument doc)
        {
            if (GetState() == IDevice.State.off)
            {
                doc = null;
                return;
            };

            Scanner.Scan(out doc);
        }
        public void Scan(out IDocument doc, IDocument.FormatType formatType)
        {
            if (GetState() == IDevice.State.off)
            {
                doc = null;
                return;
            };

            Scanner.Scan(out doc, formatType);
        }
        public void ScanAndPrint()
        {
            if (GetState() == IDevice.State.off) return;

            IDocument document;
            Scan(out document);
            Print(in document);
        }

        public void Fax(in IDocument doc)
        {
            if (GetState() == IDevice.State.off) return;

            FaxMachine.Fax(doc);
        }
    }
}
