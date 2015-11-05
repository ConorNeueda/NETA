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
}
