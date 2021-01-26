using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Web.Controllers
{
    /// <summary>
    /// API chung cho các entity
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// CreatedBy: PVPhong (07/01/2021)
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseEntityController<TEntity> : ControllerBase
    {
        IBaseService<TEntity> _baseService;
        public BaseEntityController(IBaseService<TEntity> baseService)
        {
            _baseService = baseService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var entities = _baseService.GetAll();
            return Ok(entities);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var entity = _baseService.GetById(id);
            return Ok(entity);
        }

        [HttpPost]
        public IActionResult Post(TEntity entity)
        {
            var result = _baseService.Add(entity);
            if (result.MISACode == ApplicationCore.Enums.MISACode.NotValid)
            {
                return BadRequest(result.data);
            }
            return Ok(result);
        }

        [HttpPut]
        public IActionResult Put(TEntity entity)
        {
            var result = _baseService.Update(entity);
            if (result.MISACode == ApplicationCore.Enums.MISACode.NotValid)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var entity = _baseService.Delete(id);
            return Ok(entity);
        }
    }
}
