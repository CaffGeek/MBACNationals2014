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
        public static MessageDispatcher Dispatcher;
        public static IParticipantQueries ParticipantQueries;
        public static IContingentQueries ContingentQueries;
        public static IContingentViewQueries ContingentViewQueries;
        public static IReservationQueries ReservationQueries;

        public static void Setup()
        {
            var readModelFilePath = HttpContext.Current.Server.MapPath("~/App_Data/MBACReadModels.db");

            Dispatcher = new MessageDispatcher(new SqlEventStore(Properties.Settings.Default.DefaultConnection));

            Dispatcher.ScanInstance(new ParticipantCommandHandlers());
            Dispatcher.ScanInstance(new ContingentCommandHandlers());
            //TODO: Dispatcher.ScanInstance(new ReservationCommandHandlers());

            ParticipantQueries = new ParticipantQueries(readModelFilePath);
            Dispatcher.ScanInstance(ParticipantQueries);

            ContingentQueries = new ContingentQueries(readModelFilePath);
            Dispatcher.ScanInstance(ContingentQueries);

            ContingentViewQueries = new ContingentViewQueries(readModelFilePath);
            Dispatcher.ScanInstance(ContingentViewQueries);

            ReservationQueries = new ReservationQueries(readModelFilePath);
            Dispatcher.ScanInstance(ReservationQueries);

            File.Delete(readModelFilePath);
            Dispatcher.RepublishEvents(); //TODO: HACK: each time the app starts, the readmodel is regenerated
        }
    }
}