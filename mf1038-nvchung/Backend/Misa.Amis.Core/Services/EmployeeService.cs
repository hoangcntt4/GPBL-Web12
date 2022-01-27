using Misa.Amis.Core.Entities;
using Misa.Amis.Core.Exceptions;
using Misa.Amis.Core.Interfaces.Repositories;
using Misa.Amis.Core.Interfaces.Services;
using System.Text.RegularExpressions;

namespace Misa.Amis.Core.Services
{
  public class EmployeeService : BaseService<Employee>, IEmployeeService
  {
    readonly IDepartmentRepository _dpmRepo;
    public EmployeeService(IEmployeeRepository repo, IDepartmentRepository dpmRepo) : base(repo)
    {
      _dpmRepo = dpmRepo;
    }

    public string NextCode()
    {
      var code = (Repo as IEmployeeRepository).GetLastTouchedCode();//EmployeeCode moi nhat
      if (string.IsNullOrEmpty(code)) return "NV1";//tra ve NV1 neu chua co ban ghi nao trong csdl
      var match = Regex.Match(code, @"\d+$");//phan so cuoi cung
      if (match.Success)//neu co phan so cuoi cung
      {
        var numStr = match.Groups[0].Value;
        var prefix = code[..match.Index];//phan chu
        if (int.TryParse(numStr, out int num))//parse phan so sang int
        {
          return $"{prefix}{num + 1}";//tang phan so them 1 roi ghep voi phan chu
        }
      }
      return $"{code}1";
    }

    protected override void Validate(Employee entity, bool isUpdate)
    {
      //validate DateOfBirth
      var emp = Repo.GetByCode(entity.EmployeeCode);
      if (emp.DateOfBirth != null && emp.DateOfBirth > DateTime.Now)
      {
        throw new ValidationException("Ngày sinh không được lớn hơn ngày hiện tại", "DateOfBirth");
      }
      //validate EmployeeCode
      if (!(emp == null || (isUpdate && emp.EmployeeId == entity.EmployeeId)))
      {
        throw new ValidationException("Mã nhân viên đã tồn tại", "EmployeeCode");
      }
      //validate DepartmentId
      if (entity.DepartmentId.HasValue && _dpmRepo.GetById(entity.DepartmentId.Value) == null)
      {
        throw new ValidationException("Mã đơn vị không tồn tại", "DepartmentId");
      }
    }
  }
}
