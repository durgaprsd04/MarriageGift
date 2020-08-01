using System.IO;
using MarriageGift.Model;
using MarriageGift.Model.Interfaces;
using MarriageGift.Model.CustomerModel;
using System.Runtime.Serialization.Formatters.Binary;
using log4net;
using MarriageGift.FAO.Interfaces;

namespace MarriageGift.FAO
{
    public class SaveToFileFao: ISaveToFileFao
    {
        private readonly ILog logger;
        private readonly FileStream streamWriter;
        private readonly StreamWriter csvWriter;

        public SaveToFileFao(ILog logger, FileStream  streamWriter, StreamWriter csvWriter)
        {
            this.logger = logger;
            this.streamWriter = streamWriter;
            this.csvWriter = csvWriter;
        }

        public void SaveObject(ICustomer baseObject)
        {
            var customer = (Customer)baseObject;
            var formatter = new BinaryFormatter();
            formatter.Serialize(streamWriter, baseObject);
        }

        public void WriteRecords(IBaseObject record)
        {
            csvWriter.WriteLine(record.ToString());
            csvWriter.Close();
        }
    }
}
