using Misa.Amis.Core.Attributes;
using Misa.Amis.Core.Entities;
using Misa.Amis.Core.Exceptions;
using Misa.Amis.Core.Interfaces.Repositories;
using Misa.Amis.Core.Interfaces.Services;
using System.Reflection;
using static Misa.Amis.Core.Utils.EntityUtils;

namespace Misa.Amis.Core.Services
{
  /// <summary>
  /// Lop service base
  /// </summary>
  /// <typeparam name="T">Entity type</typeparam>
  public class BaseService<T> : IBaseService<T> where T : BaseEntity
  {
    protected readonly IBaseRepository<T> Repo;

    public BaseService(IBaseRepository<T> repo)
    {
      Repo = repo;
    }
    /// <summary>
    /// Custom validate
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="isUpdate">True neu la update</param>
    protected virtual void Validate(T entity, bool isUpdate) { }
    /// <summary>
    /// Validate du lieu chung
    /// </summary>
    /// <param name="entity"></param>
    /// <exception cref="ValidationException"></exception>
    private void CommonValidate(T entity)
    {
      foreach (var prop in typeof(T).GetProperties())//duyet qua cac property
      {
        var value = prop.GetValue(entity);//gia tri property
        var dpName = GetDisplayName(prop);//ten hien thi
        foreach (var attr in prop.GetCustomAttributes<ValidationAttribute>())//duyet qua cac validate attribute
        {
          if (!attr.IsValid(value))//kiem tra hop le
          {
            throw new ValidationException(attr.FormatErrorMessage(dpName), prop.Name);
          }
        }
      }
    }
    public int Insert(T entity)
    {
      //thuc hien validate
      CommonValidate(entity);
      Validate(entity, false);
      //thuc hien insert
      return Repo.Insert(entity);
    }

    public int Update(Guid id, T entity)
    {
      GetPrimaryKeyProperty<T>().SetValue(entity, id);//set value cho khoa chinh
      //thuc hien validate
      CommonValidate(entity);
      Validate(entity, true);
      //thuc hien update
      return Repo.Update(id, entity);
    }
  }
}
