using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISA_API
{
    //Interface
    public interface ITipType
    {
        string GetCategory();
    }

    //Concrete Implementation
    public class HealthTip : ITipType
    {
        public string GetCategory()
        {
            return "/tip/health";
        }
    }

    public class WeatherTip : ITipType
    {
        public string GetCategory()
        {
            return "/tip/weather";
        }
    }

    public class StudyTip : ITipType
    {
        public string GetCategory()
        {
            return "/tip/study";
        }
    }

}
