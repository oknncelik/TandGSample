using Autofac;
using Product.Business.Abstruct;
using Product.Business.Concreate;
using Product.Data.Abstruct.Repositories;
using Product.Data.Concreate.Repositories;

namespace Product.Api.Modules
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductManager>().As<IProductManager>();
            builder.RegisterType<ProductRepository>().As<IProductRepository>();

        }
    }
}
