﻿using System.Collections.Generic;

namespace WebFrontend.Models.Participant
{
    public class Index
    {
        public List<MBACNationals.ReadModels.Teams.Team> Teams { get; set; }
        public List<MBACNationals.ReadModels.Participants.Participant> Participants { get; set; }
    }
}