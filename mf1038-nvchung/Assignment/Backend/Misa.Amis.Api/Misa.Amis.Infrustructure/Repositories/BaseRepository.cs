using Dapper;
using Misa.Amis.Core.Entities;
using Misa.Amis.Core.Interfaces.Repositories;
using MySqlConnector;
using static Misa.Amis.Core.Utils.EntityUtils;

namespace Misa.Amis.Infrastructure.Repositories
{
  /// <summary>
  /// Lop Base Repository
  /// </summary>
  /// <typeparam name="T">Entity type</typeparam>
  public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
  {
    /// <summary>
    /// Connection factory function
    /// </summary>
    protected readonly Func<MySqlConnection> GetConnection;
    /// <summary>
    /// Ten bang ma thuc the dai dien
    /// </summary>
    protected readonly string TableName;
    /// <summary>
    /// Ten cac cot
    /// </summary>
    protected readonly IEnumerable<string> ColumnNames;
    public BaseRepository(Func<MySqlConnection> getConnection)
    {
      GetConnection = getConnection;
      TableName = GetTableName<T>();
      ColumnNames = GetColumnNames<T>();
    }
    /// <summary>
    /// Build DynamicParameters tu thuc the
    /// </summary>
    /// <param name="entity">Entity type</param>
    /// <returns></returns>
    protected DynamicParameters BuildDynamicParameters(T entity)
    {
      var dp = new DynamicParameters();
      foreach (var col in ColumnNames)//duyet qua ten cot
      {
        var val = typeof(T).GetProperty(col)!.GetValue(entity);//gia tri property
        dp.Add(col, val);
      }
      return dp;
    }
    /// <summary>
    /// Method phan trang va tim kiem
    /// </summary>
    /// <param name="pageSize">So ban ghi/trang</param>
    /// <param name="pageNumber">So trang</param>
    /// <param name="condition">Dieu kien tim kiem</param>
    /// <param name="conditionParams">Parameters cua dieu kien tim kiem</param>
    /// <returns></returns>
    protected Page<T> GetPaged(int pageSize, int pageNumber, string? condition = null, DynamicParameters? conditionParams = null)
    {
      var sql = $"create temporary table filteredTable select * from View_{TableName}{(condition != null ? $" where {condition}" : string.Empty)} order by coalesce(ModifiedDate,CreatedDate) desc; select * from filteredTable limit @take offset @skip; select count(*) from filteredTable; drop temporary table filteredTable;";//lenh truy van tim kiem va phan trang
      //khoi tao ket noi
      using var db = GetConnection();
      //khoi tao cac parameters
      var parameters = new DynamicParameters(conditionParams);
      var take = Math.Max(1, pageSize);//so ban ghi/trang
      var skip = Math.Max(0, (pageNumber - 1) * take);//so ban ghi bi bo qua
      parameters.Add("take", take);
      parameters.Add("skip", skip);
      //truy van du lieu
      var res = db.QueryMultiple(sql, parameters);
      var data = res.Read<T>();//cac ban ghi cua trang
      var totalRecords = res.Read<int>().First();//tong so ban ghi
      return new Page<T>((int)Math.Ceiling(totalRecords / (double)take), totalRecords, data);
    }
    public virtual int Delete(Guid id)
    {
      var sql = $"delete from {TableName} where {TableName}Id=@id";
      using var db = GetConnection();
      return db.Execute(sql, new { id });
    }

    public virtual IEnumerable<T> GetAll()
    {
      var sql = $"select * from View_{TableName}";
      using var db = GetConnection();
      return db.Query<T>(sql);
    }

    public virtual T GetByCode(string code)
    {
      var sql = $"select * from {TableName} where {TableName}Code=@code";
      using var db = GetConnection();
      return db.QueryFirstOrDefault<T>(sql, new { code });
    }

    public virtual T GetById(Guid id)
    {
      var sql = $"select * from View_{TableName} where {TableName}Id=@id";
      using var db = GetConnection();
      return db.QueryFirstOrDefault<T>(sql, new { id });
    }
    public virtual int Insert(T entity)
    {
      //set gia tri cho khoa chinh, set CreatedDate
      GetPrimaryKeyProperty<T>().SetValue(entity, Guid.NewGuid());
      entity.CreatedDate = DateTime.Now;
      //insert into [TableName] (col1,col2,...) values (@var1,@var2,...)
      var vars = string.Join(",", ColumnNames.Select(c => $"@{c}"));//(@var1,@var2,...)
      var cols = string.Join(",", ColumnNames);//(col1,col2,...)
      var sql = $"insert into {TableName} ({cols}) values ({vars})";
      using var db = GetConnection();
      return db.Execute(sql, BuildDynamicParameters(entity));
    }

    public virtual int Update(Guid id, T entity)
    {
      //set gia tri ModifiedDate
      entity.ModifiedDate = DateTime.Now;
      //update [TableName] set col1=@var1, col2=@var2, ... where [TableName]Id=@id
      var sets = string.Join(",", GetColumnNames<T>(false).Select(c => $"{c}=@{c}"));//col1=@var1,col2=@var2,...
      var sql = $"update {TableName} set {sets} where {TableName}Id=@id";
      using var db = GetConnection();
      var dp = BuildDynamicParameters(entity);
      dp.Add("id", id);//them parameter khoa chinh
      return db.Execute(sql, dp);
    }
  }
}
