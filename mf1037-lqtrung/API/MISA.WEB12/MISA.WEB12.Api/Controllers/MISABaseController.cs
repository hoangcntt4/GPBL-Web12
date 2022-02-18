using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB12.Core.Exceptions;
using MISA.WEB12.Core.Interfaces.Infrastructure;
using MISA.WEB12.Core.Interfaces.Service;

namespace MISA.WEB12.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MISABaseController<TypeEntity> : ControllerBase
    {
        #region Fields
        IBaseService<TypeEntity> _baseSevice;
        IBaseRepository<TypeEntity> _baseRepository;

        /// <summary>
        /// Bắt lỗi
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="userMessage"></param>
        /// <returns></returns>
        protected object CatchMISAValidateException(Exception ex, string userMessage)
        {
            var response = new
            {
                devMsg = ex.Message,
                userMsg = userMessage,
                data = ex.InnerException
            };
            return response;
        }
        #endregion

        #region Constructor
        public MISABaseController(IBaseService<TypeEntity> baseSevice, IBaseRepository<TypeEntity> baseRepository)
        {
            _baseSevice = baseSevice;
            _baseRepository = baseRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns>
        /// 200 - nếu có dữ liệu
        /// 400 - có lỗi về nghiệp vụ
        /// 500 - có exception
        /// created by LQTrung (28/01/2022)
        /// </returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var data = _baseRepository.GetAll();
                return Ok(data);
            }
            catch (MISAValidateException ex)
            {
                var response = CatchMISAValidateException(ex, ex.Message);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = CatchMISAValidateException(ex, Core.Resources.MISAResourceVN.ErrorException);
                return StatusCode(500, response);
            }
        }

        /// <summary>
        /// Lấy dữ liệu theo Id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>
        /// 200 - nếu có dữ liệu
        /// 204 - không có dữ liệu
        /// 400 - có lỗi về nghiệp vụ
        /// 500 - có exception
        /// </returns>
        /// created by LQTrung (28/01/2022)
        [HttpGet("{entityId}")]
        public IActionResult GetById(Guid entityId)
        {
            try
            {
                var data = _baseRepository.GetById(entityId);
                return Ok(data);
            }
            catch (MISAValidateException ex)
            {
                var response = CatchMISAValidateException(ex, ex.Message);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = CatchMISAValidateException(ex, Core.Resources.MISAResourceVN.ErrorException);
                return StatusCode(500, response);
            }
        }

        /// <summary>
        /// Thêm mới dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>
        /// 201 - Thêm dữ liệu thành công
        /// 400 - Có lỗi về nghiệp vụ
        /// 500 - Có exception
        /// </returns>
        /// created by LQTrung (28/01/2022)
        [HttpPost]
        public IActionResult Post(TypeEntity entity)
        {
            try
            {
                var data = _baseSevice.InsertService(entity);
                return StatusCode(201, data);
            }
            catch (MISAValidateException ex)
            {
                var response = CatchMISAValidateException(ex, ex.Message);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = CatchMISAValidateException(ex, Core.Resources.MISAResourceVN.ErrorException);
                return StatusCode(500, response);
            }
        }

        /// <summary>
        /// Cập nhật dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityId"></param>
        /// <returns>
        /// 200 - cập nhật thành công
        /// 204 - không có dữ liệu
        /// 400 - có lỗi về nghiệp vụ
        /// 500 - có exception
        /// </returns>
        /// created by LQTrung (28/01/2022)
        [HttpPut("{entityId}")]
        public IActionResult Put(TypeEntity entity, Guid entityId)
        {
            try
            {
                var data = _baseSevice.UpdateService(entity, entityId);
                return Ok(data);
            }
            catch (MISAValidateException ex)
            {
                var response = CatchMISAValidateException(ex, ex.Message);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = CatchMISAValidateException(ex, Core.Resources.MISAResourceVN.ErrorException);
                return StatusCode(500, response);
            }
        }


        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>
        /// 200 - xóa thành công
        /// 204 - không có dữ liệu
        /// 400 - có lỗi về nghiệp vụ
        /// 500 - có exception
        /// </returns>
        /// created by LQTrung (28/01/2022)
        [HttpDelete("{entityId}")]
        public IActionResult Delete(Guid entityId)
        {
            try
            {
                var data = _baseRepository.Delete(entityId);
                return Ok(data);
            }
            catch (MISAValidateException ex)
            {
                var response = CatchMISAValidateException(ex, ex.Message);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = CatchMISAValidateException(ex, Core.Resources.MISAResourceVN.ErrorException);
                return StatusCode(500, response);
            }
        }
        #endregion
    }
}
