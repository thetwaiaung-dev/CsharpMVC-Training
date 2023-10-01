using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Collections.Generic;
using MvcTraining.Models;

namespace MvcTraining.Controllers
{
    public class BaseController : Controller
    {
        public DataTablesRequest GetFormRequest()
        {
            DataTablesRequest formRequest = new DataTablesRequest();

            formRequest.Draw = Request.Form["draw"].FirstOrDefault();
            formRequest.Start = Request.Form["start"].FirstOrDefault();
            formRequest.Length = Request.Form["length"].FirstOrDefault();
            formRequest.SortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            formRequest.SortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            formRequest.PageSize = formRequest.Length != null ? Convert.ToInt32(formRequest.Length) : 0;
            formRequest.Skip = formRequest.Start != null ? Convert.ToInt32(formRequest.Start) : 0;
            formRequest.Search = Request.Form["search[value]"].FirstOrDefault();

            return formRequest;
        }
        public ActionResult ToJson<T>(String draw, int recordsTotal, int recordsFiltered, List<T> data)
        {
            return Json(
                new
                {
                    draw,
                    recordsFiltered,
                    recordsTotal,
                    data
                });
        }

    }
}
