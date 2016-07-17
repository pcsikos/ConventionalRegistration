
using Flubar;
using Flubar.Configuration;
using Flubar.Diagnostics;
using Flubar.RegistrationProducers;
using Flubar.TypeFiltering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnitTestGenerator;
using UnitTestGenerator.DynamicProxy;
using TypeFilter = Flubar.TypeFiltering.TypeFilter;

#pragma warning disable RECS0026 // Possible unassigned object created by 'new'
#pragma warning disable RECS0001 // Class is declared partial but has only one part
        namespace Flubar.Tests
        {
            [TestClass]
            public partial class StrategySelectorTests
            {
                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void UsingSingleInterfaceStrategy_ExcludingNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var strategySelector = ProxyGenerator.CreateProxy<StrategySelector>();		 
                    strategySelector.UsingSingleInterfaceStrategy(null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void UsingAllInterfacesStrategy_ExcludingNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var strategySelector = ProxyGenerator.CreateProxy<StrategySelector>();		 
                    strategySelector.UsingAllInterfacesStrategy(null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void UsingStrategy_RegistrationProducerNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var strategySelector = ProxyGenerator.CreateProxy<StrategySelector>();		 
                    strategySelector.UsingStrategy(null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Constructor_TypesNullValueGiven_ShouldThrowArgumentNullException()
                {
                    new StrategySelector(null);		 
                }

            }
        }
        namespace Flubar.Tests
        {
            [TestClass]
            public partial class RegistrationEntryTests
            {
                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Constructor__Type_ImplementationTypeNullValueGiven_ShouldThrowArgumentNullException()
                {
                    new RegistrationEntry(null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Constructor__Type_Type_ImplementationTypeNullValueGiven_ShouldThrowArgumentNullException()
                {
                    new RegistrationEntry((Type)null, ProxyGenerator.CreateProxy<Type>());		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Constructor__Type_Type_ServiceTypeNullValueGiven_ShouldThrowArgumentNullException()
                {
                    new RegistrationEntry(ProxyGenerator.CreateProxy<Type>(), (Type)null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Constructor__Type_IEnumerableType_ImplementationTypeNullValueGiven_ShouldThrowArgumentNullException()
                {
                    new RegistrationEntry((Type)null, new [] {ProxyGenerator.CreateProxy<Type>(), ProxyGenerator.CreateProxy<Type>()});		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Constructor__Type_IEnumerableType_ServiceTypesNullValueGiven_ShouldThrowArgumentNullException()
                {
                    new RegistrationEntry(ProxyGenerator.CreateProxy<Type>(), (IEnumerable<Type>)null);		 
                }

            }
        }
        namespace Flubar.Tests
        {
            [TestClass]
            public partial class RegistrationPerformerTests
            {
                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void RegisterEach_RegistrationExecutionNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var registrationPerformer = ProxyGenerator.CreateProxy<RegistrationPerformer>();		 
                    registrationPerformer.RegisterEach(null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void RegisterAll_RegistrationExecutionNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var registrationPerformer = ProxyGenerator.CreateProxy<RegistrationPerformer>();		 
                    registrationPerformer.RegisterAll(null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Constructor_RegistrationsNullValueGiven_ShouldThrowArgumentNullException()
                {
                    new RegistrationPerformer(null);		 
                }

            }
        }
        namespace Flubar.Tests.Diagnostics
        {
            [TestClass]
            public partial class DiagnosticLoggerTests
            {
                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Info_MessageNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var diagnosticLogger = ProxyGenerator.CreateProxy<DiagnosticLogger>();		 
                    diagnosticLogger.Info(null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Info_FormatNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var diagnosticLogger = ProxyGenerator.CreateProxy<DiagnosticLogger>();		 
                    diagnosticLogger.Info(null, new [] {new Object(), new Object()});		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Info_ArgsNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var diagnosticLogger = ProxyGenerator.CreateProxy<DiagnosticLogger>();		 
                    diagnosticLogger.Info(Value.Create<string>(), null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Warning_MessageNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var diagnosticLogger = ProxyGenerator.CreateProxy<DiagnosticLogger>();		 
                    diagnosticLogger.Warning(null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Constructor_BehaviorConfigurationNullValueGiven_ShouldThrowArgumentNullException()
                {
                    new DiagnosticLogger(null);		 
                }

            }
        }
        namespace Flubar.Tests.Configuration
        {
            [TestClass]
            public partial class BehaviorConfigurationTests
            {
                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void ExcludeServices_FilterNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var behaviorConfiguration = new BehaviorConfiguration();		 
                    behaviorConfiguration.ExcludeServices((Func<Type, bool>)null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void ExcludeServices_ServiceTypesToExcludeNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var behaviorConfiguration = new BehaviorConfiguration();		 
                    behaviorConfiguration.ExcludeServices((Type[])null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Constructor_FilterNullValueGiven_ShouldThrowArgumentNullException()
                {
                    new BehaviorConfiguration(null);		 
                }

            }
        }
        namespace Flubar.Tests.TypeFiltering
        {
            [TestClass]
            public partial class ImplementationFilterTests
            {
                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void ExcludeImplementation_ImplementationNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var implementationFilter = new ImplementationFilter();		 
                    implementationFilter.ExcludeImplementation(null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void GetAllowedServices_ImplementationNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var implementationFilter = new ImplementationFilter();		 
                    implementationFilter.GetAllowedServices(null, new [] {ProxyGenerator.CreateProxy<Type>(), ProxyGenerator.CreateProxy<Type>()});		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void GetAllowedServices_ServicesNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var implementationFilter = new ImplementationFilter();		 
                    implementationFilter.GetAllowedServices(ProxyGenerator.CreateProxy<Type>(), null);		 
                }

            }
        }
        namespace Flubar.Tests.TypeFiltering
        {
            [TestClass]
            public partial class ServiceFilterAggregatorTests
            {
                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void GetAllowedServices_ImplementationNullValueGiven_ShouldThrowArgumentNullException()
                {
                    serviceFilterAggregator.GetAllowedServices(null, new [] {ProxyGenerator.CreateProxy<Type>(), ProxyGenerator.CreateProxy<Type>()});		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void GetAllowedServices_ServicesNullValueGiven_ShouldThrowArgumentNullException()
                {
                    serviceFilterAggregator.GetAllowedServices(ProxyGenerator.CreateProxy<Type>(), null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Constructor_FiltersNullValueGiven_ShouldThrowArgumentNullException()
                {
                    new ServiceFilterAggregator(null);		 
                }

            }
        }
        namespace Flubar.Tests.TypeFiltering
        {
            [TestClass]
            public partial class ServiceImplementationTests
            {
                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void AddImlementation_ImplementationTypeNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var serviceImplementation = ProxyGenerator.CreateProxy<ServiceImplementation>();		 
                    serviceImplementation.AddImlementation(null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Constructor_ServiceTypeNullValueGiven_ShouldThrowArgumentNullException()
                {
                    new ServiceImplementation(null);		 
                }

            }
        }
        namespace Flubar.Tests.TypeFiltering
        {
            [TestClass]
            public partial class TypeFilterTests
            {
                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void ExcludeType_TypeNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var typeFilter = new TypeFilter();		 
                    typeFilter.ExcludeType(null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Contains_TypeNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var typeFilter = new TypeFilter();		 
                    typeFilter.Contains(null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void AddFilter_FilterNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var typeFilter = new TypeFilter();		 
                    typeFilter.AddFilter(null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void GetAllowedServices_ImplementationNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var typeFilter = new TypeFilter();		 
                    typeFilter.GetAllowedServices(null, new [] {ProxyGenerator.CreateProxy<Type>(), ProxyGenerator.CreateProxy<Type>()});		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void GetAllowedServices_ServicesNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var typeFilter = new TypeFilter();		 
                    typeFilter.GetAllowedServices(ProxyGenerator.CreateProxy<Type>(), null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Constructor_TypesNullValueGiven_ShouldThrowArgumentNullException()
                {
                    new TypeFilter(null);		 
                }

            }
        }
        namespace Flubar.Tests.TypeFiltering
        {
            [TestClass]
            public partial class AssemblySelectorTests
            {
                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void From_IEnumerableAssembly_AssembliesNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var assemblySelector = new AssemblySelector();		 
                    assemblySelector.From((IEnumerable<Assembly>)null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void From_IEnumerableString_AssembliesNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var assemblySelector = new AssemblySelector();		 
                    assemblySelector.From((IEnumerable<string>)null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void From_AssemblyArray_AssembliesNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var assemblySelector = new AssemblySelector();		 
                    assemblySelector.From((Assembly[])null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void From_StringArray_AssembliesNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var assemblySelector = new AssemblySelector();		 
                    assemblySelector.From((String[])null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void FromAssemblyContaining_TypeArray_TypesNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var assemblySelector = new AssemblySelector();		 
                    assemblySelector.FromAssemblyContaining((Type[])null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void FromAssemblyContaining_IEnumerableType_TypesNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var assemblySelector = new AssemblySelector();		 
                    assemblySelector.FromAssemblyContaining((IEnumerable<Type>)null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void FromAssembliesMatching_RegexPatternsNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var assemblySelector = new AssemblySelector();		 
                    assemblySelector.FromAssembliesMatching(null);		 
                }

            }
        }
        namespace Flubar.Tests.TypeFiltering
        {
            [TestClass]
            public partial class ServiceMappingTrackerTests
            {
                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void ExcludeRegistration_RegistrationNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var serviceMappingTracker = ProxyGenerator.CreateProxy<ServiceMappingTracker>();		 
                    serviceMappingTracker.ExcludeRegistration(null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void ExcludeService_ServiceTypeNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var serviceMappingTracker = ProxyGenerator.CreateProxy<ServiceMappingTracker>();		 
                    serviceMappingTracker.ExcludeService(null, ProxyGenerator.CreateProxy<Type>());		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void ExcludeServices_ServiceTypesNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var serviceMappingTracker = ProxyGenerator.CreateProxy<ServiceMappingTracker>();		 
                    serviceMappingTracker.ExcludeServices(null, ProxyGenerator.CreateProxy<Type>());		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void ExcludeServices_ImplementationNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var serviceMappingTracker = ProxyGenerator.CreateProxy<ServiceMappingTracker>();		 
                    serviceMappingTracker.ExcludeServices(new [] {ProxyGenerator.CreateProxy<Type>(), ProxyGenerator.CreateProxy<Type>()}, null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void GetAllowedServices_ImplementationNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var serviceMappingTracker = ProxyGenerator.CreateProxy<ServiceMappingTracker>();		 
                    serviceMappingTracker.GetAllowedServices(null, new [] {ProxyGenerator.CreateProxy<Type>(), ProxyGenerator.CreateProxy<Type>()});		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void GetAllowedServices_ServicesNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var serviceMappingTracker = ProxyGenerator.CreateProxy<ServiceMappingTracker>();		 
                    serviceMappingTracker.GetAllowedServices(ProxyGenerator.CreateProxy<Type>(), null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Constructor_LoggerNullValueGiven_ShouldThrowArgumentNullException()
                {
                    new ServiceMappingTracker(null);		 
                }

            }
        }
        namespace Flubar.Tests.TypeFiltering
        {
            [TestClass]
            public partial class TypeSelectorTests
            {
                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Including_IEnumerableType_TypesNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var typeSelector = ProxyGenerator.CreateProxy<TypeSelector>();		 
                    typeSelector.Including((IEnumerable<Type>)null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Including_TypeArray_TypesNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var typeSelector = ProxyGenerator.CreateProxy<TypeSelector>();		 
                    typeSelector.Including((Type[])null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Excluding_IEnumerableType_TypesNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var typeSelector = ProxyGenerator.CreateProxy<TypeSelector>();		 
                    typeSelector.Excluding((IEnumerable<Type>)null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Excluding_TypeArray_TypesNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var typeSelector = ProxyGenerator.CreateProxy<TypeSelector>();		 
                    typeSelector.Excluding((Type[])null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void WithoutAttribute_AttributeTypeNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var typeSelector = ProxyGenerator.CreateProxy<TypeSelector>();		 
                    typeSelector.WithoutAttribute(null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void WithAttribute_AttributeTypeNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var typeSelector = ProxyGenerator.CreateProxy<TypeSelector>();		 
                    typeSelector.WithAttribute(null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Where_FilterNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var typeSelector = ProxyGenerator.CreateProxy<TypeSelector>();		 
                    typeSelector.Where(null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void IsImplementingGenericType_GenericTypeDefinitionNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var typeSelector = ProxyGenerator.CreateProxy<TypeSelector>();		 
                    typeSelector.IsImplementingGenericType(null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Select_FilterNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var typeSelector = ProxyGenerator.CreateProxy<TypeSelector>();		 
                    typeSelector.Select(null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Constructor_TypesNullValueGiven_ShouldThrowArgumentNullException()
                {
                    new TypeSelector(null);		 
                }

            }
        }
        namespace Flubar.Tests.TypeFiltering
        {
            [TestClass]
            public partial class ServiceExtractorTests
            {
                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void GetAllowedServices_ImplementationTypeNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var serviceExtractor = new ServiceExtractor();		 
                    serviceExtractor.GetAllowedServices(null, new [] {ProxyGenerator.CreateProxy<Type>(), ProxyGenerator.CreateProxy<Type>()});		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void GetAllowedServices_ServicesNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var serviceExtractor = new ServiceExtractor();		 
                    serviceExtractor.GetAllowedServices(ProxyGenerator.CreateProxy<Type>(), null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void RegisterMonitoredType_ServiceTypeNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var serviceExtractor = new ServiceExtractor();		 
                    serviceExtractor.RegisterMonitoredType(null);		 
                }

            }
        }
        namespace Flubar.Tests.RegistrationProducers
        {
            [TestClass]
            public partial class DefaultInterfaceRegistrationProducerTests
            {
                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Constructor_RegistrationServiceSelectorNullValueGiven_ShouldThrowArgumentNullException()
                {
                    new DefaultInterfaceRegistrationProducer(null);		 
                }

            }
        }
        namespace Flubar.Tests.RegistrationProducers
        {
            [TestClass]
            public partial class CompatibleServiceLookupTests
            {
                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void GetServicesFrom_ImplementationNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var compatibleServiceLookup = new CompatibleServiceLookup();		 
                    compatibleServiceLookup.GetServicesFrom(null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Constructor_FilterNullValueGiven_ShouldThrowArgumentNullException()
                {
                    new CompatibleServiceLookup(null);		 
                }

            }
        }
        namespace Flubar.Tests.RegistrationProducers
        {
            [TestClass]
            public partial class MultipleInterfaceRegistrationProducerTests
            {
                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Constructor_RegistrationServiceSelectorNullValueGiven_ShouldThrowArgumentNullException()
                {
                    new MultipleInterfaceRegistrationProducer(null);		 
                }

            }
        }
#pragma warning restore RECS0026 // Possible unassigned object created by 'new'
#pragma warning restore RECS0001 // Class is declared partial but has only one part
