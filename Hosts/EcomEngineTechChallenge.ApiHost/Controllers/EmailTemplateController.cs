﻿using System;
using System.Composition;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EcomEngineTechChallenge.Business.Common.Entities;
using EcomEngineTechChallenge.Contracts;
using Eml.ControllerBase;
using Eml.DataRepository.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EcomEngineTechChallenge.ApiHost.Controllers
{
    [Export]
    public class EmailTemplateController : CrudControllerBase<Guid, EmailTemplate>
    {
        [ImportingConstructor]
        public EmailTemplateController(IDataRepositoryGuid<EmailTemplate> repository)
            : base(repository)
        {
        }

        [HttpGet]
        public override async Task<IActionResult> GetItems(string search = "", int? page = 1, bool? desc = false, int? field = 0)
        {
            return await DoGetItemsAsync(search, page.Value, desc.Value, field.Value);
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> Get([FromRoute]Guid id)
        {
            return await DoGetAsync(id);
        }

        [HttpGet("Suggestions")]
        public override async Task<IActionResult> Suggestions(string search = "")
        {
            return await DoGetSuggestionsAsync(search);
        }

        [HttpPut("{id}")]
        public override async Task<IActionResult> Put([FromRoute]Guid id, [FromBody]EmailTemplate item)
        {
            return await DoPutAsync(id, item);
        }

        [HttpPost]
        public override async Task<IActionResult> Post([FromBody]EmailTemplate item)
        {
            return await DoPostAsync(item);
        }

        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete([FromRoute]Guid id, string reason = "")
        {
            return await DoDeleteAsync(id, reason);
        }

        protected override async Task<SearchResponse<EmailTemplate>> GetItemsAsync(string search, int page, bool desc, int sortColumn)
        {
            search = search.ToLower();
            Expression<Func<EmailTemplate, bool>> whereClause = r => search == "" || r.SearchableName.ToLower().Contains(search);

            var orderBy = GetOrderBy((eSortColumn)sortColumn, desc);
            var items = await repository.GetPagedListAsync(page, whereClause, orderBy);

            return new SearchResponse<EmailTemplate>(items, items.TotalItemCount, repository.PageSize);
        }

        private static Func<IQueryable<EmailTemplate>, IQueryable<EmailTemplate>> GetOrderBy(eSortColumn sortColumn, bool isdesc)
        {
            Func<IQueryable<EmailTemplate>, IQueryable<EmailTemplate>> orderBy = r => r.OrderByDescending(x => x.SearchableName);

            if (isdesc)
            {
                switch (sortColumn)
                {
                    case eSortColumn.EmailLabel:
                        orderBy = r => r.OrderByDescending(x => x.EmailLabel);
                        break;
                    case eSortColumn.FromAddress:
                        orderBy = r => r.OrderByDescending(x => x.FromAddress);
                        break;
                    case eSortColumn.DateUpdated:
                        orderBy = r => r.OrderByDescending(x => x.DateUpdated);
                        break;
                    default:
                        break;
                }

                return orderBy;
            }

            switch (sortColumn)
            {
                case eSortColumn.EmailLabel:
                    orderBy = r => r.OrderBy(x => x.EmailLabel);
                    break;
                case eSortColumn.FromAddress:
                    orderBy = r => r.OrderBy(x => x.FromAddress);
                    break;
                case eSortColumn.DateUpdated:
                    orderBy = r => r.OrderBy(x => x.DateUpdated);
                    break;
                default:
                    orderBy = r => r.OrderBy(x => x.SearchableName);
                    break;
            }

            return orderBy;
        }
    }
}
