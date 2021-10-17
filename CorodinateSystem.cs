using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RocketCoordinateCheckerAPI
{
    public class CorodinateSystem
    {
        public string clashResult = "";
        public string landingPositionResult = "";
        public string coordinateConfigurationResult = "";
        public bool IsClashExist = false;
        public bool IsCoorditanesAreGood = false;
        public bool IsConfigValuesAreGood = false;
        public bool IsLandingPositionCorrect = false;
        public bool IsPlatformgPositionCorrect = false;
        public int sizplatformSize_Xe_X { get; set; }
        public int platformSize_Y { get; set; }
        public CorodinateSystem()
        {

        }

        public void CheckCoordinates(int coordinate_X, int coordinate_Y)
        {
            try
            {
                //  Just Check if we have proper integers and equal X and Y values.. 
                int tempCoordinate_X = int.Parse(coordinate_X.ToString()); 
                int tempCoordinate_Y = int.Parse(coordinate_Y.ToString());

                if ((coordinate_X > 0 && coordinate_X < GlobalVariables.landingSize_X ) && (coordinate_Y > 0 && coordinate_Y < GlobalVariables.landingSize_Y)) // We ant platform coordinate inside the landing are
                {
                    coordinateConfigurationResult = Constants.OK;
                    IsCoorditanesAreGood = true; // ONLY THIS IS VALID COORDINATE
                }
                else
                {
                    coordinateConfigurationResult = Constants.ERROR + " : " + Constants.WrongDimensions; // 400: Bad Request.  NOTE: Error descriptions can be detailed for better UX                    
                    IsCoorditanesAreGood = false;
                }
            }
            catch (Exception ex)
            {
                IsCoorditanesAreGood = false;

                //---------  Error Handling/Logging will be here  -------------
                Console.WriteLine(ex.Message);                
                //-------------------------------------------------------------
            }
        }

        public void ConfigureCoordinates(int coordinate_X, int coordinate_Y, int sizeX, int sizeY)
        {
            try
            {
                //  Just Check if we have proper integers and equal X and Y values.. 
                int tempCoordinate_X = int.Parse(coordinate_X.ToString());
                int tempCoordinate_Y = int.Parse(coordinate_Y.ToString());

                // We just want square area.  **Can be modified.
                if (sizeX != sizeY)
                {
                    IsConfigValuesAreGood = false;
                }
                else
                {
                    
                    //Check Positioning; We ant platform coordinate inside the landing are
                    if ( (coordinate_X > 0 && coordinate_X < GlobalVariables.landingSize_X) && (coordinate_Y > 0 && coordinate_Y < GlobalVariables.landingSize_Y)) 
                    {
                        // We also want proper integers for coordinate values; just need to check ===>> IsCoorditanesAreGood
                        CheckCoordinates(coordinate_X, coordinate_Y);
                        if (IsCoorditanesAreGood == true)
                        {
                            // Set Platform size
                            GlobalVariables.platformSize_X = sizeX;
                            GlobalVariables.platformSize_Y = sizeY;
                        }


                        // We dont want bigger platform size than the landing area
                        if (GlobalVariables.platformSize_X < GlobalVariables.landingSize_X && GlobalVariables.platformSize_Y < GlobalVariables.landingSize_Y) 
                        {                            
                            if (IsCoorditanesAreGood == true) 
                            {
                                GlobalVariables.platformCoordinate_X = coordinate_X;
                                GlobalVariables.platformCoordinate_Y = coordinate_Y;

                                coordinateConfigurationResult = Constants.OK;
                                IsConfigValuesAreGood = true; 
                            }
                            else
                            {
                                coordinateConfigurationResult = Constants.ERROR + " : " + Constants.WrongPlatformDimensions;
                                IsConfigValuesAreGood = false;
                            }
                        }
                        else
                        {
                            coordinateConfigurationResult = Constants.ERROR + " : " + Constants.PlatformBiggerThanLandingArea;
                            IsConfigValuesAreGood = false;
                        }
                    }
                    else
                    {
                        coordinateConfigurationResult = Constants.ERROR + " : " + Constants.PlatformOutsideTheLandingArea;
                        IsConfigValuesAreGood = false;
                    }

                }
            }
            catch (Exception ex)
            {
                IsCoorditanesAreGood = false;

                //---------  Error Handling/Logging will be here  -------------
                Console.WriteLine(ex.Message);
                //-------------------------------------------------------------
            }
        }

        public void CheckPreviosRocketCoordinates(int coordinateX, int coordinateY)
        {
            try
            {
                if (GlobalVariables.previousRocketCoordinateX > 0 && GlobalVariables.previousRocketCoordinateY > 0) // If there 's prev'os land'ng
                {
                    // Calculate difference of X coordinates as absolute numbers because we dont consider positive/negatif direction
                    int X_CoordinateAbsValue = Math.Abs(GlobalVariables.previousRocketCoordinateX - coordinateX);
                    int Y_CoordinateAbsValue = Math.Abs(GlobalVariables.previousRocketCoordinateY - coordinateY);

                    //If we are inside the borders
                    if (X_CoordinateAbsValue >= 2 && Y_CoordinateAbsValue >= 2) // we want enough distance
                    {
                        IsClashExist = false;  // We want enough space, with greater values than "1" 
                        clashResult = Constants.OkForLanding;
                    }
                    else
                    {
                        IsClashExist = true; // We simply don't want close locations to avoid clashes
                        clashResult = Constants.Clash;
                    }
                }
                else
                {
                    IsClashExist = false;
                    clashResult = Constants.OkForLanding; // If No previous landing exists yet.
                }
            }
            catch (Exception ex)
            {
                IsClashExist = true; // A Potential claches !
                clashResult = Constants.Clash;

                //---------  Error Handling/Logging will be here  -------------
                Console.WriteLine(ex.Message);
                //-------------------------------------------------------------
            }
        }

        public void CheckForLanding(int coordinateX, int coordinateY)
        {
            try
            {
                //Check Coordinate values
                CheckCoordinates(coordinateX, coordinateY);

                if (IsCoorditanesAreGood == true) // Check results of CheckCoordinates() and  CheckPreviosRocketCoordinates()
                {

                    CheckPreviosRocketCoordinates(coordinateX, coordinateY);
                    if (IsClashExist == false) // If there is No clash
                    {
                        // LANDING POSITION calculations
                        int X_LandingCoordinateAbsValue = Math.Abs(GlobalVariables.landingSize_X - coordinateX);
                        int Y_LandingCoordinateAbsValue = Math.Abs(GlobalVariables.landingSize_Y - coordinateY);

                        // PLATFORM POSITION calculations
                        //int X_PlatformCoordinateAbsValue = Math.Abs(GlobalVariables.platformCoordinate_X - coordinateX); 
                        //int Y_PlatformCoordinateAbsValue = Math.Abs(GlobalVariables.platformCoordinate_Y - coordinateY); 

                        // check to see if coordinates are inside the LANDING area.
                        if ((X_LandingCoordinateAbsValue >= 0 && X_LandingCoordinateAbsValue < GlobalVariables.landingSize_X) && (Y_LandingCoordinateAbsValue >= 0 && Y_LandingCoordinateAbsValue < GlobalVariables.landingSize_Y))
                        {
                            // check to see if coordinates are inside the PLATFORM area.
                            if ((coordinateX > GlobalVariables.platformCoordinate_X && coordinateX < (GlobalVariables.platformCoordinate_X + GlobalVariables.platformSize_X)) &&
                                (coordinateY > GlobalVariables.platformCoordinate_Y && coordinateY < (GlobalVariables.platformCoordinate_Y + GlobalVariables.platformSize_Y))
                               )
                            {
                                IsLandingPositionCorrect = true;  // OK for landing
                                landingPositionResult = Constants.OkForLanding;

                                //Iflanding is OK. Then also set it as the "previousCoordinate" for the next comparisions
                                GlobalVariables.previousRocketCoordinateX = coordinateX;
                                GlobalVariables.previousRocketCoordinateY = coordinateY;
                            }
                            else
                            {
                                IsLandingPositionCorrect = false; // Outside the Platform
                                landingPositionResult = Constants.OutOfPlatform;
                            }
                        }
                        else
                        {
                            IsLandingPositionCorrect = false; // Outside the Landing Area
                            landingPositionResult = Constants.OutOfPlatform;
                        }
                    }
                    else
                    {
                        IsLandingPositionCorrect = false; // Outside the Landing Area
                        landingPositionResult = Constants.Clash;
                    }
                }
                else
                {
                    IsLandingPositionCorrect = false; // Outside the Landing Area
                    landingPositionResult = Constants.WrongDimensions;
                }
            }
            catch (Exception ex)
            {
                IsLandingPositionCorrect = false; // Potential Out Of Platform case !
                landingPositionResult = Constants.OutOfPlatform;

                //---------  Error Handling/Logging will be here  -------------
                Console.WriteLine(ex.Message);
                //-------------------------------------------------------------
            }
        }


    }
}
