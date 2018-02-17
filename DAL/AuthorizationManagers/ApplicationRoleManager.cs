using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models.AuthorizationModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using DAL.DataContext;

namespace DAL.AuthorizationManagers
{
    public class ApplicationRoleManager:RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(RoleStore<ApplicationRole> store):base(store)
        {

        }
        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options,
                                            IOwinContext context)
        {
            DataContex db = context.Get<DataContex>();
            ApplicationRoleManager manager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
            return manager;
        }
    }
}
