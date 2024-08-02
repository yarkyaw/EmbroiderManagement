// Decompiled with JetBrains decompiler
// Type: EmbroideryRepo.Extension.EFExtensions
// Assembly: EmbroideryRepo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 647504C0-1B35-4864-B572-F8603CBAB0B5
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroideryRepo\bin\Debug\netcoreapp3.1\EmbroideryRepo.dll

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace EmbroideryRepo.Extension
{
  public static class EFExtensions
  {
    public static DbCommand LoadStoredProc(
      this DbContext context,
      string storedProcName,
      bool prependDefaultSchema = true,
      short commandTimeout = 30)
    {
      DbCommand command = context.Database.GetDbConnection().CreateCommand();
      command.CommandTimeout = (int) commandTimeout;
      if (prependDefaultSchema)
      {
        string defaultSchema = context.Model.GetDefaultSchema();
        if (defaultSchema != null)
          storedProcName = defaultSchema + "." + storedProcName;
      }
      command.CommandText = storedProcName;
      command.CommandType = CommandType.StoredProcedure;
      return command;
    }

    public static DbCommand WithSqlParam(
      this DbCommand cmd,
      string paramName,
      object paramValue,
      Action<DbParameter> configureParam = null)
    {
      if (string.IsNullOrEmpty(cmd.CommandText) && cmd.CommandType != CommandType.StoredProcedure)
        throw new InvalidOperationException("Call LoadStoredProc before using this method");
      DbParameter parameter = cmd.CreateParameter();
      parameter.ParameterName = paramName;
      parameter.Value = paramValue != null ? paramValue : (object) DBNull.Value;
      if (configureParam != null)
        configureParam(parameter);
      cmd.Parameters.Add((object) parameter);
      return cmd;
    }

    public static DbCommand WithSqlParam(
      this DbCommand cmd,
      string paramName,
      Action<DbParameter> configureParam = null)
    {
      if (string.IsNullOrEmpty(cmd.CommandText) && cmd.CommandType != CommandType.StoredProcedure)
        throw new InvalidOperationException("Call LoadStoredProc before using this method");
      DbParameter parameter = cmd.CreateParameter();
      parameter.ParameterName = paramName;
      if (configureParam != null)
        configureParam(parameter);
      cmd.Parameters.Add((object) parameter);
      return cmd;
    }

    public static DbCommand WithSqlParam(
      this DbCommand cmd,
      string paramName,
      SqlParameter parameter)
    {
      if (string.IsNullOrEmpty(cmd.CommandText) && cmd.CommandType != CommandType.StoredProcedure)
        throw new InvalidOperationException("Call LoadStoredProc before using this method");
      cmd.Parameters.Add((object) parameter);
      return cmd;
    }

    public static DbCommand WithSqlParams(this DbCommand cmd, SqlParameter[] parameters)
    {
      if (string.IsNullOrEmpty(cmd.CommandText) && cmd.CommandType != CommandType.StoredProcedure)
        throw new InvalidOperationException("Call LoadStoredProc before using this method");
      foreach (SqlParameter parameter in parameters)
        cmd.Parameters.Add((object) parameter);
      return cmd;
    }

    public static void ExecuteStoredProc(
      this DbCommand command,
      Action<EFExtensions.SprocResults> handleResults,
      CommandBehavior commandBehaviour = CommandBehavior.Default,
      bool manageConnection = true)
    {
      if (handleResults == null)
        throw new ArgumentNullException(nameof (handleResults));
      using (command)
      {
        if (manageConnection && command.Connection.State == ConnectionState.Closed)
          command.Connection.Open();
        try
        {
          using (DbDataReader reader = command.ExecuteReader(commandBehaviour))
          {
            EFExtensions.SprocResults sprocResults = new EFExtensions.SprocResults(reader);
            handleResults(sprocResults);
          }
        }
        finally
        {
          if (manageConnection)
            command.Connection.Close();
        }
      }
    }

    public static async Task ExecuteStoredProcAsync(
      this DbCommand command,
      Action<EFExtensions.SprocResults> handleResults,
      CommandBehavior commandBehaviour = CommandBehavior.Default,
      CancellationToken ct = default (CancellationToken),
      bool manageConnection = true)
    {
      if (handleResults == null)
        throw new ArgumentNullException(nameof (handleResults));
      using (command)
      {
        if (manageConnection && command.Connection.State == ConnectionState.Closed)
          await command.Connection.OpenAsync(ct).ConfigureAwait(false);
        try
        {
          using (DbDataReader reader = await command.ExecuteReaderAsync(commandBehaviour, ct).ConfigureAwait(false))
          {
            EFExtensions.SprocResults sprocResults = new EFExtensions.SprocResults(reader);
            handleResults(sprocResults);
            sprocResults = (EFExtensions.SprocResults) null;
          }
        }
        finally
        {
          if (manageConnection)
            command.Connection.Close();
        }
      }
    }

    public static int ExecuteStoredNonQuery(
      this DbCommand command,
      CommandBehavior commandBehaviour = CommandBehavior.Default,
      bool manageConnection = true)
    {
      int num = -1;
      using (command)
      {
        if (command.Connection.State == ConnectionState.Closed)
          command.Connection.Open();
        try
        {
          num = command.ExecuteNonQuery();
        }
        finally
        {
          if (manageConnection)
            command.Connection.Close();
        }
      }
      return num;
    }

    public static async Task<int> ExecuteStoredNonQueryAsync(
      this DbCommand command,
      CommandBehavior commandBehaviour = CommandBehavior.Default,
      CancellationToken ct = default (CancellationToken),
      bool manageConnection = true)
    {
      int numberOfRecordsAffected = -1;
      using (command)
      {
        if (command.Connection.State == ConnectionState.Closed)
          await command.Connection.OpenAsync(ct).ConfigureAwait(false);
        try
        {
          numberOfRecordsAffected = await command.ExecuteNonQueryAsync().ConfigureAwait(false);
        }
        finally
        {
          if (manageConnection)
            command.Connection.Close();
        }
      }
      return numberOfRecordsAffected;
    }

    public class SprocResults
    {
      private DbDataReader _reader;

      public SprocResults(DbDataReader reader) => this._reader = reader;

      public IList<T> ReadToList<T>() => this.MapToList<T>(this._reader);

      public T? ReadToValue<T>() where T : struct => this.MapToValue<T>(this._reader);

      public Task<bool> NextResultAsync() => this._reader.NextResultAsync();

      public Task<bool> NextResultAsync(CancellationToken ct) => this._reader.NextResultAsync(ct);

      public bool NextResult() => this._reader.NextResult();

      private IList<T> MapToList<T>(DbDataReader dr)
      {
        List<T> objList = new List<T>();
        List<PropertyInfo> props = typeof (T).GetRuntimeProperties().ToList<PropertyInfo>();
        Dictionary<string, DbColumn> dictionary = dr.GetColumnSchema().Where<DbColumn>((Func<DbColumn, bool>) (x => props.Any<PropertyInfo>((Func<PropertyInfo, bool>) (y => y.Name.ToLower() == x.ColumnName.ToLower())))).ToDictionary<DbColumn, string>((Func<DbColumn, string>) (key => key.ColumnName.ToLower()));
        if (dr.HasRows)
        {
          while (dr.Read())
          {
            T instance = Activator.CreateInstance<T>();
            foreach (PropertyInfo propertyInfo in props)
            {
              if (dictionary.ContainsKey(propertyInfo.Name.ToLower()))
              {
                DbColumn dbColumn = dictionary[propertyInfo.Name.ToLower()];
                if (dbColumn != null && dbColumn.ColumnOrdinal.HasValue)
                {
                  object obj = dr.GetValue(dbColumn.ColumnOrdinal.Value);
                  propertyInfo.SetValue((object) instance, obj == DBNull.Value ? (object) null : obj);
                }
              }
            }
            objList.Add(instance);
          }
        }
        return (IList<T>) objList;
      }

      private T? MapToValue<T>(DbDataReader dr) where T : struct => dr.HasRows && dr.Read() ? (dr.IsDBNull(0) ? new T?() : new T?(dr.GetFieldValue<T>(0))) : new T?();
    }
  }
}
