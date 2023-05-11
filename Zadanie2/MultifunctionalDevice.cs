using Base;

namespace Zadanie2
{
    public class MultifunctionalDevice : BaseDevice, IFax
    {
        public new int Counter { get; private set; } = 0;
        public int PrintCounter { get; private set; } = 0;
        public int ScanCounter { get; private set; } = 0;
        public int FaxCounter { get; private set; } = 0;

        public new void PowerOn()
        {
            if (state == IDevice.State.on) return;
            state = IDevice.State.on;
            Counter++;
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
            PrintCounter++;
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            if (GetState() == IDevice.State.off)
            {
                document = null;
                return;
            };

            document = new ImageDocument($"ImageScan{ScanCounter:D3}");

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

            ScanCounter++;
        }

        public void Scan(out IDocument document)
        {
            if (GetState() == IDevice.State.off)
            {
                document = null;
                return;
            };

            document = new ImageDocument($"ImageScan{ScanCounter:D3}");

            var outputString = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss ");
            outputString += $"Scan: {document.GetFileName()}.jpg";
            Console.WriteLine(outputString);

            ScanCounter++;
        }

        public void ScanAndPrint()
        {
            if (GetState() == IDevice.State.off) return;

            IDocument document;
            Scan(out document);
            Print(document);
        }

        public void Fax(in IDocument document)
        {
            if (GetState() == IDevice.State.off) return;

            var outputString = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss ");
            outputString += $"Fax: {document.GetFileName()}.";
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
            FaxCounter++;
        }
    }
}