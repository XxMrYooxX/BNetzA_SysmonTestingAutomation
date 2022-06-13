using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public UseCaseRemoteDesktop createRemoteDesktop()
        {
            return new UseCaseRemoteDesktop();
        }
    }
}
