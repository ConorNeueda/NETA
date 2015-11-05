using NetaService;
using NetaDAL;

class Utils
{
    public static BBandPassRate CreteBBandPassRateFromDBView(view_passrates_by_broadband view)
    {
        BBandPassRate ret = new BBandPassRate();

        ret.PostCode = view.postcode;
        ret.SchoolName = view.school_name;
        ret.PassRate = view.percent_5_pass_grades_gcse;
        ret.AverageSpeed = (decimal)view.average_speed_mbps;
        return ret;
    }

    public static AverageSpeed_AveragePass_County CreateCountyAveragesFromTable(average_performance_broadband table)
    {
        AverageSpeed_AveragePass_County ret = new AverageSpeed_AveragePass_County();

        ret.CountyID = table.county_id_fk;
        ret.AveragePass = table.Average_Pass_of_5_GCSES;
        ret.AverageSpeed = table.Average_Speed_mbps;
        ret.Id = table.id;

        return ret;
    }

    public static AuthorityPop_SyncSpeed CreateUptakeFromTable(la_pop_speed_ranked table)
    {
        AuthorityPop_SyncSpeed ret = new AuthorityPop_SyncSpeed();
        ret.ID = table.id;
        ret.Authority = table.authority;
        ret.PopSize = table.pop_size;
        ret.AverageSync = table.average_sync_speed;

        return ret;
    }

    public static AuthorityEmployment_Speed CreateAuthEmploymentSpeedFromTable(authority_employment_syncspeed_rankings table)
    {
        AuthorityEmployment_Speed ret = new AuthorityEmployment_Speed();
        ret.ID = table.authority_id;
        ret.Authority = table.authority_name;
        ret.EmploymentRate = table.employment_rate;
        ret.SyncSpeed = table.sync_speed;
        ret.EmploymentRate = table.employment_rate;
        ret.SpeedRanking = table.speed_ranking;

        return ret;
    }

    public static SpearmansRank CreateCorrelationsTable(spearmans_ranks table)
    {
        SpearmansRank ret = new SpearmansRank();
        ret.CorrelationID = table.correlation_id;
        ret.CorrelationName = table.correlation_name;
        ret.SpearmansRho = table.spearmans_rho;
        return ret;
    }
}
