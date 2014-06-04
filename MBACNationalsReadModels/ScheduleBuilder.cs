using System;
using System.Collections.Generic;
using BowlingCentre = MBACNationals.ReadModels.ScheduleQueries.BowlingCentre;
using Game = MBACNationals.ReadModels.ScheduleQueries.Game;

namespace MBACNationals.ReadModels
{
    static class ScheduleBuilder
    {
        public static ScheduleQueries.Schedule Singles(string division)
        {
            return new ScheduleQueries.Schedule(Guid.NewGuid())
            {
                Division = division,
                Games = new List<Game>
                {
                     new Game(01, "SO", "AB", 1, BowlingCentre.Rossmere), new Game(01, "NO", "MB", 3, BowlingCentre.Rossmere), new Game(01, "BC", "NL", 5, BowlingCentre.Rossmere), new Game(01, "QC", "SK", 7, BowlingCentre.Rossmere), 
                     new Game(02, "BC", "SK", 1, BowlingCentre.Rossmere), new Game(02, "NL", "QC", 3, BowlingCentre.Rossmere), new Game(02, "MB", "SO", 5, BowlingCentre.Rossmere), new Game(02, "AB", "NO", 7, BowlingCentre.Rossmere), 
                     new Game(03, "NL", "NO", 1, BowlingCentre.Rossmere), new Game(03, "SK", "SO", 3, BowlingCentre.Rossmere), new Game(03, "QC", "AB", 5, BowlingCentre.Rossmere), new Game(03, "MB", "BC", 7, BowlingCentre.Rossmere), 
                     new Game(04, "QC", "MB", 1, BowlingCentre.Rossmere), new Game(04, "AB", "BC", 3, BowlingCentre.Rossmere), new Game(04, "NO", "SK", 5, BowlingCentre.Rossmere), new Game(04, "SO", "NL", 7, BowlingCentre.Rossmere), 
                     new Game(05, "SK", "AB", 1, BowlingCentre.Rossmere), new Game(05, "MB", "NL", 3, BowlingCentre.Rossmere), new Game(05, "SO", "QC", 5, BowlingCentre.Rossmere), new Game(05, "BC", "NO", 7, BowlingCentre.Rossmere), 
                     new Game(06, "NO", "SO", 1, BowlingCentre.Rossmere), new Game(06, "BC", "QC", 3, BowlingCentre.Rossmere), new Game(06, "NL", "SK", 5, BowlingCentre.Rossmere), new Game(06, "AB", "MB", 7, BowlingCentre.Rossmere), 
                     new Game(07, "AB", "NL", 1, BowlingCentre.Rossmere), new Game(07, "SK", "MB", 3, BowlingCentre.Rossmere), new Game(07, "QC", "NO", 5, BowlingCentre.Rossmere), new Game(07, "SO", "BC", 7, BowlingCentre.Rossmere), 

                     new Game(08, "SO", "NL", 1, BowlingCentre.Coronation), new Game(08, "MB", "QC", 3, BowlingCentre.Coronation), new Game(08, "BC", "AB", 5, BowlingCentre.Coronation), new Game(08, "SK", "NO", 7, BowlingCentre.Coronation), 
                     new Game(09, "SK", "BC", 1, BowlingCentre.Coronation), new Game(09, "NO", "AB", 3, BowlingCentre.Coronation), new Game(09, "QC", "NL", 5, BowlingCentre.Coronation), new Game(09, "SO", "MB", 7, BowlingCentre.Coronation), 
                     new Game(10, "NO", "QC", 1, BowlingCentre.Coronation), new Game(10, "BC", "SO", 3, BowlingCentre.Coronation), new Game(10, "MB", "SK", 5, BowlingCentre.Coronation), new Game(10, "NL", "AB", 7, BowlingCentre.Coronation), 
                     new Game(11, "MB", "AB", 1, BowlingCentre.Coronation), new Game(11, "SK", "NL", 3, BowlingCentre.Coronation), new Game(11, "SO", "NO", 5, BowlingCentre.Coronation), new Game(11, "QC", "BC", 7, BowlingCentre.Coronation), 
                     new Game(12, "QC", "SO", 1, BowlingCentre.Coronation), new Game(12, "NO", "BC", 3, BowlingCentre.Coronation), new Game(12, "NL", "MB", 5, BowlingCentre.Coronation), new Game(12, "AB", "SK", 7, BowlingCentre.Coronation), 
                     new Game(13, "NO", "NL", 1, BowlingCentre.Coronation), new Game(13, "SO", "SK", 3, BowlingCentre.Coronation), new Game(13, "AB", "QC", 5, BowlingCentre.Coronation), new Game(13, "BC", "MB", 7, BowlingCentre.Coronation), 
                     new Game(14, "AB", "SO", 1, BowlingCentre.Coronation), new Game(14, "MB", "NO", 3, BowlingCentre.Coronation), new Game(14, "NL", "BC", 5, BowlingCentre.Coronation), new Game(14, "SK", "QC", 7, BowlingCentre.Coronation), 
 
                     new Game(15, "SK", "AB", 1, BowlingCentre.Academy), new Game(15, "MB", "NL", 3, BowlingCentre.Academy), new Game(15, "QC", "SO", 5, BowlingCentre.Academy), new Game(15, "BC", "NO", 7, BowlingCentre.Academy), 
                     new Game(16, "SO", "BC", 1, BowlingCentre.Academy), new Game(16, "QC", "NO", 3, BowlingCentre.Academy), new Game(16, "MB", "SK", 5, BowlingCentre.Academy), new Game(16, "NL", "AB", 7, BowlingCentre.Academy), 
                     new Game(17, "NO", "MB", 1, BowlingCentre.Academy), new Game(17, "SO", "AB", 3, BowlingCentre.Academy), new Game(17, "BC", "NL", 5, BowlingCentre.Academy), new Game(17, "SK", "QC", 7, BowlingCentre.Academy), 
                     new Game(18, "NL", "SK", 1, BowlingCentre.Academy), new Game(18, "BC", "QC", 3, BowlingCentre.Academy), new Game(18, "AB", "MB", 5, BowlingCentre.Academy), new Game(18, "NO", "SO", 7, BowlingCentre.Academy), 
                     new Game(19, "AB", "BC", 1, BowlingCentre.Academy), new Game(19, "SK", "NO", 3, BowlingCentre.Academy), new Game(19, "NL", "SO", 5, BowlingCentre.Academy), new Game(19, "QC", "MB", 7, BowlingCentre.Academy), 
                     new Game(20, "NO", "NL", 1, BowlingCentre.Academy), new Game(20, "MB", "BC", 3, BowlingCentre.Academy), new Game(20, "QC", "AB", 5, BowlingCentre.Academy), new Game(20, "SO", "SK", 7, BowlingCentre.Academy), 
                     new Game(21, "MB", "SO", 1, BowlingCentre.Academy), new Game(21, "NL", "QC", 3, BowlingCentre.Academy), new Game(21, "BC", "SK", 5, BowlingCentre.Academy), new Game(21, "AB", "NO", 7, BowlingCentre.Academy), 
                }
            };
        }

        public static ScheduleQueries.Schedule TournamentLadies()
        {
            return new ScheduleQueries.Schedule(Guid.NewGuid())
            {
                Division = "Tournament Ladies",
                Games = new List<Game>
                {
                     new Game(01, "MB", "AB", 01, BowlingCentre.Academy), new Game(01, "NO", "BC", 05, BowlingCentre.Academy), new Game(01, "NL", "SO", 09, BowlingCentre.Academy),
                     new Game(02, "NL", "AB", 05, BowlingCentre.Academy), new Game(02, "SO", "MB", 07, BowlingCentre.Academy), new Game(02, "SK", "NO", 09, BowlingCentre.Academy),
                     new Game(03, "SO", "NO", 01, BowlingCentre.Academy), new Game(03, "MB", "SK", 03, BowlingCentre.Academy), new Game(03, "BC", "NL", 07, BowlingCentre.Academy),
                     new Game(04, "NO", "NL", 03, BowlingCentre.Academy), new Game(04, "AB", "SK", 07, BowlingCentre.Academy), new Game(04, "BC", "SO", 11, BowlingCentre.Academy),
                     new Game(05, "BC", "AB", 03, BowlingCentre.Academy), new Game(05, "SO", "SK", 05, BowlingCentre.Academy), new Game(05, "NO", "MB", 13, BowlingCentre.Academy),
                     new Game(06, "MB", "BC", 09, BowlingCentre.Academy), new Game(06, "AB", "NO", 11, BowlingCentre.Academy), new Game(06, "SK", "NL", 13, BowlingCentre.Academy),
                     new Game(07, "SK", "BC", 01, BowlingCentre.Academy), new Game(07, "NL", "MB", 11, BowlingCentre.Academy), new Game(07, "AB", "SO", 13, BowlingCentre.Academy),
                                                                                                                                              
                     new Game(08, "NO", "SO", 15, BowlingCentre.Academy), new Game(08, "NL", "BC", 19, BowlingCentre.Academy), new Game(08, "SK", "MB", 21, BowlingCentre.Academy), 
                     new Game(09, "SK", "AB", 15, BowlingCentre.Academy), new Game(09, "NL", "NO", 17, BowlingCentre.Academy), new Game(09, "SO", "BC", 21, BowlingCentre.Academy), 
                     new Game(10, "MB", "SO", 17, BowlingCentre.Academy), new Game(10, "NO", "SK", 19, BowlingCentre.Academy), new Game(10, "NL", "AB", 21, BowlingCentre.Academy), 
                     new Game(11, "MB", "NL", 15, BowlingCentre.Academy), new Game(11, "BC", "SK", 17, BowlingCentre.Academy), new Game(11, "SO", "AB", 19, BowlingCentre.Academy), 
                     new Game(12, "AB", "MB", 23, BowlingCentre.Academy), new Game(12, "BC", "NO", 25, BowlingCentre.Academy), new Game(12, "SO", "NL", 29, BowlingCentre.Academy), 
                     new Game(13, "SK", "SO", 23, BowlingCentre.Academy), new Game(13, "AB", "BC", 27, BowlingCentre.Academy), new Game(13, "MB", "NO", 29, BowlingCentre.Academy), 
                     
                     new Game(14, "SK", "NL", 03, BowlingCentre.Rossmere), new Game(14, "AB", "NO", 07, BowlingCentre.Rossmere), new Game(14, "MB", "BC", 09, BowlingCentre.Rossmere), 
                     new Game(15, "AB", "SO", 03, BowlingCentre.Rossmere), new Game(15, "MB", "NL", 05, BowlingCentre.Rossmere), new Game(15, "BC", "SK", 07, BowlingCentre.Rossmere),
                     new Game(16, "NO", "MB", 03, BowlingCentre.Rossmere), new Game(16, "BC", "AB", 05, BowlingCentre.Rossmere), new Game(16, "SO", "SK", 09, BowlingCentre.Rossmere),
                     new Game(17, "SO", "NO", 15, BowlingCentre.Rossmere), new Game(17, "NL", "BC", 17, BowlingCentre.Rossmere), new Game(17, "SK", "MB", 19, BowlingCentre.Rossmere),
                     new Game(18, "BC", "SO", 13, BowlingCentre.Rossmere), new Game(18, "AB", "SK", 17, BowlingCentre.Rossmere), new Game(18, "NO", "NL", 19, BowlingCentre.Rossmere),
                                                                                                                                                  
                     new Game(19, "SO", "MB", 23, BowlingCentre.Academy), new Game(19, "SK", "NO", 27, BowlingCentre.Academy), new Game(19, "AB", "NL", 29, BowlingCentre.Academy),
                     new Game(20, "MB", "AB", 25, BowlingCentre.Academy), new Game(20, "NL", "SO", 27, BowlingCentre.Academy), new Game(20, "NO", "BC", 29, BowlingCentre.Academy),
                     new Game(21, "NO", "AB", 23, BowlingCentre.Academy), new Game(21, "NL", "SK", 25, BowlingCentre.Academy), new Game(21, "BC", "MB", 27, BowlingCentre.Academy),
                }
            };
        }
    }
}
