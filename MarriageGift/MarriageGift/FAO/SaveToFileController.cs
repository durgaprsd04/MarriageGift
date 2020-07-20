using System.IO;

using MarriageGift.Model;
using log4net;
using MarriageGift.FAO.Interfaces;

namespace MarriageGift.FAO
{
    public class SaveToFileController: ISaveToFileFao
    {
        private readonly ILog logger;
        private readonly StreamWriter streamWriter;
        public SaveToFileController(ILog logger, StreamWriter streamWriter)
        {
            this.logger = logger;
            this.streamWriter = streamWriter;
        }
        public void SaveCustomer(IBaseObject customer)
        {
            WriteRecords(customer.ToString());
        }
        public void WriteRecords(string record)
        {
            streamWriter.WriteLine(record);
        }
    }
}
