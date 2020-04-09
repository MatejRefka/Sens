using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using Sens.DataModels;
using System;
using Sens.Utility;

namespace Sens {

    /// <summary>
    /// Establishes a connection between the DB and Project using a connection String.
    /// Sets up read and write query requests to the databse.
    /// </summary>

    public static class SQLiteDataAccess {


        #region Setup Connection String 
        //read the default connection string from App.config
        private static string ReadConnectionString(string id = "Default") {

            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
        #endregion Setup Connection String

        #region Write Queries
        //save a new record
        public static void WriteNewProfile(ProfileModel profile) {

            using (IDbConnection dbConnection = new SQLiteConnection(ReadConnectionString())) {

                dbConnection.Execute("INSERT INTO Profile (ID, ProfileName, BackgroundURL, ButtonMacro, SensitivityFrom, Sensitivity, DPI, Apex, CSGO, CS16, CSS, GMod, HL2, L4D2, Minecraft, Paladins," +
                    " Portal2, Quake, Rust, Smite, TF2, current) VALUES (@ID, @ProfileName, @BackgroundURL, @ButtonMacro, @SensitivityFrom, @Sensitivity, @DPI, @Apex, @CSGO, @CS16, @CSS, @GMod, " +
                    "@HL2, @L4D2, @Minecraft, @Paladins, @Portal2, @Quake, @Rust, @Smite, @TF2, @current)", profile);
            }
        }

        //edit a record where current = 1
        public static void WriteEditedProfile(ProfileModel profile) {

            using (IDbConnection dbConnection = new SQLiteConnection(ReadConnectionString())) {

                dbConnection.Execute("UPDATE Profile SET ProfileName=@ProfileName, BackgroundURL=@BackgroundURL, ButtonMacro=@ButtonMacro, SensitivityFrom=@SensitivityFrom, Sensitivity=@Sensitivity," +
                    "DPI=@DPI, Apex=@Apex, CSGO=@CSGO, CS16=@CS16, CSS=@CSS, GMod=@GMod, HL2=@HL2, L4D2=@L4D2, Minecraft=@Minecraft, Paladins=@Paladins, Quake=@Quake, Rust=@Rust, Smite=@Smite, TF2=@TF2, " +
                    "Portal2=@Portal2 WHERE current=1", profile);
            }
        }
        #endregion Write Queries

        #region Read Queries

        //return one record (ProfileModel) where current = 1
        public static List<ProfileModel> LoadCurrentProfile() {

            using (IDbConnection dbConnection = new SQLiteConnection(ReadConnectionString())) {

                var result = dbConnection.Query<ProfileModel>("SELECT * FROM Profile WHERE current=1", new DynamicParameters());
                List<ProfileModel> tempTest = new List<ProfileModel>();
                return result.ToList();
            }
        }

        //return all id values
        public static List<int> AllIDs() {

            using (IDbConnection dbConnection = new SQLiteConnection(ReadConnectionString())) {

                var result = dbConnection.Query<int>("SELECT id FROM Profile", new DynamicParameters());
                return result.ToList();
            }
        }

        //return all records in a list (List<ProfileModel>)
        public static List<ProfileModel> LoadAllProfiles() {

            using (IDbConnection dbConnection = new SQLiteConnection(ReadConnectionString())) {

                var result = dbConnection.Query<ProfileModel>("SELECT * FROM Profile", new DynamicParameters());
                return result.ToList();
            }
        }
        #endregion Read Queries

        #region Deleting records on x button click

        public static void DeleteRecord(IntWrapper iWrapper) {

            using (IDbConnection dbConnection = new SQLiteConnection(ReadConnectionString())) {

                dbConnection.Execute("DELETE FROM Profile WHERE id=@Integer", iWrapper);
            }
        }
        public static void UpdateID(NewOldIDWrapper idWrapper) {

            using (IDbConnection dbConnection = new SQLiteConnection(ReadConnectionString())) {

                dbConnection.Execute("UPDATE Profile SET id=@NewID WHERE id=@OldID", idWrapper);
            }
        }


        #endregion Deleting records on x button click

        //specific read/write:

        #region Update 'current' field for each record in the Profile table
        //each method sets 'current = 1' for each profile/record

        public static void CurrentProfile1() {

            using (IDbConnection dbConnection = new SQLiteConnection(ReadConnectionString())) {

                dbConnection.Execute("UPDATE Profile SET current=1 WHERE id=1");
            }
        }
        public static void CurrentProfile2() {

            using (IDbConnection dbConnection = new SQLiteConnection(ReadConnectionString())) {

                dbConnection.Execute("UPDATE Profile SET current=1 WHERE id=2");
            }
        }
        public static void CurrentProfile3() {

            using (IDbConnection dbConnection = new SQLiteConnection(ReadConnectionString())) {

                dbConnection.Execute("UPDATE Profile SET current=1 WHERE id=3");
            }
        }
        public static void CurrentProfile4() {

            using (IDbConnection dbConnection = new SQLiteConnection(ReadConnectionString())) {

                dbConnection.Execute("UPDATE Profile SET current=1 WHERE id=4");
            }
        }
        public static void CurrentProfile5() {

            using (IDbConnection dbConnection = new SQLiteConnection(ReadConnectionString())) {

                dbConnection.Execute("UPDATE Profile SET current=1 WHERE id=5");
            }
        }
        public static void CurrentProfile6() {

            using (IDbConnection dbConnection = new SQLiteConnection(ReadConnectionString())) {

                dbConnection.Execute("UPDATE Profile SET current=1 WHERE id=6");
            }
        }
        public static void CurrentProfile7() {

            using (IDbConnection dbConnection = new SQLiteConnection(ReadConnectionString())) {

                dbConnection.Execute("UPDATE Profile SET current=1 WHERE id=7");
            }
        }
        public static void CurrentProfile8() {

            using (IDbConnection dbConnection = new SQLiteConnection(ReadConnectionString())) {

                dbConnection.Execute("UPDATE Profile SET current=1 WHERE id=8");
            }
        }
        public static void CurrentProfile9() {

            using (IDbConnection dbConnection = new SQLiteConnection(ReadConnectionString())) {

                dbConnection.Execute("UPDATE Profile SET current=1 WHERE id=9");
            }
        }
        #endregion Update 'current' field for each record in the Profile table

        #region Reset all 'current' fields in all records to 0
        public static void ResetAllCurrentFields() {

            using (IDbConnection dbConnection = new SQLiteConnection(ReadConnectionString())) {

                dbConnection.Execute("UPDATE Profile SET current=0");
            }
        }
        #endregion Reset all 'current' fields in all records to 0

    }
}

