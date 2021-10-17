using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RocketCoordinateCheckerAPI
{
    public static class GlobalVariables
    {
        //Fixed (const) values of Landing Area 
        public const int landingSize_X = 100;  
        public const int landingSize_Y = 100;

        //Configurable X-Y size of Platform Area.
        public static int platformSize_X = 10; //Default is 0
        public static int platformSize_Y = 10; //Default is 0
        public static int platformCoordinate_X = 0;
        public static int platformCoordinate_Y = 0;

        //Coordinates of previos rocket's location;  Default is -1 which means no previous rocket landed.
        public static int previousRocketCoordinateX = -1;
        public static int previousRocketCoordinateY = -1;
    }


    public static class Constants
    {
        public static string OK = "OK";
        public static string Clash = "clash";
        public static string ERROR = "ERROR";
        public static string OkForLanding = "ok for landing";        
        public static string OutOfPlatform = "out of platform";        
        public static string WrongPlatformDimensions = "Wrong Platform Dimensions";
        public static string WrongDimensions = "Dimensions are out of the scope!";
        public static string PlatformBiggerThanLandingArea = "Platform area should not bigger than the Landing area";
        public static string PlatformOutsideTheLandingArea = "Platform coordinate should not be outside the Landing area";
}
}
