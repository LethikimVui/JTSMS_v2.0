using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email_Deviation
{
    public class DataViewModel
    {
        public string scriptid { get; set; }
        public string reqId { get; set; }
        public string station { get; set; }
        public string platform { get; set; }
        public int custId { get; set; }
        public string custName { get; set; }
        public string assemblyNumber { get; set; }
        public string assemblyRevision { get; set; }
        public string aging { get; set; }
        public string closureDate { get; set; }
        public string expiryDate { get; set; }

        //reqId, reqNumber, custId, stationId, assemblyNumber, assemblyRevision, , scriptname, scriptrev, description, filehash, typeId, PCNorDevNumber, changeDetail, statusId, submitDate, closureDate, firmware, firmwareRevision, platformId, scriptFileName, encriptedFileName, routeStepId, deviationClosedDate, isActive, createdBy, createdName, createdEmail, creationDate, updatedBy, updatedName, updatedEmail, updateDate, aging
    }
}
