using Microsoft.AspNetCore.Mvc;
using Misa.Amis.Core.Entities;
using Misa.Amis.Core.Exceptions;
using Misa.Amis.Core.Interfaces.Repositories;
using Misa.Amis.Core.Interfaces.Services;
using static Misa.Amis.Core.Utils.EntityUtils;

namespace Misa.Amis.Api.Controllers
{
  /// <summary>
  /// Base controller
  /// </summary>
  /// <typeparam name="T">Entity type</typeparam>
  public class BaseController<T> : ControllerBase where T : BaseEntity
  {
    protected readonly IBaseRepository<T> Repo;
    protected readonly IBaseService<T> Service;

    public BaseController(IBaseRepository<T> repo, IBaseService<T> service)
    {
      Repo = repo;
      Service = service;
    }
    /// <summary>
    /// Tra ve ban ghi bang id hoac throw Exception neu null
    /// </summary>
    /// <param name="id">id thuc the</param>
    /// <returns></returns>
    /// <exception cref="ResourceNotFoundException"></exception>
    protected T GetOrThrow(Guid id)
    {
      var entity = Repo.GetById(id);
      if (entity == null)
      {
        throw new ResourceNotFoundException(GetDisplayName(typeof(T)));
      }
      return entity;
    }
    /// <summary>
    /// Lay tat ca ban ghi
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult GetAll()
    {
      return Ok(Repo.GetAll());
    }
    /// <summary>
    /// Lay mot ban ghi theo id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
      var entity = GetOrThrow(id);
      return Ok(entity);
    }
    /// <summary>
    /// Tao moi mot ban ghi
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>So ban ghi duoc them moi</returns>
    [HttpPost]
    public IActionResult Create(T entity)
    {
      return Created("", Service.Insert(entity));
    }
    /// <summary>
    /// Cap nhat mot ban ghi theo id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="entity"></param>
    /// <returns>So ban ghi duoc cap nhat</returns>
    [HttpPut("{id}")]
    public IActionResult Update(Guid id, T entity)
    {
      GetOrThrow(id);
      return Ok(Service.Update(id, entity));
    }
    /// <summary>
    /// Xoa mot ban ghi theo id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>So ban ghi bi xoa</returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
      GetOrThrow(id);
      return Ok(Repo.Delete(id));
    }
  }
}
