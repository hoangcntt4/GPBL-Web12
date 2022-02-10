using Misa.Amis.Core.Entities;
using Misa.Amis.Core.Exceptions;
using Misa.Amis.Core.Interfaces.Repositories;
using Misa.Amis.Core.Interfaces.Services;

namespace Misa.Amis.Core.Services
{
  public class DepartmentService : BaseService<Department>, IDepartmentService
  {
    public DepartmentService(IBaseRepository<Department> repo) : base(repo)
    {
    }

    protected override void Validate(Department entity, bool isUpdate)
    {
      //kiem tra hop le ma don vi
      var dpm = Repo.GetById(entity.DepartmentId);
      if (!(dpm == null || (isUpdate && dpm.DepartmentId == entity.DepartmentId)))
      {
        var msg = string.Format(Resources.Text.VEMExisted, Resources.Text.DPNDepartmentCode);
        throw new ValidationException(msg, "DepartmentCode");
      }
    }
  }
}
