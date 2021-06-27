using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComponentUtil.Webs.Controllers;

namespace ComponentWebApi.Api.Controllers
{
    /// <summary>
    /// Base
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public abstract class BaseController : WebApiControllerBase
    {
    }
}
