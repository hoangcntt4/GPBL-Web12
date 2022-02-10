using Misa.Amis.Core.Attributes;
using Misa.Amis.Core.Entities;
using Misa.Amis.Core.Enums;
using Misa.Amis.Core.Exceptions;
using Misa.Amis.Core.Interfaces.Repositories;
using Misa.Amis.Core.Interfaces.Services;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
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

    public ExcelPackage ExportExcel(ISet<string>? excludedProps)
    {
      var excel = new ExcelPackage();
      excel.Workbook.Properties.Author = "Misa company";
      excel.Workbook.Properties.Title = "Employee List";
      var sheet = excel.Workbook.Worksheets.Add("Employees");
      sheet.Cells[1, 1].Value = "STT";
      var props = typeof(T).GetProperties();
      if (excludedProps != null)
      {
        props = props.ToHashSet().ExceptBy(excludedProps, prop => prop.Name).ToArray();
      }
      for (int i = 0; i < props.Length; i++)
      {
        var cell = sheet.Cells[1, i + 1];
        cell.Value = GetDisplayName(props[i]);
      }
      var header = sheet.Cells[1, 1, 1, props.Length];
      header.Style.Fill.PatternType = ExcelFillStyle.Solid;
      header.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
      header.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
      header.Style.Font.Bold = true;
      var data = Repo.GetAll().ToList();
      for (var i = 0; i < data.Count; i++)
      {
        sheet.Cells[i + 2, 1].Value = i + 1;
        for (var j = 0; j < props.Length; j++)
        {
          var cell = sheet.Cells[i + 2, j + 1];
          var val = props[j].GetValue(data[i]);
          switch (val)
          {
            case DateTime:
              cell.Style.Numberformat.Format = "dd/mm/yyyy";
              cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
              break;
            case Enum e:
              val = e.GetDisplayName();
              break;
          }
          cell.Value = val;
        }
      }
      sheet.Cells.AutoFitColumns();
      return excel;
    }
  }
}
