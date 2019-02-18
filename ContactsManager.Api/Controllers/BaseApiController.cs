using AutoMapper;
using ContactsManager.DTO;
using ContactsManager.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ContactsManager.Api.Controllers
{
    [RoutePrefix("api")]
    public abstract class BaseApiController<TRepository, TEntity, TModel> : ApiController
                                            where TEntity : class, IEntity<int>
                                            where TModel : ModelBase<int>
                                            where TRepository : IBaseRepository<TEntity>
    {
        private readonly TRepository _repository;
        protected const int _maxPageSize = 8;


        public BaseApiController(TRepository repository) {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get() {
            try {
                return Ok(Mapper.Map<IEnumerable<TModel>>(await _repository.GetAllAsync()).ToList());
            }
            catch (Exception ex) {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(int Id) {
            try {
                var item = await _repository.GetAsync(Id);
                if (item == null)
                    return NotFound();

                return Ok(item);
            }
            catch (Exception ex) {
                return InternalServerError(ex);
            }
        }


        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]TModel param) {
            try {
                if (param == null)
                    return BadRequest();

                var result = await _repository.AddAsync(Mapper.Map<TEntity>(param));

                if (result == -1) {
                    return BadRequest();
                }

                param.Id = result;
                return Created(Request.RequestUri + "/" + result.ToString(), param);
            }
            catch (Exception ex) {
                return InternalServerError(ex);
            }
        }


        [HttpPut]
        public async Task<IHttpActionResult> Put(int Id, [FromBody]TModel param) {
            try {
                if (param == null)
                    return BadRequest();

                param.Id = Id;
                await _repository.UpdateAsync(Mapper.Map<TEntity>(param));
                return Ok(param);
            }
            catch (Exception ex) {
                return InternalServerError(ex);
            }
        }


        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int Id) {
            try {
                var item = await _repository.FindAsync(e => e.Id == Id);

                if (item == null)
                    return NotFound();

                await _repository.DeleteAsync(item);
                return StatusCode(System.Net.HttpStatusCode.NoContent);
            }
            catch (Exception ex) {
                return InternalServerError(ex);
            }
        }
    }
}
