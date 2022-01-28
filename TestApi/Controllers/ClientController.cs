using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using TestApi.Models;

namespace TestApi.Controllers
{
    public class ClientController : ApiController
    {
        private JuleTrainEntities _ent { get; set; }

        public ClientController()
        {
            _ent = new JuleTrainEntities();
        }

        public async Task<HttpResponseMessage> Get() 
        {
            var clientModel = await _ent.Client.Select(x => new ClientModel()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                MiddleName = x.MiddleName,
                LastName = x.LastName,
                Email = x.Email, 
                Phone = x.Phone
            }).ToListAsync();

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, clientModel);
        }

        public async Task<HttpResponseMessage> Post([FromBody] ClientModel clientModel)
        {
            var client = new Client()
            {
                FirstName = clientModel.FirstName,
                MiddleName = clientModel.MiddleName,
                LastName = clientModel.LastName,
                Login = clientModel.Login,
                Password = clientModel.Password,
                DateBirth = clientModel.DateBirth,
                Email = clientModel.Email,
                Phone = clientModel.Phone,
                DateCreated = DateTime.Now,
                LastEnter = DateTime.Now
            };

            _ent.Client.Add(client);
            await _ent.SaveChangesAsync();

            return Request.CreateResponse(System.Net.HttpStatusCode.OK);
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {
            var client = await _ent.Client.FindAsync(id);

            if (client != null)
            {
                _ent.Client.Remove(client);
                await _ent.SaveChangesAsync();

                return Request.CreateResponse(System.Net.HttpStatusCode.OK, "Давнные успешно удалены!");
            }

            return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest);

        }
    }
}