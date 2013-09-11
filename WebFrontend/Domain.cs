using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Edument.CQRS;
using MBACNationals.ReadModels;
using MBACNationals.Participant;

namespace WebFrontend
{
    public static class Domain
    {
        public static MessageDispatcher Dispatcher;
        public static IParticipantQueries ParticipantQueries;

        public static void Setup()
        {
            Dispatcher = new MessageDispatcher(new SqlEventStore(Properties.Settings.Default.DefaultConnection));

            Dispatcher.ScanInstance(new ParticipantCommandHandlers());

            ParticipantQueries = new Participants();
            Dispatcher.ScanInstance(ParticipantQueries);
        }
    }
}