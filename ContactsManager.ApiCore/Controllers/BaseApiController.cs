using AutoMapper;
using ContactsManager.ApiCore.Helpers;
using ContactsManager.ApiCore.Models;
using ContactsManager.DTO;
using ContactsManager.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ContactsManager.ApiCore.Controllers
{
    //[ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BaseApiController<TRepository, TEntity, TModel> : ControllerBase
                                            where TEntity : class, IEntity<int>
                                            where TModel : ModelBase<int>
                                            where TRepository : IBaseRepository<TEntity>
    {
        private readonly TRepository _repository;
        private readonly IUrlHelper _urlHelper;
        public BaseApiController(TRepository repository, IUrlHelper urlHelper) {
            _repository = repository;
            _urlHelper = urlHelper;
        }

        [HttpGet(Name = nameof(Get))]
        [Route("[Controller]/GetAll")]
        public virtual async Task<ActionResult> Get() {
            var item = Mapper.Map<TModel>(await _repository.GetAllAsync());
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpGet(Name = nameof(Get))]
        public virtual async Task<ActionResult> Get(int Id) {
            var item = Mapper.Map<TModel>(await _repository.GetAsync(Id));
            if (item == null)
                return NotFound();

            return Ok(item);
        }


        [HttpPost(Name = nameof(Post))]
        public virtual async Task<ActionResult> Post([FromBody]TModel param) {
            if (param == null)
                return BadRequest();

            var result = await _repository.AddAsync(Mapper.Map<TEntity>(param));

            if (result == -1) {
                return BadRequest();
            }

            param.Id = result;
            return CreatedAtRoute(nameof(Get), new { id = result },
                    param);
        }


        [HttpPut]
        public virtual async Task<ActionResult> Put(int Id, [FromBody]TModel param) {
            if (param == null)
                return BadRequest();

            param.Id = Id;
            await _repository.UpdateAsync(Mapper.Map<TEntity>(param));
            return Ok(param);
        }


        [HttpDelete]
        public virtual async Task<ActionResult> Delete(int Id) {
            var item = await _repository.FindAsync(e => e.Id == Id);

            if (item == null)
                return NotFound();

            await _repository.DeleteAsync(item);
            return NoContent();
        }


        protected List<LinkDto> CreateLinksForCollection(QueryParameters queryParameters, int totalCount) {
            var links = new List<LinkDto>();

            // self 
            links.Add(
             new LinkDto(_urlHelper.Link("GetAll", new {
                 pagecount = queryParameters.PageCount,
                 page = queryParameters.Page,
                 orderby = queryParameters.OrderBy
             }), "self", "GET"));

            links.Add(new LinkDto(_urlHelper.Link("GetAll", new {
                pagecount = queryParameters.PageCount,
                page = 1,
                orderby = queryParameters.OrderBy
            }), "first", "GET"));

            links.Add(new LinkDto(_urlHelper.Link("GetAll", new {
                pagecount = queryParameters.PageCount,
                page = queryParameters.GetTotalPages(totalCount),
                orderby = queryParameters.OrderBy
            }), "last", "GET"));

            if (queryParameters.HasNext(totalCount)) {
                links.Add(new LinkDto(_urlHelper.Link("GetAll", new {
                    pagecount = queryParameters.PageCount,
                    page = queryParameters.Page + 1,
                    orderby = queryParameters.OrderBy
                }), "next", "GET"));
            }

            if (queryParameters.HasPrevious()) {
                links.Add(new LinkDto(_urlHelper.Link("GetAll", new {
                    pagecount = queryParameters.PageCount,
                    page = queryParameters.Page - 1,
                    orderby = queryParameters.OrderBy
                }), "previous", "GET"));
            }

            links.Add(
               new LinkDto(_urlHelper.Link(nameof(Post), null),
               "post",
               "POST"));

            return links;
        }

        protected dynamic ExpandSingleItem(TEntity item) {
            var links = GetLinks(item.Id);
            TModel model = Mapper.Map<TModel>(item);

            var resourceToReturn = item.ToDynamic() as IDictionary<string, object>;
            resourceToReturn.Add("links", links);

            return resourceToReturn;
        }

        private IEnumerable<LinkDto> GetLinks(int id) {
            var links = new List<LinkDto>();

            links.Add(
              new LinkDto(_urlHelper.Link(nameof(Get), new { id = id }),
              "self",
              "GET"));

            links.Add(
              new LinkDto(_urlHelper.Link(nameof(Delete), new { id = id }),
              "delete_item",
              "DELETE"));

            links.Add(
              new LinkDto(_urlHelper.Link(nameof(Post), null),
              "create_item",
              "POST"));

            links.Add(
               new LinkDto(_urlHelper.Link(nameof(Put), new { id = id }),
               "update_item",
               "PUT"));

            return links;
        }
    }
}
