// Decompiled with JetBrains decompiler
// Type: EmbroideryData.DTO.ProductWeightDTO
// Assembly: EmbroideryData, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85FC7BD1-C71A-46CF-AFDC-ECA6425FE3FE
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroiderManagement\obj\Release\netcoreapp3.1\PubTmp\Out\EmbroideryData.dll

namespace EmbroideryData.DTO
{
  public class ProductWeightDTO
  {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Gram { get; set; }
        public string LocalizeName { get; set; }
        public string FullName => this.Name + "(" + this.LocalizeName + ")";
    }
}
