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

    public static TopBroadbandSpeed CreateTopSpeedsFromPostcodeSpeeds(postcode_speed postcodeView)
    {
        TopBroadbandSpeed topBroadband = new TopBroadbandSpeed();

        topBroadband.Lat = (decimal)postcodeView.latitude;
        topBroadband.Lng = (decimal)postcodeView.longitude;
 
        topBroadband.AverageSpeed = (decimal)postcodeView.average_speed_mbps;

        return topBroadband;
    }

}
