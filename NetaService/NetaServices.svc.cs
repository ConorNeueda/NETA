using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

using NetaDAL;

namespace NetaService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class NetaServices : INetaService
    {
        public List<TopBroadbandSpeed> Coordinates()
        {
            List<TopBroadbandSpeed> lstCoordinates = new List<TopBroadbandSpeed>();

            using(NetaDBEntities netaDB = new NetaDBEntities())
            {
                var data = from  p in netaDB.postcode_speed
                           where p.average_speed_mbps == 30
                           select p;

                foreach(postcode_speed item in data)
                {
                    TopBroadbandSpeed topSpeed = Utils.CreateTopSpeedsFromPostcodeSpeeds(item);

                    lstCoordinates.Add(topSpeed);
                }
            }

            return lstCoordinates;
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public List<BBandPassRate> MyView()
        {
            List<BBandPassRate> lstPassRates = new List<BBandPassRate>();

            using (NetaDBEntities neta = new NetaDBEntities())
            {
                var myData = from p in neta.view_passrates_by_broadband select p;

                foreach(view_passrates_by_broadband viewItem in myData)
                {
                    BBandPassRate bbRate = Utils.CreteBBandPassRateFromDBView(viewItem);

                    lstPassRates.Add(bbRate);
                }
            }

            return lstPassRates;
        }
    }
}
