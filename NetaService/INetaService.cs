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
        List<BBandPassRate> MyView();

        [OperationContract]
        List<average_performance_broadband> getAverages();

        [OperationContract]
        List<AuthorityPop_SyncSpeed> getUptakeByAuthority();

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        List<SpearmansRank> CreateSpearmansRankTable();

        [OperationContract]
        decimal GetSchoolPR_BroadbandCorrelation();

        [OperationContract]
        decimal GetCountyAveragePerformance_BroadbandCorrelation();

        [OperationContract]
        decimal GetAuthorityPop_SyncSpeedCorrelation();

        [OperationContract]
        decimal GetAuthoritySpeed_EmploymentCorrelation();
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


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class AuthorityPop_SyncSpeed
    {
        public AuthorityPop_SyncSpeed(NetaClassLib.la_pop_speed_ranked rankedTable)
        {
            ID = rankedTable.id;
            Authority = rankedTable.authority;
            PopSize = rankedTable.pop_size;
            AverageSync = rankedTable.average_sync_speed;
        }

        public AuthorityPop_SyncSpeed()
        {

        }

        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Authority { get; set; }

        [DataMember]
        public long PopSize { get; set; }

        [DataMember]
        public decimal? AverageSync { get; set; }

    }

    [DataContract]
    public class AuthorityEmployment_Speed
    {
        public AuthorityEmployment_Speed(NetaClassLib.authority_employment_syncspeed_rankings table)
        {
            ID = table.authority_id;
            Authority = table.authority_name;
            SyncSpeed = table.sync_speed;
            EmploymentRate = table.employment_rate;
            SpeedRanking = table.speed_ranking;
            EmploymentRanking = table.employment_ranking;
        }

        public AuthorityEmployment_Speed()
        {

        }

        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Authority { get; set; }

        [DataMember]
        public decimal SyncSpeed { get; set; }

        [DataMember]
        public int EmploymentRate { get; set; }

        [DataMember]
        public int? EmploymentRanking { get; set; }

        [DataMember]
        public int? SpeedRanking { get; set; }

    }

    [DataContract]
    public class SpearmansRank
    {

        public SpearmansRank(NetaClassLib.spearmans_ranks myTable)
        {
            CorrelationID = myTable.correlation_id;
            CorrelationName = myTable.correlation_name;
            SpearmansRho = myTable.spearmans_rho;
        }

        public SpearmansRank()
        {

        }

        [DataMember]
        public int CorrelationID { get; set; }

        [DataMember]
        public string CorrelationName { get; set; }

        [DataMember]
        public decimal SpearmansRho { get; set; }
    }

}
