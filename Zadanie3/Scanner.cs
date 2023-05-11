﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base;

namespace Zadanie3
{
    public class Scanner : BaseDevice, IScanner
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
        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            if (GetState() == IDevice.State.off)
            {
                document = null;
                return;
            };

            document = new ImageDocument($"ImageScan{Counter:D3}");

            var outputString = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss ");
            outputString += $"Scan: {document.GetFileName()}.";
            switch (formatType)
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
        }

        public void Scan(out IDocument document)
        {
            if (GetState() == IDevice.State.off)
            {
                document = null;
                return;
            };

            document = new ImageDocument($"ImageScan{Counter:D3}");

            var outputString = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss ");
            outputString += $"Scan: {document.GetFileName()}.jpg";
            Console.WriteLine(outputString);

            Counter++;
        }
    }
}
