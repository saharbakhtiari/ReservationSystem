using Domain.FileManager;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.IO;

namespace Infrastructure.FileManager
{
    public class PdfFileFilter : IFileFilter
    {
        public byte[] Filter(byte[] data)
        {
            using (MemoryStream memoryStream = new MemoryStream(data))
            {
                PdfReader R = new PdfReader(memoryStream);
                //Loop through each piece of meta data and remove it
                foreach (KeyValuePair<string, string> KV in R.Info)
                {
                    R.Info.Remove(KV.Key);
                }
                
                using (MemoryStream ms = new MemoryStream())
                {
                    using (Document Doc = new Document())
                    {
                        //Use the PdfCopy object to copy each page
                        using (PdfCopy writer = new PdfCopy(Doc, ms))
                        {
                            Doc.Open();
                            //Loop through each page
                            for (int i = 1; i <= R.NumberOfPages; i++)
                            {
                                //Add it to the new document
                                writer.AddPage(writer.GetImportedPage(R, i));
                            }
                            Doc.Close();
                        }
                        return ms.ToArray();
                    }
                }
            }
        }
    }
}
