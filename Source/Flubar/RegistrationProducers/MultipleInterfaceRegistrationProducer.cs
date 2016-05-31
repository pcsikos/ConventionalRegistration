﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flubar.RegistrationProducers
{
    public class MultipleInterfaceRegistrationProducer : AbstractRegistrationProducer
    {
        public MultipleInterfaceRegistrationProducer(IRegistrationServiceSelector registrationServiceSelector,
            IServiceFilter serviceFilter)
            : base(registrationServiceSelector, serviceFilter)
        {
        }

        protected override bool IsApplicable(Type service, Type implementation)
        {
            return true;
        }
    }
}
