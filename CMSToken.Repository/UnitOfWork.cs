using System;
using Evolent.Data;
using Evolent.Repository.Contact;

namespace Evolent.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EvolentContext context;

       
        public IContactRepository Contacts { get; private set; }

       

        public UnitOfWork(EvolentContext context)
        {
            this.context = context;

            Contacts = new ContactRepository(context);

        }

        public int Complete()
        {
            try
            {
                return context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }


        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Cleanup 
            context.Dispose();
        }

       
    }
}
