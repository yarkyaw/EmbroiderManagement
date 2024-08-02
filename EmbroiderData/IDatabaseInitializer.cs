// Decompiled with JetBrains decompiler
// Type: EmbroideryData.IDatabaseInitializer
// Assembly: EmbroideryData, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85FC7BD1-C71A-46CF-AFDC-ECA6425FE3FE
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroiderManagement\obj\Release\netcoreapp3.1\PubTmp\Out\EmbroideryData.dll

using System.Threading.Tasks;

namespace EmbroideryData
{
  public interface IDatabaseInitializer
  {
    Task SeedAsync();
  }
}
