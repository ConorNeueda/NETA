using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using NetaDAL;

namespace NetaService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface INetaService
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        List<BBandPassRate> MyView();

        [OperationContract]
        List<TopBroadbandSpeed> Coordinates();

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here
    }

    [DataContract]
    public class BBandPassRate
    {
        public BBandPassRate(NetaDAL.view_passrates_by_broadband myView)
        {
            PostCode = myView.postcode;
            AverageSpeed = (decimal)myView.average_speed_mbps;
            PassRate = (decimal)myView.percent_5_pass_grades_gcse;
            SchoolName = myView.school_name;

        }

        public BBandPassRate()
        {
        }

        [DataMember]
        public string PostCode { get; set; }
        [DataMember]
        public decimal AverageSpeed { get; set; }
        [DataMember]
        public decimal PassRate { get; set; }
        [DataMember]
        public string SchoolName { get; set; }
    }

    [DataContract]
    public class TopBroadbandSpeed
    {
        public TopBroadbandSpeed(NetaDAL.postcode_speed postcodeSpeed)
        {
            AverageSpeed = (decimal)postcodeSpeed.average_speed_mbps;
            Lat = (decimal)postcodeSpeed.latitude;
            Lng = (decimal)postcodeSpeed.longitude;
        }

        public TopBroadbandSpeed() { }

        [DataMember]
        public decimal AverageSpeed { get; set; }
        [DataMember]
        public decimal Lat { get; set; }
        [DataMember]
        public decimal Lng { get; set; }
    }
    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
