using Edument.CQRS;
using MBACNationals.ReadModels;
using MBACNationals.Participant;
using MBACNationals.Contingent;
using System.IO;
using System.Web;

namespace WebFrontend
{
    public static class Domain
    {
        public static bool IsRebuilding { get; private set; }
        public static string ReadModelFilePath { get; private set; }

        public static MessageDispatcher Dispatcher;
        public static IParticipantQueries ParticipantQueries;
        public static IParticipantProfileQueries ParticipantProfileQueries;
        public static IContingentQueries ContingentQueries;
        public static IContingentViewQueries ContingentViewQueries;
        public static IContingentTravelPlanQueries ContingentTravelPlanQueries;
        public static IContingentPracticePlanQueries ContingentPracticePlanQueries;
        public static IContingentEventHistoryQueries ContingentEventHistoryQueries;
        public static IReservationQueries ReservationQueries;

        public static void Setup()
        {
            ReadModelFilePath = HttpContext.Current.Server.MapPath("~/App_Data/MBACReadModels.db");

            Dispatcher = new MessageDispatcher(new SqlEventStore(Properties.Settings.Default.DefaultConnection));

            Dispatcher.ScanInstance(new ParticipantCommandHandlers());
            Dispatcher.ScanInstance(new ContingentCommandHandlers());

            ParticipantQueries = new ParticipantQueries(ReadModelFilePath);
            Dispatcher.ScanInstance(ParticipantQueries);

            ParticipantProfileQueries = new ParticipantProfileQueries(ReadModelFilePath);
            Dispatcher.ScanInstance(ParticipantProfileQueries);

            ContingentQueries = new ContingentQueries(ReadModelFilePath);
            Dispatcher.ScanInstance(ContingentQueries);

            ContingentViewQueries = new ContingentViewQueries(ReadModelFilePath);
            Dispatcher.ScanInstance(ContingentViewQueries);

            ContingentTravelPlanQueries = new ContingentTravelPlanQueries(ReadModelFilePath);
            Dispatcher.ScanInstance(ContingentTravelPlanQueries);

            ContingentPracticePlanQueries = new ContingentPracticePlanQueries(ReadModelFilePath);
            Dispatcher.ScanInstance(ContingentPracticePlanQueries);

            ContingentEventHistoryQueries = new ContingentEventHistoryQueries(ReadModelFilePath);
            Dispatcher.ScanInstance(ContingentEventHistoryQueries);

            ReservationQueries = new ReservationQueries(ReadModelFilePath);
            Dispatcher.ScanInstance(ReservationQueries);
        }

        public static void RebuildReadModels()
        {
            var bakFile = HttpContext.Current.Server.MapPath("~/app_offline.bak");
            var htmFile = HttpContext.Current.Server.MapPath("~/app_offline.htm");
            File.Copy(bakFile, htmFile, true);

            if (File.Exists(ReadModelFilePath))
                File.Delete(ReadModelFilePath);
            
            Dispatcher.RepublishEvents();

            File.Delete(htmFile);
        }
    }
}