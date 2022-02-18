using Dapper;
using MISA.WEB12.Core.Attribute;
using MISA.WEB12.Core.Interfaces.Infrastructure;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB12.Infrastructure.Repository
{
    public class BaseRepository<TypeEntity> : IBaseRepository<TypeEntity>
    {
        #region Fields
        //Khai báo thông tin CSDL:
        protected readonly string ConnectionString = "" +
            "Host = localhost;" +
            "Port = 3306;" +
            "Database = misa.amis;" +
            "User Id = root;" +
            "Password = trung8";
        protected DynamicParameters Parameters;
        //Khai báo tên đối tượng
        string _className = typeof(TypeEntity).Name;
        //Lấy ra tất cả các thuộc tính:
        PropertyInfo[] _properties = typeof(TypeEntity).GetProperties();
        //Lấy ra thuộc tính được đánh dấu là primaryKey
        PropertyInfo _primaryKey = typeof(TypeEntity).GetProperties().First(x => x.IsDefined(typeof(PrimaryKey)));
        #endregion

        #region Methods
        /// <summary>
        /// Lấy toàn bộ danh sách
        /// </summary>
        /// <returns>Danh sách đối tượng</returns>
        /// Created by LQTrung(21/01/2022)
        public IEnumerable<TypeEntity> GetAll()
        {
            using (var sqlConnection  = new MySqlConnection(ConnectionString))
            {
                //Thực hiện truy vấn dữ liệu trong databse:
                var entities = sqlConnection.Query<TypeEntity>($"select * from {_className}");
                return entities;
            }
        }

        /// <summary>
        /// Lấy dữ liệu theo entityId
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>Một đối tượng</returns>
        /// Created by LQTrung(21/01/2022)
        public TypeEntity GetById(Guid entityId)
        {
            using (var sqlConnection = new MySqlConnection(ConnectionString))
            {
                //Khai báo truy vấn:
                var sqlCommand = $"select * from {_className} where {_primaryKey.Name} = @{_primaryKey.Name}";
                Parameters = new DynamicParameters();
                Parameters.Add($"@{_primaryKey.Name}", entityId);

                //Thực hiện truy vấn dữ liệu trong database:
                var entity = sqlConnection.QueryFirstOrDefault<TypeEntity>(sqlCommand, param: Parameters);
                return entity;
            }
        }

        /// <summary>
        /// Thêm mới dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Số bản ghi đã thêm mới</returns>
        /// Created by LQTrung (26/01/2022)
        public int Insert(TypeEntity entity)
        {
            using (var sqlConnection = new MySqlConnection(ConnectionString))
            {
                Parameters = new DynamicParameters();

                //tạo mới giá trị cho PrimaryKey:
                _primaryKey.SetValue(entity, Guid.NewGuid());

                //Khai báo truy vấn:
                var columns = new StringBuilder();
                var columnsValue = new StringBuilder();
                string delimiter = "";
                foreach (var property in _properties)
                {
                    //kiểm tra property được đánh dấu là MISAInsertQuery không
                    var insertQuery = Attribute.IsDefined(property, typeof(MISAInsertQuery));
                    if (insertQuery == true)
                    {
                        continue;
                    }

                    //Lấy tên của Property:
                    var propName = property.Name;
                    columns.Append($"{delimiter}{propName}");
                    columnsValue.Append($"{delimiter}@{propName}");
                    delimiter = ",";

                    //Add giá trị của Property vào Parameters
                    var value = property.GetValue(entity);
                    Parameters.Add($"@{property.Name}", value);
                }

                var sqlCommand = $"insert into {_className} ({columns}) values ({columnsValue})";
                //Thực hiện thêm mới:
                var res = sqlConnection.Execute(sqlCommand, param: Parameters);
                return res;
            }
        }

        /// <summary>
        /// Cập nhật dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityId"></param>
        /// <returns>Số bản ghi đã cập nhật</returns>
        /// Created by LQTrung (26/01/2022)
        public int Update(TypeEntity entity, Guid entityId)
        {
            using (var sqlConnection = new MySqlConnection(ConnectionString))
            {
                Parameters = new DynamicParameters();

                //Khai báo truy vấn:
                var sqlColumns = new StringBuilder();
                string delimiter = "";
                foreach (var property in _properties)
                {
                    if (property != _primaryKey)
                    {
                        //kiểm tra property được đánh dấu là MISAInsertQuery không
                        var insertQuery = Attribute.IsDefined(property, typeof(MISAInsertQuery));
                        if (insertQuery == true)
                        {
                            continue;
                        }

                        //Lấy tên của Property:
                        var propName = property.Name;
                        sqlColumns.Append($"{delimiter}{propName}=@{propName}");
                        delimiter = ",";
                        //Add giá trị của Property vào Parameters
                        var value = property.GetValue(entity);
                        Parameters.Add($"@{property.Name}", value);
                    }
                }
                var sqlCommand = $"update {_className} set {sqlColumns} where {_primaryKey.Name} = @{_primaryKey.Name}";
                Parameters.Add($"@{_primaryKey.Name}", entityId);
                //Thực hiện cập nhật:
                var res = sqlConnection.Execute(sqlCommand, param: Parameters);
                return res;
            }
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>Số bản ghi đã xóa</returns>
        /// Created by LQTrung(21/01/2022)
        public int Delete(Guid entityId)
        {
            using (var sqlConnection = new MySqlConnection(ConnectionString))
            {
                Parameters = new DynamicParameters();

                //Khai báo truy vấn:
                var sqlCommand = $"delete from {_className} where {_primaryKey.Name} = @{_primaryKey.Name}";

                Parameters.Add($"@{_primaryKey.Name}", entityId);
                //Thực hiện xóa:
                var res = sqlConnection.Execute(sqlCommand, param: Parameters);
                return res;
            }
        }

        /// <summary>
        /// Kiểm tra trùng lặp entityCode
        /// </summary>
        /// <param name="entityCode"></param>
        /// <returns></returns>
        ///  /// Created by LQTrung(21/01/2022)
        public bool CheckDuplicate(string propName, string propValue, Guid? entityId)
        {
            using (var sqlConnection = new MySqlConnection(ConnectionString))
            {
                Parameters = new DynamicParameters();
                var sqlCommand = string.Empty;
                if (entityId == null)
                {
                    //khai báo truy vấn check trùng cho Insert
                    sqlCommand = $"select {propName} from {_className} where {propName} = @{propName}";
                }
                else
                {
                    //khai báo truy vấn check trùng cho Update
                    sqlCommand = $"select {propName} from {_className} where {propName} = @{propName} and {_primaryKey.Name} != @{_primaryKey.Name}";
                    Parameters.Add($"@{_primaryKey.Name}", entityId);
                }
                Parameters.Add($"@{propName}", propValue);
                //Thực hiện truy vấn
                var res = sqlConnection.QueryFirstOrDefault(sqlCommand, param: Parameters);
                if (res != null)
                {
                    return true;
                }
                return false;
            }
        }
        #endregion
    }
}
