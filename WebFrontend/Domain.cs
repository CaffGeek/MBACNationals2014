﻿using Edument.CQRS;
using MBACNationals.ReadModels;
using MBACNationals.Participant;
using MBACNationals.Contingent;
using System.IO;

namespace WebFrontend
{
    public static class Domain
    {
        public static MessageDispatcher Dispatcher;
        public static IParticipantQueries ParticipantQueries;
        public static IContingentQueries ContingentQueries;
        public static IContingentViewQueries ContingentViewQueries;
        public static IReservationQueries ReservationQueries;

        public static void Setup()
        {
            Dispatcher = new MessageDispatcher(new SqlEventStore(Properties.Settings.Default.DefaultConnection));

            Dispatcher.ScanInstance(new ParticipantCommandHandlers());
            Dispatcher.ScanInstance(new ContingentCommandHandlers());
            //TODO: Dispatcher.ScanInstance(new ReservationCommandHandlers());

            ParticipantQueries = new ParticipantQueries();
            Dispatcher.ScanInstance(ParticipantQueries);

            ContingentQueries = new ContingentQueries();
            Dispatcher.ScanInstance(ContingentQueries);

            ContingentViewQueries = new ContingentViewQueries();
            Dispatcher.ScanInstance(ContingentViewQueries);

            ReservationQueries = new ReservationQueries();
            Dispatcher.ScanInstance(ReservationQueries);

            File.Delete(@"C:\Users\chadh\Documents\GitHub\MBACNationals2014\WebFrontend\App_Data\MBACReadModels.db");
            Dispatcher.RepublishEvents(); //TODO: HACK: each time the app starts, the readmodel is regenerated
        }
    }
}