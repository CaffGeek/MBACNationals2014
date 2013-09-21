using Edument.CQRS;
using MBACNationals.Participant;
using MBACNationals.ReadModels;
using MBACNationals.Team;
using System;
using System.IO;

namespace WebFrontend
{
    public static class Domain
    {
        public static MessageDispatcher Dispatcher;
        public static IParticipantQueries ParticipantQueries;
        public static ITeamQueries TeamQueries;

        public static void Setup()
        {
            Dispatcher = new MessageDispatcher(new SqlEventStore(Properties.Settings.Default.DefaultConnection));

            Dispatcher.ScanInstance(new ParticipantCommandHandlers());
            Dispatcher.ScanInstance(new TeamCommandHandlers());

            ParticipantQueries = new Participants();
            Dispatcher.ScanInstance(ParticipantQueries);

            TeamQueries = new Teams();
            Dispatcher.ScanInstance(TeamQueries);

            File.Delete(@"C:\Users\chadh\Documents\GitHub\MBACNationals2014\WebFrontend\App_Data\MBACReadModels.db");
            Dispatcher.RepublishEvents(); //TODO: HACK: each time the app starts, the readmodel is regenerated
        }
    }
}