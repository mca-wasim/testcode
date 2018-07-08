using Evolent.Data;
using Evolent.Repository;
using Evolent.Service.Contacts;
using System.Web.Http;
using System.Linq;

namespace Evolent.Web.Controllers.Api
{
    [RoutePrefix("api/v1/contact")]
    public class ContactController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly EvolentContext context;
        private readonly IContactService contactService;

        public ContactController()
        {
            context = new EvolentContext();
            unitOfWork = new UnitOfWork(this.context);
            this.contactService = new ContactService(unitOfWork);
        }

        [HttpPost]
        [Route("add")]
        [AcceptVerbs("POST")]
        public IHttpActionResult Post([FromBody]Contact contact)
        {
            if (!ModelState.IsValid)
                return BadRequest("Contact details are not valid");

            var response = this.contactService.AddContact(contact);
            return Ok(response);
        }

        [HttpGet]
        [Route("{Id}")]
        public IHttpActionResult Get(int id)
        {
            var response = contactService.GetContactById(id);
            return Ok(response);
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var response = contactService.GetAllContacts().ToList();
            return Ok(response);
        }

        [HttpPut]
        [Route("{id:int}")]
        [AcceptVerbs("PUT")]
        public IHttpActionResult Put(int id, [FromBody]Contact contact)
        {
            if (!ModelState.IsValid)
                return BadRequest("Contact details are not valid");

            var result = contactService.UpdateContact(contact);

            if (result != 0)
            {
                return Ok(1);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        [AcceptVerbs("DELETE")]
        public IHttpActionResult Delete(int id)
        {
            var result = contactService.DeleteContact(id);

            if (result != 0)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }

        }
    }
}
