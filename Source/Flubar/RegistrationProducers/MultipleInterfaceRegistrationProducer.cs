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
            ITypeFilter typeFilter)
            : base(registrationServiceSelector, typeFilter)
        {
        }

        protected override bool IsApplicable(Type service, Type implementation)
        {
            return true;
        }
    }
}
