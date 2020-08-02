using System.IO;
using Autofac;
using log4net;
using MarriageGift.DAO.Interfaces;
using MarriageGift.DAO.Wrappers;
using MarriageGift.FAO;
using MarriageGift.FAO.Interfaces;
using MarriageGift.Model.CustomerModel;
using MarriageGift.Model.Interfaces;
using MarriageGift.Controller;
using MarriageGift.Controller.Interfaces;

namespace MarriageGiftAPI
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // The generic ILogger<TCategoryName> service was added to the ServiceCollection by ASP.NET Core.
            // It was then registered with Autofac using the Populate method. All of this starts
            // with the `UseServiceProviderFactory(new AutofacServiceProviderFactory())` that happens in Program and registers Autofac
            // as the service provider.          
            ILog logger = LogManager.GetLogger(typeof(AutofacModule));
            builder.RegisterType<EventDaoWrapper>().As<IEventDao>();
            builder.RegisterType<CustomerDaoWrapper>().As<ICustomerDao>();
            builder.RegisterType<GiftDaoWrapper>().As<IGiftDao>();
            builder.RegisterType<InvitationDaoWrapper>().As<IInvitationDao>();
            builder.RegisterType<OccasionDaoWrapper>().As<IOccassionDao>();
            builder.Register(s => new StreamWriter("customer.txt", true)).As<StreamWriter>();
            builder.Register(f => new FileStream("customer.dat", FileMode.Create)).As<FileStream>();
            builder.RegisterType<SaveToFileFao>().As<ISaveToFileFao>();
            builder.Register(c => new Customer("Jeff", "pass@jeff")).As<ICustomer>();
            builder.Register(l=>logger).As<ILog>();   
            builder.Register(c => new CustomerActionController(c.Resolve<ICustomer>(), c.Resolve<ICustomerDao>(), c.Resolve<IEventDao>(), c.Resolve<IInvitationDao>(), c.Resolve<IOccassionDao>(), c.Resolve<IGiftDao>(), c.Resolve<ISaveToFileFao>(), c.Resolve<ILog>()))
                .As<ICustomerController>()
                .InstancePerLifetimeScope();
            builder.Register(c =>logger)
            .As<ILog>()
                .InstancePerLifetimeScope();
        }
    }
}
