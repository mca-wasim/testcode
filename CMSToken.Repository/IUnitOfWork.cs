using Evolent.Repository.Contact;
using System;
using System.Threading.Tasks;

namespace Evolent.Repository
{
    public interface IUnitOfWork:IDisposable
    {
        IContactRepository Contacts { get;  }
      
        int Complete();

       

    }
}
