using System;
using System.Collections.Generic;
using System.IO;
using MarriageGift.Controller.Interfaces;
using MarriageGift.Model;
using log4net;
namespace MarriageGift.Controller
{
    public class SaveToFileController: ISaveToFileController
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
