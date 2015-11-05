using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Script.Serialization;

using NetaDAL;

namespace NetaService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class NetaServices : INetaService
    {
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

        public List<average_performance_broadband> getAverages()
        {
            List<average_performance_broadband> list = new List<average_performance_broadband>();

            using (NetaDBEntities db = new NetaDBEntities())
            {
                var data = (from p in db.average_performance_broadband
                            select p);

                foreach (average_performance_broadband item in data)
                {
                    average_performance_broadband viewAverage = new average_performance_broadband();
                    viewAverage.county_id_fk = item.county_id_fk;
                    viewAverage.Average_Speed_mbps = item.Average_Speed_mbps;
                    viewAverage.Average_Pass_of_5_GCSES = item.Average_Pass_of_5_GCSES;
                    list.Add(viewAverage);
                }
                return list;
            }
        }

        public average_performance_broadband[] getCountyAveragePerformanceArray()
        {
            List<average_performance_broadband> list = new List<average_performance_broadband>();
            using (NetaDBEntities db = new NetaDBEntities())
            {
                var data = (from p in db.average_performance_broadband
                            select p);

                foreach (average_performance_broadband item in data)
                {
                    average_performance_broadband viewAverage = new average_performance_broadband();
                    viewAverage.Average_Pass_of_5_GCSES = item.Average_Pass_of_5_GCSES;
                    list.Add(viewAverage);
                }
                var array = list.ToArray();
                return array;
            }
        }

        public List<AuthorityPop_SyncSpeed> getUptakeByAuthority()
        {
            List<AuthorityPop_SyncSpeed> list = new List<AuthorityPop_SyncSpeed>();

            using (NetaDBEntities neta = new NetaDBEntities())
            {
                var myData = (from p in neta.la_pop_speed_ranked
                              select p);

                foreach (la_pop_speed_ranked item in myData)
                {
                    AuthorityPop_SyncSpeed apss = Utils.CreateUptakeFromTable(item);

                    list.Add(apss);
                }
            }
            return list;
        }

        public List<AuthorityEmployment_Speed> GetAuthorityEmployment_Speed()
        {
            List<AuthorityEmployment_Speed> list = new List<AuthorityEmployment_Speed>();

            using (NetaDBEntities neta = new NetaDBEntities())
            {
                var data = (from p in neta.authority_employment_syncspeed_rankings
                            select p);

                foreach (authority_employment_syncspeed_rankings item in data)
                {
                    AuthorityEmployment_Speed aes = Utils.CreateAuthEmploymentSpeedFromTable(item);
                    list.Add(aes);
                }
            }

            return list;
        }

        public List<SpearmansRank> CreateSpearmansRankTable()
        {
            List<SpearmansRank> list = new List<SpearmansRank>();

            using (NetaDBEntities neta = new NetaDBEntities())
            {
                var myData = (from p in neta.spearmans_ranks
                              select p);

                foreach (spearmans_ranks item in myData)
                {
                    SpearmansRank sr = Utils.CreateCorrelationsTable(item);

                    list.Add(sr);
                }
            }
            return list;
        }

        public decimal GetSchoolPR_BroadbandCorrelation()
        {
            int id = 3;
            decimal correlation;
            using (NetaDBEntities neta = new NetaDBEntities())
            {
                var getCorrelation = (from p in neta.spearmans_ranks
                                      where p.correlation_id == id
                                      select p.spearmans_rho).First();

                correlation = getCorrelation;
            }
            return correlation;
        }

        public decimal GetCountyAveragePerformance_BroadbandCorrelation()
        {
            int id = 1;
            decimal correlation;
            using (NetaDBEntities neta = new NetaDBEntities())
            {
                var getCorrelation = (from p in neta.spearmans_ranks
                                      where p.correlation_id == id
                                      select p.spearmans_rho).First();

                correlation = getCorrelation;
            }
            return correlation;
        }

        public decimal GetAuthorityPop_SyncSpeedCorrelation()
        {
            int id = 2;
            decimal correlation;
            using (NetaDBEntities neta = new NetaDBEntities())
            {
                var getCorrelation = (from p in neta.spearmans_ranks
                                      where p.correlation_id == id
                                      select p.spearmans_rho).First();

                correlation = getCorrelation;
            }
            return correlation;
        }

        public decimal GetAuthoritySpeed_EmploymentCorrelation()
        {
            int id = 4;
            decimal correlation;

            using (NetaDBEntities neta = new NetaDBEntities())
            {
                var getCorrelation = (from p in neta.spearmans_ranks
                                      where p.correlation_id == id
                                      select p.spearmans_rho).First();

                correlation = getCorrelation;
            }
            return correlation;
        }
    }
}
