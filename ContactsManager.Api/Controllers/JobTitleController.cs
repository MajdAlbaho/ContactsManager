using AutoMapper;
using CacheCow.Server.WebApi;
using ContactsManager.Api.Helper;
using ContactsManager.Repository.Entities;
using ContactsManager.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;

namespace ContactsManager.Api.Controllers
{
    public class JobTitleController : BaseApiController<IJobTitleRepository, JobTitle, DTO.JobTitle>
    {
        private readonly IJobTitleRepository _jobTitleRepository;

        public JobTitleController(IJobTitleRepository repository)
            : base(repository) {
            _jobTitleRepository = repository;
        }

        [HttpGet]
        [Route("api/JobTitle", Name = "JobTitlesList"), HttpCache(DefaultExpirySeconds = 60)]
        public IHttpActionResult Get(string sort = "Id",
                    string EnName = null, string ArName = null,
                    int page = 1, int pageSize = 3,
                    string fields = null) {
            try {
                List<string> listOfFields = new List<string>();
                if (fields != null) {
                    listOfFields = fields.ToLower().Split(',').ToList();
                }

                var items = _jobTitleRepository.GetAll()?
                    .ApplySort(sort)
                    .Where(e => (EnName == null || e.EnName == EnName))
                    .Where(e => (ArName == null || e.ArName == ArName));

                int totalCount = items.Count();
                int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                if (pageSize > _maxPageSize)
                    pageSize = _maxPageSize;

                var urlHelper = new UrlHelper(Request);

                var prevLink = page > 1 ? urlHelper.Link("JobTitlesList",
                    new {
                        page = page - 1,
                        pageSize,
                        fields,
                        EnName,
                        ArName,
                        sort
                    }) : "";
                var nextLink = page < totalPages ? urlHelper.Link("JobTitlesList",
                    new {
                        page = page + 1,
                        pageSize,
                        fields,
                        EnName,
                        ArName,
                        sort
                    }) : "";


                var paginationHeader = new {
                    currentPage = page,
                    pageSize,
                    totalCount,
                    totalPages,
                    previousPageLink = prevLink,
                    nextPageLink = nextLink
                };

                HttpContext.Current.Response.Headers.Add("X-Pagination",
                    Newtonsoft.Json.JsonConvert.SerializeObject(paginationHeader));

                return Ok(items
                    .ApplySort(sort)
                    .Skip(pageSize * (page - 1))
                    .Take(pageSize).ToList()
                    .Select(e => ExpandObject.CreateDataShapedObject(Mapper.Map<DTO.JobTitle>(e), listOfFields))
                    .ToList());
            }
            catch (Exception ex) {
                return InternalServerError(ex);
            }
        }
    }
}
