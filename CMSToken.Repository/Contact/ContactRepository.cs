using System.Collections.Generic;
using System.Linq;
using Evolent.Data;
using System.Data.Entity;
using System;

namespace Evolent.Repository.Contact
{
    public class ContactRepository : Repository<Data.Contact>, IContactRepository
    {
        public EvolentContext EvolentContext { get; set; }
        public ContactRepository(EvolentContext Context) : base(Context)
        {
            this.EvolentContext = Context;
        }

       

    }
}
