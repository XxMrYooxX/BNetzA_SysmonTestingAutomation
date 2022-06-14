using BNetzA_SysmonTestingAutomation.UseCases;

namespace BNetzA_SysmonTestingAutomation.Gateways
{
    internal class UseCaseGateway
    {
        public UseCaseGateway()
        {

        }

        public UseCaseDateierstellung createUseCaseDateierstellung()
        {
            return new UseCaseDateierstellung();
        }

        public UseCaseDruckvorgang createUseCaseDruckvorgang()
        {
            return new UseCaseDruckvorgang();
        }

        public UseCaseEmail createUseCaseEmail()
        {
            return new UseCaseEmail();
        }

        public UseCaseRemoteDesktop createUseCaseRemoteDesktop()
        {
            return new UseCaseRemoteDesktop();
        }
    }
}
