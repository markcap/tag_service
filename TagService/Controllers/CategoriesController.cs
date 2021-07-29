using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TagService.Middleware;
using TagService.Services;

namespace TagService.Controllers
{
    [Route("v{version:apiVersion}/categories")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(ILogger<CategoriesController> logger, ICategoriesService categoriesService)
        {
            _logger = logger;
            _categoriesService = categoriesService;
        }

        /// <summary>Gets all Categories.</summary>
        /// <remarks>
        /// 
        /// Sample request:
        ///
        ///     GET /v1/categories
        ///     
        /// </remarks>
        /// <returns>List of all Category Names</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseData), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseData), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<string>>> Get()
        {
            var categories = await _categoriesService.GetAllAsync();

            return Ok(categories);
        }

        /// <summary>Gets Category of the given feature.</summary>
        /// <param name="featureId">The feature identifier (i.e., database key).</param>
        /// <remarks>
        /// 
        /// Sample request:
        ///
        ///     GET /v1/categories/featureId/576
        ///     
        /// </remarks>
        /// <returns>Category Name</returns>
        [HttpGet("featureId/{featureId:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseData), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponseData), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseData), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> GetByFeatureId(long featureId)
        {
            var category = await _categoriesService.GetByFeatureIdAsync(featureId);

            return Ok(category);
        }
    }
}
