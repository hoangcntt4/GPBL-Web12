using Misa.Amis.Core.Entities;
using Misa.Amis.Core.Exceptions;
using Misa.Amis.Core.Interfaces.Repositories;
using Misa.Amis.Core.Interfaces.Services;

namespace Misa.Amis.Core.Services
{
  public class EmployeeService : BaseService<Employee>, IEmployeeService
  {
    readonly IDepartmentRepository _dpmRepo;
    public EmployeeService(IEmployeeRepository repo, IDepartmentRepository dpmRepo) : base(repo)
    {
      _dpmRepo = dpmRepo;
    }

    protected override void Validate(Employee entity, bool isUpdate)
    {
      //validate DateOfBirth
      if (entity.DateOfBirth != null && entity.DateOfBirth > DateTime.Now)
      {
        var msg = string.Format(Resources.Text.VEMLessThan, Resources.Text.DPNDateOfBirth, Resources.Text.DPNCurrentDate);
        throw new ValidationException(msg, "DateOfBirth");
      }
      if (entity.IdentityDate != null && entity.IdentityDate > DateTime.Now)
      {
        var msg = string.Format(Resources.Text.VEMLessThan, Resources.Text.DPNIndetityDate, Resources.Text.DPNCurrentDate);
        throw new ValidationException(msg, "DateOfBirth");
      }
      var emp = Repo.GetByCode(entity.EmployeeCode);
      //validate EmployeeCode
      if (!(emp == null || (isUpdate && emp.EmployeeId == entity.EmployeeId)))
      {
        var msg = string.Format(Resources.Text.VEMExisted, Resources.Text.DPEmployeeCode);
        throw new ValidationException(msg, "EmployeeCode");
      }
      //validate DepartmentId
      if (entity.DepartmentId.HasValue && _dpmRepo.GetById(entity.DepartmentId.Value) == null)
      {
        var msg = string.Format(Resources.Text.VEMNotExisted, Resources.Text.DPNDepartmentCode);
        throw new ValidationException(msg, "DepartmentId");
      }
    }

  }
}
